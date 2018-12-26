using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    public class StaticParameter
    {
        /// <summary>
        /// 电池类型
        /// </summary>
        public int battery_type { get; set; }
        /// <summary>
        /// 电池串联节数
        /// </summary>
        public int battery_serial { get; set; }
        /// <summary>
        /// 电池容量
        /// </summary>
        public int Battery_cap { get; set; }
        /// <summary>
        /// 电压最小值
        /// </summary>
        public double vol_min { get; set; }
        /// <summary>
        /// 电压最大值
        /// </summary>
        public double vol_max { get; set; }
        /// <summary>
        /// 电阻最小值
        /// </summary>
        public double imp_min { get; set; }
        /// <summary>
        /// 电阻最大值
        /// </summary>
        public double imp_max { get; set; }
        /// <summary>
        /// 过电流最小值
        /// </summary>
        public double ovc_min { get; set; }
        /// <summary>
        /// 过电流最大值
        /// </summary>
        public double ovc_max { get; set; }
        /// <summary>
        /// R1最小值
        /// </summary>
        public double r1_min { get; set; }
        /// <summary>
        /// R1最大值
        /// </summary>
        public double r1_max { get; set; }
        /// <summary>
        /// R2最小值
        /// </summary>
        public double r2_min { get; set; }
        /// <summary>
        /// R2最大值
        /// </summary>
        public double r2_max { get; set; }
        /// <summary>
        /// 码片类型1
        /// </summary>
        public int id_type1 { get; set; }
        /// <summary>
        /// 码片类型2
        /// </summary>
        public int id_type2 { get; set; }

        /// <summary>
        /// 短路时间设置
        /// </summary>
        public int short_time { get; set; }
        /// <summary>
        /// 开路时间设置
        /// </summary>
        public int open_time { get; set; }

        /// <summary>
        /// 负载允许最大压降
        /// </summary>
        public double detV_Max { get; set; }
        /// <summary>
        /// 保留
        /// </summary>
        public int reserve { get; set; }

    }
}
