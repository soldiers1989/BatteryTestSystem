using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_ElectricCoreBll
    {
        BatteryTestSystem_ElectricCoreDal Dal = new BatteryTestSystem_ElectricCoreDal();
        /// <summary>
        /// 按二维码/条码查询电芯表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<BatteryTestSystem_ElectricCore> selectBatteryTestSystem_ElectricCoreBll(string QRCode, string SQLCommand)
        {
            return Dal.selectBatteryTestSystem_ElectricCoreDal(QRCode, SQLCommand);
        }
        /// <summary>
        /// 第一次插入电芯表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool intsertBatteryTestSystem_ElectricCore1Bll(BatteryTestSystem_ElectricCore data, string SQLCommand)
        {
            try
            {
                if (Dal.intsertBatteryTestSystem_ElectricCore1Dal(data, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新第二次测试
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool updateBatteryTestSystem_ElectricCore2Bll(BatteryTestSystem_ElectricCore data, string SQLCommand)
        {
            try
            {
                if (Dal.updateBatteryTestSystem_ElectricCore2Dal(data, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }

        }

        public bool updateBatteryTestSystem_ElectricCore3Bll(BatteryTestSystem_ElectricCore data, string SQLCommand)
        {
            try
            {
                if (Dal.updateBatteryTestSystem_ElectricCore3Dal(data, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }
    }
}
