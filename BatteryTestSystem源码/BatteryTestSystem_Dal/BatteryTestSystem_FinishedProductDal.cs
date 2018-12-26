using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class BatteryTestSystem_FinishedProductDal
    {
        public List<BatteryTestSystem_FinishedProduct> selectBatteryTestSystem_FinishedProductDal(string QRCode, string SQLCommand)
        {

            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                List<BatteryTestSystem_FinishedProduct> list = new List<BatteryTestSystem_FinishedProduct>();
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
                            list.Add(new BatteryTestSystem_FinishedProduct()
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
                                BatteryType = reader.IsDBNull(25) ? "" : reader.GetString(25)
                            });
                        }
                        return list;
                    }
                    else
                    {
                        list.Add(new BatteryTestSystem_FinishedProduct());
                        return list;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public int insertBatteryTestSystem_FinishedProduct1Dal(BatteryTestSystem_FinishedProduct data,string SQLCommand)
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
            };

                return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
            }
            catch
            {
                throw;
            }
        }

        public int updateBatteryTestSystem_FinishedProduc2tDal(BatteryTestSystem_FinishedProduct data,string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=data.QRCode},
                new SqlParameter("@vol_open2",SqlDbType.Float){Value=data.vol_open2},
                new SqlParameter("@vol_load2",SqlDbType.Float){Value=data.vol_load2},
                new SqlParameter("@ovc2",SqlDbType.Float){Value=data.ovc2},
                new SqlParameter("@imp2",SqlDbType.Float){Value=data.imp2},
                new SqlParameter("@IDimp2",SqlDbType.Float){Value=data.IDimp2},
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

        public int updateBatteryTestSystem_FinishedProduct3Dal(BatteryTestSystem_FinishedProduct data, string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);

                SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@QRCode",SqlDbType.VarChar,50){Value=data.QRCode},
                new SqlParameter("@vol_open3",SqlDbType.Float){Value=data.vol_open3},
                new SqlParameter("@vol_load3",SqlDbType.Float){Value=data.vol_load3},
                new SqlParameter("@ovc3",SqlDbType.Float){Value=data.ovc3},
                new SqlParameter("@imp3",SqlDbType.Float){Value=data.imp3},
                new SqlParameter("@IDimp3",SqlDbType.Float){Value=data.IDimp3},
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
