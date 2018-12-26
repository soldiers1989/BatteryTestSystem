using BatteryTestSystem_Comm;
using BatteryTestSystem_Model;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 电池测试系统.TestSystems.TestSystemsCalss
{
    public delegate void SendEventHandler(FinishedProductTesting msg);

    public delegate void SendEventHandler1(StaticParameter msg);

    public delegate void SenEventString(string msg);

    public class TestSystemComport
    {
        public event SendEventHandler SendEvent;

        public event SendEventHandler1 SendEvent1;

        public event SenEventString SendEventString;

        TestSystem TS;

        SerialPortUtil comPort = new SerialPortUtil();

        private string Equipment = "";

        /// <summary>
        /// 端口
        /// </summary>
        private string COM;
        /// <summary>
        /// 波特率
        /// </summary>
        private string com_baudRate;
        /// <summary>
        /// 校验位
        /// </summary>
        private string Par = "";
        /// <summary>
        /// 数据位
        /// </summary>
        private string Databits = "";
        /// <summary>
        /// 停止位
        /// </summary>
        private string Stop = "";

        StringBuilder StrData = new StringBuilder();

        private int StrCount=0;

        public TestSystemComport()
        {

        }
        public TestSystemComport(string com, string com_baudtate, string databits, string par, string stop, string equipment,int strcount, TestSystem ts)
        {
            this.COM = com;
            this.com_baudRate = com_baudtate;
            this.TS = ts;
            this.Par = par;
            this.Databits = databits; 
            this.Stop = stop;
            this.Equipment = equipment;
            this.StrCount = strcount;
            opencomPort();
        }
        /// <summary>
        /// 获取停止位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        StopBits getStopBits(string str)
        {
            StopBits rsb = StopBits.One;
            switch (str)
            {
                case "0":
                    rsb = StopBits.None;
                    break;
                case "1":
                    rsb = StopBits.One;
                    break;
                case "1.5":
                    rsb = StopBits.OnePointFive;
                    break;
                case "2":
                    rsb = StopBits.Two;
                    break;
            }

            return rsb;
        }
        /// <summary>
        /// 获取校验位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        Parity getParity(string str)
        {
            Parity pt = Parity.None;
            switch (str)
            {
                case "None":
                    pt = Parity.None;
                    break;
                case "Mark":
                    pt = Parity.Mark;
                    break;
                case "Even":
                    pt = Parity.Even;
                    break;
                case "Odd":
                    pt = Parity.Odd;
                    break;
                case "Space":
                    pt = Parity.Space;
                    break;
            }

            return pt;
        }
        /// <summary>
        /// 获取数据位
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        SerialPortDatabits getSerialPortDatabits(string str)
        {
            SerialPortDatabits databits = SerialPortDatabits.EightBits;
            switch (str)
            {
                case "5":
                    databits = SerialPortDatabits.FiveBits;
                    break;
                case "6":
                    databits = SerialPortDatabits.SixBits;
                    break;
                case "7":
                    databits = SerialPortDatabits.SeventBits;
                    break;
                case "8":
                    databits = SerialPortDatabits.EightBits;
                    break;
            }
            return databits;
        }
        /// <summary>
        /// 发送串口
        /// </summary>
        public void ScanComPort(List<string> list)
        {
            byte[] buff = new byte[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                buff[i] = byte.Parse(list[i], System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            comPort.WriteData(buff);
        }

        void opencomPort()
        {

                if (!comPort.IsOpen)
                {
                    comPort.PortName = COM;
                    comPort.BaudRate = GetSerialPortBaudRates(com_baudRate);
                    comPort.DataBits = getSerialPortDatabits(Databits);
                    comPort.Parity = getParity(Par);
                    comPort.StopBits = getStopBits(Stop);
                    comPort.DataReceived += new DataReceivedEventHandler(comPort_DataReceived1);
                    comPort.OpenPort();
                }

                byte[] buff = new byte[] { 0xB1, 0xB1 };

                comPort.WriteData(buff);
            

        }
        /// <summary>
        /// 串口接收
        /// </summary>
        /// <param name="e"></param>
        void comPort_DataReceived1(DataReceivedEventArgs e)
        {
            if (Equipment == "设备")
            this.comPort_DataReceived_Data_Processing(byteToHexStr(e.DataRecv));
            else if (Equipment == "扫描枪")
                this.comPort_DataReceived_Data_Processing(System.Text.Encoding.UTF8.GetString(e.DataRecv));
        }

        void comPort_DataReceived_Data_Processing(string str)
        {
            if (Equipment == "设备")
            {
                StrData.Append(str);
                //快速测试结果集
                if (StrData.ToString().Substring(0, 2) == "D1")
                {
                    if (StrData.Length >= 32)
                    {

                        this.SendEvent(GetFinishedProductTesting(StrData.ToString()));//快速测试结果集
                        StrData.Clear();
                    }
                }
                //询问静态参数
                else if (StrData.ToString().Substring(0, 2) == "C1")
                {
                    if (StrData.Length >= 84)
                    {
                        this.SendEvent1(GetStaticParameter(StrData.ToString()));//委托获得静态参数
                        StrData.Clear();
                    }
                }
                else if (StrData.ToString().Substring(0, 2) == "A0")
                {
                    MessageBox.Show("更改设备参数完成");
                    StrData.Clear();
                }
                else if (StrData.ToString().Substring(0, 2) == "B1")
                {
                    if (StrData.Length >= 34)
                    {
                        StrData.Clear();
                    }
                }
                else
                {
                    StrData.Clear();
                }
            }
            else if(Equipment=="扫描枪")
            {
                StrData.Append(str);
                if (StrData.ToString().Contains("\r\n"))
                {
                    if (StrData.ToString().Length >= StrCount)
                    {
                        this.SendEventString(StrData.ToString());
                        StrData.Clear();
                    }
                    else
                        StrData.Clear();
                }
            }

            
        }

        public static string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }
            return returnStr;
        }

        FinishedProductTesting GetFinishedProductTesting(string str)
        {
            FinishedProductTesting Finishept = new FinishedProductTesting()
            {
                vol_open = (GetNumber(str.Substring(2, 2), str.Substring(4, 2))/1000).ToString(),
                vol_load = (GetNumber(str.Substring(6, 2), str.Substring(8, 2))/1000).ToString(),
                imp = (GetNumber(str.Substring(10, 2), str.Substring(12, 2))/10).ToString(),
                ovc = (GetNumber(str.Substring(14, 2), str.Substring(16, 2))/1000).ToString(),
                r1 = GetNumberR1(str.Substring(18, 2), str.Substring(20, 2)).ToString(),
                r2=GetNumberR1(str.Substring(22, 2), str.Substring(24, 2)).ToString(),
                ERR_FLAG = GetERR_FLAG(str.Substring(26, 2),str.Substring(28, 2))
            };


            return Finishept;
        }

        StaticParameter GetStaticParameter(string str)
        {
            StaticParameter StaticP = new StaticParameter()
            {
                battery_type = Convert.ToInt32( GetNumber(str.Substring(6, 2), str.Substring(8, 2))),
                battery_serial = Convert.ToInt32(GetNumber(str.Substring(10, 2), str.Substring(12, 2))),
                Battery_cap = Convert.ToInt32(GetNumber(str.Substring(14, 2), str.Substring(16, 2))),
                vol_min = GetNumber(str.Substring(18, 2), str.Substring(20, 2))/1000,
                vol_max = GetNumber(str.Substring(22, 2), str.Substring(24, 2))/1000,
                imp_min = GetNumber(str.Substring(26, 2), str.Substring(28, 2))/10,
                imp_max = GetNumber(str.Substring(30, 2), str.Substring(32, 2))/10,
                ovc_min = GetNumber(str.Substring(34, 2), str.Substring(36, 2))/1000,
                ovc_max = GetNumber(str.Substring(38, 2), str.Substring(40, 2))/1000,
                r1_min = GetNumberR1(str.Substring(42, 2), str.Substring(44, 2)),
                r1_max = GetNumberR1(str.Substring(46, 2), str.Substring(48, 2)),
                r2_min = GetNumberR1(str.Substring(50, 2), str.Substring(52, 2)),
                r2_max = GetNumberR1(str.Substring(54, 2), str.Substring(56, 2)),
                id_type1 = Convert.ToInt32(GetNumber(str.Substring(58, 2), str.Substring(60, 2))),
                id_type2 = Convert.ToInt32(GetNumber(str.Substring(62, 2), str.Substring(64, 2))),
                short_time = Convert.ToInt32(GetNumber(str.Substring(66, 2), str.Substring(68, 2))),
                open_time = Convert.ToInt32(GetNumber(str.Substring(70, 2), str.Substring(72, 2))),
                detV_Max = GetNumber(str.Substring(74, 2), str.Substring(76, 2))/1000,
                reserve = Convert.ToInt32(GetNumber(str.Substring(78, 2), str.Substring(80, 2))),
            };

            return StaticP;
        }
        float GetNumberR1(string str1, string str2)
        {
            string str = str2 + str1;

            return GetResistance(Convert.ToUInt16(str, 16));
        }
        /// <summary>
        /// 阻值R1R2算法
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        float GetResistance(UInt16 Value)
        {
            float Res;
            if ((Value & 0x8000) == 0)
            {
                Res = (float)Value / 10;
            }
            else
            {
                Res = (float)((UInt16)(Value & 0x7fff)) / 100;
            }
            return Res;
        }
        double GetNumber(string str1,string str2)
        {
            string str = str2 + str1;

            return Convert.ToInt32(str, 16);
        }

        string GetERR_FLAG(string str1, string str2)
        {
            string str = str2 + str1;
            string Rstr = "";
            switch (str)
            {
                case "0000":
                    Rstr = "测试通过";
                    break;
                case "0001":
                    Rstr = "电压偏低";
                    break;
                case "0002":
                    Rstr = "电压偏高";
                    break;
                case "0004":
                    Rstr = "内阻偏低";
                    break;
                case "0008":
                    Rstr = "内阻偏高";
                    break;
                case "0010":
                    Rstr = "充电失败";
                    break;
                case "0020":
                    Rstr = "放电失败";
                    break;
                case "0040":
                    Rstr = "过电流值偏低";
                    break;
                case "0080":
                    Rstr = "过电流值偏高";
                    break;
                case "0100":
                    Rstr = "R2值偏低";
                    break;
                case "0200":
                    Rstr = "R2值偏高";
                    break;
                case "0400":
                    Rstr = "ID电阻值偏低";
                    break;
                case "0800":
                    Rstr = "ID电阻值偏高";
                    break;
                case "1000":
                    Rstr = "ID错误";
                    break;
                case "2000":
                    Rstr = "短路保护错误";
                    break;
                case "4000":
                    Rstr = "过充保护失败";
                    break;
                case "8000":
                    Rstr = "过放保护失败";
                    break;
            }
            return Rstr;
        }

        /// <summary>
        /// 获得波特率结构组
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public SerialPortBaudRates GetSerialPortBaudRates(string str)
        {
            switch (str)
            {
                case "75":
                    return SerialPortBaudRates.BaudRate_75;
                case "110":
                    return SerialPortBaudRates.BaudRate_110;
                case "150":
                    return SerialPortBaudRates.BaudRate_150;
                case "300":
                    return SerialPortBaudRates.BaudRate_300;
                case "600":
                    return SerialPortBaudRates.BaudRate_600;
                case "1200":
                    return SerialPortBaudRates.BaudRate_1200;
                case "2400":
                    return SerialPortBaudRates.BaudRate_2400;
                case "4800":
                    return SerialPortBaudRates.BaudRate_4800;
                case "9600":
                    return SerialPortBaudRates.BaudRate_9600;
                case "14400":
                    return SerialPortBaudRates.BaudRate_14400;
                case "19200":
                    return SerialPortBaudRates.BaudRate_19200;
                case "28800":
                    return SerialPortBaudRates.BaudRate_28800;
                case "38400":
                    return SerialPortBaudRates.BaudRate_38400;
                case "56000":
                    return SerialPortBaudRates.BaudRate_56000;
                case "57600":
                    return SerialPortBaudRates.BaudRate_57600;
                case "115200":
                    return SerialPortBaudRates.BaudRate_115200;
                case "128000":
                    return SerialPortBaudRates.BaudRate_128000;
                case "230400":
                    return SerialPortBaudRates.BaudRate_230400;
                case "256000":
                    return SerialPortBaudRates.BaudRate_256000;
            }

            return new SerialPortBaudRates();
        }

        public void CloseComport()
        {
            if (comPort.IsOpen)
                comPort.ClosePort();
        }
        /// <summary>
        /// 拿到参数的十六进制的四位翻转值
        /// </summary>
        /// <returns></returns>
        string GetHx(double Number)
        {
            string str = "0000";

            int i = Convert.ToInt32(Number);

            string Hx = Convert.ToString(i, 16);

            if (str.Length > Hx.Length)
            {
                string str2 = str.Remove(str.Length - Hx.Length - 1, Hx.Length);
                Hx = str2 + Hx;

                return GetPLC2(Hx).ToUpper();
            }
            else
            {
                return GetPLC2(Hx).ToUpper();
            }
        }
        /// <summary>
        /// GetPLCnumber4直接换位2
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        string GetPLC2(string str)
        {
            if (str == "")
            {
                return "";
            }
            string str3 = str.Substring(0, 2);
            string str4 = str.Remove(0, 2);

            return str4 + str3;
        }
        /// <summary>
        /// 更新设备参数
        /// </summary>
        /// <param name="data"></param>
        public void DOWNLOAD(StaticParameter data)
        {
            try
            {
                List<string> list = new List<string>();
                list.Add("A1");
                list.Add("E7");
                list.Add("00");
                list.Add(GetHx(data.battery_type).Substring(0, 2));
                list.Add(GetHx(data.battery_type).Substring(2, 2));
                list.Add(GetHx(data.battery_serial).Substring(0, 2));
                list.Add(GetHx(data.battery_serial).Substring(2, 2));
                list.Add(GetHx(data.Battery_cap).Substring(0, 2));
                list.Add(GetHx(data.Battery_cap).Substring(2, 2));
                list.Add(GetHx(data.vol_min).Substring(0, 2));
                list.Add(GetHx(data.vol_min).Substring(2, 2));
                list.Add(GetHx(data.vol_max).Substring(0, 2));
                list.Add(GetHx(data.vol_max).Substring(2, 2));
                list.Add(GetHx(data.imp_min).Substring(0, 2));
                list.Add(GetHx(data.imp_min).Substring(2, 2));
                list.Add(GetHx(data.imp_max).Substring(0, 2));
                list.Add(GetHx(data.imp_max).Substring(2, 2));
                list.Add(GetHx(data.ovc_min).Substring(0, 2));
                list.Add(GetHx(data.ovc_min).Substring(2, 2));
                list.Add(GetHx(data.ovc_max).Substring(0, 2));
                list.Add(GetHx(data.ovc_max).Substring(2, 2));
                list.Add(GetHx(data.r1_min).Substring(0, 2));
                list.Add(GetHx(data.r1_min).Substring(2, 2));
                list.Add(GetHx(data.r1_max).Substring(0, 2));
                list.Add(GetHx(data.r1_max).Substring(2, 2));
                list.Add(GetHx(data.r2_min).Substring(0, 2));
                list.Add(GetHx(data.r2_min).Substring(2, 2));
                list.Add(GetHx(data.r2_max).Substring(0, 2));
                list.Add(GetHx(data.r2_max).Substring(2, 2));
                list.Add(GetHx(data.id_type1).Substring(0, 2));
                list.Add(GetHx(data.id_type1).Substring(2, 2));
                list.Add(GetHx(data.id_type2).Substring(0, 2));
                list.Add(GetHx(data.id_type2).Substring(2, 2));
                list.Add(GetHx(data.short_time).Substring(0, 2));
                list.Add(GetHx(data.short_time).Substring(2, 2));
                list.Add(GetHx(data.open_time).Substring(0, 2));
                list.Add(GetHx(data.open_time).Substring(2, 2));
                list.Add(GetHx(data.detV_Max).Substring(0, 2));
                list.Add(GetHx(data.detV_Max).Substring(2, 2));
                list.Add(GetHx(data.reserve).Substring(0, 2));
                list.Add(GetHx(data.reserve).Substring(2, 2));
                list.Add("50");

                ScanComPort(list);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


    }
}
