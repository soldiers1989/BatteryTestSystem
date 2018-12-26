using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BatteryTestSystem_Comm;

namespace BatteryTestSystem_Dal
{
    public static class SQLhelp
    {
        private static readonly string conStr = GetConnectionsSet.StrConnection(System.Windows.Forms.Application.StartupPath + "\\XML\\IPConnect.xml");//ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;

        /// <summary>
        /// 返回影响行数SQL，增删改
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="cmdType">存储过程</param>
        /// <param name="pms">参数数组</param>
        /// <returns>int</returns>
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    using (SqlCommand comd = new SqlCommand(sql, conn))
                    {
                        comd.CommandType = cmdType;
                        if (pms != null)
                        {
                            comd.Parameters.AddRange(pms);
                        }
                        conn.Open();
                        return comd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 返回单个值SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">存储过程</param>
        /// <param name="pms">参数数组</param>
        /// <returns>object</returns>
        public static object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(conStr))
                {
                    using (SqlCommand comd = new SqlCommand(sql, conn))
                    {
                        comd.CommandType = cmdType;
                        if (pms != null)
                        {
                            comd.Parameters.AddRange(pms);
                        }
                        conn.Open();

                        return comd.ExecuteScalar();
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 返回多行多列的SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">存储过程</param>
        /// <param name="pms">参数数组</param>
        /// <returns>SqlDataReader</returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            try
            {
                SqlConnection conn = new SqlConnection(conStr);

                using (SqlCommand comd = new SqlCommand(sql, conn))
                {
                    comd.CommandType = cmdType;
                    if (pms != null)
                    {
                        comd.Parameters.AddRange(pms);
                    }
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }

                        return comd.ExecuteReader(CommandBehavior.CloseConnection);
                    }
                    catch
                    {
                        conn.Close();
                        conn.Dispose();
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 返回DataTble的SQL
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">存储过程</param>
        /// <param name="pms">参数数组</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conStr))
                {
                    adapter.SelectCommand.CommandType = cmdType;
                    if (pms != null)
                    {
                        adapter.SelectCommand.Parameters.AddRange(pms);
                    }
                    adapter.Fill(dt);
                }
                return dt;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获得SQL语句
        /// </summary>
        /// <param name="Command"></param>
        /// <returns>string</returns>
        public static string GetSQLCommand(string Command)
        {
            try
            {
                string sql = "select SQLstring from BatteryTestSystem_Command where Command=@cmd";

                SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@cmd",SqlDbType.VarChar,100){Value=Command}
            };
                return Convert.ToString(ExecuteScalar(sql, CommandType.Text, pms));
            }
            catch { throw; }
            
        }
    }
}
