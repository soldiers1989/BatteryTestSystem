using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class dataManagementDal
    {
        /// <summary>
        /// 电芯
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<dataManagementModel> selectBatteryTestSystem_ElectricCoreFuzzyQueryDal(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (QRCode.Length > 0)
            {
                whereList.Add(" QRCode=@QRCode ");
                listsqlpar.Add(new SqlParameter("@QRCode", SqlDbType.VarChar, 50) { Value = QRCode });
            }
            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime>=@UpdateTime1 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime<=@UpdateTime2 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
                sql.Append(" order by UpdateTime ");
            }

            if (listsqlpar.Count > 0)
            {
                SqlParameter[] pms = listsqlpar.ToArray();
                try
                {
                    using(SqlDataReader reader=SQLhelp.ExecuteReader(sql.ToString(),CommandType.Text,pms))
                    {
                        if(reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new dataManagementModel()
                                {
                                    QRCode=reader.GetString(0),
                                    vol_open1=reader.IsDBNull(1)?0:reader.GetDouble(1),
                                    vol_load1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                    imp1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                    Kvalue1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                    Time1 = reader.IsDBNull(5) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(5),
                                    vol_open2 = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                    vol_load2 = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                                    imp2 = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                                    Kvalue2 = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),
                                    Time2 = reader.IsDBNull(10) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(10),
                                    timeInterval2 = reader.IsDBNull(11) ? 0 : reader.GetDouble(11),
                                    vol_open3 = reader.IsDBNull(12) ? 0 : reader.GetDouble(12),
                                    vol_load3 = reader.IsDBNull(13) ? 0 : reader.GetDouble(13),
                                    imp3 = reader.IsDBNull(14) ? 0 : reader.GetDouble(14),
                                    Kvalue3 = reader.IsDBNull(15) ? 0 : reader.GetDouble(15),
                                    Time3 = reader.IsDBNull(16) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(16),
                                    timeInterval3 = reader.IsDBNull(17) ? 0 : reader.GetDouble(17),
                                    TestFrequency=reader.IsDBNull(18)?0:reader.GetInt32(18),
                                    BatteryType=reader.IsDBNull(19)?"":reader.GetString(19),
                                    ElectricCoreOrFinishedProduct="电芯"
                                });
                            }
                        }
                        return list;
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new dataManagementModel()
                                {
                                    QRCode = reader.GetString(0),
                                    vol_open1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                    vol_load1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                    imp1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                    Kvalue1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                    Time1 = reader.IsDBNull(5) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(5),
                                    vol_open2 = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                    vol_load2 = reader.IsDBNull(7) ? 0 : reader.GetDouble(7),
                                    imp2 = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                                    Kvalue2 = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),
                                    Time2 = reader.IsDBNull(10) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(10),
                                    timeInterval2 = reader.IsDBNull(11) ? 0 : reader.GetDouble(11),
                                    vol_open3 = reader.IsDBNull(12) ? 0 : reader.GetDouble(12),
                                    vol_load3 = reader.IsDBNull(13) ? 0 : reader.GetDouble(13),
                                    imp3 = reader.IsDBNull(14) ? 0 : reader.GetDouble(14),
                                    Kvalue3 = reader.IsDBNull(15) ? 0 : reader.GetDouble(15),
                                    Time3 = reader.IsDBNull(16) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(16),
                                    timeInterval3 = reader.IsDBNull(17) ? 0 : reader.GetDouble(17),
                                    TestFrequency = reader.IsDBNull(18) ? 0 : reader.GetInt32(18),
                                    BatteryType = reader.IsDBNull(19) ? "" : reader.GetString(19),
                                    ElectricCoreOrFinishedProduct = "电芯"
                                });
                            }
                        }
                        return list;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public List<dataManagementModel> selectBatteryTestSystem_FinishedProductFuzzyQueryDal(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (QRCode.Length > 0)
            {
                whereList.Add(" QRCode=@QRCode ");
                listsqlpar.Add(new SqlParameter("@QRCode", SqlDbType.VarChar, 50) { Value = QRCode });
            }
            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime>=@UpdateTime1 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime<=@UpdateTime2 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
                sql.Append(" order by UpdateTime ");
            }

            if (listsqlpar.Count > 0)
            {
                SqlParameter[] pms = listsqlpar.ToArray();
                try
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text, pms))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new dataManagementModel()
                                {
                                QRCode = reader.GetString(0),
                                    vol_open1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                    vol_load1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                    imp1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                    ovc1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                    IDimp1 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                    Kvalue1 = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                    Time1 = reader.IsDBNull(7) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(7),
                                    vol_open2 = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                                    vol_load2 = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),
                                    imp2 = reader.IsDBNull(10) ? 0 : reader.GetDouble(10),
                                    ovc2 = reader.IsDBNull(11) ? 0 : reader.GetDouble(11),
                                    IDimp2 = reader.IsDBNull(12) ? 0 : reader.GetDouble(12),
                                    Kvalue2 = reader.IsDBNull(13) ? 0 : reader.GetDouble(13),
                                    Time2 = reader.IsDBNull(14) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(14),
                                    timeInterval2 = reader.IsDBNull(15) ? 0 : reader.GetDouble(15),
                                    vol_open3 = reader.IsDBNull(16) ? 0 : reader.GetDouble(16),
                                    vol_load3 = reader.IsDBNull(17) ? 0 : reader.GetDouble(17),
                                    imp3 = reader.IsDBNull(18) ? 0 : reader.GetDouble(18),
                                    ovc3 = reader.IsDBNull(19) ? 0 : reader.GetDouble(19),
                                    IDimp3 = reader.IsDBNull(20) ? 0 : reader.GetDouble(20),
                                    Kvalue3 = reader.IsDBNull(21) ? 0 : reader.GetDouble(21),
                                    Time3 = reader.IsDBNull(22) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(22),
                                    timeInterval3 = reader.IsDBNull(23) ? 0 : reader.GetDouble(23),
                                    TestFrequency = reader.IsDBNull(24) ? 0 : reader.GetInt32(24),
                                    BatteryType = reader.IsDBNull(25) ? "" : reader.GetString(25),
                                    ElectricCoreOrFinishedProduct = "成品"
                                });
                            }
                        }
                        return list;
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new dataManagementModel()
                                {
                                QRCode = reader.GetString(0),
                                vol_open1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                vol_load1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                imp1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                ovc1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                IDimp1 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                Kvalue1 = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                Time1 = reader.IsDBNull(7) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(7),
                                vol_open2 = reader.IsDBNull(8) ? 0 : reader.GetDouble(8),
                                vol_load2 = reader.IsDBNull(9) ? 0 : reader.GetDouble(9),
                                imp2 = reader.IsDBNull(10) ? 0 : reader.GetDouble(10),
                                ovc2 = reader.IsDBNull(11) ? 0 : reader.GetDouble(11),
                                IDimp2 = reader.IsDBNull(12) ? 0 : reader.GetDouble(12),
                                Kvalue2 = reader.IsDBNull(13) ? 0 : reader.GetDouble(13),
                                Time2 = reader.IsDBNull(14) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(14),
                                timeInterval2 = reader.IsDBNull(15) ? 0 : reader.GetInt32(15),
                                vol_open3 = reader.IsDBNull(16) ? 0 : reader.GetDouble(16),
                                vol_load3 = reader.IsDBNull(17) ? 0 : reader.GetDouble(17),
                                imp3 = reader.IsDBNull(18) ? 0 : reader.GetDouble(18),
                                ovc3 = reader.IsDBNull(19) ? 0 : reader.GetDouble(19),
                                IDimp3 = reader.IsDBNull(20) ? 0 : reader.GetDouble(20),
                                Kvalue3 = reader.IsDBNull(21) ? 0 : reader.GetDouble(21),
                                Time3 = reader.IsDBNull(22) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(22),
                                timeInterval3 = reader.IsDBNull(23) ? 0 : reader.GetInt32(23),
                                TestFrequency = reader.IsDBNull(24) ? 0 : reader.GetInt32(24),
                                BatteryType = reader.IsDBNull(25) ? "" : reader.GetString(25),
                                    ElectricCoreOrFinishedProduct = "成品"
                                });
                            }
                        }
                        return list;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 模糊删除电芯表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_ElectricCoreConditionDal(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (QRCode.Length > 0)
            {
                whereList.Add(" QRCode=@QRCode ");
                listsqlpar.Add(new SqlParameter("@QRCode", SqlDbType.VarChar, 50) { Value = QRCode });
            }
            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime>=@UpdateTime1 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime<=@UpdateTime2 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
            }

            if (listsqlpar.Count > 0)
            {
                try
                {
                    SqlParameter[] pms = listsqlpar.ToArray();

                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text, pms);
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                try
                {
                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 模糊删除成品表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_FinishedProductConditionDal(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (QRCode.Length > 0)
            {
                whereList.Add(" QRCode=@QRCode ");
                listsqlpar.Add(new SqlParameter("@QRCode", SqlDbType.VarChar, 50) { Value = QRCode });
            }
            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime>=@UpdateTime1 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" UpdateTime<=@UpdateTime2 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
            }

            if (listsqlpar.Count > 0)
            {
                try
                {
                    SqlParameter[] pms = listsqlpar.ToArray();

                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text, pms);
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                try
                {
                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text);
                }
                catch
                {
                    throw;
                }
            }
        }

        public int deleteBatteryTestSystem_ErorrMessageConditionDal(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (QRCode.Length > 0)
            {
                whereList.Add(" QRCode=@QRCode ");
                listsqlpar.Add(new SqlParameter("@QRCode", SqlDbType.VarChar, 50) { Value = QRCode });
            }
            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" Time1>=@Time1 ");
                listsqlpar.Add(new SqlParameter("@Time1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" Time1<=@Time2 ");
                listsqlpar.Add(new SqlParameter("@Time2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
            }

            if (listsqlpar.Count > 0)
            {
                try
                {
                    SqlParameter[] pms = listsqlpar.ToArray();

                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text, pms);
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                try
                {
                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text);
                }
                catch
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// 按二维码/条码删除
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_ElectricCoreQRCodeDal(string QRCode,string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=QRCode}
            };
                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
            
        }
        /// <summary>
        /// 查询无身份表数据
        /// </summary>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<dataManagementModel> selectBatteryTestSystem_UnidentitiesTestFuzzyQueryDal(string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" Time1>=@UpdateTime1 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" Time1<=@UpdateTime2 ");
                listsqlpar.Add(new SqlParameter("@UpdateTime2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
                sql.Append(" order by Time1 ");
            }

            if (listsqlpar.Count > 0)
            {
                SqlParameter[] pms = listsqlpar.ToArray();
                try
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text, pms))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new dataManagementModel()
                                {
                                    vol_open1 = reader.IsDBNull(0) ? 0 : reader.GetDouble(0),
                                    vol_load1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                    imp1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                    ovc1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                    IDimp1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                    Kvalue1 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                    Time1 = reader.IsDBNull(6) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(6),
                                    TestFrequency = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                    BatteryType = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    ElectricCoreOrFinishedProduct = reader.GetBoolean(10)?"成品":"电芯",
                                });
                            }
                        }
                        return list;
                    }
                }
                catch
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    using (SqlDataReader reader = SQLhelp.ExecuteReader(sql.ToString(), CommandType.Text))
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                list.Add(new dataManagementModel()
                                {
                                    vol_open1 = reader.IsDBNull(0) ? 0 : reader.GetDouble(0),
                                    vol_load1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                    imp1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                    ovc1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                    IDimp1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                    Kvalue1 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                    Time1 = reader.IsDBNull(6) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(6),
                                    TestFrequency = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                                    BatteryType = reader.IsDBNull(8) ? "" : reader.GetString(8),
                                    ElectricCoreOrFinishedProduct = reader.GetBoolean(10) ? "成品" : "电芯",
                                });
                            }
                        }
                        return list;
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public int deleteBatteryTestSystem_UnidentitiesTestDal(string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            StringBuilder sql = new StringBuilder(SQLhelp.GetSQLCommand(SQLCommand));

            List<dataManagementModel> list = new List<dataManagementModel>();

            List<SqlParameter> listsqlpar = new List<SqlParameter>();
            List<string> whereList = new List<string>();

            if (BatteryType.Length > 0)
            {
                whereList.Add(" BatteryType=@BatteryType ");
                listsqlpar.Add(new SqlParameter("@BatteryType", SqlDbType.VarChar, 50) { Value = BatteryType });
            }
            if (beginTime.Year.ToString() != "1")
            {
                whereList.Add(" Time1>=@Time1 ");
                listsqlpar.Add(new SqlParameter("@Time1", SqlDbType.DateTime) { Value = beginTime });
            }
            if (endTime.Year.ToString() != "1")
            {
                whereList.Add(" Time1<=@Time2 ");
                listsqlpar.Add(new SqlParameter("@Time2", SqlDbType.DateTime) { Value = endTime });
            }

            if (whereList.Count > 0)
            {
                sql.Append(" and ");//只要有查询条件，就拼接一个where
                //然后把后面的查询条件连起来
                sql.Append(string.Join(" and ", whereList));
            }

            if (listsqlpar.Count > 0)
            {
                try
                {
                    SqlParameter[] pms = listsqlpar.ToArray();

                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text, pms);
                }
                catch
                {
                    throw;
                }

            }
            else
            {
                try
                {
                    return SQLhelp.ExecuteNonQuery(sql.ToString(), CommandType.Text);
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
