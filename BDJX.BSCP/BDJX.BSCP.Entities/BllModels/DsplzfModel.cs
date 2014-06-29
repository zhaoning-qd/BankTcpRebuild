using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 定时批量支付请求报文实体
    /// </summary>
    public class DsplzfModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string Pch { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Wjmc { get; set; }

        /// <summary>
        /// 付款账号（中心）
        /// </summary>
        public string Fkzh { get; set; }

        /// <summary>
        /// 笔数
        /// </summary>
        public string Bs { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public string Je { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Beiz { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Pch = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 20);
            this.Wjmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 24, 30);
            this.Fkzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 54, 30);
            this.Bs = BasicOperation.GetStringFromRequestMsg(recvBytes, 84, 6);
            this.Je = BasicOperation.GetStringFromRequestMsg(recvBytes, 90, 12);
            this.Beiz = BasicOperation.GetStringFromRequestMsg(recvBytes, 106, 60);

        }
    }
}
