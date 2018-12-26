using BatteryTestSystem_Dal;
using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Bll
{
    public class BatteryTestSystem_DayAnalysisBll
    {
        BatteryTestSystem_DayAnalysisDal Dal = new BatteryTestSystem_DayAnalysisDal();

        /// <summary>
        /// 插入当天数据表
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool insertBatteryTestSystem_DayAnalysisBll(BatteryTestSystem_DayAnalysis data, string SQLCommand)
        {
            try
            {
                if (Dal.insertBatteryTestSystem_DayAnalysisDal(data, SQLCommand) > 0)
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
        /// 删除
        /// </summary>
        /// <param name="data"></param>
        /// <param name="SQLCommand"></param>
        /// <returns></returns>
        public bool deleteBatteryTestSystem_DayAnalysisBll(DateTime data, string SQLCommand)
        {
            try
            {
                if (Dal.deleteBatteryTestSystem_DayAnalysisDal(data, SQLCommand) > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                throw;
            }
        }

        public Statistics_DayAnalysis selectBatteryTestSystem_DayAnalysisStatisticsBll(DateTime now,string SQLCommand)
        {
            try
            {
                return Dal.selectBatteryTestSystem_DayAnalysisStatisticsDal(now, SQLCommand);
            }
            catch
            {
                throw;
            }
        }

        public List<BatteryTestSystem_DayAnalysis> selectBatteryTestSystem_DayAnalysisFinishedProduct1Bll(DateTime now, string SQLCommand)
        {
            try
            {
                return Dal.selectBatteryTestSystem_DayAnalysisFinishedProduct1Dal(now, SQLCommand);
            }
            catch
            {
                throw;
            }
        }
    }
}
