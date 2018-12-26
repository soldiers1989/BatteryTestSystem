using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;

namespace BatteryTestSystem_Bll
{
    public class dataManagementBll
    {
        dataManagementDal Dal = new dataManagementDal();

        /// <summary>
        /// 电芯
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<dataManagementModel> selectBatteryTestSystem_ElectricCoreFuzzyQueryBll(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.selectBatteryTestSystem_ElectricCoreFuzzyQueryDal(QRCode, BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 成品
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<dataManagementModel> selectBatteryTestSystem_FinishedProductFuzzyQueryBll(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.selectBatteryTestSystem_FinishedProductFuzzyQueryDal(QRCode, BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 模糊删除电芯表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_ElectricCoreConditionBll(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.deleteBatteryTestSystem_ElectricCoreConditionDal(QRCode, BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 模糊删除成品表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_FinishedProductConditionBll(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.deleteBatteryTestSystem_FinishedProductConditionDal(QRCode, BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 模糊删除异常信息表
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_ErorrMessageConditionBll(string QRCode, string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.deleteBatteryTestSystem_ErorrMessageConditionDal(QRCode, BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 按二维码/条码删除数据
        /// </summary>
        /// <param name="QRCode"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_ElectricCoreQRCodeBll(string QRCode,string SQLCommand)
        {
            try
            {
                return Dal.deleteBatteryTestSystem_ElectricCoreQRCodeDal(QRCode, SQLCommand);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 查询无身份表数据
        /// </summary>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public List<dataManagementModel> selectBatteryTestSystem_UnidentitiesTestFuzzyQueryBll(string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.selectBatteryTestSystem_UnidentitiesTestFuzzyQueryDal(BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除无身份信息表
        /// </summary>
        /// <param name="BatteryType"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public int deleteBatteryTestSystem_UnidentitiesTestBll(string BatteryType, DateTime beginTime, DateTime endTime, string SQLCommand)
        {
            try
            {
                return Dal.deleteBatteryTestSystem_UnidentitiesTestDal(BatteryType, beginTime, endTime, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
    }
}
