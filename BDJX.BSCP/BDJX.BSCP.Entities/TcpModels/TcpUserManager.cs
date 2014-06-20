using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace BDJX.BSCP.Entities.TcpModels
{
    /// <summary>
    /// Tcp连接用户管理类，保存Tcp Client连接的信息
    /// </summary>
    public class TcpUserManager
    {
        public BinaryReader br { get; set; }
        public BinaryWriter bw { get; set; }
        public TcpClient client { get; set; }
        private NetworkStream networkStream;

        /// <summary>
        /// 构造函数，初始化客户端及流
        /// </summary>
        /// <param name="client">客户端连接</param>
        public TcpUserManager(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
            br = new BinaryReader(networkStream);
            bw = new BinaryWriter(networkStream);
        }

        /// <summary>
        /// 关闭流及客户端连接
        /// </summary>
        public void Close()
        {
            br.Close();
            bw.Close();
            client.Close();
        }
    }
}
