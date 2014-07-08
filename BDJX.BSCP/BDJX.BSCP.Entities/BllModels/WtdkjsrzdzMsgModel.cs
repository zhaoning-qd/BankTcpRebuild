using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 网厅支取实时交易日终对账响应报文实体
    /// </summary>
    public class WtdkjsrzdzMsgModel
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
        /// 机构码
        /// </summary>
        public byte[] Jgm { get; set; }

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
        /// 构造函数，初始化
        /// </summary>
        public WtdkjsrzdzMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Fhxx, 60);
            BasicOperation.InitializeByteArray(this.Jgm, 2);
            BasicOperation.InitializeByteArray(this.Wjmc, 30);
            BasicOperation.InitializeByteArray(this.Zbs, 6);
            BasicOperation.InitializeByteArray(this.Zje, 16);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(WtdkjsrzdzModel model, string fileName)
        {
            ResRtnValueModel modelRtn = new ResRtnValueModel();
            modelRtn.RtnCodeArray = new int[] { 1, 2, 4, 24, 27, 28, 32 };//返回值可能情况
            string fhz = modelRtn.GetRtnValueOnline();
            string fhxx = modelRtn.RtnInfo;

            BasicOperation.SetByteArray(this.Length, "0122");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, fhz);
            BasicOperation.SetByteArray(this.Fhxx, fhxx);
            BasicOperation.SetByteArray(this.Jgm, model.Jgm);
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
            s += Encoding.Default.GetString(this.Jgm);
            s += Encoding.Default.GetString(this.Wjmc);
            s += Encoding.Default.GetString(this.Zbs);
            s += Encoding.Default.GetString(this.Zje);

            return s;
        }
    }
}
