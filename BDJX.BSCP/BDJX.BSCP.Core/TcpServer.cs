using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Configuration;
using System.Threading;

using BDJX.BSCP.Entities.BllModels;
using BDJX.BSCP.Entities.TcpModels;
using BDJX.BSCP.Common;

namespace BDJX.BSCP.Core
{
    /// <summary>
    /// TCP服务端
    /// </summary>
    public class TcpServer
    {
        /// <summary>
        /// Tcp连接信息
        /// </summary>
        private TcpEndPoint tcpEndPoint;

        /// <summary>
        /// Tcp连接管理
        /// </summary>
        private TcpUserManager tcpUserManager;

        /// <summary>
        /// Tcp运行时信息
        /// </summary>
        private TcpRunningTimeInfo tcpRunningTimeInfo;

        /// <summary>
        /// 业务入口
        /// </summary>
        private BllEntryPoint bllEntryPoint;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TcpServer()
        {
            tcpEndPoint = new TcpEndPoint();
            tcpRunningTimeInfo = new TcpRunningTimeInfo();
            bllEntryPoint = new BllEntryPoint();

            string strPort = ConfigurationManager.AppSettings["port"];
            bool isSuccess = Int32.TryParse(strPort, out tcpEndPoint.port);
            if (!isSuccess)
            {
                LogHelper.WriteLogError("Tcp服务端错误:", "监听端口号设置错误");
            }

            tcpEndPoint.localAddr = Dns.GetHostAddresses(Dns.GetHostName());
            tcpEndPoint.ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
        }

        /// <summary>
        /// 开始监听Tcp连接请求
        /// </summary>
        public void StartTcpListening()
        {
            TcpClient newClient = null;
            tcpEndPoint.myListener = new TcpListener(tcpEndPoint.ipAddress, tcpEndPoint.port);
            tcpEndPoint.myListener.Start(10);//最多监听10个连接请求
            LogHelper.WriteLogInfo("开始Tcp监听,时间",DateTime.Now.ToString());
            tcpRunningTimeInfo.Ht.Add("StartListening",string.Format(">>在端口{0}开始监听,等待公积金中心发起连接请求...",tcpEndPoint.port));
            BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["StartListening"].ToString());
            while (true)
            {
                try
                {
                    newClient = tcpEndPoint.myListener.AcceptTcpClient();//接收客户端的连接请求，创建一个连接通道;
                    LogHelper.WriteLogInfo("成功与客户端建立连接", newClient.Client.RemoteEndPoint);
                    tcpRunningTimeInfo.Ht.Add("ConnectSuccess", string.Format(">>与客户端{0}建立连接", newClient.Client.RemoteEndPoint));
                    BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["ConnectSuccess"].ToString());
                }
                catch (Exception ex)
                {
                    tcpRunningTimeInfo.Ht.Add("ConnectFail", "建立与客户端的连接错误");
                    BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["ConnectFail"].ToString());
                    LogHelper.WriteLogException("建立与客户端的连接错误", ex);
                    break;
                }

                //在新线程中打开与客户端的TCP连接通道，进行监听;
                tcpUserManager = new TcpUserManager(newClient);
                Thread newThread = new Thread(new ParameterizedThreadStart(StartListeningClient));
                newThread.Start(tcpUserManager);
            }
        }

        /// <summary>
        /// 监听客户端
        /// </summary>
        /// <param name="tcpUserManager"></param>
        public void StartListeningClient(object tcpUserManager)
        {
            TcpUserManager u = tcpUserManager as TcpUserManager;
            TcpClient myClient = u.client;
            while (true)
            {
                try
                {
                    string recvString = null;
                    bllEntryPoint.Hb = Encoding.Default.GetString(u.br.ReadBytes(2));

                    //读取4个字节，用来确定要读取的字节数;
                    int countRead;
                    countRead = Convert.ToInt32(Encoding.Default.GetString(u.br.ReadBytes(4)));

                    byte[] recvBytes = new byte[countRead];
                    recvBytes = u.br.ReadBytes(countRead);
                    recvString = Encoding.Default.GetString(recvBytes);

                    tcpRunningTimeInfo.Ht.Add("ReceiveMsg", string.Format("接收到的请求报文:{0}",recvString));
                    BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["ReceiveMsg"].ToString());
                    LogHelper.WriteLogInfo("接收到请求报文", recvString);

                    //业务处理过程;
                    HandleRequest(u, recvBytes, bllEntryPoint);
                    tcpRunningTimeInfo.Ht.Add("Fengexian", "----------------------------------------------------------------");
                    BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["Fengexian"].ToString());
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("与客户端断开连接,原因：{0}", ex.Message);
                    tcpRunningTimeInfo.Ht.Add("Unconnect", string.Format("与客户端断开连接,原因:{0}", ex.Message));
                    BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["Unconnect"].ToString());
                    LogHelper.WriteLogException("与客户端断开连接", ex);
                    break;
                }
            }
        }

        /// <summary>
        /// 向客户端发送消息，byte[];
        /// </summary>
        /// <param name="user"></param>
        /// <param name="msg"></param>
        private void SendMsgToClient(TcpUserManager user, byte[] msg)
        {
            try
            {
                Console.WriteLine("发送响应报文：\n{0},字节数：{1}", Encoding.Default.GetString(msg), msg.Length);
                tcpRunningTimeInfo.Ht.Add("SendMsg", string.Format("发送响应报文：\n{0},字节数：{1}", Encoding.Default.GetString(msg), msg.Length));
                BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["SendMsg"].ToString());
                user.bw.Write(msg);
                LogHelper.WriteLogInfo("发送响应报文", Encoding.Default.GetString(msg));
                user.bw.Flush();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("发送响应报文时出错,错误信息：{0}", ex.Message);
                tcpRunningTimeInfo.Ht.Add("SendError", string.Format("发送响应报文时出错,错误信息：{0}", ex.Message));
                BasicOperation.ShowInfo(tcpRunningTimeInfo.Ht["SendError"].ToString());
                LogHelper.WriteLogException("发送响应报文时发生异常", ex);
            }
        }

        /// <summary>
        /// 处理公积金中心的请求;
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private void HandleRequest(TcpUserManager user, byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            //获取交易码，根据交易码调用具体的业务对象;
            bllEntryPoint.Jym = Encoding.Default.GetString(recvBytes).Substring(0, 4);
            byte[] returnBytes;

            

            if (bllEntryPoint.Jym == "2000" || bllEntryPoint.Jym == "2006")
            {
                //MsgFirstBusinessSuper m = BusinessFactory.CreateInstance<MsgFirstBusinessSuper>(assemblyName, namespaceName, className);
                //SendToGjj(user, m.GenerateResponseRealTimeMsg(recvBytes));
                //m.HandleBusiness(recvBytes, whichBank);
            }
            else
            {
                //GjjBusinessSuper g = BusinessFactory.CreateInstance<GjjBusinessSuper>(assemblyName, namespaceName, className);
                //returnBytes = g.HandleBusiness(recvBytes, whichBank);
                //SendToGjj(user, returnBytes);
            }
        }
    }
}
