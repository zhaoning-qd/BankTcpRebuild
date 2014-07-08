using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 贷款对账信息分发响应报文实体
    /// </summary>
    public class DkdzxxffMsgModel
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
        /// 明细文件名称
        /// </summary>
        public byte[] Mxwj { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public DkdzxxffMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Mxwj, 60);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(DkdzxxffModel model,string fileName)
        {
            ResRtnValueModel modelRtn = new ResRtnValueModel();
            modelRtn.RtnCodeArray = new int[] { 1, 2, 4, 24, 27, 28, 32 };//返回值可能情况
            string fhz = modelRtn.GetRtnValueOnline();

            BasicOperation.SetByteArray(this.Length, "0068");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, fhz);
            BasicOperation.SetByteArray(this.Mxwj, fileName);
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
            s += Encoding.Default.GetString(this.Mxwj);

            return s;
        }
    }
}
