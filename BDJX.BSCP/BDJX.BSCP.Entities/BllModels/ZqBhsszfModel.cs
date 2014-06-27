using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 支取--本行实时支付请求报文实体
    /// </summary>
    public class ZqBhsszfModel
    {

        /// <summary>
        /// /交易码
        /// </summary>
        public string Jym{get;set;}

        /// <summary>
        /// 批次号
        /// </summary>
        public string Pch{get;set;}

        /// <summary>
        /// 付款人账号(中心)
        /// </summary>
        public string Fkrzh{get;set;}

        /// <summary>
        /// 付款人名称(中心)
        /// </summary>
        public string Fkrmc { get; set; }

        /// <summary>
        /// 付款银行名称
        /// </summary>
        public string Fkyhmc { get; set; }

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
        /// 金额
        /// </summary>
        public string Je { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Beiz { get; set; }

        /// <summary>
        /// 银行流水
        /// </summary>
        public string Yhls { get; set; }

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
            this.Fkyhmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 114, 60);
            this.Skrzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 174, 30);
            this.Skrmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 204, 60);
            this.Skrmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 264, 60);
            this.Je = BasicOperation.GetStringFromRequestMsg(recvBytes, 324, 12);
            this.Beiz = BasicOperation.GetStringFromRequestMsg(recvBytes, 336, 60);
        }
    }
}
