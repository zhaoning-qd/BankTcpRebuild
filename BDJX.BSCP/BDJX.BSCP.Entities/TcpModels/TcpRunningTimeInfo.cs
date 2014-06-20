using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDJX.BSCP.Entities.TcpModels
{
    public class TcpRunningTimeInfo
    {
        public Hashtable Ht { get; set; }

        public TcpRunningTimeInfo()
        {
            Ht = new Hashtable();
        }
    }
}
