using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.IBLL;
using BDJX.BSCP.Common;
using BDJX.BSCP.Entities.BllModels;
using BDJX.BSCP.IDAL;
using System.IO;

namespace BDJX.BSCP.BLL
{
    /// <summary>
    /// 贷款批量收回发起业务处理
    /// </summary>
    public class DaiKuanPiLiangShouHuiFaQi : IDaiKuanPiLiangShouHuiFaQi
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        DkplshfqModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        DkplshfqMsgModel modelMsg;

        /// <summary>
        /// 用于实现IBllManagment接口中的ResponseMsg属性
        /// </summary>
        byte[] responseMsg;

        /// <summary>
        /// 响应报文--已在IBllManagment接口中声明
        /// </summary>
        public byte[] ResponseMsg
        {
            get
            {
                return responseMsg;
            }
            set
            {
                responseMsg = value;
            }
        }

        /// <summary>
        /// 数据库操作类
        /// </summary>
        IDb2Operation db2Operation;

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public DaiKuanPiLiangShouHuiFaQi()
        {
            model = new DkplshfqModel();
            modelMsg = new DkplshfqMsgModel();
            db2Operation = BdjxFactory.CreateInstance<IDb2Operation>("BDJX.BSCP.DAL.dll", "BDJX.BSCP.DAL.Db2Operation");
        }

        /// <summary>
        /// 业务处理
        /// </summary>
        public void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            try
            {
                GenerageResponseMsg(recvBytes);
                LoanBatchWithDrawDetail(bllEntryPoint.Hb, model);
                LogHelper.WriteLogInfo("贷款批量收回发起", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("贷款批量收回发起业务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes">请求报文</param>
        private void GenerageResponseMsg(byte[] recvBytes)
        {
            //解析请求报文
            model.GetValue(recvBytes);

            //产生响应报文
            modelMsg.SetValue(model);
            this.ResponseMsg = Encoding.Default.GetBytes(modelMsg.ToMsgString());
        }

        /// <summary>
        /// 生成贷款批量处理文件
        /// </summary>
        /// <param name="hb">行别</param>
        /// <param name="model">请求报文</param>
        private void LoanBatchWithDrawDetail(string hb, DkplshfqModel model)
        {

            //从ftp服务器取文件,本程序中是从本机读取;
            string fileFromPath = BasicOperation.GetFilePath(hb); ;
            string inputLine = "";
            StringBuilder outputLine;

            DateTime dt = DateTime.Now;
            string strDate = dt.ToString("yyyyMMdd");
            string tail = model.Wjmc.Substring(3);
            string outFile = "DKR" + tail;//返回文件的名称 ;
            string filePath = fileFromPath + outFile;

            using (StreamReader sr = new StreamReader(fileFromPath + model.Wjmc, Encoding.GetEncoding("gb2312")))
            {
                inputLine = sr.ReadLine();//读取第一行汇总数据;
                string[] s = inputLine.Split(new char[] { '~' });
                s[3] = s[2];
                inputLine = s[0] + "~";
                inputLine += s[1];
                inputLine += "~";
                inputLine += s[2];
                inputLine += "~";
                inputLine += s[3];
                inputLine += "~";
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312")))
                {
                    sw.WriteLine(inputLine);
                }

                for (int i = 1; i <= Convert.ToInt32(model.Zjls); i++)
                {
                    inputLine = sr.ReadLine();

                    string[] inputArray = inputLine.Split(new char[] { '~' });

                    outputLine = new StringBuilder();
                    outputLine.Append("M~");
                    outputLine.Append(inputArray[1]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[2]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[3]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[3]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[5]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[6]);
                    outputLine.Append("~");
                    outputLine.Append("0000");//银行扣款的状态标志;
                    outputLine.Append("~");

                    outputLine.Append(inputArray[8]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[9]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[10]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[11]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[12]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[13]);
                    outputLine.Append("~");


                    using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.GetEncoding("gb2312")))
                    {
                        sw.WriteLine(outputLine.ToString());
                    }
                }
            }
            Console.WriteLine("文件处理成功");

            //模拟前置机动作：更新djplzxzf的zt字段;
            string command = "update djplzxzf set zt='3' where djhm='" + model.Pch + "'";
            db2Operation.ExecuteDB2Update(command);          
        }
    }
}
