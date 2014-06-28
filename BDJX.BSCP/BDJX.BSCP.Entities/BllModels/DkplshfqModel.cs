using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款批量收回发起请求报文实体
    /// </summary>
    public class DkplshfqModel
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
        /// 总记录数
        /// </summary>
        public string Zjls { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public string Zje { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string Wjmc { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Pch = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 20);
            this.Zjls = BasicOperation.GetStringFromRequestMsg(recvBytes, 24, 6);
            this.Zje = BasicOperation.GetStringFromRequestMsg(recvBytes, 30, 12);
            this.Wjmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 42, 30);
           
        }
    }
}
