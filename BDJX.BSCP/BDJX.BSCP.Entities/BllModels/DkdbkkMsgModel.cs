using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款单笔扣款响应报文实体
    /// </summary>
    public class DkdbkkMsgModel
    {
        /// <summary>
        /// 数据包长
        /// </summary>
        public byte[] Length { get; set; }

        /// <summary>
        /// 交易码
        /// </summary>
        public byte[] Jym { get; set; }

        /// <summary>
        /// 返回值
        /// </summary>
        public byte[] Fhz { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        public byte[] Fhxx { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public byte[] Lx { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public byte[] Kh { get; set; }

        /// <summary>
        /// 应扣金额
        /// </summary>
        public byte[] Ykje { get; set; }

        /// <summary>
        /// 实扣金额
        /// </summary>
        public byte[] Skje { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public byte[] Xm { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public byte[] Sfz { get; set; }

        /// <summary>
        /// 差额标志
        /// </summary>
        public byte[] Cebz { get; set; }

        /// <summary>
        /// 还款期次
        /// </summary>
        public byte[] Hkqc { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public byte[] Hth { get; set; }

        /// <summary>
        /// 应扣本金
        /// </summary>
        public byte[] Ykbj { get; set; }

        /// <summary>
        /// 应扣利息
        /// </summary>
        public byte[] Yklx { get; set; }

        /// <summary>
        /// 银行流水
        /// </summary>
        public byte[] Yhls { get; set; }

        /// <summary>
        /// 预留
        /// </summary>
        public byte[] Yl { get; set; }

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public DkdbkkMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Fhxx, 60);
            BasicOperation.InitializeByteArray(this.Lx, 2);
            BasicOperation.InitializeByteArray(this.Kh, 30);
            BasicOperation.InitializeByteArray(this.Ykje, 10);
            BasicOperation.InitializeByteArray(this.Skje, 10);
            BasicOperation.InitializeByteArray(this.Xm, 20);
            BasicOperation.InitializeByteArray(this.Sfz, 18);
            BasicOperation.InitializeByteArray(this.Cebz, 1);
            BasicOperation.InitializeByteArray(this.Hkqc, 3);
            BasicOperation.InitializeByteArray(this.Hth, 12);
            BasicOperation.InitializeByteArray(this.Ykbj, 10);
            BasicOperation.InitializeByteArray(this.Yklx, 10);
            BasicOperation.InitializeByteArray(this.Yhls, 20);
            BasicOperation.InitializeByteArray(this.Yl, 60);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(DkdbkkModel model)
        {
            BasicOperation.SetByteArray(this.Length, "0274");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, "0000");
            BasicOperation.SetByteArray(this.Fhxx, "success");
            BasicOperation.SetByteArray(this.Lx, "22");
            BasicOperation.SetByteArray(this.Kh, model.Kh);
            BasicOperation.SetByteArray(this.Ykje, model.Ykje);
            BasicOperation.SetByteArray(this.Skje, model.Skje);
            BasicOperation.SetByteArray(this.Xm, model.Xm);
            BasicOperation.SetByteArray(this.Sfz, model.Sfz);
            BasicOperation.SetByteArray(this.Cebz, model.Cebz);
            BasicOperation.SetByteArray(this.Hkqc, model.Hkqc);
            BasicOperation.SetByteArray(this.Hth, model.Hth);
            BasicOperation.SetByteArray(this.Ykbj, model.Ykbj);
            BasicOperation.SetByteArray(this.Yklx, model.Yklx);
            //银行流水
            Random radom = new Random();
            BasicOperation.SetByteArray(this.Yhls, BasicOperation.GenerateLongBankSerialNum(radom.Next(99)));
            BasicOperation.SetByteArray(this.Yl, model.Yl);
        }

        /// <summary>
        /// 实体字段转换为字符串
        /// </summary>
        /// <returns>字符串形式的响应报文</returns>
        public string ToMsgString()
        {
            string s = string.Empty;
            s += Encoding.Default.GetString(this.Length);
            s += Encoding.Default.GetString(this.Jym);
            s += Encoding.Default.GetString(this.Fhz);
            s += Encoding.Default.GetString(this.Fhxx);
            s += Encoding.Default.GetString(this.Lx);
            s += Encoding.Default.GetString(this.Kh);
            s += Encoding.Default.GetString(this.Ykje);
            s += Encoding.Default.GetString(this.Skje);
            s += Encoding.Default.GetString(this.Xm);
            s += Encoding.Default.GetString(this.Sfz);
            s += Encoding.Default.GetString(this.Cebz);
            s += Encoding.Default.GetString(this.Hkqc);
            s += Encoding.Default.GetString(this.Hth);
            s += Encoding.Default.GetString(this.Ykbj);
            s += Encoding.Default.GetString(this.Yklx);
            s += Encoding.Default.GetString(this.Yhls);
            s += Encoding.Default.GetString(this.Yl);

            return s;
        }
    }
}
