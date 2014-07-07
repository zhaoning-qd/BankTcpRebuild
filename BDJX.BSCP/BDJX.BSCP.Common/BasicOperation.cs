using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDJX.BSCP.Common
{
    /// <summary>
    /// 基本操作类
    /// </summary>
    public class BasicOperation
    {
        /// <summary>
        /// 显示信息到客户端
        /// </summary>
        /// <param name="info">要显示的信息</param>
        public static void ShowInfo(string info)
        {
            Console.WriteLine(info);
        }

        /// <summary>
        /// 根据id获取对应的类名
        /// </summary>
        /// <param name="id">要创建的类对应的id</param>
        /// <returns>类名</returns>
        public static string GetClassNameFromXML(string id)
        {
            string className = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load("BLLComponents.xml");
            XmlNodeList nodeList = doc.SelectSingleNode("components").ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                
                if (node.Attributes[0].Value == id)
                {
                    className = node.Attributes[1].Value;
                }

            }
        
            return className;
        }

        /// <summary>
        /// 截取字节数组的元素;
        /// </summary>
        /// <param name="bytes">源字节数组</param>
        /// <param name="index">起始位置</param>
        /// <param name="count">截取的个数</param>
        /// <returns></returns>
        public static byte[] SubBytesArray(byte[] bytes, int index, int count)
        {
            byte[] b = new byte[count];
            for (int i = index; i < count + index; i++)
            {
                b[i - index] = bytes[i];
            }

            return b;

        }

        /// <summary>
        /// 解析请求报文，根据位置截取报文信息字符串
        /// </summary>
        /// <param name="bytes">请求报文</param>
        /// <param name="index">要截取的起始位置</param>
        /// <param name="count">要截取的长度</param>
        /// <returns></returns>
        public static string GetStringFromRequestMsg(byte[] bytes,int index, int count)
        {
            return Encoding.Default.GetString(SubBytesArray(bytes, index, count)).TrimEnd();
        }

        /// <summary>
        /// 初始化字节数组;
        /// </summary>
        /// <param name="a"></param>
        /// <param name="length"></param>
        public static void InitializeByteArray(byte[] a, int length)
        {
            byte[] t = Encoding.Default.GetBytes(" ");
            for (int i = 0; i < length; i++)
            {
                a[i] = t[0];
            }
        }

        /// <summary>
        /// 使用字符串给字节数组赋值;
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void SetByteArray(byte[] a, string b)
        {
            byte[] t = Encoding.Default.GetBytes(b);
            for (int i = 0; i < t.Length; i++)
            {
                a[i] = t[i];
            }
        }

        /// <summary>
        /// 银行流水号：日期+序号
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GenerateLongBankSerialNum(int i)
        {
            //获取系统当前日期
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month + 1;
            int day = DateTime.Now.Day;

            string strDate = year.ToString() + month.ToString() + day.ToString();
            if (i < 10)
            {
                return strDate + "0" + i.ToString();
            }
            else
            {
                return strDate + i.ToString();
            }
        }

        /// <summary>
        /// 获取方法的执行权限
        /// </summary>
        /// <returns></returns>
        public static bool GetExecutePermission()
        {
            string s = ConfigurationManager.AppSettings["execPermission"];
            return (s == "execute");
        }

        /// <summary>
        /// 获取文件来源或保存路径
        /// </summary>
        /// <param name="whichBank"></param>
        /// <returns></returns>
        public static string GetFilePath(string whichBank)
        {
            string fileFromPath = "";
            switch (whichBank)
            {
                case "01":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_01"];
                    break;
                case "02":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_02"];
                    break;
                case "03":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_03"];
                    break;
                case "04":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_04"];
                    break;
                case "05":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_05"];
                    break;
                case "11":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_11"];
                    break;
                case "19":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_19"];
                    break;
                case "31":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_31"];
                    break;
                case "32":
                    fileFromPath = ConfigurationManager.AppSettings["gjjFilePath_32"];
                    break;
                default:
                    break;
            }

            return fileFromPath;
        }

        /// <summary>
        /// 模拟产生批次号
        /// </summary>
        /// <param name="codeHead"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GenerateBatchCode(string codeHead, int i)
        {
            if (i < 10)
            {
                return codeHead + "0" + i.ToString();
            }
            else
            {
                return codeHead + i.ToString();
            }

        }

        /// <summary>
        /// 模拟产生姓名
        /// </summary>
        /// <param name="nameHead"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GenerateName(string nameHead, int i)
        {
            string[] nameDetail = {"一","二","三","四","五","六","七","八","九","十","十一","十二","十三","十四",
                                      "十五","十六","十七","十八","十九","二十" };
            return nameHead + nameDetail[i];
        }

        /// <summary>
        /// 根据整数返回交易的返回值--4位代码，在线业务类
        /// </summary>
        /// <param name="i"></param>
        /// <returns>交易返回值</returns>
        public static string GetOnlineTranscationReturnValue(int i)
        {
            string rtn = string.Empty;
            switch(i)
            {
                case 1://成功
                    rtn = "0000";
                    break;
                case 2://其他错误
                    rtn = "0001";
                    break;
                case 3://流水号重复
                    rtn = "0004";
                    break;
                case 4: //系统繁忙
                    rtn = "0005";
                    break;
                case 5: //密码错误
                    rtn = "0006";
                    break;
                case 6: //金额不足
                    rtn = "0007";
                    break;
                case 7: //转账金额超限
                    rtn = "0008";
                    break;
                case 8: //查无该卡号
                    rtn = "0009";
                    break;
                case 9: //卡号无效
                    rtn = "0010";
                    break;
                case 10: //需要冲正的流水号不存在
                    rtn = "0011";
                    break;
                case 11: //已签约
                    rtn = "0012";
                    break;
                case 12: //身份证和姓名与中心不符
                    rtn = "0013";
                    break;
                case 13: //批量未完成，请稍候取结果
                    rtn = "0014";
                    break;
                case 14: //身份证号不存在--新增
                    rtn = "0015";
                    break;
                case 15: //身份证号和银行卡号不符（删除）
                    rtn = "0016";
                    break;
                case 16: //账号与姓名不符
                    rtn = "0017";
                    break;
                case 17: //批量代发或代扣文件不存在
                    rtn = "0031";
                    break;
                case 18: //代扣或代发文件汇总与明细不符
                    rtn = "0032";
                    break;
                case 19: //代扣或代发请求已接收，不能重发请求
                    rtn = "0033";
                    break;
                case 20: //银行代发呀代扣工作尚未完成，请稍后取结果
                    rtn = "0034";
                    break;
                case 21: //查不到数据（对账）
                    rtn = "0035";
                    break;
                case 22: //没有代发或代扣请求，不能取结果
                    rtn = "0036";
                    break;
                case 23: //代发或代扣文本格式不正确
                    rtn = "0038";
                    break;
                case 24: //银行系统故障
                    rtn = "0039";
                    break;
                case 25: //原支付交易已冲正
                    rtn = "0042";
                    break;
                case 26: //银行与中心户名不一致
                    rtn = "0043";
                    break;
                case 27: //银行主机交易未成功
                    rtn = "0045";
                    break;
                case 28: //银行返回通信包正确，但内容不正确
                    rtn = "0046";
                    break;
                case 29: //银行当天没有联名卡支付记录
                    rtn = "0097";
                    break;
                case 30: //信息错误或是未签约
                    rtn = "0098";
                    break;
                case 31: //没有明细结果
                    rtn = "0099";
                    break;
                case 32: //通信包无法正确解析
                    rtn = "2002";
                    break;
                case 33: //密码错误
                    rtn = "2005";
                    break;
                case 34: //非本行签约用户
                    rtn = "9999";
                    break;
                case 35: //文件不存在
                    rtn = "1031";
                    break;
                case 36: //文件汇总与明细不符
                    rtn = "1032";
                    break;
                case 37: //文件接收，不能重复请求
                    rtn = "1033";
                    break;
                case 38: //中心系统故障
                    rtn = "1039";
                    break;
                case 39: //身份证和合同号不符
                    rtn = "1001";
                    break;
                case 40: //身份证和姓名不符
                    rtn = "1002";
                    break;
                case 41: //合同号和姓名不符
                    rtn = "1003";
                    break;
                case 42: //银行已接收日切标志
                    rtn = "1004";
                    break;
                default:
                    rtn = "0000";
                    break;
            }
            return rtn;
        }

        /// <summary>
        /// 批量明细文件类交易返回值
        /// </summary>
        /// <param name="i"></param>
        /// <returns>交易返回值</returns>
        public static string GetBacthTranscationReturnValue(int i)
        {
            string rtn = string.Empty;
            switch(i)
            {
                case 1://成功
                    rtn = "00";
                    break;
                case 2://账号不存在
                    rtn = "01";
                    break;
                case 3://账户名不存在
                    rtn = "02";
                    break;
                case 4://账户余额不足支付
                    rtn = "03";
                    break;
                case 5://账户密码错误
                    rtn = "10";
                    break;
                case 6://账户状态错误
                    rtn = "11";
                    break;
                case 7://业务已撤销
                    rtn = "20";
                    break;
                case 8://其他错误
                    rtn = "90";
                    break;
                case 9://回执超时
                    rtn = "0b";
                    break;
                default:
                    rtn = "00";
                    break;
            }
            return rtn;
        }
    }
}
