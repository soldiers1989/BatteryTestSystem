using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    public class BatteryTestSystem_UnidentitiesTest
    {
        /// <summary>
        /// 测试电池类型
        /// </summary>
        public string BatteryType { get; set; }
        /// <summary>
        /// 开路电压1
        /// </summary>
        public double vol_open1 { get; set; }

        /// <summary>
        /// 第一次测试时间
        /// </summary>
        public DateTime Time1 { get; set; }

        /// <summary>
        /// 内阻1
        /// </summary>
        public double imp1 { get; set; }
        /// <summary>
        /// 过电流保护值1
        /// </summary>
        public double ovc1 { get; set; }
        /// <summary>
        /// ID电阻1
        /// </summary>
        public double IDimp1 { get; set; }
        /// <summary>
        /// 测试次数
        /// </summary>
        public int TestFrequency { get; set; }
        /// <summary>
        /// K值1
        /// </summary>
        public double Kvalue1 { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErorrText { get; set; }

        /// <summary>
        /// 负载电压1
        /// </summary>
        public double vol_load1 { get; set; }
        /// <summary>
        /// 成功与否
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 是否成品
        /// </summary>
        public bool ElectricCoreOrFinishedProduct { get; set; }
    }
}
