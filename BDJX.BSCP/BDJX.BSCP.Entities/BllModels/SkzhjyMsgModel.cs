using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 收款账户检验响应报文实体
    /// </summary>
    public class SkzhjyMsgModel
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
        /// 账号
        /// </summary>
        public byte[] Zh { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public byte[] Zhmc { get; set; }

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public SkzhjyMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Fhxx, 60);
            BasicOperation.InitializeByteArray(this.Zh, 30);
            BasicOperation.InitializeByteArray(this.Zhmc, 60);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(SkzhjyModel model)
        {
            BasicOperation.SetByteArray(this.Length, "0158");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, "0000");
            BasicOperation.SetByteArray(this.Fhxx, "success");
            BasicOperation.SetByteArray(this.Zh, model.Zh);
            BasicOperation.SetByteArray(this.Zhmc, model.Zhmc);
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
            s += Encoding.Default.GetString(this.Zh);
            s += Encoding.Default.GetString(this.Zhmc);

            return s;
        }
    }
}
