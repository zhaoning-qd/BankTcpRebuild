using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BDJX.BSCP.IBLL;
using BDJX.BSCP.Common;
using BDJX.BSCP.Entities.BllModels;

namespace BDJX.BSCP.BLL
{
    /// <summary>
    /// 支取--本行实时支付
    /// </summary>
    public class ZqBenHangShiShiZhiFu : IZqBenHangShiShiZhiFu
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        ZqBhsszfModel model = new ZqBhsszfModel();

        /// <summary>
        /// 响应报文实体
        /// </summary>
        ZqBhsszfMsgModel bhsszfMsg = new ZqBhsszfMsgModel();

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
        /// 业务处理
        /// </summary>
        public void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            try
            {
                GenerageResponseMsg(recvBytes);
                LogHelper.WriteLogInfo("本行实时支付", "成功完成业务操作");
            }            
            catch(Exception ex)
            {
                LogHelper.WriteLogException("本行实时支付", ex);
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes"></param>
        private void GenerageResponseMsg(byte[] recvBytes)
        {
            //解析请求报文
            model.GetValue(recvBytes);

            //产生响应报文
            bhsszfMsg.SetValue(model);
            this.ResponseMsg = Encoding.Default.GetBytes(bhsszfMsg.ToMsgString());
        }

        
    }
}
