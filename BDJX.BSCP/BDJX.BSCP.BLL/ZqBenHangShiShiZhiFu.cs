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
    /// 支取--本行实时支付
    /// </summary>
    public class ZqBenHangShiShiZhiFu : IZqBenHangShiShiZhiFu
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        ZqBhsszfModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        ZqBhsszfMsgModel bhsszfMsg;

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
        public ZqBenHangShiShiZhiFu()
        {
            model = new ZqBhsszfModel();
            bhsszfMsg = new ZqBhsszfMsgModel();
            db2Operation = BdjxFactory.CreateInstance<IDb2Operation>("BDJX.BSCP.DAL.dll", "BDJX.BSCP.DAL.Db2Operation");
        }

        /// <summary>
        /// 业务处理
        /// </summary>
        public void DisposeOfBusiness(byte[] recvBytes, BllEntryPoint bllEntryPoint)
        {
            try
            {
                GenerageResponseMsg(recvBytes);
                UpdateZbInfo(BasicOperation.GetExecutePermission());
                LogHelper.WriteLogInfo("本行实时支付", "成功完成业务操作");
            }            
            catch(Exception ex)
            {
                LogHelper.WriteLogException("本行实时支付业务失败", ex);
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
            bhsszfMsg.SetValue(model);
            this.ResponseMsg = Encoding.Default.GetBytes(bhsszfMsg.ToMsgString());
        }

        /// <summary>
        /// 更新账表分户账和账表明细账
        /// </summary>
        private void UpdateZbInfo(bool execPermission)
        {
            if (execPermission)
            {            
                ZbfhzModel zbfhz = new ZbfhzModel();
                ZbmxzModel zbmxz = new ZbmxzModel();

                zbmxz.Zh = model.Skrzh;
                int iBs = db2Operation.GetCountByZh(zbmxz);
                zbmxz.Bc = (iBs + 1).ToString();
                zbmxz.Fse = model.Je;
                zbmxz.Yhls = Encoding.Default.GetString(bhsszfMsg.Yhls);
                zbmxz.Pjhm = model.Pch;
                zbmxz.Jdbz = "2";
                zbmxz.Ywlx = "1";
                zbmxz.Dfzh = model.Fkrzh;
                zbmxz.Dfhm = model.Fkrmc;
                zbmxz.Zxjsh = model.Fkrzh;

                zbfhz.Yhzh = zbmxz.Zh;
                zbfhz.Bs = zbmxz.Bc;
                zbfhz.Hm = model.Skrmc;

                db2Operation.UpateZbfhzAndZbmxz(zbmxz, zbfhz);
            }
        }

        
    }
}
