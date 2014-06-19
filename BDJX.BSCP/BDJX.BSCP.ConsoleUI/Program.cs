using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.IBLL;
using BDJX.BSCP.Common;


namespace BDJX.BSCP.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ITest iTest = BdjxFactory.CreateInstance<ITest>("BDJX.BSCP.BLL.dll", "BDJX.BSCP.BLL.Test");
            Console.WriteLine(iTest.SayHello());
        }
    }
}
