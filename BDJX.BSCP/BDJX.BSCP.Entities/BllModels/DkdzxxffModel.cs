using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款对账信息分发请求报文实体
    /// </summary>
    public class DkdzxxffModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 明细文件名称
        /// </summary>
        public string Mxwj { get; set; }

        /// <summary>
        /// 明细笔数
        /// </summary>
        public string Mxbs { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public string Qsrq { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public string Zzrq { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Mxwj = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 60);
            this.Mxbs = BasicOperation.GetStringFromRequestMsg(recvBytes, 64, 4);
            this.Qsrq = BasicOperation.GetStringFromRequestMsg(recvBytes, 68, 8);
            this.Zzrq = BasicOperation.GetStringFromRequestMsg(recvBytes, 76, 8);            
        }
    }
}
