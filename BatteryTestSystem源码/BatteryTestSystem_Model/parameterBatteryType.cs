using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryTestSystem_Model
{
    /// <summary>
    /// 电池测试记录
    /// </summary>
    public class parameterBatteryType
    {
        /// <summary>
        /// 计数
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 电池类型
        /// </summary>
        public string BatteryType { get; set; }
        /// <summary>
        /// 二维码/条码
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        /// OCV
        /// </summary>
        public string OCV { get; set; }
        /// <summary>
        /// 开路电压
        /// </summary>
        public string vol_open { get; set; }
        /// <summary>
        /// OCR
        /// </summary>
        public string OCR { get; set; }
        /// <summary>
        /// 内阻
        /// </summary>
        public string imp { get; set; }
        /// <summary>
        /// K值
        /// </summary>
        public string Kvlues { get; set; }
        /// <summary>
        ///测试时间
        /// </summary>
        public string Time { get; set; }

    }
}
