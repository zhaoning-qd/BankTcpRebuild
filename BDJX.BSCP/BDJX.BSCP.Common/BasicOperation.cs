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
    }
}
