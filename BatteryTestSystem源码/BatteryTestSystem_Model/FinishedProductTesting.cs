using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    public class FinishedProductTesting
    {
        /// <summary>
        /// 开路电压
        /// </summary>
        public string vol_open { get; set; }
        /// <summary>
        /// 负载电压
        /// </summary>
        public string vol_load { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        public string imp { get; set; }
        /// <summary>
        /// 过电流保护值
        /// </summary>
        public string ovc { get; set; }
        /// <summary>
        /// 电阻1阻值
        /// </summary>
        public string r1 { get; set; }
        /// <summary>
        /// 电阻2阻值
        /// </summary>
        public string r2 { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string ERR_FLAG { get; set; }
    }
}
