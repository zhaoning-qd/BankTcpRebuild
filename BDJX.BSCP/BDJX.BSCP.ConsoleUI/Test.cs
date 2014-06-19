using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.IBLL;

namespace BDJX.BSCP.BLL
{
    public class Test :ITest
    {
        public string SayHello()
        {
            return "hello";
        }
    }
}
