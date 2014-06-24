using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.Common;

namespace BDJX.BSCP.Entities.BllModels
{
    public class ZqBhsszfMsgModel
    {
        /// <summary>
        /// 数据包长度
        /// </summary>
        public byte[] length{get;set;}
        
        /// <summary>
        /// 交易码
        /// </summary>
        public byte[] Jym{get;set;}

        /// <summary>
        /// 返回值
        /// </summary>
        public byte[] ReturnCode{get;set;}

        /// <summary>
        /// 批次号
        /// </summary>
        public byte[] Pch{get;set;}

        /// <summary>
        /// 银行流水
        /// </summary>
        public byte[] Yhls{get;set;}

        /// <summary>
        /// 付款人账号
        /// </summary>
        public byte[] Fkrzh{get;set;}

        /// <summary>
        /// 付款人名称（中心）
        /// </summary>
        public byte[] Fkrmc{get;set;}

        /// <summary>
        /// 付款银行名称
        /// </summary>
        public byte[] Fkyhmc{get;set;}

        /// <summary>
        /// 收款人账号
        /// </summary>
        public byte[] Skrzh{get;set;}

        /// <summary>
        /// 收款人名称
        /// </summary>
        public byte[] Skrmc{get;set;}

        /// <summary>
        /// 收款银行名称
        /// </summary>
        public byte[] Skyhmc{get;set;}

        /// <summary>
        /// 金额
        /// </summary>
        public byte[] Je{get;set;}

        /// <summary>
        /// 备注
        /// </summary>
        public byte[] Beiz{get;set;}

        /// <summary>
        /// 构造函数，初始化字节数组
        /// </summary>
        public ZqBhsszfMsgModel()
        {
            BasicOperation.InitializeByteArray(length,4);
            BasicOperation.InitializeByteArray(Jym,4);
            BasicOperation.InitializeByteArray(ReturnCode,4);
            BasicOperation.InitializeByteArray(Pch,20);
            BasicOperation.InitializeByteArray(Yhls,20);
            BasicOperation.InitializeByteArray(Fkrzh,30);
            BasicOperation.InitializeByteArray(Fkrmc,60);
            BasicOperation.InitializeByteArray(Fkyhmc,60);
            BasicOperation.InitializeByteArray(Skrzh,30);
            BasicOperation.InitializeByteArray(Skrmc,60);
            BasicOperation.InitializeByteArray(Skyhmc,60);
            BasicOperation.InitializeByteArray(Je,12);
            BasicOperation.InitializeByteArray(Beiz,60);
        }

        /// <summary>
        /// 设置响应报文
        /// </summary>
        /// <param name="model">请求报文信息实体</param>
        public void SetValue(ZqBhsszfModel model)
        {
            BasicOperation.SetByteArray(this.length, "0420");
            BasicOperation.SetByteArray(this.Jym, model.Jym);
            BasicOperation.SetByteArray(this.ReturnCode, "0000");
            BasicOperation.SetByteArray(this.Pch, model.Pch);

            //银行流水
            Random radom = new Random();
            BasicOperation.SetByteArray(this.Yhls, BasicOperation.GenerateLongBankSerialNum(radom.Next(99)));

            BasicOperation.SetByteArray(this.Fkrzh, model.Fkrzh);
            BasicOperation.SetByteArray(this.Fkrmc, model.Fkrmc);
            BasicOperation.SetByteArray(this.Fkyhmc, model.Fkyhmc);
            BasicOperation.SetByteArray(this.Skrzh, model.Skrzh);
            BasicOperation.SetByteArray(this.Skrmc, model.Skrmc);
            BasicOperation.SetByteArray(this.Skyhmc, model.Skyhmc);
            BasicOperation.SetByteArray(this.Je, model.Je);
            BasicOperation.SetByteArray(this.Beiz, model.Beiz);
        }

        /// <summary>
        /// 实体字段转换为字符串
        /// </summary>
        /// <returns>字符串形式的响应报文</returns>
        public string ToMsgString()
        {
            string result = string.Empty;
            result += Encoding.Default.GetString(this.length);
            result += Encoding.Default.GetString(this.Jym);
            result += Encoding.Default.GetString(this.ReturnCode);
            result += Encoding.Default.GetString(this.Pch);
            result += Encoding.Default.GetString(this.Yhls);
            result += Encoding.Default.GetString(this.Fkrzh);
            result += Encoding.Default.GetString(this.Fkrmc);
            result += Encoding.Default.GetString(this.Fkyhmc);
            result += Encoding.Default.GetString(this.Skrzh);
            result += Encoding.Default.GetString(this.Skrmc);
            result += Encoding.Default.GetString(this.Skyhmc);
            result += Encoding.Default.GetString(this.Je);
            result += Encoding.Default.GetString(this.Beiz);

            return result;
        }
    }
}
