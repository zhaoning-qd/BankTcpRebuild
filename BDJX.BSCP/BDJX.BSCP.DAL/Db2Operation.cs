using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using BDJX.BSCP.IDAL;
using IBM.Data.DB2;
using BDJX.BSCP.Entities.BllModels;
using BDJX.BSCP.Common;

namespace BDJX.BSCP.DAL
{
    /// <summary>
    /// DB2数据库操作
    /// </summary>
    public class Db2Operation : IDb2Operation
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private string db2ConnString;

        /// <summary>
        /// 连接对象
        /// </summary>
        private DB2Connection db2Conn;

        /// <summary>
        /// 命令对象
        /// </summary>
        private DB2Command db2Cmd;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Db2Operation()
        {
            db2ConnString = ConfigurationManager.AppSettings["DB2Connection"];
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        /// <returns>是否成功</returns>
        private void Open()
        {
            db2Conn = new DB2Connection(this.db2ConnString);
            try
            {
                db2Conn.Open();
            }
            catch(Exception ex)
            {
                LogHelper.WriteLogException("Database connect error in Db2Operation.Open()", ex);
                LogHelper.WriteLogError("Database connection string", db2ConnString);
                throw;
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        private void Close()
        {
            db2Conn.Close();
        }

        /// <summary>
        /// 执行DB2数据库更新命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public void ExecuteDB2Update(string command)
        {
            this.Open();
            try
            {
                db2Cmd = new DB2Command(command, db2Conn);
                db2Cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("Execute sql command error in Db2Operation.ExecuteDB2Update()", ex);
                LogHelper.WriteLogError("The sql executed is", command);
                throw;
            }
            finally
            {
                this.Close();
            }
        }

        /// <summary>
        /// 查询符合条件的的记录数
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int ExecuteCountQuery(string command)
        {
            int count = 0;
            this.Open();
            try
            {               
                db2Cmd = new DB2Command(command, db2Conn);
                count = Convert.ToInt32(db2Cmd.ExecuteScalar());               
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("Execute sql command error in Db2Operation.ExecuteCountQuery()", ex);
                LogHelper.WriteLogError("The sql executed is", command);
                throw;
            }
            finally
            {
                this.Close();
            }

            return count;
        }

        //*********************************************************************************
        /// <summary>
        /// 根据起始日期和结束日期查询zbmxz结果集
        /// </summary>
        /// <param name="qsrq">起始日期</param>
        /// <param name="zzrq">结束日期</param>
        /// <returns>对象list</returns>
        public List<ZbmxzModel> GetZbmxzByJyrq(string qsrq, string zzrq)
        {
            List<ZbmxzModel> list = new List<ZbmxzModel>();
            ZbmxzModel zbmxz;
            string cmdString = "select * from zbmxz where jyrq between '" + qsrq + "' and '" + zzrq + "'";
            this.Open();
            try
            {               
                db2Cmd = new DB2Command(cmdString, this.db2Conn);
                DB2DataReader dr = db2Cmd.ExecuteReader();
                while (dr.Read())
                {
                    zbmxz = new ZbmxzModel();
                    zbmxz.Bc = dr.GetInt32(1).ToString();
                    zbmxz.Zh = dr.GetString(2);
                    zbmxz.Jyrq = dr.GetDate(3).ToShortDateString();
                    zbmxz.Jysj = dr.GetDateTime(4).ToLongTimeString();
                    zbmxz.Fse = dr.GetDecimal(5).ToString();
                    zbmxz.Ye = dr.GetDecimal(6).ToString();
                    zbmxz.Yhls = dr.GetString(7);
                    zbmxz.Pjhm = dr.GetString(8);
                    zbmxz.Jdbz = dr.GetString(9);
                    zbmxz.Ywlx = dr.GetString(10);
                    zbmxz.Dfzh = dr.GetString(11);
                    zbmxz.Dfhm = dr.GetString(12);
                    zbmxz.Zxjsh = dr.GetString(13);
                    list.Add(zbmxz);
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("Execute sql command error in Db2Operation.GetZbmxzByJyrq()", ex);
                throw;
            }
            finally
            {
                this.Close();
            }

            return list;
        }

        /// <summary>
        /// 根据日切日期查询zbmxz结果集
        /// </summary>
        /// <param name="rqrq">日切日期</param>
        /// <returns></returns>
        public List<ZbmxzModel> GetZbmxzByRqrq(string rqrq)
        {
            List<ZbmxzModel> list = new List<ZbmxzModel>();
            ZbmxzModel zbmxz;
            string cmdString = "select * from zbmxz where djrq >= '" + rqrq + "'";
            this.Open();
            try
            {
                db2Cmd = new DB2Command(cmdString, this.db2Conn);
                DB2DataReader dr = db2Cmd.ExecuteReader();
                while (dr.Read())
                {
                    zbmxz = new ZbmxzModel();
                    zbmxz.Bc = dr.GetInt32(1).ToString();
                    zbmxz.Zh = dr.GetString(2);
                    zbmxz.Jyrq = dr.GetDate(3).ToShortDateString();
                    zbmxz.Jysj = dr.GetDateTime(4).ToLongTimeString();
                    zbmxz.Fse = dr.GetDecimal(5).ToString();
                    zbmxz.Ye = dr.GetDecimal(6).ToString();
                    zbmxz.Yhls = dr.GetString(7);
                    zbmxz.Pjhm = dr.GetString(8);
                    zbmxz.Jdbz = dr.GetString(9);
                    zbmxz.Ywlx = dr.GetString(10);
                    zbmxz.Dfzh = dr.GetString(11);
                    zbmxz.Dfhm = dr.GetString(12);
                    zbmxz.Zxjsh = dr.GetString(13);
                    list.Add(zbmxz);
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("Execute sql command error in Db2Operation.GetZbmxzByRqrq()", ex);
                throw;
            }
            finally
            {
                this.Close();
            }

            return list;
        }

        /// <summary>
        /// 查询zbmxz中某账号的笔数
        /// </summary>
        /// <param name="zbmxz">zbmxz实体</param>
        /// <returns>某账号对应的记录数</returns>
        public int GetCountByZh(ZbmxzModel zbmxz)
        {          
            return this.ExecuteCountQuery(zbmxz.ToCountStringByZh());
        }

        /// <summary>
        /// 查询zbfhz中某账号的笔数
        /// </summary>
        /// <param name="zbfhz">zbfhz实体</param>
        /// <returns>某账号对应的记录数</returns>
        public int GetCountByZh(ZbfhzModel zbfhz)
        {        
            return this.ExecuteCountQuery(zbfhz.ToCountStringByZh());
        }

        /// <summary>
        ///更新zbfhz和zbmxz
        /// </summary>
        /// <param name="lineArray"></param>
        /// <returns></returns>
        public int UpateZbfhzAndZbmxz(ZbmxzModel zbmxz, ZbfhzModel zbfhz)
        {
            //账表明细账
            int iBs = GetCountByZh(zbmxz);
            if (iBs == -1)
            {
                return -1;
            }
            else
            {
                string cmd = zbmxz.ToInsertString();
                ExecuteDB2Update(cmd);
            }

            //账表分户账
            int num = GetCountByZh(zbfhz);

            if (num == -1)
            {
                return -1;
            }
            if (num == 0)
            {
                ExecuteDB2Update(zbfhz.ToInsertString());
            }
            else
            {
                ExecuteDB2Update(zbfhz.ToUpdateString());
            }

            return 0;
        }

        /// <summary>
        /// 更新登记日切日期
        /// </summary>
        public void UpdateDjrq()
        {
            string currDate = DateTime.Now.ToShortDateString();
            string cmdString = "update djrqb set djrqrq = '" + currDate + "'";
            this.ExecuteDB2Update(cmdString);
        }

        /// <summary>
        /// 获取登记日切日期
        /// </summary>
        /// <returns></returns>
        public string GetDjrqrq()
        {
            string djrq = string.Empty;
            string cmdString = "select * from djrqb";
            this.Open();
            try
            {
                db2Cmd = new DB2Command(cmdString, this.db2Conn);
                DB2DataReader dr = db2Cmd.ExecuteReader();
                while (dr.Read())
                {
                    djrq = dr.GetString(7);
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("Execute sql command error in Db2Operation.GetDjrqrq()", ex);
                throw;
            }
            finally
            {
                this.Close();
            }

            return djrq;
        }

        
    }
}
