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
    /// 卡余额查询 业务处理类
    /// </summary>
    public class KayuEChaxun : IKaYuEChaXun
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        KyecxModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        KyecxMsgModel modelMsg;

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
        public KayuEChaxun()
        {
            model = new KyecxModel();
            modelMsg = new KyecxMsgModel();
        }

        /// <summary>
        /// 业务处理
        /// </summary>
        public void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            try
            {
                GenerageResponseMsg(recvBytes);
                LogHelper.WriteLogInfo("卡余额查询", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("卡余额查询", ex);
                throw;
            }
        }

        /// <summary>
        /// 产生响应报文
        /// </summary>
        /// <param name="recvBytes">请求报文</param>
        private void GenerageResponseMsg(byte[] recvBytes)
        {
            //解析请求报文
            model.GetValue(recvBytes);

            //产生响应报文
            modelMsg.SetValue(model);
            this.ResponseMsg = Encoding.Default.GetBytes(modelMsg.ToMsgString());
        }
    }
}
