using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    /// <summary>
    /// 银行记账日终对账响应报文实体
    /// </summary>
    public class YhjzrzdzMsgModel
    {
        /// <summary>
        /// 包长
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
        /// 对账单名称
        /// </summary>
        public byte[] Dzdmc { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public byte[] Zhmc { get; set; }

        /// <summary>
        /// 银行账号
        /// </summary>
        public byte[] Yhzh { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public byte[] Qsrq { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        public byte[] Zzrq { get; set; }

        /// <summary>
        /// 汇总记录数
        /// </summary>
        public byte[] Hzjls { get; set; }

        /// <summary>
        /// 汇总借方笔数
        /// </summary>
        public byte[] Hzjfbs { get; set; }

        /// <summary>
        /// 汇总借方发生额
        /// </summary>
        public byte[] Hzjffsz { get; set; }

        /// <summary>
        /// 汇总贷方笔数
        /// </summary>
        public byte[] Hzdfbs { get; set; }

        /// <summary>
        /// 汇总贷方发生额
        /// </summary>
        public byte[] Hzdffse { get; set; }

        /// <summary>
        /// 对账日期
        /// </summary>
        public byte[] Dzrq { get; set; }

        /// <summary>
        /// 对账文件名称
        /// </summary>
        public byte[] Dzwjmc { get; set; }

        /// <summary>
        /// 构造函数，初始化
        /// </summary>
        public YhjzrzdzMsgModel()
        {
            BasicOperation.InitializeByteArray(this.Length, 4);
            BasicOperation.InitializeByteArray(this.Jym, 4);
            BasicOperation.InitializeByteArray(this.Fhz, 4);
            BasicOperation.InitializeByteArray(this.Dzdmc, 20);
            BasicOperation.InitializeByteArray(this.Zhmc, 60);
            BasicOperation.InitializeByteArray(this.Yhzh, 30);
            BasicOperation.InitializeByteArray(this.Qsrq, 8);
            BasicOperation.InitializeByteArray(this.Zzrq, 8);
            BasicOperation.InitializeByteArray(this.Hzjls, 8);
            BasicOperation.InitializeByteArray(this.Hzjfbs, 8);
            BasicOperation.InitializeByteArray(this.Hzjffsz, 12);
            BasicOperation.InitializeByteArray(this.Hzdfbs, 8);
            BasicOperation.InitializeByteArray(this.Hzdffse, 12);
            BasicOperation.InitializeByteArray(this.Dzrq, 8);
            BasicOperation.InitializeByteArray(this.Dzwjmc, 60);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(YhjzrzdzModel model,string fileName)
        {
            ResRtnValueModel modelRtn = new ResRtnValueModel();
            modelRtn.RtnCodeArray = new int[] { 1, 2, 4, 24, 27, 28, 32 };//返回值可能情况
            string fhz = modelRtn.GetRtnValueOnline();

            BasicOperation.SetByteArray(this.Length, "0250");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.Fhz, fhz);
            BasicOperation.SetByteArray(this.Dzdmc, "山东路分行对账单");
            BasicOperation.SetByteArray(this.Zhmc, "杭州住房公积金管理中心萧山分中心");
            BasicOperation.SetByteArray(this.Yhzh, "37546458677");
            BasicOperation.SetByteArray(this.Qsrq, model.Qsrq);
            BasicOperation.SetByteArray(this.Zzrq, model.Zzrq);
            BasicOperation.SetByteArray(this.Hzjls, "5");
            BasicOperation.SetByteArray(this.Hzjfbs, "2");
            BasicOperation.SetByteArray(this.Hzjffsz, "1000000");
            BasicOperation.SetByteArray(this.Hzdfbs, "3");
            BasicOperation.SetByteArray(this.Hzdffse, "1000000");

            DateTime dt = DateTime.Now;
            string strDate = dt.ToString("yyyyMMdd");
            BasicOperation.SetByteArray(this.Dzrq, strDate);
            BasicOperation.SetByteArray(this.Dzwjmc, fileName);

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
            result += Encoding.Default.GetString(this.Dzdmc);
            result += Encoding.Default.GetString(this.Zhmc);
            result += Encoding.Default.GetString(this.Yhzh);
            result += Encoding.Default.GetString(this.Qsrq);
            result += Encoding.Default.GetString(this.Zzrq);
            result += Encoding.Default.GetString(this.Hzjls);
            result += Encoding.Default.GetString(this.Hzjfbs);
            result += Encoding.Default.GetString(this.Hzjffsz);
            result += Encoding.Default.GetString(this.Hzdfbs);
            result += Encoding.Default.GetString(this.Hzdffse);
            result += Encoding.Default.GetString(this.Dzrq);
            result += Encoding.Default.GetString(this.Dzwjmc);

            return result;
        }
           
    }
}
