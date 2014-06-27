using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using BDJX.BSCP.IBLL;
using BDJX.BSCP.Common;
using BDJX.BSCP.Entities.BllModels;
using BDJX.BSCP.IDAL;

namespace BDJX.BSCP.BLL
{
    /// <summary>
    /// 银行对账日张对账业务处理类
    /// </summary>
    public class YinHangJiZhangRiZhongDuiZhang : IYinHangJiZhangRiZhongDuiZhang
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        YhjzrzdzModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        YhjzrzdzMsgModel modelMsg;

        /// <summary>
        /// 用于实现IBllManagment接口中的ResponseMsg属性
        /// </summary>
        byte[] responseMsg;

        /// <summary>
        /// 响应报文--已在IBllManagment接口中声明
        /// </summary>
        public byte[] ResponseMsg
        {
            get
            {
                return responseMsg;
            }
            set
            {
                responseMsg = value;
            }
        }

        /// <summary>
        /// 数据库操作类
        /// </summary>
        IDb2Operation db2Operation;

        /// <summary>
        /// 构造函数
        /// </summary>
        public YinHangJiZhangRiZhongDuiZhang()
        {
            model = new YhjzrzdzModel();
            modelMsg = new YhjzrzdzMsgModel();
            db2Operation = BdjxFactory.CreateInstance<IDb2Operation>("BDJX.BSCP.DAL.dll", "BDJX.BSCP.DAL.Db2Operation");
        }

        /// <summary>
        /// 业务处理
        /// </summary>
        public void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            try
            {
                //解析请求报文
                model.GetValue(recvBytes);

                string fileName = string.Empty;
                fileName = this.GenetrateCountCheckingFile(bllEntryPoint.Hb,model.Jym,model.Yhzh,model.Qsrq,model.Zzrq);
                GenerageResponseMsg(recvBytes,fileName);

                LogHelper.WriteLogInfo("银行记账日终对账", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("银行记账日终对账业务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes">请求报文</param>
        private void GenerageResponseMsg(byte[] recvBytes,string fileName)
        {
            //产生响应报文
            modelMsg.SetValue(model, fileName);
            this.ResponseMsg = Encoding.Default.GetBytes(modelMsg.ToMsgString());
        }

        /// <summary>
        /// 银行日记账对账--生成对账明细
        /// </summary>
        /// <param name="whichBank">行别</param>
        /// <param name="transcationCode">交易码</param>
        /// <param name="bankCount">银行账号</param>
        /// <param name="qsrq">起始日期</param>
        /// <param name="zzrq">终止日期</param>
        /// <returns>对账文件名称</returns>
        private string GenetrateCountCheckingFile(string whichBank, string transcationCode, string bankCount, string qsrq, string zzrq)
        {
            string fileName = string.Empty;
            fileName += "YHDZ";
            fileName += bankCount;
            fileName += "_";
            fileName += qsrq;
            fileName += "_";
            fileName += zzrq;

            //详细文件内容
            List<ZbmxzModel> list = db2Operation.GetZbmxzByJyrq(model.Qsrq, model.Zzrq);
            string filePath = BasicOperation.GetFilePath(whichBank) + fileName;//文件的完整路径

            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);

            //明细行
            for (int i = 1; i <= list.Count; i++)
            {
                string detailLine = string.Empty;
                detailLine += "M";
                detailLine += "~";
                detailLine += "杭州住房公积金管理中心萧山分中心";//账户名称，需要修改
                detailLine += "~";
                detailLine += list[i].Zh;
                detailLine += "~";
                detailLine += list[i].Yhls;
                detailLine += "~";
                detailLine += list[i].Pjhm;
                detailLine += "~";
                detailLine += list[i].Jyrq;
                detailLine += "~";
                detailLine += list[i].Jysj;
                detailLine += "~";
                detailLine += "划款";//摘要
                detailLine += "~";
                detailLine += list[i].Jdbz;
                detailLine += "~";
                detailLine += list[i].Fse;
                detailLine += "~";
                detailLine += list[i].Ye;
                detailLine += "~";
                detailLine += list[i].Dfhm;
                detailLine += "~";
                detailLine += list[i].Dfzh;
                detailLine += "~";

                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.GetEncoding("gb2312")))
                {
                    sw.WriteLine(detailLine);
                }

                //汇总行
                using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312")))
                {
                    string summaryLine = string.Empty;
                    summaryLine += "H";
                    summaryLine += "~";
                    summaryLine += Encoding.Default.GetString(modelMsg.Hzjfbs);//借方笔数
                    summaryLine += "~";
                    summaryLine += Encoding.Default.GetString(modelMsg.Hzjffsz);//借方发生额
                    summaryLine += "~";
                    summaryLine += Encoding.Default.GetString(modelMsg.Hzdfbs);//贷方笔数
                    summaryLine += "~";
                    summaryLine += Encoding.Default.GetString(modelMsg.Hzdffse);//贷方发生额
                    summaryLine += "~";
                    summaryLine += "435654";//余额
                    summaryLine += "~";
                    sw.WriteLine(summaryLine);//汇总行
                }
            }

            return fileName;
        }
    }
}
