using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    public class TestMessage
    {
        /// <summary>
        /// 计数
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 二维码/条码
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public string Equipment { get; set; }
        /// <summary>
        /// 电池类型
        /// </summary>
        public string BatteryType { get; set; }
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
        /// 测试次数
        /// </summary>
        public string TestFrequency { get; set; }
        /// <summary>
        /// K值
        /// </summary>
        public string Kvlues { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string ERR_FLAG { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 成功与否
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// 实际间距时间小时数
        /// </summary>
        public double TimeIntervalActual1 { get; set; }
    }
}
