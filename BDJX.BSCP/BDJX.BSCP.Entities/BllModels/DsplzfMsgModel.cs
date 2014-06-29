using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 定时批量支付相应报文实体
    /// </summary>
    public class DsplzfMsgModel
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
        /// 文件名称
        /// </summary>
        public byte[] Wjmc { get; set; }

        /// <summary>
        /// 笔数
        /// </summary>
        public byte[] Bs { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public byte[] Je { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public byte[] Beiz { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DsplzfMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Pch, 20);
            BasicOperation.InitializeByteArray(this.Wjmc, 30);
            BasicOperation.InitializeByteArray(this.Bs, 6);
            BasicOperation.InitializeByteArray(this.Je, 12);
            BasicOperation.InitializeByteArray(this.Beiz, 60);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(DsplzfModel model,string fileName)
        {
            BasicOperation.SetByteArray(this.Length, "0136");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, "0000");
            BasicOperation.SetByteArray(this.Pch, model.Pch);
            BasicOperation.SetByteArray(this.Wjmc, fileName);
            BasicOperation.SetByteArray(this.Bs, model.Bs);
            BasicOperation.SetByteArray(this.Je, model.Je);
            BasicOperation.SetByteArray(this.Beiz, model.Beiz);
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
            s += Encoding.Default.GetString(this.Wjmc);
            s += Encoding.Default.GetString(this.Bs);
            s += Encoding.Default.GetString(this.Je);
            s += Encoding.Default.GetString(this.Beiz);

            return s;
        }

    }
}
