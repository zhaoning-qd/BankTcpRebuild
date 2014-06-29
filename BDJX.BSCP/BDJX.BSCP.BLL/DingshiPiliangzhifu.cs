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
    /// 定时批量支付
    /// </summary>
    public class DingshiPiliangzhifu : IDingshiPiliangzhifu
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        DsplzfModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        DsplzfMsgModel modelMsg;

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
        public DingshiPiliangzhifu()
        {
            model = new DsplzfModel();
            modelMsg = new DsplzfMsgModel();
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
                GeneratePiLiangFile(out fileName);
                GenerageResponseMsg(recvBytes, fileName);
                LogHelper.WriteLogInfo("网厅缴存--网银转账缴存对账", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("网厅缴存--网银转账缴存对账业务失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes">请求报文</param>
        private void GenerageResponseMsg(byte[] recvBytes, string fileName)
        {
            //产生响应报文
            modelMsg.SetValue(model, fileName);
            this.ResponseMsg = Encoding.Default.GetBytes(modelMsg.ToMsgString());
        }

        /// <summary>
        /// 产生批量支付文件
        /// </summary>
        /// <param name="fileName"></param>
        private void GeneratePiLiangFile(out string fileName)
        {
            fileName = model.Wjmc;
        }
    }
}
