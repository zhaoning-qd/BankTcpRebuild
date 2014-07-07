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
    /// 网厅缴存--直扣交易日终对账
    /// </summary>
    public class WtZhikouRizhongduizhang :IWtZhikouRizhongduizhang
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        WtzkjyrzdzModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        WtzkjyrzdzMsgModel modelMsg;

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
        public WtZhikouRizhongduizhang()
        {
            model = new WtzkjyrzdzModel();
            modelMsg = new WtzkjyrzdzMsgModel();
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
                GenerateDuiZhangDetail(bllEntryPoint.Hb, out fileName);
                GenerageResponseMsg(fileName);
                LogHelper.WriteLogInfo("网厅缴存--直扣交易日终对账", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("网厅缴存--直扣交易日终对账业务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes">请求报文</param>
        private void GenerageResponseMsg(string fileName)
        {
            //产生响应报文
            modelMsg.SetValue(model, fileName);
            this.ResponseMsg = Encoding.Default.GetBytes(modelMsg.ToMsgString());
        }

        /// <summary>
        /// 产生对账明细
        /// </summary>
        private void GenerateDuiZhangDetail(string hb, out string outFileName)
        {
            string fileName = "";
            fileName += model.Jgm;
            fileName += "G50";
            fileName += "_W";
            DateTime dt = new DateTime();
            string strDate = dt.ToString("yyyyMMdd");
            fileName += strDate;
            fileName += ".";
            fileName += "380910";//6位银行代号
            outFileName = fileName;

            List<ZbmxzModel> list = db2Operation.GetZbmxzByRqrq(db2Operation.GetDjrqrq());
            string filePath = BasicOperation.GetFilePath(hb) + fileName;//文件的完整路径
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312")))
            {
                //汇总行：机构码,交易日期,总金额,总笔数
                string summaryLine = string.Empty;
                summaryLine += model.Jgm;
                summaryLine += ",";
                summaryLine += strDate;
                summaryLine += ",";
                summaryLine += model.Zje;
                summaryLine += ",";
                summaryLine += model.Zbs;
                summaryLine += ",";
                sw.WriteLine(summaryLine);
            }

            //明细行
            for (int i = 1; i <= list.Count; i++)
            {
                string detailLine = string.Empty;
                detailLine += i.ToString();
                detailLine += ",";
                detailLine += list[i].Jyrq;
                detailLine += ",";
                detailLine += list[i].Jysj;
                detailLine += ",";
                detailLine += BasicOperation.GenerateBatchCode("110000000", i);//批次号
                detailLine += ",";
                detailLine += BasicOperation.GenerateName("李", i);
                detailLine += ",";
                detailLine += list[i].Zh;
                detailLine += ",";
                detailLine += list[i].Fse;
                detailLine += ",";
                detailLine += list[i].Yhls;//银行流水
                detailLine += ",";
                detailLine += list[i].Jdbz;//记账标志
                detailLine += ",";
                detailLine += list[i].Yhls;//备注中添写银行流水号
                detailLine += ",";

                using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.GetEncoding("gb2312")))
                {
                    sw.WriteLine(detailLine);
                }
            }
        }
    }
}
