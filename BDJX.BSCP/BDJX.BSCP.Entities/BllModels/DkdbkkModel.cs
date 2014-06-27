using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款单笔扣款请求报文实体
    /// </summary>
    public class DkdbkkModel
    {
        /// <summary>
        /// 交易码
        /// </summary>
        public string Jym { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Lx { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string Kh { get; set; }

        /// <summary>
        /// 应扣金额
        /// </summary>
        public string Ykje { get; set; }

        /// <summary>
        /// 实扣金额
        /// </summary>
        public string Skje { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Xm { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string Sfz { get; set; }

        /// <summary>
        /// 差额标志
        /// </summary>
        public string Cebz { get; set; }

        /// <summary>
        /// 还款期次
        /// </summary>
        public string Hkqc { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string Hth { get; set; }

        /// <summary>
        /// 应扣本金
        /// </summary>
        public string Ykbj { get; set; }

        /// <summary>
        /// 应扣利息
        /// </summary>
        public string Yklx { get; set; }

        /// <summary>
        /// 卡所在开户行号
        /// </summary>
        public string Kszkhhh { get; set; }

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
        public string Skryhmc { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        public string Yl { get; set; }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="recvBytes"></param>
        public void GetValue(byte[] recvBytes)
        {
            this.Jym = BasicOperation.GetStringFromRequestMsg(recvBytes, 0, 4);
            this.Lx = BasicOperation.GetStringFromRequestMsg(recvBytes, 4, 2);
            this.Kh = BasicOperation.GetStringFromRequestMsg(recvBytes, 6, 30);
            this.Ykje = BasicOperation.GetStringFromRequestMsg(recvBytes, 36, 10);
            this.Skje = BasicOperation.GetStringFromRequestMsg(recvBytes, 46, 10);
            this.Xm = BasicOperation.GetStringFromRequestMsg(recvBytes, 56, 20);
            this.Sfz = BasicOperation.GetStringFromRequestMsg(recvBytes, 76, 18);
            this.Cebz = BasicOperation.GetStringFromRequestMsg(recvBytes, 94, 1);
            this.Hkqc = BasicOperation.GetStringFromRequestMsg(recvBytes, 95, 3);
            this.Hth = BasicOperation.GetStringFromRequestMsg(recvBytes, 98, 12);
            this.Ykbj = BasicOperation.GetStringFromRequestMsg(recvBytes, 110, 10);
            this.Yklx = BasicOperation.GetStringFromRequestMsg(recvBytes, 120, 10);
            this.Kszkhhh = BasicOperation.GetStringFromRequestMsg(recvBytes, 130, 12);
            this.Skrzh = BasicOperation.GetStringFromRequestMsg(recvBytes, 142, 30);
            this.Skrmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 172, 60);
            this.Skryhmc = BasicOperation.GetStringFromRequestMsg(recvBytes, 232, 60);
            this.Yl = BasicOperation.GetStringFromRequestMsg(recvBytes, 292, 60);
        }


    }
}
