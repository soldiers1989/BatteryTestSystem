using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryTestSystem_Bll
{
    public class TestSystemBll
    {
        /// <summary>
        /// 电池类型表
        /// </summary>
        BatteryTestSystem_BatteryTypeBll BatteryBll = new BatteryTestSystem_BatteryTypeBll();
        /// <summary>
        /// 成品表
        /// </summary>
        BatteryTestSystem_FinishedProductBll FBatteryBll = new BatteryTestSystem_FinishedProductBll();
        /// <summary>
        /// 电芯表
        /// </summary>
        BatteryTestSystem_ElectricCoreBll EBatteryBll = new BatteryTestSystem_ElectricCoreBll();

        BatteryTestSystem_ErorrMessageBll ErorrBll = new BatteryTestSystem_ErorrMessageBll();
        /// <summary>
        /// 当天数据报表
        /// </summary>
        BatteryTestSystem_DayAnalysisBll DayAnalysisBll = new BatteryTestSystem_DayAnalysisBll();
        /// <summary>
        /// 覆盖
        /// </summary>
        public bool Cover { get; set; }

        public TestSystemBll()
        {
            this.Cover = false;
        }
        /// <summary>
        /// 获得电池类型表数据
        /// </summary>
        /// <returns></returns>
        public List<BatteryTestSystem_BatteryType> GetBatteryType()
        {
            return BatteryBll.selectBatteryTestSystem_BatteryTypeBll("selectBatteryTestSystem_BatteryType");
        }
        /// <summary>
        /// 获得电池类型名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetBatteryTypeStr(List<BatteryTestSystem_BatteryType> data)
        {
            List<string> list = new List<string>();

            foreach (BatteryTestSystem_BatteryType item in data)
            {
                list.Add(item.BatteryType);
            }

            return list;
        }
        /// <summary>
        /// 查找电池类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="BatteryType"></param>
        /// <returns></returns>
        public BatteryTestSystem_BatteryType SelectBatteryType(List<BatteryTestSystem_BatteryType> data,string BatteryType)
        {
            foreach (BatteryTestSystem_BatteryType item in data)
            {
                if (item.BatteryType == BatteryType)
                    return item;
            }

            return new BatteryTestSystem_BatteryType();
        }

        public TestMessage MainTestMessage(TestMessage data, string txt_QRCode, BatteryTestSystem_BatteryType BatteryType, BatteryTestSystem_ElectricCore electricData)
        {
            TestMessage Rts = new TestMessage();
            try
            {
                switch (electricData.TestFrequency)
                {
                    case 0:
                        if (GetERR_FLAG(data.ERR_FLAG))//判断设备测试结果是否通过
                        {
                            Rts = data;
                            Rts.Kvlues = "0";
                            Rts.TestFrequency = "1";
                            Rts.Result = true;
                            Rts.TimeIntervalActual1 = 0;
                            if (EBatteryBll.intsertBatteryTestSystem_ElectricCore1Bll(GetBatteryTestSystem_ElectricCore(data, 1), "intsertBatteryTestSystem_ElectricCore1"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                {
                                    Rts.ERR_FLAG = "上传数据失败!";
                                    Rts.Result = false;
                                }
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "1";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = 0;
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, false), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += ",上传数据失败!";
                            }
                        }
                        break;
                    case 1:
                        if (GetERR_FLAG(data.ERR_FLAG))//判断设备测试结果是否通过
                        {
                            double Kvauls = 0;

                            switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, electricData.vol_open1, electricData.Time1, BatteryType, ref Kvauls,true))
                            {
                                case 1:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "2";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                                case 2:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "2";
                                    Rts.Result = false;
                                    Rts.ERR_FLAG = "K值异常!";
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                    {
                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, false), "insertBatteryTestSystem_ErorrMessage"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                        }
                                    }
                                    break;
                                case 3:
                                    if (this.Cover)//updateBatteryTestSystem_FinishedProduct1
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "2";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                        if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = "0";
                                            Rts.TestFrequency = "1";
                                            Rts.Result = true;
                                            Rts.ERR_FLAG = "数据已放弃!";
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = true;
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                            if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                {
                                                    Rts.ERR_FLAG = "上传数据失败!";
                                                    Rts.Result = false;

                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    if (this.Cover)
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "2";
                                        Rts.Result = false;
                                        Rts.ERR_FLAG = "K值异常!";
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                        if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                        {

                                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!数据已放弃!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                            if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                            {

                                                if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    Rts.ERR_FLAG += "上传数据失败!";
                                                }
                                            }
                                        }
                                    }

                                    break;
                                case 5:
                                    Rts = data;
                                    Rts.Kvlues = "0";
                                    Rts.TestFrequency = "1";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = 0;
                                    if (EBatteryBll.intsertBatteryTestSystem_ElectricCore1Bll(GetBatteryTestSystem_ElectricCore(data, 1), "updateBatteryTestSystem_ElectricCore1"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "2";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += ",上传数据失败!";
                            }
                        }
                        break;
                    case 2:
                        if (GetERR_FLAG(data.ERR_FLAG))//判断设备测试结果是否通过
                        {
                            double Kvauls = 0;

                            switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, electricData.vol_open2, electricData.Time2, BatteryType, ref Kvauls,true))
                            {
                                case 1:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                                case 2:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = false;
                                    Rts.ERR_FLAG = "K值异常!";
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {

                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            Rts.ERR_FLAG += "上传数据失败!";
                                        }
                                    }
                                    break;
                                case 3:
                                    if (this.Cover)//updateBatteryTestSystem_FinishedProduct1
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                        if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = electricData.Kvalue2.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = true;
                                            Rts.ERR_FLAG = "数据已放弃!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = true;
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                            if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                {
                                                    Rts.ERR_FLAG = "上传数据失败!";
                                                    Rts.Result = false;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    if (this.Cover)
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = false;
                                        Rts.ERR_FLAG = "K值异常!";
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                        if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                        {

                                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!数据已放弃!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                            if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                            {
                                                if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    Rts.ERR_FLAG += "上传数据失败!";
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 5:
                                    switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, electricData.vol_open1, electricData.Time1, BatteryType, ref Kvauls,false))
                                    {
                                        case 1:
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = true;
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                            if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                {
                                                    Rts.ERR_FLAG = "上传数据失败!";
                                                    Rts.Result = false;
                                                }
                                            }
                                            break;
                                        case 2:
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time1);
                                            if (EBatteryBll.updateBatteryTestSystem_ElectricCore2Bll(GetBatteryTestSystem_ElectricCore(data, 2), "updateBatteryTestSystem_ElectricCore2"))
                                            {
                                                if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    Rts.ERR_FLAG += "上传数据失败!";
                                                }
                                            }
                                            break;
                                        case 3:
                                            if (this.Cover)//updateBatteryTestSystem_FinishedProduct1
                                            {
                                                Rts = data;
                                                Rts.Kvlues = Kvauls.ToString();
                                                Rts.TestFrequency = "3";
                                                Rts.Result = true;
                                                Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                                if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    {
                                                        Rts.ERR_FLAG = "上传数据失败!";
                                                        Rts.Result = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = electricData.Kvalue2.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = true;
                                                    Rts.ERR_FLAG = "数据已放弃!";
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                                }
                                                else
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = Kvauls.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = true;
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                                    {
                                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                        {
                                                            Rts.ERR_FLAG = "上传数据失败!";
                                                            Rts.Result = false;
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 4:
                                            if (this.Cover)
                                            {
                                                Rts = data;
                                                Rts.Kvlues = Kvauls.ToString();
                                                Rts.TestFrequency = "3";
                                                Rts.Result = false;
                                                Rts.ERR_FLAG = "K值异常!";
                                                Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                                if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                                {

                                                    if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                    {
                                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                            Rts.ERR_FLAG += "上传数据失败!";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = Kvauls.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = false;
                                                    Rts.ERR_FLAG = "K值异常!数据已放弃!";
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                                }
                                                else
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = Kvauls.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = false;
                                                    Rts.ERR_FLAG = "K值异常!";
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                                    {
                                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                        {
                                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                                Rts.ERR_FLAG += "上传数据失败!";
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        //default:
                                        //    Rts = data;
                                        //    Rts.Kvlues = electricData.Kvalue2.ToString();
                                        //    Rts.TestFrequency = "3";
                                        //    Rts.Result = true;
                                        //    Rts.ERR_FLAG = "数据已放弃!";
                                        //    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                        //    break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "3";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += "上传数据失败!";
                            }
                        }
                        break;
                    case 3:
                        if (GetERR_FLAG(data.ERR_FLAG))
                        {
                            double Kvauls = 0;

                            switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, electricData.vol_open2, electricData.Time2, BatteryType, ref Kvauls,true))
                            {
                                case 1:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                                case 2:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = false;
                                    Rts.ERR_FLAG = "K值异常!";
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {
                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            Rts.ERR_FLAG += "上传数据失败!";
                                        }
                                    }
                                    break;
                                case 3:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                                case 4:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = false;
                                    Rts.ERR_FLAG = "K值异常!";
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {
                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            Rts.ERR_FLAG += "上传数据失败!";
                                        }
                                    }
                                    break;
                                case 5:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                                    if (EBatteryBll.updateBatteryTestSystem_ElectricCore3Bll(GetBatteryTestSystem_ElectricCore(data, 3), "updateBatteryTestSystem_ElectricCore3"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "3";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, electricData.Time2);
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, false, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += "上传数据失败!";
                            }
                        }
                        break;
                }


                return Rts;
            }
            catch
            {
                throw;
            }
        }

        public TestMessage MainTestMessage(TestMessage data, string txt_QRCode, BatteryTestSystem_BatteryType BatteryType, BatteryTestSystem_FinishedProduct FinishedData)
        {
            TestMessage Rts = new TestMessage();
            try
            {
                switch (FinishedData.TestFrequency)
                {
                    case 0:
                        if (GetERR_FLAG(data.ERR_FLAG))
                        {
                            Rts = data;
                            Rts.Kvlues = "0";
                            Rts.TestFrequency = "1";
                            Rts.Result = true;
                            Rts.TimeIntervalActual1 = 0;
                            if (FBatteryBll.insertBatteryTestSystem_FinishedProduct1Bll(GetBatteryTestSystem_FinishedProduct(data, 1), "insertBatteryTestSystem_FinishedProduct1"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                {
                                    Rts.ERR_FLAG = "上传数据失败!";
                                    Rts.Result = false;
                                }
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "1";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = 0;
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += ",上传数据失败!";
                            }
                        }
                        break;
                    case 1:
                        if (GetERR_FLAG(data.ERR_FLAG))
                        {
                            double Kvauls = 0;

                            switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, FinishedData.vol_open1, FinishedData.Time1, BatteryType, ref Kvauls,true))
                            {
                                case 1:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "2";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                                case 2:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "2";
                                    Rts.Result = false;
                                    Rts.ERR_FLAG = "K值异常!";
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                    {
                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            Rts.ERR_FLAG += "上传数据失败!";
                                        }
                                    }
                                    break;
                                case 3:
                                    if (this.Cover)//updateBatteryTestSystem_FinishedProduct1
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "2";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = "0";
                                            Rts.TestFrequency = "1";
                                            Rts.Result = true;
                                            Rts.ERR_FLAG = "数据已放弃!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = true;
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                            if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                {
                                                    Rts.ERR_FLAG = "上传数据失败!";
                                                    Rts.Result = false;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    if (this.Cover)
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "2";
                                        Rts.Result = false;
                                        Rts.ERR_FLAG = "K值异常!";
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                        {

                                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!数据已放弃!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                            if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                            {
                                                if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    Rts.ERR_FLAG += "上传数据失败!";
                                                }
                                            }
                                        }
                                    }

                                    break;
                                case 5:
                                        Rts = data;
                                        Rts.Kvlues = "0";
                                        Rts.TestFrequency = "1";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = 0;
                                        if (FBatteryBll.insertBatteryTestSystem_FinishedProduct1Bll(GetBatteryTestSystem_FinishedProduct(data, 1), "updateBatteryTestSystem_FinishedProduct1"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    break;
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "2";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += ",上传数据失败!";
                            }
                        }
                        break;
                    case 2:
                        if (GetERR_FLAG(data.ERR_FLAG))
                        {
                            double Kvauls = 0;

                            switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, FinishedData.vol_open2, FinishedData.Time2, BatteryType, ref Kvauls,true))
                            {
                                case 1:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                                case 2:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = false;
                                    Rts.ERR_FLAG = "K值异常!";
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                    {
                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            Rts.ERR_FLAG += "上传数据失败!";
                                        }
                                    }
                                    break;
                                case 3:
                                    if (this.Cover)//updateBatteryTestSystem_FinishedProduct1
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                        {
                                            if (DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = FinishedData.Kvalue2.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = true;
                                            Rts.ERR_FLAG = "数据已放弃!";
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = true;
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                            if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                {
                                                    Rts.ERR_FLAG = "上传数据失败!";
                                                    Rts.Result = false;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 4:
                                    if (this.Cover)
                                    {
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = false;
                                        Rts.ERR_FLAG = "K值异常!";
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                        {
                                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!数据已放弃!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        }
                                        else
                                        {
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "3";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                            if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                            {
                                                if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    Rts.ERR_FLAG += "上传数据失败!";
                                                }
                                            }
                                        }
                                    }
                                    break;
                                case 5:
                                    switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, FinishedData.vol_open1, FinishedData.Time1, BatteryType, ref Kvauls,false))
                                    {
                                        case 1:
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = true;
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                            if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                {
                                                    Rts.ERR_FLAG = "上传数据失败!";
                                                    Rts.Result = false;
                                                }
                                            }
                                            break;
                                        case 2:
                                            Rts = data;
                                            Rts.Kvlues = Kvauls.ToString();
                                            Rts.TestFrequency = "2";
                                            Rts.Result = false;
                                            Rts.ERR_FLAG = "K值异常!";
                                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                            if (FBatteryBll.updateBatteryTestSystem_FinishedProduc2tBll(GetBatteryTestSystem_FinishedProduct(data, 2), "updateBatteryTestSystem_FinishedProduc2"))
                                            {
                                                if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                {
                                                    if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    Rts.ERR_FLAG += "上传数据失败!";
                                                }
                                            }
                                            break;
                                        case 3:
                                            if (this.Cover)//updateBatteryTestSystem_FinishedProduct1
                                            {
                                                Rts = data;
                                                Rts.Kvlues = Kvauls.ToString();
                                                Rts.TestFrequency = "3";
                                                Rts.Result = true;
                                                Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                                if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                                {
                                                    if (DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                    {
                                                        Rts.ERR_FLAG = "上传数据失败!";
                                                        Rts.Result = false;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = FinishedData.Kvalue2.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = true;
                                                    Rts.ERR_FLAG = "数据已放弃!";
                                                }
                                                else
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = Kvauls.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = true;
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                                    {
                                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                        {
                                                            Rts.ERR_FLAG = "上传数据失败!";
                                                            Rts.Result = false;
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        case 4:
                                            if (this.Cover)
                                            {
                                                Rts = data;
                                                Rts.Kvlues = Kvauls.ToString();
                                                Rts.TestFrequency = "3";
                                                Rts.Result = false;
                                                Rts.ERR_FLAG = "K值异常!";
                                                Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                                if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                                {
                                                    if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                    {
                                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                            Rts.ERR_FLAG += "上传数据失败!";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (MessageBox.Show("与上一次测试间距时间超过一个小时，却未等于或超过最小间,是否放弃此次测试数据?", "系统提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = Kvauls.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = false;
                                                    Rts.ERR_FLAG = "K值异常!数据已放弃!";
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                                }
                                                else
                                                {
                                                    Rts = data;
                                                    Rts.Kvlues = Kvauls.ToString();
                                                    Rts.TestFrequency = "3";
                                                    Rts.Result = false;
                                                    Rts.ERR_FLAG = "K值异常!";
                                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                                    {
                                                        if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                                        {
                                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                                Rts.ERR_FLAG += "上传数据失败!";
                                                        }
                                                    }
                                                }
                                            }
                                            break;
                                        //default:
                                        //    Rts = data;
                                        //    Rts.Kvlues = FinishedData.Kvalue2.ToString();
                                        //    Rts.TestFrequency = "3";
                                        //    Rts.Result = true;
                                        //    Rts.ERR_FLAG = "数据已放弃!";
                                        //    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        //    break;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "3";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time1);
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += "上传数据失败!";
                            }
                        }
                        break;
                    case 3:
                        if (GetERR_FLAG(data.ERR_FLAG))
                        {
                            double Kvauls = 0;

                            switch (KValue(Convert.ToDouble(data.vol_open), data.UpdateTime, FinishedData.vol_open2, FinishedData.Time2, BatteryType, ref Kvauls,true))
                            {
                                case 1:
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    break;
                                case 2:
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = false;
                                        Rts.ERR_FLAG = "K值异常!";
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                        {
                                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                            }
                                        }
                                    break;
                                case 3:
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = true;
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                        {
                                            if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                            {
                                                Rts.ERR_FLAG = "上传数据失败!";
                                                Rts.Result = false;
                                            }
                                        }
                                    break;
                                case 4:
                                        Rts = data;
                                        Rts.Kvlues = Kvauls.ToString();
                                        Rts.TestFrequency = "3";
                                        Rts.Result = false;
                                        Rts.ERR_FLAG = "K值异常!";
                                        Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                        if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                        {
                                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                                            {
                                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                                Rts.ERR_FLAG += "上传数据失败!";
                                            }
                                        }
                                    break;
                                case 5:
                                    Rts = data;
                                    Rts.Kvlues = Kvauls.ToString();
                                    Rts.TestFrequency = "3";
                                    Rts.Result = true;
                                    Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                                    if (FBatteryBll.updateBatteryTestSystem_FinishedProduct3Bll(GetBatteryTestSystem_FinishedProduct(data, 3), "updateBatteryTestSystem_FinishedProduct3"))
                                    {
                                        if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                        {
                                            Rts.ERR_FLAG = "上传数据失败!";
                                            Rts.Result = false;
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Rts = data;
                            Rts.TestFrequency = "3";
                            Rts.Result = false;
                            Rts.TimeIntervalActual1 = GetTimeHous(data.UpdateTime, FinishedData.Time2);
                            if (ErorrBll.insertBatteryTestSystem_ErorrMessageBll(GetBatteryTestSystem_ErorrMessage(data, true), "insertBatteryTestSystem_ErorrMessage"))
                            {
                                if (!DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(GetBatteryTestSystem_DayAnalysis(data, true, Rts.Result), "insertBatteryTestSystem_DayAnalysis"))
                                Rts.ERR_FLAG += "上传数据失败!";
                            }
                        }
                        break;
                }


                return Rts;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 设备测试结果
        /// </summary>
        /// <param name="ERR_FLAG"></param>
        /// <returns></returns>
        public bool GetERR_FLAG(string ERR_FLAG)
        {
            if(ERR_FLAG=="测试通过")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        double GetTimeHous( DateTime nowTime,DateTime OldTime)
        {

            TimeSpan TimeValu = nowTime - OldTime;//获得跟上一次测试的时间差

            return Convert.ToDouble((TimeValu.TotalHours).ToString("0.0000"));//时间差小时数
        }

        int KValue(double vol_open, DateTime nowTime, double Oldvol_open, DateTime OldTime, BatteryTestSystem_BatteryType BatteryType, ref double Kvauls,bool bl)
        {
            try
            {

                TimeSpan TimeValu = nowTime - OldTime;//获得跟上一次测试的时间差

                double HoursValus = TimeValu.TotalHours;//时间差小时数

                double valuVol = Convert.ToDouble((HoursValus * BatteryType.VoltageDifference).ToString("0.0000"));//标准K值

                if (HoursValus >= BatteryType.TimeInterval)
                {
                    Kvauls = System.Math.Abs(Convert.ToDouble((vol_open - Oldvol_open).ToString("0.0000")));//实际K值

                    if (Kvauls <= valuVol)
                    {
                        return 1;//K值正常，测试时间与最小时间差正常
                    }
                    else return 2;//K值异常,测试时间与最小时间差正常
                }
                else if (HoursValus < 1 && bl)
                {
                    return 5;
                }
                else
                {
                    Kvauls = System.Math.Abs(Convert.ToDouble((vol_open - Oldvol_open).ToString("0.0000")));//实际K值//Convert.ToDouble((HoursValus * BatteryType.VoltageDifference).ToString("0.0000"));//获得此次K值

                    if (Kvauls <= valuVol)
                    {
                        return 3;//K值正常，测试时间与最小时间差异常
                    }
                    else return 4;//K值异常，测试时间与最小时间差异常
                }
            }
            catch
            {
                throw;
            }
        }

        BatteryTestSystem_FinishedProduct GetBatteryTestSystem_FinishedProduct(TestMessage data,int i)
        {
            BatteryTestSystem_FinishedProduct Rdata = new BatteryTestSystem_FinishedProduct();
            switch (i)
            {
                case 1:
                    Rdata = new BatteryTestSystem_FinishedProduct()
                    {
                        BatteryType=data.BatteryType,
                        IDimp1=Convert.ToDouble(data.r1==""?data.r2:data.r1),
                        imp1 = Convert.ToDouble(data.imp),
                        Kvalue1 = Convert.ToDouble(data.Kvlues),
                        vol_open1 = Convert.ToDouble(data.vol_open),
                        vol_load1 = Convert.ToDouble(data.vol_load),
                        ovc1 = Convert.ToDouble(data.ovc),
                        QRCode=data.QRCode,
                        TestFrequency = Convert.ToInt32(data.TestFrequency)
                    };       
                    break;
                case 2:
                    Rdata = new BatteryTestSystem_FinishedProduct()
                    {
                        BatteryType = data.BatteryType,
                        IDimp2 = Convert.ToDouble(data.r1 == "" ? data.r2 : data.r1),
                        imp2 = Convert.ToDouble(data.imp),
                        Kvalue2 = Convert.ToDouble(data.Kvlues),
                        vol_open2 = Convert.ToDouble(data.vol_open),
                        vol_load2 = Convert.ToDouble(data.vol_load),
                        ovc2 = Convert.ToDouble(data.ovc),
                        QRCode = data.QRCode,
                        TestFrequency = Convert.ToInt32(data.TestFrequency),
                        timeInterval2 = Convert.ToDouble(data.TimeIntervalActual1)
                    }; 
                    break;
                case 3:
                    Rdata = new BatteryTestSystem_FinishedProduct()
                    {
                        BatteryType = data.BatteryType,
                        IDimp3 = Convert.ToDouble(data.r1 == "" ? data.r2 : data.r1),
                        imp3 = Convert.ToDouble(data.imp),
                        Kvalue3 = Convert.ToDouble(data.Kvlues),
                        vol_open3 = Convert.ToDouble(data.vol_open),
                        vol_load3 = Convert.ToDouble(data.vol_load),
                        ovc3 = Convert.ToDouble(data.ovc),
                        QRCode = data.QRCode,
                        TestFrequency = Convert.ToInt32(data.TestFrequency),
                        timeInterval3 = Convert.ToDouble(data.TimeIntervalActual1)
                    }; 
                    break;
            }

                 return Rdata;
        }

        BatteryTestSystem_ElectricCore GetBatteryTestSystem_ElectricCore(TestMessage data, int i)
        {
            BatteryTestSystem_ElectricCore Rdata = new BatteryTestSystem_ElectricCore();
            switch (i)
            {
                case 1:
                    Rdata = new BatteryTestSystem_ElectricCore()
                    {
                        BatteryType = data.BatteryType,
                        imp1 = Convert.ToDouble(data.imp),
                        Kvalue1 = Convert.ToDouble(data.Kvlues),
                        vol_open1 = Convert.ToDouble(data.vol_open),
                        vol_load1 = Convert.ToDouble(data.vol_load),
                        QRCode = data.QRCode,
                        TestFrequency = Convert.ToInt32(data.TestFrequency)
                    };
                    break;
                case 2:
                    Rdata = new BatteryTestSystem_ElectricCore()
                    {
                        BatteryType = data.BatteryType,
                        imp2 = Convert.ToDouble(data.imp),
                        Kvalue2 = Convert.ToDouble(data.Kvlues),
                        vol_open2 = Convert.ToDouble(data.vol_open),
                        vol_load2 = Convert.ToDouble(data.vol_load),
                        QRCode = data.QRCode,
                        TestFrequency = Convert.ToInt32(data.TestFrequency),
                        timeInterval2=Convert.ToDouble(data.TimeIntervalActual1)
                    };
                    break;
                case 3:
                    Rdata = new BatteryTestSystem_ElectricCore()
                    {
                        BatteryType = data.BatteryType,
                        imp3 = Convert.ToDouble(data.imp),
                        Kvalue3 = Convert.ToDouble(data.Kvlues),
                        vol_open3 = Convert.ToDouble(data.vol_open),
                        vol_load3 = Convert.ToDouble(data.vol_load),
                        QRCode = data.QRCode,
                        TestFrequency = Convert.ToInt32(data.TestFrequency),
                        timeInterval3=Convert.ToDouble(data.TimeIntervalActual1)
                    };
                    break;
            }

            return Rdata;
        }

        BatteryTestSystem_ErorrMessage GetBatteryTestSystem_ErorrMessage(TestMessage data, bool electricCoreOrFinishedProduct)
        {
            try
            {
                BatteryTestSystem_ErorrMessage Rdata = new BatteryTestSystem_ErorrMessage()
                {
                    BatteryType = data.BatteryType,
                    IDimp1 = electricCoreOrFinishedProduct?Convert.ToDouble(data.r2):0,
                    imp1 = Convert.ToDouble(data.imp),
                    Kvalue1 = Convert.ToDouble(data.Kvlues),
                    vol_open1 = Convert.ToDouble(data.vol_open),
                    vol_load1 = Convert.ToDouble(data.vol_load),
                    ovc1 = electricCoreOrFinishedProduct?Convert.ToDouble(data.ovc):0,
                    QRCode = data.QRCode,
                    TestFrequency = Convert.ToInt32(data.TestFrequency),
                    ElectricCoreOrFinishedProduct = electricCoreOrFinishedProduct,
                    ErorrText = data.ERR_FLAG
                };

                return Rdata;
            }
            catch
            {
                throw;
            }
        }

        public BatteryTestSystem_DayAnalysis GetBatteryTestSystem_DayAnalysis(TestMessage data, bool electricCoreOrFinishedProduct,bool bl)
        {
            try
            { 
            BatteryTestSystem_DayAnalysis Rdata = new BatteryTestSystem_DayAnalysis()
            {
                BatteryType = data.BatteryType,
                IDimp1 = electricCoreOrFinishedProduct ? Convert.ToDouble(data.r2) : 0,
                imp1 = Convert.ToDouble(data.imp),
                Kvalue1 = Convert.ToDouble(data.Kvlues),
                vol_open1 = Convert.ToDouble(data.vol_open),
                vol_load1 = Convert.ToDouble(data.vol_load),
                ovc1 = electricCoreOrFinishedProduct ? Convert.ToDouble(data.ovc) : 0,
                QRCode = data.QRCode,
                TestFrequency = Convert.ToInt32(data.TestFrequency),
                ElectricCoreOrFinishedProduct = electricCoreOrFinishedProduct,
                ErorrText = data.ERR_FLAG,
                Result=bl
            };
            return Rdata;
            }
            catch
            {
                throw;
            }
        }
    }
}
