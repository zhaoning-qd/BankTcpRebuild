using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using BDJX.BSCP.Core;
using BDJX.BSCP.Common;


namespace BDJX.BSCP.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t------------银行模拟程序------------");
            //银行作为服务端，监听公积金中心的请求;
            TcpServer bankServer = new TcpServer();
            Thread t = new Thread(bankServer.StartTcpListening);
            t.Start();
        }
    }
}
