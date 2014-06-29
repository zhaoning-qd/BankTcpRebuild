using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.IBLL;
using BDJX.BSCP.Common;
using BDJX.BSCP.Entities.BllModels;
using BDJX.BSCP.IDAL;

namespace BDJX.BSCP.BLL
{
    /// <summary>
    /// 网厅支取——实时交易日终对账
    /// </summary>
    public class WtzqShishijiaoyiRizhongduizhang :IWtzqShishijiaoyiRizhongduizhang
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        WtzqssjyrzdzModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        WtzqssjyrzdzMsgModel modelMsg;

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
        public WtzqShishijiaoyiRizhongduizhang()
        {
            model = new WtzqssjyrzdzModel();
            modelMsg = new WtzqssjyrzdzMsgModel();
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
                GenerateDuiZhangDetail(out fileName);
                GenerageResponseMsg(fileName);
                LogHelper.WriteLogInfo("网厅支取——实时交易日终对账", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("网厅支取——实时交易日终对账业务失败", ex);
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
        private void GenerateDuiZhangDetail(out string outFileName)
        {
            string strDate = DateTime.Now.ToShortDateString();
            string fileName = "";
            fileName += model.Jgm;
            fileName += "Z";//支取
            fileName += "_W";
            fileName += strDate;
            fileName += ".";
            fileName += "380910";//6位银行代号

            outFileName = fileName;
        }

    }
}
