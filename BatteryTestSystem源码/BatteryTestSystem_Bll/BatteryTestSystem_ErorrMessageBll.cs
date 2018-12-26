using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_ErorrMessageBll
    {
        BatteryTestSystem_ErorrMessageDal Dal = new BatteryTestSystem_ErorrMessageDal();
        /// <summary>
        /// 插入异常信息表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertBatteryTestSystem_ErorrMessageBll(BatteryTestSystem_ErorrMessage data,string SQLCommand)
        {
            if (Dal.insertBatteryTestSystem_ErorrMessageDal(data, SQLCommand) > 0)
                return true;
            else
                return false;
        }


    }
}
