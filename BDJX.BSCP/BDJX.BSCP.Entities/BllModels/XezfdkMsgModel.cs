using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 小额支付代扣发起响应报文实体
    /// </summary>
    public class XezfdkMsgModel
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
        /// 批次号
        /// </summary>
        public byte[] Pch { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public XezfdkMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Pch, 20);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(XezfdkModel model)
        {
            ResRtnValueModel modelRtn = new ResRtnValueModel();
            modelRtn.RtnCodeArray = new int[] { 1, 2, 4, 24, 27, 28, 32 };//返回值可能情况
            string fhz = modelRtn.GetRtnValueOnline();

            BasicOperation.SetByteArray(this.Length, "0028");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, fhz);
            BasicOperation.SetByteArray(this.Pch, model.Pch);
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
            s += Encoding.Default.GetString(this.Pch);

            return s;
        }
    }
}
