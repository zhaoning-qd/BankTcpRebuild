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
                LogHelper.WriteLogException("数据库连接失败", ex);
                throw new Exception("数据库连接失败,具体错误请查看日志");
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
        public bool ExecuteDB2Update(string command)
        {
            try
            {
                this.Open();
                db2Cmd = new DB2Command(command, db2Conn);
                db2Cmd.ExecuteNonQuery();
                this.Close();

                return true;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("数据库操作--执行命令失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 查询记录数查询
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int ExecuteCountQuery(string command)
        {
            int count = 0;
            try
            {
                this.Open();
                db2Cmd = new DB2Command(command, db2Conn);
                count = Convert.ToInt32(db2Cmd.ExecuteScalar());
                this.Close();

                return count;

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogException("数据库操作--查询记录数失败", ex);
                return -1;
            }
        }

        
    }
}
