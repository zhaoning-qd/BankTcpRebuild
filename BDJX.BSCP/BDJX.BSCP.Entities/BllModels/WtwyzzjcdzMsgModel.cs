using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 网厅缴存--网银转账缴存对账响应报文实体
    /// </summary>
    public class WtwyzzjcdzMsgModel
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
        /// 银行账号
        /// </summary>
        public byte[] Yhzh { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public byte[] Wjmc { get; set; }

        /// <summary>
        /// 总笔数
        /// </summary>
        public byte[] Zbs { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public byte[] Zje { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public WtwyzzjcdzMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Fhxx, 60);
            BasicOperation.InitializeByteArray(this.Yhzh, 30);
            BasicOperation.InitializeByteArray(this.Wjmc, 30);
            BasicOperation.InitializeByteArray(this.Zbs, 6);
            BasicOperation.InitializeByteArray(this.Zje, 16);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(WtwyzzjcdzModel model,string fileName)
        {
            BasicOperation.SetByteArray(this.Length, "0150");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, "0000");
            BasicOperation.SetByteArray(this.Fhxx, "success");
            BasicOperation.SetByteArray(this.Yhzh, model.Yhzh);
            BasicOperation.SetByteArray(this.Wjmc, fileName);
            BasicOperation.SetByteArray(this.Zbs, model.Zbs);
            BasicOperation.SetByteArray(this.Zje, model.Zje);
            
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
            s += Encoding.Default.GetString(this.Yhzh);
            s += Encoding.Default.GetString(this.Wjmc);
            s += Encoding.Default.GetString(this.Zbs);
            s += Encoding.Default.GetString(this.Zje);
            
            return s;
        }

    }
}
