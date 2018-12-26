using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class BatteryTestSystem_BatteryTypeDal
    {
        /// <summary>
        /// 读取类型配置表内容
        /// </summary>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<BatteryTestSystem_BatteryType> selectBatteryTestSystem_BatteryTypeDal(string SQLCommand)
        {
            try
            {
                string sql = SQLhelp.GetSQLCommand(SQLCommand);
                List<BatteryTestSystem_BatteryType> list = new List<BatteryTestSystem_BatteryType>();
                using (SqlDataReader reader = SQLhelp.ExecuteReader(sql, CommandType.Text))
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new BatteryTestSystem_BatteryType()
                            {
                                BatteryType = reader.GetString(0),
                                VoltageDifference = reader.GetDouble(1),
                                TimeInterval = reader.GetDouble(2),
                                ElectricCore = reader.GetBoolean(3),
                                FinishedProduct = reader.GetBoolean(4)
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
        /// <summary>
        /// 增改
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertBatteryTestSystem_BatteryTypeDal(BatteryTestSystem_BatteryType data,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@BatteryType",SqlDbType.VarChar,50){Value=data.BatteryType},
                new SqlParameter("@VoltageDifference",SqlDbType.Float){Value=data.VoltageDifference},
                new SqlParameter("@TimeInterval",SqlDbType.Float){Value=data.TimeInterval},
                new SqlParameter("@ElectricCore",SqlDbType.Bit){Value=data.ElectricCore},
                new SqlParameter("@FinishedProduct",SqlDbType.Bit){Value=data.FinishedProduct}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        public int deleteBatteryTestSystem_BatteryTypeDal(string BatteryType,string SQLCommand)
        {
            string sql = SQLhelp.GetSQLCommand(SQLCommand);
            SqlParameter[] pms = new SqlParameter[]
            {
                new SqlParameter("@BatteryType",SqlDbType.VarChar,50){Value=BatteryType}
            };

            return SQLhelp.ExecuteNonQuery(sql, CommandType.Text, pms);
        }
    }
}
