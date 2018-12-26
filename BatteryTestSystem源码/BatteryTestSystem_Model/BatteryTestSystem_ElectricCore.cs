using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    /// <summary>
    /// 电芯表
    /// </summary>
    public class BatteryTestSystem_ElectricCore
    {
        /// <summary>
        /// 二维码或者条码
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        /// 开路电压1
        /// </summary>
        public double vol_open1 { get; set; }
        /// <summary>
        /// 负载电压1
        /// </summary>
        public double vol_load1 { get; set; }
        /// <summary>
        /// 内阻1
        /// </summary>
        public double imp1 { get; set; }
        /// <summary>
        /// K值1
        /// </summary>
        public double Kvalue1 { get; set; }
        /// <summary>
        /// 第一次测试时间
        /// </summary>
        public DateTime Time1 { get; set; }

        /// <summary>
        /// 开路电压2
        /// </summary>
        public double vol_open2 { get; set; }
        /// <summary>
        /// 负载电压2
        /// </summary>
        public double vol_load2 { get; set; }
        /// <summary>
        /// 内阻2
        /// </summary>
        public double imp2 { get; set; }
        /// <summary>
        /// K值2
        /// </summary>
        public double Kvalue2 { get; set; }
        /// <summary>
        /// 第二次测试时间
        /// </summary>
        public DateTime Time2 { get; set; }
        /// <summary>
        /// 第一次测试与第二次测试实际间隔时间
        /// </summary>
        public double timeInterval2 { get; set; }

        /// <summary>
        /// 开路电压3
        /// </summary>
        public double vol_open3 { get; set; }
        /// <summary>
        /// 负载电压3
        /// </summary>
        public double vol_load3 { get; set; }
        /// <summary>
        /// 内阻3
        /// </summary>
        public double imp3 { get; set; }
        /// <summary>
        /// K值3
        /// </summary>
        public double Kvalue3 { get; set; }
        /// <summary>
        /// 第三次测试时间
        /// </summary>
        public DateTime Time3 { get; set; }
        /// <summary>
        /// 第二次测试与第三次测试实际间隔时间
        /// </summary>
        public double timeInterval3 { get; set; }
        /// <summary>
        /// 测试次数
        /// </summary>
        public int TestFrequency { get; set; }
        /// <summary>
        /// 测试电池类型
        /// </summary>
        public string BatteryType { get; set; }
    }
}
