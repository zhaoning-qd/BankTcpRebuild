using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Entities.BllModels;

namespace BDJX.BSCP.IDAL
{
    public interface IDb2Operation
    {
        /// <summary>
        /// ADO.NET 更新DB2数据库中的表;
        /// </summary>
        bool ExecuteDB2Update(string command);

    }
}
