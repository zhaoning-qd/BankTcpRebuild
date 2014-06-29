using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 网厅缴存--网银转账缴存对账请求报文实体
    /// </summary>
    public class WtwyzzjcdzModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public string Yhzh { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public string Qsrq { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public string Zzrq { get; set; }

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
            this.Yhzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 30);
            this.Qsrq = BasicOperation.GetStringFromRequestMsg(recvBytes, 34, 8);
            this.Zzrq = BasicOperation.GetStringFromRequestMsg(recvBytes, 42, 8);
            this.Zbs = BasicOperation.GetStringFromRequestMsg(recvBytes, 50, 6);
            this.Zje = BasicOperation.GetStringFromRequestMsg(recvBytes, 56, 16);


        }
    }
}
