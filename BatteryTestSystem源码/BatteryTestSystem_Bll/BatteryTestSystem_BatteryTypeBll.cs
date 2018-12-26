using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_BatteryTypeBll
    {
        BatteryTestSystem_BatteryTypeDal dal = new BatteryTestSystem_BatteryTypeDal();
        /// <summary>
        /// 读取类型配置表内容
        /// </summary>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<BatteryTestSystem_BatteryType> selectBatteryTestSystem_BatteryTypeBll(string SQLCommand)
        {
            return dal.selectBatteryTestSystem_BatteryTypeDal(SQLCommand);
        }
        /// <summary>
        /// 增改
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertBatteryTestSystem_BatteryTypeBll(BatteryTestSystem_BatteryType data,string SQLCommand)
        {
            if (dal.insertBatteryTestSystem_BatteryTypeDal(data, SQLCommand) > 0)
                return true;
            else return false;
        }

        public bool deleteBatteryTestSystem_BatteryTypeBll(string BatteryType, string SQLCommand)
        {
            if (dal.deleteBatteryTestSystem_BatteryTypeDal(BatteryType, SQLCommand) > 0)
                return true;
            else return false;
        }
    }
}
