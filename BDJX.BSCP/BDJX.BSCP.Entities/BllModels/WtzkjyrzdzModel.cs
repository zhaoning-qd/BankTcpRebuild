using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 网厅缴存--直扣交易日终对账请求报文实体
    /// </summary>
    public class WtzkjyrzdzModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 开始批次号
        /// </summary>
        public string Kspch { get; set; }

        /// <summary>
        /// 结束批次号
        /// </summary>
        public string Jspch { get; set; }

        /// <summary>
        /// 机构码
        /// </summary>
        public string Jgm { get; set; }

        /// <summary>
        /// 总笔数
        /// </summary>
        public string Zbs { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public string Zje { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Kspch = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 20);
            this.Jspch = BasicOperation.GetStringFromRequestMsg(recvBytes, 24, 20);
            this.Jgm = BasicOperation.GetStringFromRequestMsg(recvBytes, 44, 2);
            this.Zbs = BasicOperation.GetStringFromRequestMsg(recvBytes, 46, 6);
            this.Zje = BasicOperation.GetStringFromRequestMsg(recvBytes, 52, 16);
        }
    }
}
