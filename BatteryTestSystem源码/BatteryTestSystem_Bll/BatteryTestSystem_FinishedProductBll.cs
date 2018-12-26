using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_FinishedProductBll
    {
        BatteryTestSystem_FinishedProductDal Dal = new BatteryTestSystem_FinishedProductDal();
        /// <summary>
        /// 按二维码/条码查询成品表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<BatteryTestSystem_FinishedProduct> selectBatteryTestSystem_FinishedProductBll(string QRCode, string SQLCommand)
        {
            try
            {
                return Dal.selectBatteryTestSystem_FinishedProductDal(QRCode, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 第一次插入测试数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertBatteryTestSystem_FinishedProduct1Bll(BatteryTestSystem_FinishedProduct data,string SQLCommand)
        {
            try
            {
                if (Dal.insertBatteryTestSystem_FinishedProduct1Dal(data, SQLCommand) > 0)
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
        public bool updateBatteryTestSystem_FinishedProduc2tBll(BatteryTestSystem_FinishedProduct data,string SQLCommand)
        {
            try
            {
                if (Dal.updateBatteryTestSystem_FinishedProduc2tDal(data, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }

        }
        public bool updateBatteryTestSystem_FinishedProduct3Bll(BatteryTestSystem_FinishedProduct data, string SQLCommand)
        {
            try
            {
                if (Dal.updateBatteryTestSystem_FinishedProduct3Dal(data, SQLCommand) > 0)
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
