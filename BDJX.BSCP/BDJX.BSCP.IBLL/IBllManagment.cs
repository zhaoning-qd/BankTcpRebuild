using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Entities.BllModels;

namespace BDJX.BSCP.IBLL
{
    /// <summary>
    /// 业务操作统一接口，所有具体业务接口必须继承该接口
    /// </summary>
    public interface IBllManagment
    {
        /// <summary>
        /// 响应报文
        /// </summary>
        byte[] ResponseMsg { get; set; }

        /// <summary>
        /// 业务处理
        /// </summary>
        void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint);
    }
}
