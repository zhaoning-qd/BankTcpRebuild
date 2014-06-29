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
    /// 小额支付代扣发起业务处理
    /// </summary>
    public class XiaoezhifuDaikoufaqi :IXiaoezhifuDaikoufaqi
    {
        /// <summary>
        /// 请求报文实体
        /// </summary>
        XezfdkModel model;

        /// <summary>
        /// 响应报文实体
        /// </summary>
        XezfdkMsgModel modelMsg;

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

        public XiaoezhifuDaikoufaqi()
        {
            model = new XezfdkModel();
            modelMsg = new XezfdkMsgModel();
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
                ExecuteDetail(bllEntryPoint.Hb, model);
                LogHelper.WriteLogInfo("小额支付代扣发起", "成功完成业务操作");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("小额支付代扣发起业务失败", ex);
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

        /// <summary>
        /// 业务处理详情
        /// </summary>
        /// <param name="hb">行别</param>
        /// <param name="model">请求报文</param>
        private void ExecuteDetail(string hb, XezfdkModel model)
        {
            //具体处理过程;
            //从ftp服务器取文件,本程序中是从本机读取;
            string fileFromPath = BasicOperation.GetFilePath(hb);
            string inputLine = "";
            StringBuilder outputLine;

            DateTime dt = DateTime.Now;
            string strDate = dt.ToString("yyyyMMdd");
            string tail = model.Wjmc.Substring(4);
            string outFile = "HRB_" + tail;//返回文件的名称 ;
            string filePath = fileFromPath + outFile;

            using (StreamReader sr = new StreamReader(fileFromPath + model.Wjmc, Encoding.GetEncoding("gb2312")))
            {
                inputLine = sr.ReadLine();//读取第一行汇总数据;
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding("gb2312")))
                {
                    sw.WriteLine(inputLine);
                }
                for (int i = 1; i <= Convert.ToInt32(model.Zbs); i++)
                {
                    inputLine = sr.ReadLine();

                    string[] inputArray = inputLine.Split(new char[] { '~' });
                    string kkzt = BatchWithHolding(1);//扣款状态,全部返回成功;
                    string kkxx = "0" + kkzt;//扣款信息;
                    //生成银行流水号;
                    string yhlsh = "";
                    yhlsh += strDate;
                    if (i < 10)
                    {
                        yhlsh += "0";
                    }
                    yhlsh += i.ToString();

                    outputLine = new StringBuilder();
                    outputLine.Append("M~");
                    outputLine.Append(inputArray[1]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[2]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[3]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[4]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[5]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[6]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[7]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[8]);
                    outputLine.Append("~");
                    outputLine.Append(kkzt);
                    outputLine.Append("~");
                    outputLine.Append(kkxx);
                    outputLine.Append("~");
                    outputLine.Append(i.ToString());//汇划报文顺序号
                    outputLine.Append("~");
                    outputLine.Append(yhlsh);
                    outputLine.Append("~");
                    outputLine.Append(strDate);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[9]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[10]);
                    outputLine.Append("~");
                    outputLine.Append(inputArray[11]);
                    outputLine.Append("~");

                    using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.GetEncoding("gb2312")))
                    {
                        sw.WriteLine(outputLine.ToString());
                    }

                    //更新账表分户账和账表明细账
                    UpdateZbInfo(BasicOperation.GetExecutePermission(), inputArray);
                }
            }

            //模拟前置机动作：更新djplzxzf的zt字段;
            string command = "update djplzxzf set zt='3' where djhm='" + model.Pch+ "'";
            db2Operation.ExecuteDB2Update(command);
        }

        /// <summary>
        /// 批量扣款结果;
        /// </summary>
        /// <param name="zt"></param>
        /// <returns></returns>
        private string BatchWithHolding(int zt)
        {
            //扣款，返回结果;
            switch (zt)
            {
                case 1://成功;
                    return "00";
                case 2://账号不存在;
                    return "01";
                case 3://账户名不存在;
                    return "02";
                case 4://账户余额不足支付;
                    return "03";
                case 5://账户密码错误;
                    return "10";
                case 6://账户状态错误;
                    return "11";
                case 7://业务已撤销;
                    return "20";
                case 8://其它错误;
                    return "90";
                case 9://回执超时;
                    return "0b";
                default:
                    return "";

            }
        }

        /// <summary>
        /// 更新账表分户账和账表明细账
        /// </summary>
        private void UpdateZbInfo(bool execPermission,string[] lineArray)
        {
            if (execPermission)
            {
                ZbfhzModel zbfhz = new ZbfhzModel();
                ZbmxzModel zbmxz = new ZbmxzModel();

                zbmxz.Zh = lineArray[5];
                int iBs = db2Operation.GetCountByZh(zbmxz);
                zbmxz.Bc = (iBs + 1).ToString();
                zbmxz.Fse = lineArray[3];

                Random radom = new Random();
                zbmxz.Yhls = BasicOperation.GenerateLongBankSerialNum(radom.Next(99));

                zbmxz.Yhls = model.Pch;
                zbmxz.Pjhm = lineArray[10];
                zbmxz.Jdbz = "2";
                zbmxz.Ywlx = "1";
                zbmxz.Dfzh = lineArray[2];
                zbmxz.Dfhm = lineArray[3];
                zbmxz.Zxjsh = lineArray[1];

                zbfhz.Yhzh = zbmxz.Zh;
                zbfhz.Bs = zbmxz.Bc;
                zbfhz.Hm = lineArray[6];

                db2Operation.UpateZbfhzAndZbmxz(zbmxz, zbfhz);
            }
        }
    }
}
