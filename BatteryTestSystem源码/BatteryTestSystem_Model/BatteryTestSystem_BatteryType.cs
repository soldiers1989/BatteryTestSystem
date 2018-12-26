using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    /// <summary>
    /// 电池类型表
    /// </summary>
    public class BatteryTestSystem_BatteryType
    {
        /// <summary>
        /// 电池类型
        /// </summary>
        public string BatteryType { get; set; }
        /// <summary>
        /// 每小时电压压差
        /// </summary>
        public double VoltageDifference { get; set; }
        /// <summary>
        /// 与上一次最小时间间隔
        /// </summary>
        public double TimeInterval { get; set; }
        /// <summary>
        /// 是电芯
        /// </summary>
        public bool ElectricCore { get; set; }
        /// <summary>
        /// 是成品
        /// </summary>
        public bool FinishedProduct { get; set; }
    }
}
