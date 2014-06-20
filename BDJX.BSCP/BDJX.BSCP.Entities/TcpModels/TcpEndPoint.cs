using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace BDJX.BSCP.Entities.TcpModels
{
    /// <summary>
    /// Tcp终端信息
    /// </summary>
    public class TcpEndPoint
    {
        public TcpListener myListener;
        public IPAddress[] localAddr;
        public IPAddress ipAddress;
        public int port;
    }
}
