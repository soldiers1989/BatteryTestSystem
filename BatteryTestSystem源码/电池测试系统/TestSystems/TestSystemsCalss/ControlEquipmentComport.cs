using BatteryTestSystem_Comm;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;

namespace 电池测试系统.TestSystems.TestSystemsCalss
{
    public delegate void SenEventStringX(string msg);

    public class ControlEquipmentComport
    {
        public event SenEventStringX SendEventStringX;

        TestSystem TS;

        SerialPortUtil comPort = new SerialPortUtil();

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

        public ControlEquipmentComport(string com, string com_baudtate, string databits, string par, string stop, TestSystem ts)
        {
            this.COM = com;
            this.com_baudRate = com_baudtate;
            this.TS = ts;
            this.Par = par;
            this.Databits = databits;
            this.Stop = stop;
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
        /// 发送串口信息
        /// </summary>
        /// <param name="str"></param>
        public void SendComport(string str)
        {
            comPort.WriteData(str);
        }

        public void CloseComPort()
        {
            if (comPort.IsOpen)
                comPort.ClosePort();
        }
        /// <summary>
        /// 打开串口
        /// </summary>
        void opencomPort()
        {

            if (!comPort.IsOpen)
            {
                comPort.PortName = COM;
                comPort.BaudRate = GetSerialPortBaudRates(com_baudRate);
                comPort.DataBits = getSerialPortDatabits(Databits);
                comPort.Parity = getParity(Par);
                comPort.StopBits = getStopBits(Stop);
                comPort.DataReceived += comPort_DataReceived;
                comPort.OpenPort();
            }

        }
        /// <summary>
        ///串口接收事件
        /// </summary>
        /// <param name="e"></param>
        void comPort_DataReceived(DataReceivedEventArgs e)
        {
            this.comPort_DataReceived_Data_Processing(System.Text.Encoding.UTF8.GetString(e.DataRecv));
        }

        void comPort_DataReceived_Data_Processing(string str)
        {
            StrData.Append(str);
            if (StrData.ToString().Contains("\r\n"))
            {
                this.SendEventStringX(StrData.ToString());
                StrData.Clear();
            }
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
    }
}
