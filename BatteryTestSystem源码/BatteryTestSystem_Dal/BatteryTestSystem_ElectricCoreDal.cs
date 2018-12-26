using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class BatteryTestSystem_ElectricCoreDal
    {
        /// <summary>
        /// 按二维码/条码查询电芯表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<BatteryTestSystem_ElectricCore> selectBatteryTestSystem_ElectricCoreDal(string QRCode,string SQLCommand)
        {

            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                List<BatteryTestSystem_ElectricCore> list = new List<BatteryTestSystem_ElectricCore>();

                SqlParameter[] pms = new SqlParameter[]
                {
                    new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=QRCode}
                };
                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text, pms))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new BatteryTestSystem_ElectricCore()
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
                                BatteryType=reader.IsDBNull(19)?"":reader.GetString(19)
                            });
                        }
                        return list;
                    }
                    else
                    {
                        list.Add(new BatteryTestSystem_ElectricCore());
                        return list;
                    }

                }
            }
            catch
            {
                throw;
            }
        }

        public int intsertBatteryTestSystem_ElectricCore1Dal(BatteryTestSystem_ElectricCore data, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=data.QRCode},
                new SqlParameter("@vol_open1",SqlDbType.Float){Value=data.vol_open1},
                new SqlParameter("@vol_load1",SqlDbType.Float){Value=data.vol_load1},
                new SqlParameter("@imp1",SqlDbType.Float){Value=data.imp1},
                new SqlParameter("@Kvalue1",SqlDbType.Float){Value=data.Kvalue1},
                new SqlParameter("@TestFrequency",SqlDbType.Int){Value=data.TestFrequency},
                new SqlParameter("@BatteryType",SqlDbType.VarChar,50){Value=data.BatteryType},
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }

        public int updateBatteryTestSystem_ElectricCore2Dal(BatteryTestSystem_ElectricCore data, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=data.QRCode},
                new SqlParameter("@vol_open2",SqlDbType.Float){Value=data.vol_open2},
                new SqlParameter("@vol_load2",SqlDbType.Float){Value=data.vol_load2},
                new SqlParameter("@imp2",SqlDbType.Float){Value=data.imp2},
                new SqlParameter("@Kvalue2",SqlDbType.Float){Value=data.Kvalue2},
                new SqlParameter("@TestFrequency",SqlDbType.Int){Value=data.TestFrequency},
                new SqlParameter("@BatteryType",SqlDbType.VarChar,50){Value=data.BatteryType},
                new SqlParameter("@timeInterval2",SqlDbType.Float){Value=data.timeInterval2},
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }

        public int updateBatteryTestSystem_ElectricCore3Dal(BatteryTestSystem_ElectricCore data, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=data.QRCode},
                new SqlParameter("@vol_open3",SqlDbType.Float){Value=data.vol_open3},
                new SqlParameter("@vol_load3",SqlDbType.Float){Value=data.vol_load3},
                new SqlParameter("@imp3",SqlDbType.Float){Value=data.imp3},
                new SqlParameter("@Kvalue3",SqlDbType.Float){Value=data.Kvalue3},
                new SqlParameter("@TestFrequency",SqlDbType.Int){Value=data.TestFrequency},
                new SqlParameter("@BatteryType",SqlDbType.VarChar,50){Value=data.BatteryType},
                new SqlParameter("@timeInterval3",SqlDbType.Float){Value=data.timeInterval3},
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }
    }
}
