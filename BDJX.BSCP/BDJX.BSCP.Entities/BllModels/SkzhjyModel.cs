using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 收款账户检验请求报文实体
    /// </summary>
    public class SkzhjyModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string Sfz { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string Zh { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string Zhmc { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Sfz = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 18);
            this.Zh = BasicOperation.GetStringFromRequestMsg(recvBytes, 22, 30);
            this.Zhmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 52, 60);

        }
    }
}
