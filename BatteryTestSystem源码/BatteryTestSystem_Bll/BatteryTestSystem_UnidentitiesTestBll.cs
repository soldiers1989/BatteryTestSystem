using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_UnidentitiesTestBll
    {

        BatteryTestSystem_UnidentitiesTestDal Dal = new BatteryTestSystem_UnidentitiesTestDal();
        /// <summary>
        /// 插入无身份测试数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertBatteryTestSystem_UnidentitiesTestDal(BatteryTestSystem_UnidentitiesTest data,string SQLCommand)
        {
            if (Dal.insertBatteryTestSystem_UnidentitiesTestDal(data, SQLCommand) > 0)
                return true;
            else
                return false;
        }
    }
}
