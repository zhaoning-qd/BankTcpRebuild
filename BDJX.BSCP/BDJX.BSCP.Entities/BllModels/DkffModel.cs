using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款发放请求报文实体
    /// </summary>
    public class DkffModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 批次号(合同号)
        /// </summary>
        public string Pch { get; set; }

        /// <summary>
        /// 付款人账号（中心）
        /// </summary>
        public string Fkrzh { get; set; }

        /// <summary>
        /// 付款人名称（中心）
        /// </summary>
        public string Fkrmc { get; set; }

        /// <summary>
        /// 收款人账号
        /// </summary>
        public string Skrzh { get; set; }

        /// <summary>
        /// 收款人名称
        /// </summary>
        public string Skrmc { get; set; }

        /// <summary>
        /// 收款银行名称
        /// </summary>
        public string Skyhmc { get; set; }

        /// <summary>
        /// 收款银行机构代码
        /// </summary>
        public string Skyhdm { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string Je { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Pch = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 20);
            this.Fkrzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 24, 30);
            this.Fkrmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 54, 60);
            this.Skrzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 114, 30);
            this.Skrmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 144, 60);
            this.Skyhmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 204, 60);
            this.Skyhdm = BasicOperation.GetStringFromRequestMsg(recvBytes, 264, 12);
            this.Je = BasicOperation.GetStringFromRequestMsg(recvBytes, 276, 12);
        }
    }
}
