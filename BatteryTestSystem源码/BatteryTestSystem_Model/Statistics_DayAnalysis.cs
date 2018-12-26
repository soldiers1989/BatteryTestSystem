using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    /// <summary>
    /// 统计表
    /// </summary>
    public class Statistics_DayAnalysis
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// 最小值电压
        /// </summary>
        public double Minvol_open1 { get; set; }

        /// <summary>
        /// 平均值电压
        /// </summary>
        public double Avgvol_open1 { get; set; }
        /// <summary>
        /// 最大值电压
        /// </summary>
        public double Maxvol_open1 { get; set; }
        /// <summary>
        /// 最小值内阻
        /// </summary>
        public double Minimp1 { get; set; }
        /// <summary>
        /// 平均值电压
        /// </summary>
        public double Avgimp1 { get; set; }
        /// <summary>
        /// 最大值内阻
        /// </summary>
        public double Maximp1 { get; set; }
        /// <summary>
        /// 良率
        /// </summary>
        public double DirectRate { get; set; }
    }
}
