using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 卡余额查询请求报文实体
    /// </summary>
    public class KyecxModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm { get; set; }

        /// <summary>
        /// 卡折号
        /// </summary>
        public string Kzh { get; set; }

        /// <summary>
        /// 扣款金额
        /// </summary>
        public string Kkje { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Xm = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 20);
            this.Kzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 24, 30);
            this.Kkje = BasicOperation.GetStringFromRequestMsg(recvBytes, 54, 12);
            
        }
    }
}
