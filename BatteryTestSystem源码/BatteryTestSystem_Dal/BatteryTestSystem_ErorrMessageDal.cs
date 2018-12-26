using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Dal
{
    public class BatteryTestSystem_ErorrMessageDal
    {
        /// <summary>
        /// 插入异常信息表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int insertBatteryTestSystem_ErorrMessageDal(BatteryTestSystem_ErorrMessage data,string SQLCommand)
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
                new SqlParameter("@ElectricCoreOrFinishedProduct",SqlDbType.Bit){Value=data.ElectricCoreOrFinishedProduct}
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
