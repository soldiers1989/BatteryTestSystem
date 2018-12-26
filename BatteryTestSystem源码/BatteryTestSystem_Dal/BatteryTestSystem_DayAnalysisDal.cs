using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class BatteryTestSystem_DayAnalysisDal
    {
        /// <summary>
        /// 插入当天数据表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertBatteryTestSystem_DayAnalysisDal(BatteryTestSystem_DayAnalysis data,string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=data.QRCode},
                new SqlParameter("@vol_open1",SqlDbType.Float){Value=data.vol_open1},
                new SqlParameter("@vol_load1",SqlDbType.Float){Value=data.vol_load1},
                new SqlParameter("@ovc1",SqlDbType.Float){Value=data.ovc1},
                new SqlParameter("@imp1",SqlDbType.Float){Value=data.imp1},
                new SqlParameter("@IDimp1",SqlDbType.Float){Value=data.IDimp1},
                new SqlParameter("@Kvalue1",SqlDbType.Float){Value=data.Kvalue1},
                new SqlParameter("@TestFrequency",SqlDbType.Int){Value=data.TestFrequency},
                new SqlParameter("@BatteryType",SqlDbType.VarChar,50){Value=data.BatteryType},
                new SqlParameter("@ErorrText",SqlDbType.VarChar,255){Value=data.ErorrText},
                new SqlParameter("@ElectricCoreOrFinishedProduct",SqlDbType.Bit){Value=data.ElectricCoreOrFinishedProduct},
                new SqlParameter("@Result",SqlDbType.Bit){Value=data.Result},
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_DayAnalysisDal(DateTime data, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@Time1",SqlDbType.DateTime){Value=data}
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }

        public Statistics_DayAnalysis selectBatteryTestSystem_DayAnalysisStatisticsDal(DateTime now,string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                Statistics_DayAnalysis data = new Statistics_DayAnalysis();

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@Time1",SqlDbType.DateTime){Value=now}
            };

                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text, pms))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            data.Number = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                            data.Minvol_open1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1);
                            data.Avgvol_open1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2);
                            data.Maxvol_open1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3);
                            data.Minimp1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4);
                            data.Avgimp1 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5);
                            data.Maximp1 = reader.IsDBNull(6) ? 0 : reader.GetDouble(6);
                            data.DirectRate = reader.IsDBNull(7) ? 0 : reader.GetDouble(7);
                        }
                    }

                    return data;
                }
            }
            catch
            {
                throw;
            }
        }

        public List<BatteryTestSystem_DayAnalysis> selectBatteryTestSystem_DayAnalysisFinishedProduct1Dal(DateTime now, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                List<BatteryTestSystem_DayAnalysis> list = new List<BatteryTestSystem_DayAnalysis>();

                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@Time1",SqlDbType.DateTime){Value=now}
                };
                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text, pms))
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new BatteryTestSystem_DayAnalysis()
                            {
                                QRCode=reader.IsDBNull(0)?"":reader.GetString(0),
                                vol_open1 = reader.IsDBNull(1) ? 0 : reader.GetDouble(1),
                                vol_load1 = reader.IsDBNull(2) ? 0 : reader.GetDouble(2),
                                imp1 = reader.IsDBNull(3) ? 0 : reader.GetDouble(3),
                                ovc1 = reader.IsDBNull(4) ? 0 : reader.GetDouble(4),
                                IDimp1 = reader.IsDBNull(5) ? 0 : reader.GetDouble(5),
                                Kvalue1 = reader.IsDBNull(6) ? 0 : reader.GetDouble(6),
                                Time1 = reader.IsDBNull(7) ? Convert.ToDateTime("1900-01-01 00:00") : reader.GetDateTime(7),
                                TestFrequency = reader.IsDBNull(8) ? 0 : reader.GetInt32(8),
                                BatteryType = reader.IsDBNull(9) ? "" : reader.GetString(9),
                                ErorrText=reader.IsDBNull(10)?"":reader.GetString(10),
                                ElectricCoreOrFinishedProduct=reader.GetBoolean(11),
                                Result=reader.GetBoolean(12)
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
}
