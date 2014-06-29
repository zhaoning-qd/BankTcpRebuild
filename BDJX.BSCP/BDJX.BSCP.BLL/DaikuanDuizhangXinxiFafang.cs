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
    /// 贷款对账信息发放业务处理
    /// </summary>
    public class DaikuanDuizhangXinxiFafang :IDaikuanDuizhangXinxiFafang
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        DkdzxxffModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        DkdzxxffMsgModel modelMsg;

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
        /// 构造函数
        /// </summary>
        public DaikuanDuizhangXinxiFafang()
        {
            model = new DkdzxxffModel();
            modelMsg = new DkdzxxffMsgModel();
        }

        /// <summary>
        /// 业务处理
        /// </summary>
        public void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            try
            {
                string fileName = string.Empty;
                GenerateDuizhangDetail(out fileName);
                GenerageResponseMsg(recvBytes, fileName);
                LogHelper.WriteLogInfo("贷款对账信息发放", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("贷款对账信息发放业务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes">请求报文</param>
        private void GenerageResponseMsg(byte[] recvBytes,string fileName)
        {
            //解析请求报文
            model.GetValue(recvBytes);

            //产生响应报文
            modelMsg.SetValue(model, fileName);
            this.ResponseMsg = Encoding.Default.GetBytes(modelMsg.ToMsgString());
        }

        /// <summary>
        /// 产生对账明细文件
        /// </summary>
        /// <param name="fileName">产生的文件名称</param>
        private void GenerateDuizhangDetail(out string fileName)
        {
            fileName = model.Mxwj;
        }

    }
}
