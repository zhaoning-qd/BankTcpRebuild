using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款发放响应报文实体
    /// </summary>
    public class DkffMsgModel
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
        /// 批次号
        /// </summary>
        public byte[] Pch { get; set; }

        /// <summary>
        /// 银行流水
        /// </summary>
        public byte[] Yhls { get; set; }

        /// <summary>
        /// 付款人账号
        /// </summary>
        public byte[] Fkrzh { get; set; }

        /// <summary>
        /// 付款人名称
        /// </summary>
        public byte[] Fkrmc { get; set; }

        /// <summary>
        /// 付款银行名称
        /// </summary>
        public byte[] Fkyhmc { get; set; }

        /// <summary>
        /// 收款人账号
        /// </summary>
        public byte[] Skrzh { get; set; }

        /// <summary>
        /// 收款人名称
        /// </summary>
        public byte[] Skrmc { get; set; }

        /// <summary>
        /// 收款银行名称
        /// </summary>
        public byte[] Skyhmc { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public byte[] Je { get; set; }

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public DkffMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Fhxx, 60);
            BasicOperation.InitializeByteArray(this.Pch, 20);
            BasicOperation.InitializeByteArray(this.Yhls, 20);
            BasicOperation.InitializeByteArray(this.Fkrzh, 30);
            BasicOperation.InitializeByteArray(this.Fkrmc, 60);
            BasicOperation.InitializeByteArray(this.Fkyhmc, 60);
            BasicOperation.InitializeByteArray(this.Skrzh, 30);
            BasicOperation.InitializeByteArray(this.Skrmc, 60);
            BasicOperation.InitializeByteArray(this.Skyhmc, 60);
            BasicOperation.InitializeByteArray(this.Je, 12);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(DkffModel model)
        {
            BasicOperation.SetByteArray(this.Length, "0420");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, "0000");
            BasicOperation.SetByteArray(this.Fhxx, "success");
            BasicOperation.SetByteArray(this.Pch, model.Pch);
            BasicOperation.SetByteArray(this.Yhls, "success");
            BasicOperation.SetByteArray(this.Fkrzh, model.Fkrzh);
            BasicOperation.SetByteArray(this.Fkrmc, model.Fkrmc);
            BasicOperation.SetByteArray(this.Fkyhmc, "中国银行山东路支行");
            BasicOperation.SetByteArray(this.Skrzh, model.Skrzh);
            BasicOperation.SetByteArray(this.Skrmc, model.Skrmc); 
            BasicOperation.SetByteArray(this.Skyhmc, model.Skyhmc);
            BasicOperation.SetByteArray(this.Je, model.Je);
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
            s += Encoding.Default.GetString(this.Pch);
            s += Encoding.Default.GetString(this.Yhls);
            s += Encoding.Default.GetString(this.Fkrzh);
            s += Encoding.Default.GetString(this.Fkrmc);
            s += Encoding.Default.GetString(this.Fkyhmc);
            s += Encoding.Default.GetString(this.Skrzh);
            s += Encoding.Default.GetString(this.Skrmc);
            s += Encoding.Default.GetString(this.Skyhmc);
            s += Encoding.Default.GetString(this.Je);

            return s;
        }
    }
}
