using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 卡余额查询响应报文实体
    /// </summary>
    public class KyecxMsgModel
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
        /// 姓名
        /// </summary>
        public byte[] Xm { get; set; }

        /// <summary>
        /// 卡折号
        /// </summary>
        public byte[] Kzh { get; set; }

        /// <summary>
        /// 标志--余额是否足够
        /// </summary>
        public byte[] Bz{ get; set; }

        /// <summary>
        /// 卡状态
        /// </summary>
        public byte[] Kzt { get; set; }

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public KyecxMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Xm, 20);
            BasicOperation.InitializeByteArray(this.Kzh, 30);
            BasicOperation.InitializeByteArray(this.Bz, 1);
            BasicOperation.InitializeByteArray(this.Kzt, 1);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(KyecxModel model)
        {
            ResRtnValueModel modelRtn = new ResRtnValueModel();
            modelRtn.RtnCodeArray = new int[] { 1, 2, 4,16, 24, 27, 28, 32 };//返回值可能情况
            string fhz = modelRtn.GetRtnValueOnline();

            BasicOperation.SetByteArray(this.Length, "0060");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, fhz);
            BasicOperation.SetByteArray(this.Xm, model.Xm);
            BasicOperation.SetByteArray(this.Kzh, model.Kzh);
            BasicOperation.SetByteArray(this.Bz, "1");
            BasicOperation.SetByteArray(this.Kzt, "2");
        }

        /// <summary>
        /// 实体字段转换为字符串
        /// </summary>
        /// <returns>字符串形式的响应报文</returns>
        public string ToMsgString()
        {
            string result = string.Empty;
            result += Encoding.Default.GetString(this.Length);
            result += Encoding.Default.GetString(this.Jym);
            result += Encoding.Default.GetString(this.Fhz);
            result += Encoding.Default.GetString(this.Xm);
            result += Encoding.Default.GetString(this.Kzh);
            result += Encoding.Default.GetString(this.Bz);
            result += Encoding.Default.GetString(this.Kzt);
          
            return result;
        }
    }
}
