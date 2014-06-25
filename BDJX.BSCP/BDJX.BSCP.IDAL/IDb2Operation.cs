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
        void ExecuteDB2Update(string command);

        /// <summary>
        /// 查询符合条件的的记录数
        /// </summary>
        /// <param name="command">要执行的命令</param>
        /// <returns></returns>
        int ExecuteCountQuery(string command);

        /// <summary>
        /// 根据起始日期和结束日期查询zbmxz结果集
        /// </summary>
        /// <param name="qsrq">起始日期</param>
        /// <param name="zzrq">结束日期</param>
        /// <returns>对象list</returns>
        List<ZbmxzModel> GetZbmxzByJyrq(string qsrq, string zzrq);

        /// <summary>
        /// 查询zbmxz中某账号的笔数
        /// </summary>
        /// <returns></returns>
        int GetCountByZh(ZbmxzModel zbmxz);

        /// <summary>
        /// 查询zbfhz中某账号的笔数
        /// </summary>
        /// <param name="zbfhz">zbfhz实体</param>
        /// <returns>某账号对应的记录数</returns>
        int GetCountByZh(ZbfhzModel zbfhz);

        /// <summary>
        ///更新zbfhz和zbmxz
        /// </summary>
        /// <param name="lineArray"></param>
        /// <returns></returns>
        int UpateZbfhzAndZbmxz(ZbmxzModel zbmxz, ZbfhzModel zbfhz);
    }
}
