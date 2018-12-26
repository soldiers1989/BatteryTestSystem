using BatteryTestSystem_Bll;
using BatteryTestSystem_Comm;
using BatteryTestSystem_Model;
using DevComponents.DotNetBar;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using 电池测试系统.InformationSystems;
using 电池测试系统.TestSystems;
using 电池测试系统.TestSystems.TestSystemsCalss;

namespace 电池测试系统
{
    public partial class TestSystem : Office2007Form
    {
        MainForm MF;

        SynchronizationContext My_cotext = null;
        //设备
        TestSystemComport tsb;
        TestSystemComport tsb2;
        //扫描枪
        TestSystemComport tsb3;
        TestSystemComport tsb4;

        //控制设备单片机
        ControlEquipmentComport Cec;

        //类对象
        TestSystemBll MainBll = new TestSystemBll();
        BatteryTestSystem_BatteryType BatteryType1 = new BatteryTestSystem_BatteryType();
        BatteryTestSystem_BatteryType BatteryType2 = new BatteryTestSystem_BatteryType();

        BatteryTestSystem_FinishedProductBll FinishedBll = new BatteryTestSystem_FinishedProductBll();
        BatteryTestSystem_ElectricCoreBll ElectricBll = new BatteryTestSystem_ElectricCoreBll();

        BatteryTestSystem_ElectricCore electricData = new BatteryTestSystem_ElectricCore();
        BatteryTestSystem_FinishedProduct FinishedData = new BatteryTestSystem_FinishedProduct();
        BatteryTestSystem_ElectricCore electricData2 = new BatteryTestSystem_ElectricCore();
        BatteryTestSystem_FinishedProduct FinishedData2 = new BatteryTestSystem_FinishedProduct();

        BatteryTestSystem_UnidentitiesTestBll UnidentitiesTestBll = new BatteryTestSystem_UnidentitiesTestBll();
        /// <summary>
        /// 当天数据报表
        /// </summary>
        BatteryTestSystem_DayAnalysisBll DayAnalysisBll = new BatteryTestSystem_DayAnalysisBll();

        //集合
        List<FinishedProductTesting> DataList = new List<FinishedProductTesting>();
        List<TestMessage> DataListShow = new List<TestMessage>();
        List<BatteryTestSystem_BatteryType> BatteryTypeList = new List<BatteryTestSystem_BatteryType>();

        string Com; 
        string Com_baudtate;
        string Databits;
        string Par;
        string Stop;

        int DataNember = 0;
        public TestSystem(MainForm mf)
        {
            InitializeComponent();
            this.MF = mf;
            tabControl1.SelectedTab = tabControl1.Tabs[0];
        }

        void formMain_SendEvent(FinishedProductTesting msg)
        {
            My_cotext.Post(SetTexBOx, msg);
        }

        void SetTexBOx(object o)
        {
            FinishedProductTesting str = o as FinishedProductTesting;
            SetDataGridView(str, txt_QRCode.Text.Trim(),1);
        }

        void SetTexBOx2(object o)
        {
            FinishedProductTesting str = o as FinishedProductTesting;
            SetDataGridView(str, txt_QRCode2.Text.Trim(), 2);
        }

        void SetDataGridView(FinishedProductTesting data, string txt_QRCode,int i)
        {
            TestMessage Rdata = new TestMessage();
            try
            {
                if (i == 1)
                {
                    if (ck_FastTest.Checked)
                    {
                        #region 单机版
                        TestMessage TMdata = new TestMessage()
                        {
                            Number = DataNember.ToString(),
                            QRCode = txt_QRCode,
                            vol_open = data.vol_open,
                            vol_load = data.vol_load,
                            ovc = data.ovc,
                            imp = data.imp,
                            r1 = data.r1,
                            r2 = data.r2,
                            ERR_FLAG = data.ERR_FLAG,
                            BatteryType = cbe_ProductionClass.Text.Trim(),
                            UpdateTime = DateTime.Now,
                            Equipment="设备一"
                        };


                        lb_Message1.Text = TMdata.ERR_FLAG;
                        if (TMdata.ERR_FLAG == "测试通过")
                        {
                            GU_Message1.BackColor = Color.Lime;
                            Cec.SendComport("AT01a#TNGL95\r\n");
                        }
                        else
                        {
                            GU_Message1.BackColor = Color.Red;
                            Cec.SendComport("AT01a#TNRLA4\r\n");
                        }
                        lb_Message1.Left = (int)(GU_Message1.Width - lb_Message1.Width) / 2;

                        UnidentitiesTestBll.insertBatteryTestSystem_UnidentitiesTestDal(GetBatteryTestSystem_UnidentitiesTest(TMdata, ck_FinishedProduct.Checked, MainBll.GetERR_FLAG(TMdata.ERR_FLAG)), "insertBatteryTestSystem_UnidentitiesTest");
                        DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(MainBll.GetBatteryTestSystem_DayAnalysis(TMdata, ck_FinishedProduct.Checked, MainBll.GetERR_FLAG(TMdata.ERR_FLAG)), "insertBatteryTestSystem_DayAnalysis");
                        DataNember++;

                        dataGridViewX1.DataSource = new List<TestMessage>();
                        DataListShow.Insert(0, TMdata);
                        dataGridViewX1.DataSource = DataListShow;

                        return;
                        #endregion
                    }



                    if (txt_QRCode == "")
                    {
                        MessageToolscs.Tools("设备一:请先扫码!");
                        Cec.SendComport("AT01a#TNRLA4\r\n");
                        lb_Message1.Text = "设备一:请先扫码!";
                        return;
                    }
                    if (txt_VoltageDifference.Text.Trim() == "" || txt_TimeInterval.Text.Trim() == "")
                    {
                        MessageToolscs.Tools("设备一:请选择电池类型!");
                        Cec.SendComport("AT01a#TNRLA4\r\n");
                        lb_Message1.Text = "设备一:请选择电池类型!";
                        return;
                    }
                    TestMessage Tdata = new TestMessage()
                    {
                        Number = DataNember.ToString(),
                        QRCode = txt_QRCode,
                        vol_open = data.vol_open,
                        vol_load = data.vol_load,
                        ovc = data.ovc,
                        imp = data.imp,
                        r1 = data.r1,
                        r2 = data.r2,
                        ERR_FLAG = data.ERR_FLAG,
                        BatteryType = cbe_ProductionClass.Text.Trim(),
                        UpdateTime = DateTime.Now,
                        Equipment = "设备一"
                    };

                    Thread th = new Thread(delegate()
                    {
                        MainBll_MainTestMessageElectricCore(ck_ElectricCore.Checked, Tdata, txt_QRCode, BatteryType1, BatteryType2, electricData, electricData2, FinishedData, FinishedData2, 1);
                    });
                    th.IsBackground = true;
                    th.Start();
                }
                else
                {
                    #region 单机版
                    if (ck_FastTest2.Checked)
                    {
                        TestMessage TMdata = new TestMessage()
                        {
                            Number = DataNember.ToString(),
                            QRCode = txt_QRCode,
                            vol_open = data.vol_open,
                            vol_load = data.vol_load,
                            ovc = data.ovc,
                            imp = data.imp,
                            r1 = data.r1,
                            r2 = data.r2,
                            ERR_FLAG = data.ERR_FLAG,
                            BatteryType = cbe_ProductionClass2.Text.Trim(),
                            UpdateTime = DateTime.Now,
                            Equipment = "设备二"
                        };

                        lb_Message2.Text = TMdata.ERR_FLAG;
                        if (TMdata.ERR_FLAG == "测试通过")
                        {
                            GU_Message2.BackColor = Color.Lime;
                            Cec.SendComport("AT02b#TNGLFF\r\n");
                        }
                        else
                        {
                            GU_Message2.BackColor = Color.Red;
                            Cec.SendComport("AT02b#TNRL39\r\n");
                        }
                        lb_Message2.Left = (int)(GU_Message2.Width - lb_Message2.Width) / 2;
                        UnidentitiesTestBll.insertBatteryTestSystem_UnidentitiesTestDal(GetBatteryTestSystem_UnidentitiesTest(TMdata, ck_FinishedProduct2.Checked, MainBll.GetERR_FLAG(TMdata.ERR_FLAG)), "insertBatteryTestSystem_UnidentitiesTest");
                        DayAnalysisBll.insertBatteryTestSystem_DayAnalysisBll(MainBll.GetBatteryTestSystem_DayAnalysis(TMdata, ck_FinishedProduct2.Checked, MainBll.GetERR_FLAG(TMdata.ERR_FLAG)), "insertBatteryTestSystem_DayAnalysis");

                        DataNember++;

                        dataGridViewX1.DataSource = new List<TestMessage>();
                        DataListShow.Insert(0, TMdata);
                        dataGridViewX1.DataSource = DataListShow;

                        return;
                    #endregion
                    }


                    if (txt_QRCode == "")
                    {
                        MessageToolscs.Tools("设备二:请先扫码!");
                        Cec.SendComport("AT02b#TNRL39\r\n");
                        lb_Message2.Text = "设备二:请先扫码!";
                        return;
                    }
                    if (txt_VoltageDifference2.Text.Trim() == "" || txt_TimeInterval2.Text.Trim() == "")
                    {
                        MessageToolscs.Tools("设备二:请选择电池类型!");
                        Cec.SendComport("AT02b#TNRL39\r\n");
                        lb_Message2.Text = "设备二:请选择电池类型!";
                        return;
                    }

                    TestMessage Tdata = new TestMessage()
                    {
                        Number = DataNember.ToString(),
                        QRCode = txt_QRCode,
                        vol_open = data.vol_open,
                        vol_load = data.vol_load,
                        ovc = data.ovc,
                        imp = data.imp,
                        r1 = data.r1,
                        r2 = data.r2,
                        ERR_FLAG = data.ERR_FLAG,
                        BatteryType = cbe_ProductionClass2.Text.Trim(),
                        UpdateTime = DateTime.Now,
                        Equipment = "设备二"
                    };
                    Thread th = new Thread(delegate()
                    {
                        MainBll_MainTestMessageElectricCore(ck_ElectricCore2.Checked, Tdata, txt_QRCode, BatteryType1, BatteryType2, electricData, electricData2, FinishedData, FinishedData2, 2);
                    });
                    th.IsBackground = true;
                    th.Start();
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void MainBll_MainTestMessageElectricCore(bool bl, TestMessage Tdata, string txt_QRCode, BatteryTestSystem_BatteryType batteryType1, BatteryTestSystem_BatteryType batteryType2, BatteryTestSystem_ElectricCore electricdata, BatteryTestSystem_ElectricCore electricdata2, BatteryTestSystem_FinishedProduct finishedData, BatteryTestSystem_FinishedProduct finishedData2, int i)
        {
            TestMessage Rdata = new TestMessage();
            try
            {
                if (bl)
                {

                    switch (i)
                    {
                        case 1:
                            Rdata = MainBll.MainTestMessage(Tdata, txt_QRCode, batteryType1, electricdata);
                            My_cotext.Post(setControlsElectricCore, Rdata);
                            break;
                        case 2:
                            Rdata = MainBll.MainTestMessage(Tdata, txt_QRCode, batteryType2, electricdata2);
                            My_cotext.Post(setControlsElectricCore2, Rdata);
                            break;
                    }

                }
                else
                {

                    switch (i)
                    {
                        case 1:
                            Rdata = MainBll.MainTestMessage(Tdata, txt_QRCode, batteryType1, finishedData);
                            My_cotext.Post(setControlsElectricCore, Rdata);
                            break;
                        case 2:
                            Rdata = MainBll.MainTestMessage(Tdata, txt_QRCode, batteryType2, finishedData2);
                            My_cotext.Post(setControlsElectricCore2, Rdata);
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void setControlsElectricCore(object o)
        {
            TestMessage Rdata = o as TestMessage;

            txt_KValus1.Text = Rdata.Kvlues;
            txt_CKValus1.Text = (Rdata.TimeIntervalActual1 * Convert.ToDouble(txt_VoltageDifference.Text.Trim())).ToString();
            txt_TimeIntervalActual1.Text = Rdata.TimeIntervalActual1.ToString();
            SetMessageLable(Rdata.ERR_FLAG, 1, Rdata.Result);
            this.txt_QRCode.Text = "";

            DataNember++;

            dataGridViewX1.DataSource = new List<TestMessage>();
            DataListShow.Insert(0, Rdata);
            dataGridViewX1.DataSource = DataListShow;
        }
        void setControlsElectricCore2(object o)
        {
            TestMessage Rdata = o as TestMessage;

            txt_KValus2.Text = Rdata.Kvlues;
            txt_CKValus2.Text = (Rdata.TimeIntervalActual1 * Convert.ToDouble(txt_VoltageDifference2.Text.Trim())).ToString();
            txt_TimeIntervalActual2.Text = Rdata.TimeIntervalActual1.ToString();
            SetMessageLable(Rdata.ERR_FLAG, 2, Rdata.Result);

            this.txt_QRCode2.Text = "";

            DataNember++;

            dataGridViewX1.DataSource = new List<TestMessage>();
            DataListShow.Insert(0, Rdata);
            dataGridViewX1.DataSource = DataListShow;
        }

        void SetMessageLable(string Message,int i,bool bl)
        {
            switch (i)
            {
                case 1:
                    lb_Message1.Text = Message;
                    if(bl)
                    {
                        GU_Message1.BackColor = Color.Lime;
                        Cec.SendComport("AT01a#TNGL95\r\n");
                    }
                    else
                    {
                        GU_Message1.BackColor = Color.Red;
                        Cec.SendComport("AT01a#TNRLA4\r\n");     
                    }
                    lb_Message1.Left = (int)(GU_Message1.Width - lb_Message1.Width) / 2;
                    break;
                case 2:
                    lb_Message2.Text = Message;
                    if(bl)
                    {
                        GU_Message2.BackColor = Color.Lime;
                        Cec.SendComport("AT02b#TNGLFF\r\n");
                    }
                    else
                    {
                        GU_Message2.BackColor = Color.Red;
                        Cec.SendComport("AT02b#TNRL39\r\n");
                    }
                    lb_Message2.Left = (int)(GU_Message2.Width - lb_Message2.Width) / 2;
                    break;
            }
        }

        void SetMessageInitialization(int i)
        {
            switch (i)
            {
                case 1:
                    lb_Message1.Text = "等待...";
                    GU_Message1.BackColor = Color.Transparent;
                    SetMessageInitialization1();
                    break;
                case 2:
                    lb_Message2.Text = "等待...";
                    GU_Message2.BackColor = Color.Transparent;
                    SetMessageInitialization1();
                    break;
                case 3:
                    lb_Message1.Text = "等待...";
                    lb_Message2.Text = "等待...";
                    GU_Message1.BackColor = Color.Transparent;
                    GU_Message2.BackColor = Color.Transparent;
                    SetMessageInitialization1();
                    break;
            }

        }

        void SetMessageInitialization1()
        {
            lb_Message1.Left = (int)(GU_Message1.Width - lb_Message1.Width) / 2;
            lb_Message2.Left = (int)(GU_Message2.Width - lb_Message2.Width) / 2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tsb.CloseComport();
        }

        private void TestSystem_Load(object sender, EventArgs e)
        {
            My_cotext = SynchronizationContext.Current;
            this.labelItem2.Text = UsersHelp.UserName;
            GetPortName();//获得当前计算机的端口号
            MF.Visible = false;//隐藏主窗体
            com_PortMainBoard.SelectedIndex = com_PortMainBoard.Items.Count - 1;
            LoadXML(System.Windows.Forms.Application.StartupPath + "\\XML\\ParameterConfiguration.xml");//加载串口参数XML
            LoadXML1(System.Windows.Forms.Application.StartupPath + "\\XML\\ParameterConfigurationControlEquipment.xml");//加载串口参数XML
            SetBatteryTypeList(MainBll.GetBatteryType());
            SetDatagridviewShow();
            SetMessageInitialization(3);
        }

        void LoadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
                XmlNodeList list = doc.SelectSingleNode("ParameterConfiguration").ChildNodes;//获得根节点user下的所有子节点
                foreach (XmlNode item in list)
                {
                    if (item.Name == "equipment1")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "com")
                                com_PortMainBoard.Text = node.InnerText;
                            if (node.Name == "com_baudtate")
                                com_baudRateMainBoard.Text = node.InnerText;
                            if (node.Name == "databits")
                                comb_Databits.Text = node.InnerText;
                            if (node.Name == "par")
                                comb_Parity.Text = node.InnerText;
                            if (node.Name == "stop")
                                comb_StopBits.Text = node.InnerText;
                        }
                    }
                    if (item.Name == "equipment2")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "com")
                                com_PortMainBoard2.Text = node.InnerText;
                            if (node.Name == "com_baudtate")
                                com_baudRateMainBoard2.Text = node.InnerText;
                            if (node.Name == "databits")
                                comb_Databits2.Text = node.InnerText;
                            if (node.Name == "par")
                                comb_Parity2.Text = node.InnerText;
                            if (node.Name == "stop")
                                comb_StopBits2.Text = node.InnerText;
                        }
                    }
                    if (item.Name == "ScanningGun1")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "com")
                                com_ScanningGun.Text = node.InnerText;
                            if (node.Name == "com_baudtate")
                                com_ScanningGun_baudRateMainBoard.Text = node.InnerText;
                            if (node.Name == "databits")
                                com_ScanningGun_Databits.Text = node.InnerText;
                            if (node.Name == "par")
                                com_ScanningGun_StopBits.Text = node.InnerText;
                            if (node.Name == "stop")
                                com_ScanningGun_Parity.Text = node.InnerText;
                        }
                    }
                    if (item.Name == "ScanningGun2")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "com")
                                com_ScanningGun2.Text = node.InnerText;
                            if (node.Name == "com_baudtate")
                                com_ScanningGun_baudRateMainBoard2.Text = node.InnerText;
                            if (node.Name == "databits")
                                com_ScanningGun_Databits2.Text = node.InnerText;
                            if (node.Name == "par")
                                com_ScanningGun_StopBits2.Text = node.InnerText;
                            if (node.Name == "stop")
                                com_ScanningGun_Parity2.Text = node.InnerText;
                        }
                    }
                }


            }
        }

        void LoadXML1(string path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
                XmlNodeList list = doc.SelectSingleNode("ParameterConfiguration").ChildNodes;//获得根节点user下的所有子节点
                foreach (XmlNode item in list)
                {
                    if (item.Name == "equipment1")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "com")
                                Com = node.InnerText;
                            if (node.Name == "com_baudtate")
                                Com_baudtate = node.InnerText;
                            if (node.Name == "databits")
                                Databits = node.InnerText;
                            if (node.Name == "par")
                                Par = node.InnerText;
                            if (node.Name == "stop")
                                Stop = node.InnerText;
                        }
                    }
                }

                StartControlEquipment(Com, Com_baudtate, Databits, Par, Stop);

                Thread th0 = new Thread(delegate()
                {
                    WhillSenComport();
                });
                th0.IsBackground = true;
                //th0.Start();
            }
        }

        void WhillSenComport()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                Cec.SendComport("AT01a#Hello1B\r\n");
            }

        }
        /// <summary>
        /// 获得当前计算机的端口号
        /// </summary>
        void GetPortName()
        {
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            List<string> list3 = new List<string>();
            List<string> list4 = new List<string>();

            foreach (string item in System.IO.Ports.SerialPort.GetPortNames())
            {
                list.Add(item);
                list2.Add(item);
                list3.Add(item);
                list4.Add(item);
            }
            com_PortMainBoard.DataSource = list;
            com_PortMainBoard2.DataSource = list2;
            com_ScanningGun.DataSource = list3;
            com_ScanningGun2.DataSource = list4;       
        }

        void SetDatagridviewShow()
        {
            dataGridViewX1.Columns.Clear();

            DataGridViewTextBoxColumn newcol7 = new DataGridViewTextBoxColumn();
            newcol7.HeaderText = "计数";
            newcol7.Name = "Number";
            newcol7.DataPropertyName = "Number";
            newcol7.Width = 80;
            newcol7.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol7.Tag = 0;
            this.dataGridViewX1.Columns.Insert(0, newcol7);

            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "二维码/条码";
            newcol.Name = "QRCode";
            newcol.DataPropertyName = "QRCode";
            newcol.Width = 120;
            newcol.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol.Tag = 1;
            this.dataGridViewX1.Columns.Insert(1, newcol);

            DataGridViewTextBoxColumn newcol30 = new DataGridViewTextBoxColumn();
            newcol30.HeaderText = "设备";
            newcol30.Name = "Equipment";
            newcol30.DataPropertyName = "Equipment";
            newcol30.Width = 120;
            newcol30.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol30.Tag = 1;
            this.dataGridViewX1.Columns.Insert(2, newcol30);

            DataGridViewTextBoxColumn newcol34 = new DataGridViewTextBoxColumn();
            newcol34.HeaderText = "电池类型";
            newcol34.Name = "BatteryType";
            newcol34.DataPropertyName = "BatteryType";
            newcol34.Width = 100;
            newcol34.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol34.Tag = 2;
            this.dataGridViewX1.Columns.Insert(3, newcol34);

            DataGridViewTextBoxColumn newcol10 = new DataGridViewTextBoxColumn();
            newcol10.HeaderText = "开路电压";
            newcol10.Name = "vol_open";
            newcol10.DataPropertyName = "vol_open";
            newcol10.Width = 100;
            newcol10.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol10.Tag = 3;
            newcol10.Visible = true;
            this.dataGridViewX1.Columns.Insert(4, newcol10);

            DataGridViewTextBoxColumn newcol1 = new DataGridViewTextBoxColumn();
            newcol1.HeaderText = "负载电压";
            newcol1.Name = "vol_load";
            newcol1.DataPropertyName = "vol_load";
            newcol1.Width = 100;
            newcol1.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol1.Tag = 4;
            this.dataGridViewX1.Columns.Insert(5, newcol1);

            DataGridViewTextBoxColumn newcol11 = new DataGridViewTextBoxColumn();
            newcol11.HeaderText = "内阻";
            newcol11.Name = "imp";
            newcol11.DataPropertyName = "imp";
            newcol11.Width = 100;
            newcol11.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol11.Tag = 5;
            this.dataGridViewX1.Columns.Insert(6, newcol11);

            DataGridViewTextBoxColumn newcol16 = new DataGridViewTextBoxColumn();
            newcol16.HeaderText = "过流保护值";
            newcol16.Name = "ovc";
            newcol16.DataPropertyName = "ovc";
            newcol16.Width = 100;
            newcol16.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol16.Tag = 6;
            newcol16.Visible = ck_FinishedProduct.Checked || ck_FinishedProduct2.Checked;
            this.dataGridViewX1.Columns.Insert(7, newcol16);

            DataGridViewTextBoxColumn newcol2 = new DataGridViewTextBoxColumn();
            newcol2.HeaderText = "ID电阻1";
            newcol2.Name = "r1";
            newcol2.DataPropertyName = "r1";
            newcol2.Width = 100;
            newcol2.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol2.Tag = 7;
            newcol2.Visible = ck_FinishedProduct.Checked || ck_FinishedProduct2.Checked;
            this.dataGridViewX1.Columns.Insert(8, newcol2);

            DataGridViewTextBoxColumn newcol6 = new DataGridViewTextBoxColumn();
            newcol6.HeaderText = "ID电阻2";
            newcol6.Name = "r2";
            newcol6.DataPropertyName = "r2";
            newcol6.Width = 100;
            newcol6.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol6.Tag = 8;
            newcol6.Visible = ck_FinishedProduct.Checked || ck_FinishedProduct2.Checked;
            this.dataGridViewX1.Columns.Insert(9, newcol6);

            DataGridViewTextBoxColumn newcol3 = new DataGridViewTextBoxColumn();
            newcol3.HeaderText = "测试次数";
            newcol3.Name = "TestFrequency";
            newcol3.DataPropertyName = "TestFrequency";
            newcol3.Width = 80;
            newcol3.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol3.Tag = 9;
            this.dataGridViewX1.Columns.Insert(10, newcol3);

            DataGridViewTextBoxColumn newco6 = new DataGridViewTextBoxColumn();
            newco6.HeaderText = "K值";
            newco6.Name = "Kvlues";
            newco6.DataPropertyName = "Kvlues";
            newco6.Width = 100;
            newco6.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco6.Tag = 10;
            this.dataGridViewX1.Columns.Insert(11, newco6);

            DataGridViewTextBoxColumn newcol4 = new DataGridViewTextBoxColumn();
            newcol4.HeaderText = "测试结果";
            newcol4.Name = "ERR_FLAG";
            newcol4.DataPropertyName = "ERR_FLAG";
            newcol4.Width = 200;
            newcol4.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol4.Tag = 11;
            this.dataGridViewX1.Columns.Insert(12, newcol4);

            DataGridViewTextBoxColumn newco5 = new DataGridViewTextBoxColumn();
            newco5.HeaderText = "测试时间";
            newco5.Name = "UpdateTime";
            newco5.DataPropertyName = "UpdateTime";
            newco5.Width = 200;
            newco5.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco5.Tag = 12;
            this.dataGridViewX1.Columns.Insert(13, newco5);

            DataGridViewTextBoxColumn newco25 = new DataGridViewTextBoxColumn();
            newco25.HeaderText = "Result";
            newco25.Name = "Result";
            newco25.DataPropertyName = "Result";
            newco25.Width = 50;
            newco25.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco25.Tag = 13;
            newco25.Visible = false;
            this.dataGridViewX1.Columns.Insert(14, newco25);

            DataGridViewTextBoxColumn newco26 = new DataGridViewTextBoxColumn();
            newco26.HeaderText = "实际间隔小时";
            newco26.Name = "TimeIntervalActual1";
            newco26.DataPropertyName = "TimeIntervalActual1";
            newco26.Width = 200;
            newco26.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco26.Tag = 14;
            newco26.Visible = true;
            this.dataGridViewX1.Columns.Insert(15, newco26);

            DataGridViewTextBoxColumn newcol5 = new DataGridViewTextBoxColumn();
            newcol5.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewX1.Columns.Insert(16, newcol5);
        }

        private void TestSystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            Cec.SendComport("AT01a#TFRL3A\r\n");//1号红灯
            System.Threading.Thread.Sleep(50);//为了让单片机能反应过来的休眠时间
            Cec.SendComport("AT02b#TFRL0F\r\n");//2号红灯
            System.Threading.Thread.Sleep(50);
            Cec.SendComport("AT01a#TFGLD4\r\n");//1号绿灯
            System.Threading.Thread.Sleep(50);
            Cec.SendComport("AT02b#TFGLE5\r\n");//2号绿灯
            MF.Close();
        }
        /// <summary>
        /// 开始或停止测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_OpenComport_Click(object sender, EventArgs e)
        {
            if (com_PortMainBoard.Text.Trim() == "")
            {
                MessageToolscs.Tools("端口不能为空！");
                return;
            }
            if (com_baudRateMainBoard.Text.Trim() == "")
            {
                MessageToolscs.Tools("波特率不能为空！");
                return;
            }
            if (comb_Databits.Text.Trim() == "")
            {
                MessageToolscs.Tools("数据位不能为空！");
                return;
            }
            if (comb_Parity.Text.Trim() == "")
            {
                MessageToolscs.Tools("校验位不能为空！");
                return;
            }
            if (comb_StopBits.Text.Trim() == "")
            {
                MessageToolscs.Tools("停止位不能为空！");
                return;
            }
            try
            {
                if (btn_OpenComport.Text == "启动测试")
                {
                    btn_OpenComport.Text = "停止测试";
                    btn_OpenComport.Style = eDotNetBarStyle.VS2005;
                    btn_change.Enabled = true;
                    btn_read.Enabled = true;

                    tsb = new TestSystemComport(com_PortMainBoard.Text.Trim(), com_baudRateMainBoard.Text.Trim(), comb_Databits.Text.Trim(), comb_Parity.Text.Trim(), comb_StopBits.Text.Trim(),"设备",0, this);
                    tsb.SendEvent += new SendEventHandler(formMain_SendEvent);
                    tsb.SendEvent1 += tsb_SendEvent1;

                    com_PortMainBoardDQ.Text = com_PortMainBoard.Text.Trim();
                    //this.tabControl1.SelectedTab = tabControl1.Tabs["tabItem1"];
                }
                else
                {
                    btn_OpenComport.Text = "启动测试";
                    btn_OpenComport.Style = eDotNetBarStyle.StyleManagerControlled;
                    btn_change.Enabled = false;
                    btn_read.Enabled = false;

                    tsb.CloseComport();
                    com_PortMainBoardDQ.Text = "";
                }
            }
            catch (Exception ex)
            {
                btn_OpenComport.Text = "启动测试";
                btn_OpenComport.Style = eDotNetBarStyle.StyleManagerControlled;
                btn_change.Enabled = false;
                btn_read.Enabled = false;
                MessageToolscs.Tools(ex.Message);
                return;
            }

        }

        void tsb_SendEvent1(StaticParameter msg)
        {
            My_cotext.Post(SetStaticParameter, msg);
        }

        void SetStaticParameter(object o)
        {
            StaticParameter msg = o as StaticParameter;
            try
            {
                comb_battery_type.SelectedIndex = msg.battery_type;
                comb_battery_serial.SelectedIndex = msg.battery_serial;
                txt_Battery_cap.Text = msg.Battery_cap.ToString();
                txt_ovc_min.Text = msg.ovc_min.ToString("0.00");
                txt_ovc_max.Text = msg.ovc_max.ToString("0.00");
                txt_imp_min.Text = msg.imp_min.ToString();
                txt_imp_max.Text = msg.imp_max.ToString();
                txt_r1_max.Text = msg.r1_max.ToString("0.0");
                txt_r1_min.Text = msg.r1_min.ToString("0.0");
                txt_r2_max.Text = msg.r2_max.ToString("0.0");
                txt_r2_min.Text = msg.r2_min.ToString("0.0");
                txt_vol_max.Text = msg.vol_max.ToString("0.00");
                txt_vol_min.Text = msg.vol_min.ToString("0.00");
                comb_id_type1.SelectedIndex = msg.id_type1;
                comb_id_type2.SelectedIndex = msg.id_type2;
                txt_short_time.Text = msg.short_time.ToString();
                comb_open_time.SelectedIndex = msg.open_time;
                txt_detV_Max.Text = msg.detV_Max.ToString("0.00");
                comb_reserve.SelectedIndex = msg.reserve;
            }
            catch (Exception ex)
            {
                MessageToolscs.Tools(ex.Message);
            }
        }

        private void TestSystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出系统?", "系统提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_savaSetup_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\XML\\ParameterConfiguration.xml";//"equipment1"
                savaSetup(com_PortMainBoard.Text.Trim(), com_baudRateMainBoard.Text.Trim(), comb_Databits.Text.Trim(), comb_Parity.Text.Trim(), comb_StopBits.Text.Trim(), "equipment1", path);
                savaSetup(com_ScanningGun.Text.Trim(), com_ScanningGun_baudRateMainBoard.Text.Trim(), com_ScanningGun_Databits.Text.Trim(), com_ScanningGun_Parity.Text.Trim(), com_ScanningGun_StopBits.Text.Trim(), "ScanningGun1", path);
            }
            catch (Exception ex)
            {
                MessageToolscs.Tools(ex.Message);
            }
        }

        void savaSetup(string com,string com_baudtate,string databits,string par,string stop,string Element,string path)
        { 
            
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(path))
            {
                try
                {
                    //创建类型声明节点    
                    XmlNode node = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                    doc.AppendChild(node);

                    //Add the new node to the document.

                    XmlElement root = doc.CreateElement("ParameterConfiguration");//创建根节点          
                    doc.AppendChild(root);

                    XmlElement root1 = doc.CreateElement("equipment1");//创建根节点          
                    root.AppendChild(root1);

                    CreateNode(doc, root1, "com", com);
                    CreateNode(doc, root1, "com_baudtate", com_baudtate);
                    CreateNode(doc, root1, "databits", databits);
                    CreateNode(doc, root1, "par", par);
                    CreateNode(doc, root1, "stop", stop);

                    XmlElement root2 = doc.CreateElement("equipment2");//创建根节点          
                    root.AppendChild(root2);

                    CreateNode(doc, root2, "com", com);
                    CreateNode(doc, root2, "com_baudtate", com_baudtate);
                    CreateNode(doc, root2, "databits", databits);
                    CreateNode(doc, root2, "par", par);
                    CreateNode(doc, root2, "stop", stop);

                    XmlElement root3 = doc.CreateElement("ScanningGun1");//创建根节点          
                    root.AppendChild(root3);

                    CreateNode(doc, root3, "com", com);
                    CreateNode(doc, root3, "com_baudtate", com_baudtate);
                    CreateNode(doc, root3, "databits", databits);
                    CreateNode(doc, root3, "par", par);
                    CreateNode(doc, root3, "stop", stop);

                    XmlElement root4 = doc.CreateElement("ScanningGun2");//创建根节点          
                    root.AppendChild(root4);

                    CreateNode(doc, root4, "com", com);
                    CreateNode(doc, root4, "com_baudtate", com_baudtate);
                    CreateNode(doc, root4, "databits", databits);
                    CreateNode(doc, root4, "par", par);
                    CreateNode(doc, root4, "stop", stop);

                    doc.Save(path);
                    MessageToolscs.Tools("保存成功！");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    //using (FileStream stream = File.OpenRead(Path))
                    //{
                    doc.Load(path);
                    XmlNodeList list = doc.SelectSingleNode("ParameterConfiguration").ChildNodes;//获得根节点user下的所有子节点
                    foreach (XmlNode item in list)
                    {
                        if (item.Name == Element)
                        {
                            foreach (XmlNode node in item.ChildNodes)
                            {
                                if (node.Name == "com")
                                    node.InnerText = com;
                                if (node.Name == "com_baudtate")
                                    node.InnerText = com_baudtate;
                                if (node.Name == "databits")
                                    node.InnerText = databits;
                                if (node.Name == "par")
                                    node.InnerText = par;
                                if (node.Name == "stop")
                                    node.InnerText = stop;
                            }
                        }
                    }


                    doc.Save(path);
                    MessageToolscs.Tools("保存成功！");
                    //     }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBoxX1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8&&e.KeyChar.ToString()!=".")
            {
                e.Handled = true;
            }
        }

        private void btn_read_Click(object sender, EventArgs e)
        {
            if (tsb != null)
            {
                List<string> list = new List<string>();
                list.Add("53");
                list.Add("53");
                tsb.ScanComPort(list);
            }
        }

        private void btn_change_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(byte.Parse("B1", System.Globalization.NumberStyles.AllowHexSpecifier).ToString());
            try
            {
                StaticParameter data = new StaticParameter
                {
                    battery_type = this.comb_battery_type.SelectedIndex,
                    battery_serial = this.comb_battery_serial.SelectedIndex,
                    Battery_cap = Convert.ToInt32(this.txt_Battery_cap.Text.Trim()),
                    vol_min = Convert.ToDouble(this.txt_vol_min.Text.Trim()) * 1000,
                    vol_max = Convert.ToDouble(this.txt_vol_max.Text.Trim()) * 1000,
                    imp_min = Convert.ToDouble(this.txt_imp_min.Text.Trim()) * 10,
                    imp_max = Convert.ToDouble(this.txt_imp_max.Text.Trim()) * 10,
                    ovc_min = Convert.ToDouble(this.txt_ovc_min.Text.Trim()) * 1000,
                    ovc_max = Convert.ToDouble(this.txt_ovc_max.Text.Trim()) * 1000,
                    r1_min = Convert.ToDouble(this.txt_r1_min.Text.Trim()) * 10,
                    r1_max = Convert.ToDouble(this.txt_r1_max.Text.Trim()) * 10,
                    r2_min = Convert.ToDouble(this.txt_r2_min.Text.Trim()) * 10,
                    r2_max = Convert.ToDouble(this.txt_r2_max.Text.Trim()) * 10,
                    id_type1 = this.comb_id_type1.SelectedIndex,
                    id_type2 = this.comb_id_type2.SelectedIndex,
                    short_time = Convert.ToInt32(this.txt_short_time.Text.Trim()),
                    open_time = this.comb_open_time.SelectedIndex,
                    detV_Max = Convert.ToDouble(this.txt_detV_Max.Text.Trim()) * 1000,
                    reserve = comb_reserve.SelectedIndex
                };

                tsb.DOWNLOAD(data);
            }
            catch (Exception ex)
            {
                MessageToolscs.Tools(ex.Message);
            }

        }

        private void 连接配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(UsersHelp.ServerConnection || UsersHelp.Admini))
            {
                MessageBox.Show("你没有修改连接配置的权限！");
                return;
            }

            connectionsSet cs = new connectionsSet();
            cs.ShowDialog();

        }

        private void TestSystem_Resize(object sender, EventArgs e)
        {
            groupBox4.Width = this.Width / 2;
            SetMessageInitialization1();
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewX1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.dataGridViewX1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void btn_OpenComport2_Click(object sender, EventArgs e)
        {
            if (com_PortMainBoard2.Text.Trim() == "")
            {
                MessageToolscs.Tools("端口不能为空！");
                return;
            }
            if (com_baudRateMainBoard2.Text.Trim() == "")
            {
                MessageToolscs.Tools("波特率不能为空！");
                return;
            }
            if (comb_Databits2.Text.Trim() == "")
            {
                MessageToolscs.Tools("数据位不能为空！");
                return;
            }
            if (comb_Parity2.Text.Trim() == "")
            {
                MessageToolscs.Tools("校验位不能为空！");
                return;
            }
            if (comb_StopBits2.Text.Trim() == "")
            {
                MessageToolscs.Tools("停止位不能为空！");
                return;
            }
            try
            {
                if (btn_OpenComport2.Text == "启动测试")
                {
                    btn_OpenComport2.Text = "停止测试";
                    btn_OpenComport2.Style = eDotNetBarStyle.VS2005;
                    btn_change2.Enabled = true;
                    btn_read2.Enabled = true;

                    tsb2 = new TestSystemComport(com_PortMainBoard2.Text.Trim(), com_baudRateMainBoard2.Text.Trim(), comb_Databits2.Text.Trim(), comb_Parity2.Text.Trim(), comb_StopBits2.Text.Trim(),"设备",0, this);
                    tsb2.SendEvent += tsb2_SendEvent;
                    tsb2.SendEvent1 += tsb2_SendEvent1;
                    com_PortMainBoardDQ2.Text = com_PortMainBoard2.Text.Trim();
                    //this.tabControl1.SelectedTab = tabControl1.Tabs["tabItem1"];
                }
                else
                {
                    btn_OpenComport2.Text = "启动测试";
                    btn_OpenComport2.Style = eDotNetBarStyle.StyleManagerControlled;
                    btn_change2.Enabled = false;
                    btn_read2.Enabled = false;

                    tsb2.CloseComport();
                    com_PortMainBoardDQ2.Text = "";
                }
            }
            catch (Exception ex)
            {
                btn_OpenComport2.Text = "启动测试";
                btn_OpenComport2.Style = eDotNetBarStyle.StyleManagerControlled;
                btn_change2.Enabled = false;
                btn_read2.Enabled = false;
                MessageToolscs.Tools(ex.Message);
                return;
            }
        }

        void tsb2_SendEvent1(StaticParameter msg)
        {
            My_cotext.Post(SetStaticParameter2, msg);
        }

        void tsb2_SendEvent(FinishedProductTesting msg)
        {
            My_cotext.Post(SetTexBOx2, msg);
        }
        void SetStaticParameter2(object o)
        {
            StaticParameter msg = o as StaticParameter;
            try
            {
                comb_battery_type2.SelectedIndex = msg.battery_type;
                comb_battery_serial2.SelectedIndex = msg.battery_serial;
                txt_Battery_cap2.Text = msg.Battery_cap.ToString();
                txt_ovc_min2.Text = msg.ovc_min.ToString("0.00");
                txt_ovc_max2.Text = msg.ovc_max.ToString("0.00");
                txt_imp_min2.Text = msg.imp_min.ToString();
                txt_imp_max2.Text = msg.imp_max.ToString();
                txt_r1_max2.Text = msg.r1_max.ToString("0.0");
                txt_r1_min2.Text = msg.r1_min.ToString("0.0");
                txt_r2_max2.Text = msg.r2_max.ToString("0.0");
                txt_r2_min2.Text = msg.r2_min.ToString("0.0");
                txt_vol_max2.Text = msg.vol_max.ToString("0.00");
                txt_vol_min2.Text = msg.vol_min.ToString("0.00");
                comb_id_type12.SelectedIndex = msg.id_type1;
                comb_id_type22.SelectedIndex = msg.id_type2;
                txt_short_time2.Text = msg.short_time.ToString();
                comb_open_time2.SelectedIndex = msg.open_time;
                txt_detV_Max2.Text = msg.detV_Max.ToString("0.00");
                comb_reserve2.SelectedIndex = msg.reserve;
            }
            catch (Exception ex)
            {
                MessageToolscs.Tools(ex.Message);
            }
        }

        private void btn_read2_Click(object sender, EventArgs e)
        {
            if (tsb2 != null)
            {
                List<string> list = new List<string>();
                list.Add("53");
                list.Add("53");
                tsb2.ScanComPort(list);
            }
        }

        private void btn_change2_Click(object sender, EventArgs e)
        {
            try
            {
                StaticParameter data = new StaticParameter
                {
                    battery_type = this.comb_battery_type2.SelectedIndex,
                    battery_serial = this.comb_battery_serial2.SelectedIndex,
                    Battery_cap = Convert.ToInt32(this.txt_Battery_cap2.Text.Trim()),
                    vol_min = Convert.ToDouble(this.txt_vol_min2.Text.Trim()) * 1000,
                    vol_max = Convert.ToDouble(this.txt_vol_max2.Text.Trim()) * 1000,
                    imp_min = Convert.ToDouble(this.txt_imp_min2.Text.Trim()) * 10,
                    imp_max = Convert.ToDouble(this.txt_imp_max2.Text.Trim()) * 10,
                    ovc_min = Convert.ToDouble(this.txt_ovc_min2.Text.Trim()) * 1000,
                    ovc_max = Convert.ToDouble(this.txt_ovc_max2.Text.Trim()) * 1000,
                    r1_min = Convert.ToDouble(this.txt_r1_min2.Text.Trim()) * 10,
                    r1_max = Convert.ToDouble(this.txt_r1_max2.Text.Trim()) * 10,
                    r2_min = Convert.ToDouble(this.txt_r2_min2.Text.Trim()) * 10,
                    r2_max = Convert.ToDouble(this.txt_r2_max2.Text.Trim()) * 10,
                    id_type1 = this.comb_id_type12.SelectedIndex,
                    id_type2 = this.comb_id_type22.SelectedIndex,
                    short_time = Convert.ToInt32(this.txt_short_time2.Text.Trim()),
                    open_time = this.comb_open_time2.SelectedIndex,
                    detV_Max = Convert.ToDouble(this.txt_detV_Max2.Text.Trim()) * 1000,
                    reserve = comb_reserve2.SelectedIndex
                };

                tsb2.DOWNLOAD(data);
            }
            catch (Exception ex)
            {
                MessageToolscs.Tools(ex.Message);
            }

        }

        private void btn_StartScanningGun_Click(object sender, EventArgs e)
        {
            if (com_ScanningGun.Text.Trim() == "")
            {
                MessageToolscs.Tools("端口不能为空！");
                return;
            }
            if (com_ScanningGun_baudRateMainBoard.Text.Trim() == "")
            {
                MessageToolscs.Tools("波特率不能为空！");
                return;
            }
            if (com_ScanningGun_Databits.Text.Trim() == "")
            {
                MessageToolscs.Tools("数据位不能为空！");
                return;
            }
            if (com_ScanningGun_Parity.Text.Trim() == "")
            {
                MessageToolscs.Tools("校验位不能为空！");
                return;
            }
            if (com_ScanningGun_StopBits.Text.Trim() == "")
            {
                MessageToolscs.Tools("停止位不能为空！");
                return;
            }
            if (txt_QRCodeCount.Text.Trim()=="")
            {
                MessageToolscs.Tools("二维码长度不能为空！");
                return;
            }
            try
            {

                if (btn_StartScanningGun.Text == "打开扫描枪")
                {
                    btn_StartScanningGun.Text = "关闭扫描枪";
                    btn_StartScanningGun.Style = eDotNetBarStyle.VS2005;

                    tsb3 = new TestSystemComport(com_ScanningGun.Text.Trim(), com_ScanningGun_baudRateMainBoard.Text.Trim(), com_ScanningGun_Databits.Text.Trim(), com_ScanningGun_Parity.Text.Trim(), com_ScanningGun_StopBits.Text.Trim(), "扫描枪", Convert.ToInt32(txt_QRCodeCount.Text.Trim()), this);
                    tsb3.SendEventString += tsb3_SendEventString;

                    com_ScanningGunDQ.Text = com_ScanningGun.Text.Trim();

                }
                else
                {
                    btn_StartScanningGun.Text = "打开扫描枪";
                    btn_StartScanningGun.Style = eDotNetBarStyle.StyleManagerControlled;
                    com_ScanningGunDQ.Text = "";

                    tsb3.CloseComport();
                }
            }
            catch(Exception ex)
            {
                btn_StartScanningGun.Text = "打开扫描枪";
                btn_StartScanningGun.Style = eDotNetBarStyle.StyleManagerControlled;
                com_ScanningGunDQ.Text = "";
                MessageToolscs.Tools(ex.Message);
            }
        }

        void tsb3_SendEventString(string msg)
        {
            My_cotext.Post(setGun1, msg);
        }

        void setGun1(object o)
        {
            if(txt_VoltageDifference.Text.Trim()==""||txt_TimeInterval.Text.Trim()=="")
            {
                MessageBox.Show("请先选择电池类型！");
                return;
            }
            string str = o as string;
            str = str.Replace("\n", string.Empty).Replace("\r", string.Empty);
            //初始化
            electricData = new BatteryTestSystem_ElectricCore();
            FinishedData = new BatteryTestSystem_FinishedProduct();
            try
            {
                if (ck_ElectricCore.Checked)
                {
                    electricData = ElectricBll.selectBatteryTestSystem_ElectricCoreBll(str, "selectBatteryTestSystem_ElectricCore")[0];
                    SetDGVparameter("Electric", 1);
                }
                else
                {
                    FinishedData = FinishedBll.selectBatteryTestSystem_FinishedProductBll(str, "selectBatteryTestSystem_FinishedProduct")[0];
                    SetDGVparameter("Finished", 1);
                }

                txt_QRCode.Text = str;
                SetMessageInitialization(1);
                Cec.SendComport("AT01a#TFRL3A\r\n");
                System.Threading.Thread.Sleep(50);
                Cec.SendComport("AT01a#TFGLD4\r\n");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void setGun2(object o)
        {
            if (txt_VoltageDifference2.Text.Trim() == "" || txt_TimeInterval2.Text.Trim() == "")
            {
                MessageBox.Show("请先选择电池类型！");
                return;
            }
            string str = o as string;

            str = str.Replace("\n", string.Empty).Replace("\r", string.Empty);
            try
            {
                if (ck_ElectricCore2.Checked)
                {
                    electricData2 = ElectricBll.selectBatteryTestSystem_ElectricCoreBll(str, "selectBatteryTestSystem_ElectricCore")[0];
                    SetDGVparameter("Electric", 2);
                }
                else
                {
                    FinishedData2 = FinishedBll.selectBatteryTestSystem_FinishedProductBll(str, "selectBatteryTestSystem_FinishedProduct")[0];
                    SetDGVparameter("Finished", 2);
                }
                txt_QRCode2.Text = str;
                SetMessageInitialization(2);
                Cec.SendComport("AT02b#TFRL0F\r\n");
                System.Threading.Thread.Sleep(50);
                Cec.SendComport("AT02b#TFGLE5\r\n");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SetDGVparameter(string type,int i)
        {
            if (type == "Electric")
            {
                if (i == 1)
                {
                    List<parameterBatteryType> list1 = new List<parameterBatteryType>();

                    list1.Add(new parameterBatteryType()
                    {
                        Number = "1",
                        OCV = "OCV1",
                        QRCode = electricData.QRCode,
                        BatteryType = electricData.BatteryType,
                        Kvlues = electricData.Kvalue1.ToString(),
                        OCR = "OCR1",
                        vol_open = electricData.vol_open1.ToString(),
                        imp = electricData.imp1.ToString(),
                        Time = electricData.Time1.Year == 1900 ? "" : electricData.Time1.ToString()
                    });
                    list1.Add(new parameterBatteryType()
                    {
                        Number = "2",
                        OCV = "OCV2",
                        QRCode = electricData.QRCode,
                        BatteryType = electricData.BatteryType,
                        Kvlues = electricData.Kvalue2.ToString(),
                        OCR = "OCR2",
                        vol_open = electricData.vol_open2.ToString(),
                        imp = electricData.imp2.ToString(),
                        Time = electricData.Time2.Year == 1900 ? "" : electricData.Time2.ToString()
                    });
                    list1.Add(new parameterBatteryType()
                    {
                        Number = "3",
                        OCV = "OCV3",
                        QRCode = electricData.QRCode,
                        BatteryType = electricData.BatteryType,
                        Kvlues = electricData.Kvalue3.ToString(),
                        OCR = "OCR3",
                        vol_open = electricData.vol_open3.ToString(),
                        imp = electricData.imp3.ToString(),
                        Time = electricData.Time3.Year == 1900 ? "" : electricData.Time3.ToString()
                    });

                    dataGridViewX2.DataSource = list1;
                }
                else
                {
                    List<parameterBatteryType> list2 = new List<parameterBatteryType>();

                    list2.Add(new parameterBatteryType()
                    {
                        Number = "1",
                        OCV = "OCV1",
                        QRCode = electricData2.QRCode,
                        BatteryType = electricData2.BatteryType,
                        Kvlues = electricData2.Kvalue1.ToString(),
                        OCR = "OCR1",
                        vol_open = electricData2.vol_open1.ToString(),
                        imp = electricData2.imp1.ToString(),
                        Time = electricData2.Time1.Year == 1900 ? "" : electricData2.Time1.ToString()
                    });
                    list2.Add(new parameterBatteryType()
                    {
                        Number = "2",
                        OCV = "OCV2",
                        QRCode = electricData2.QRCode,
                        BatteryType = electricData2.BatteryType,
                        Kvlues = electricData2.Kvalue2.ToString(),
                        OCR = "OCR2",
                        vol_open = electricData2.vol_open2.ToString(),
                        imp = electricData2.imp2.ToString(),
                        Time = electricData2.Time2.Year == 1900 ? "" : electricData2.Time2.ToString()
                    });
                    list2.Add(new parameterBatteryType()
                    {
                        Number = "3",
                        OCV = "OCV3",
                        QRCode = electricData2.QRCode,
                        BatteryType = electricData2.BatteryType,
                        Kvlues = electricData2.Kvalue3.ToString(),
                        OCR = "OCR3",
                        vol_open = electricData2.vol_open3.ToString(),
                        imp = electricData2.imp3.ToString(),
                        Time = electricData2.Time3.Year == 1900 ? "" : electricData2.Time3.ToString()
                    });

                    dataGridViewX3.DataSource = list2;
                }
            }
            else
            {
                if (i == 1)
                {
                    List<parameterBatteryType> list1 = new List<parameterBatteryType>();

                    list1.Add(new parameterBatteryType()
                    {
                        Number = "1",
                        OCV = "OCV1",
                        QRCode = FinishedData.QRCode,
                        BatteryType = FinishedData.BatteryType,
                        Kvlues = FinishedData.Kvalue1.ToString(),
                        OCR = "OCR1",
                        vol_open = FinishedData.vol_open1.ToString(),
                        imp = FinishedData.imp1.ToString(),
                        Time = FinishedData.Time1.Year == 1900 ? "" : FinishedData.Time1.ToString()
                    });
                    list1.Add(new parameterBatteryType()
                    {
                        Number = "2",
                        OCV = "OCV2",
                        QRCode = FinishedData.QRCode,
                        BatteryType = FinishedData.BatteryType,
                        Kvlues = FinishedData.Kvalue2.ToString(),
                        OCR = "OCR2",
                        vol_open = FinishedData.vol_open2.ToString(),
                        imp = FinishedData.imp2.ToString(),
                        Time = FinishedData.Time2.Year == 1900 ? "" : FinishedData.Time2.ToString()
                    });
                    list1.Add(new parameterBatteryType()
                    {
                        Number = "3",
                        OCV = "OCV3",
                        QRCode = FinishedData.QRCode,
                        BatteryType = FinishedData.BatteryType,
                        Kvlues = FinishedData.Kvalue3.ToString(),
                        OCR = "OCR3",
                        vol_open = FinishedData.vol_open3.ToString(),
                        imp = FinishedData.imp3.ToString(),
                        Time = FinishedData.Time3.Year == 1900 ? "" : FinishedData.Time3.ToString()
                    });

                    dataGridViewX2.DataSource = list1;
                }
                else
                {
                    List<parameterBatteryType> list2 = new List<parameterBatteryType>();

                    list2.Add(new parameterBatteryType()
                    {
                        Number = "1",
                        OCV = "OCV1",
                        QRCode = FinishedData2.QRCode,
                        BatteryType = FinishedData2.BatteryType,
                        Kvlues = FinishedData2.Kvalue1.ToString(),
                        OCR = "OCR1",
                        vol_open = FinishedData2.vol_open1.ToString(),
                        imp = FinishedData2.imp1.ToString(),
                        Time = FinishedData2.Time1.Year == 1900 ? "" : FinishedData2.Time1.ToString()
                    });
                    list2.Add(new parameterBatteryType()
                    {
                        Number = "2",
                        OCV = "OCV2",
                        QRCode = FinishedData2.QRCode,
                        BatteryType = FinishedData2.BatteryType,
                        Kvlues = FinishedData2.Kvalue2.ToString(),
                        OCR = "OCR2",
                        vol_open = FinishedData2.vol_open2.ToString(),
                        imp = FinishedData2.imp2.ToString(),
                        Time = FinishedData2.Time2.Year == 1900 ? "" : FinishedData2.Time2.ToString()
                    });
                    list2.Add(new parameterBatteryType()
                    {
                        Number = "3",
                        OCV = "OCV3",
                        QRCode = FinishedData2.QRCode,
                        BatteryType = FinishedData2.BatteryType,
                        Kvlues = FinishedData2.Kvalue3.ToString(),
                        OCR = "OCR3",
                        vol_open = FinishedData2.vol_open3.ToString(),
                        imp = FinishedData2.imp3.ToString(),
                        Time = FinishedData2.Time3.Year == 1900 ? "" : FinishedData2.Time3.ToString()
                    });

                    dataGridViewX3.DataSource = list2;
                }
            }
        }

        private void 用户登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersForm uf = new UsersForm(this);
            uf.ShowDialog();
        }

                /// <summary>      
        /// 创建节点      
        /// </summary>      
        /// <param name="xmldoc">xml文档 </param>     
        /// <param name="parentnode">父节点  </param>    
        /// <param name="name">节点名</param>      
        /// <param name="value">节点值 </param>     
        ///     
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
        }

        private void btn_saveStup2_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\XML\\ParameterConfiguration.xml";//"equipment1"
                savaSetup(com_PortMainBoard2.Text.Trim(), com_baudRateMainBoard2.Text.Trim(), comb_Databits2.Text.Trim(), comb_Parity2.Text.Trim(), comb_StopBits2.Text.Trim(), "equipment2", path);
                savaSetup(com_ScanningGun2.Text.Trim(), com_ScanningGun_baudRateMainBoard2.Text.Trim(), com_ScanningGun_Databits2.Text.Trim(), com_ScanningGun_Parity2.Text.Trim(), com_ScanningGun_StopBits2.Text.Trim(), "ScanningGun2", path);
            }
            catch (Exception ex)
            {
                MessageToolscs.Tools(ex.Message);
            }
        }

        private void 刷新端口ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetPortName();//获得当前计算机的端口号
        }

        private void 电池类型管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(UsersHelp.ChangingEquipment || UsersHelp.Admini))
            {
                MessageBox.Show("你没有权限!");
                return;
            }

            BatteryTypeManagement btm = new BatteryTypeManagement(this);
            btm.ShowDialog();
        }

        private void btn_StartScanningGun2_Click(object sender, EventArgs e)
        {
            if (com_ScanningGun2.Text.Trim() == "")
            {
                MessageToolscs.Tools("端口不能为空！");
                return;
            }
            if (com_ScanningGun_baudRateMainBoard2.Text.Trim() == "")
            {
                MessageToolscs.Tools("波特率不能为空！");
                return;
            }
            if (com_ScanningGun_Databits2.Text.Trim() == "")
            {
                MessageToolscs.Tools("数据位不能为空！");
                return;
            }
            if (com_ScanningGun_Parity2.Text.Trim() == "")
            {
                MessageToolscs.Tools("校验位不能为空！");
                return;
            }
            if (com_ScanningGun_StopBits2.Text.Trim() == "")
            {
                MessageToolscs.Tools("停止位不能为空！");
                return;
            }
            if (txt_QRCodeCount2.Text.Trim() == "")
            {
                MessageToolscs.Tools("二维码长度不能为空！");
                return;
            }
            try
            {

                if (btn_StartScanningGun2.Text == "打开扫描枪")
                {
                    btn_StartScanningGun2.Text = "关闭扫描枪";
                    btn_StartScanningGun2.Style = eDotNetBarStyle.VS2005;

                    tsb4 = new TestSystemComport(com_ScanningGun2.Text.Trim(), com_ScanningGun_baudRateMainBoard2.Text.Trim(), com_ScanningGun_Databits2.Text.Trim(), com_ScanningGun_Parity2.Text.Trim(), com_ScanningGun_StopBits2.Text.Trim(), "扫描枪", Convert.ToInt32(txt_QRCodeCount2.Text.Trim()), this);
                    tsb4.SendEventString += tsb4_SendEventString;

                    com_ScanningGunDQ2.Text = com_ScanningGun2.Text.Trim();

                }
                else
                {
                    btn_StartScanningGun2.Text = "打开扫描枪";
                    btn_StartScanningGun2.Style = eDotNetBarStyle.StyleManagerControlled;
                    com_ScanningGunDQ2.Text = "";

                    tsb4.CloseComport();
                }
            }
            catch (Exception ex)
            {
                btn_StartScanningGun2.Text = "打开扫描枪";
                btn_StartScanningGun2.Style = eDotNetBarStyle.StyleManagerControlled;
                com_ScanningGunDQ2.Text = "";
                MessageToolscs.Tools(ex.Message);
            }
        }

        void tsb4_SendEventString(string msg)
        {
            My_cotext.Post(setGun2, msg);
        }


        public void SetBatteryTypeList(List<BatteryTestSystem_BatteryType> data)
        {
            this.BatteryTypeList = data;
            cbe_ProductionClass.DataSource = MainBll.GetBatteryTypeStr(BatteryTypeList);
            cbe_ProductionClass2.DataSource = MainBll.GetBatteryTypeStr(BatteryTypeList);
        }

        private void ck_ElectricCore_CheckedChanged(object sender, EventArgs e)
        {
            return;
        }

        private void cbe_ProductionClass_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetBatteryTypeBox(MainBll.SelectBatteryType(BatteryTypeList, cbe_ProductionClass.SelectedItem.ToString()), 1);

        }
        /// <summary>
        /// 赋值电池类型输入框
        /// </summary>
        /// <param name="data">电池类型实体表数据</param>
        /// <param name="i">设备代号</param>
        void SetBatteryTypeBox(BatteryTestSystem_BatteryType data,int i)
        { 
            if(i==1)
            {
                txt_VoltageDifference.Text =  data.VoltageDifference.ToString();
                txt_TimeInterval.Text =  data.TimeInterval.ToString();
                ck_ElectricCore.Checked =  data.ElectricCore;
                ck_FinishedProduct.Checked =  data.FinishedProduct;
                BatteryType1 = data;
            }
            else
            {
                txt_VoltageDifference2.Text =  data.VoltageDifference.ToString();
                txt_TimeInterval2.Text =  data.TimeInterval.ToString();
                ck_ElectricCore2.Checked =  data.ElectricCore;
                ck_FinishedProduct2.Checked =  data.FinishedProduct;
                BatteryType2 = data;
            }
            SetDatagridviewShow();
            SetDataGridViewBatteryType2();
            SetDataGridViewBatteryType3();
        }

        void SetDataGridViewBatteryType2()
        {
            dataGridViewX2.Columns.Clear();

            DataGridViewTextBoxColumn newcol7 = new DataGridViewTextBoxColumn();
            newcol7.HeaderText = "序号";
            newcol7.Name = "Number";
            newcol7.DataPropertyName = "Number";
            newcol7.Width = 50;
            newcol7.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol7.Tag = 0;
            this.dataGridViewX2.Columns.Insert(0, newcol7);

            DataGridViewTextBoxColumn newcol34 = new DataGridViewTextBoxColumn();
            newcol34.HeaderText = "电池类型";
            newcol34.Name = "BatteryType";
            newcol34.DataPropertyName = "BatteryType";
            newcol34.Width = 100;
            newcol34.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol34.Tag = 1;
            this.dataGridViewX2.Columns.Insert(1, newcol34);

            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "二维码/条码";
            newcol.Name = "QRCode";
            newcol.DataPropertyName = "QRCode";
            newcol.Width = 120;
            newcol.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol.Tag = 2;
            this.dataGridViewX2.Columns.Insert(2, newcol);

            DataGridViewTextBoxColumn newcol9 = new DataGridViewTextBoxColumn();
            newcol9.HeaderText = "OCV";
            newcol9.Name = "OCV";
            newcol9.DataPropertyName = "OCV";
            newcol9.Width = 50;
            newcol9.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol9.Tag = 3;
            this.dataGridViewX2.Columns.Insert(3, newcol9);

            DataGridViewTextBoxColumn newcol10 = new DataGridViewTextBoxColumn();
            newcol10.HeaderText = "开路电压";
            newcol10.Name = "vol_open";
            newcol10.DataPropertyName = "vol_open";
            newcol10.Width = 100;
            newcol10.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol10.Tag = 4;
            //newcol10.Visible = false;
            this.dataGridViewX2.Columns.Insert(4, newcol10);

            DataGridViewTextBoxColumn newcol3 = new DataGridViewTextBoxColumn();
            newcol3.HeaderText = "OCR";
            newcol3.Name = "OCR";
            newcol3.DataPropertyName = "OCR";
            newcol3.Width = 50;
            newcol3.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol3.Tag = 5;
            this.dataGridViewX2.Columns.Insert(5, newcol3);

            DataGridViewTextBoxColumn newcol11 = new DataGridViewTextBoxColumn();
            newcol11.HeaderText = "内阻";
            newcol11.Name = "imp";
            newcol11.DataPropertyName = "imp";
            newcol11.Width = 100;
            newcol11.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol11.Tag = 6;
            //newcol10.Visible = false;
            this.dataGridViewX2.Columns.Insert(6, newcol11);

            DataGridViewTextBoxColumn newco6 = new DataGridViewTextBoxColumn();
            newco6.HeaderText = "K值";
            newco6.Name = "Kvlues";
            newco6.DataPropertyName = "Kvlues";
            newco6.Width = 100;
            newco6.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco6.Tag = 7;
            this.dataGridViewX2.Columns.Insert(7, newco6);

            DataGridViewTextBoxColumn newcol16 = new DataGridViewTextBoxColumn();
            newcol16.HeaderText = "测试时间";
            newcol16.Name = "Time";
            newcol16.DataPropertyName = "Time";
            newcol16.Width = 200;
            newcol16.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol16.Tag = 8;
            newcol16.Visible = true;
            this.dataGridViewX2.Columns.Insert(8, newcol16);

            DataGridViewTextBoxColumn newcol5 = new DataGridViewTextBoxColumn();
            newcol5.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewX2.Columns.Insert(9, newcol5);
        }
        void SetDataGridViewBatteryType3()
        {
            dataGridViewX3.Columns.Clear();

            DataGridViewTextBoxColumn newcol7 = new DataGridViewTextBoxColumn();
            newcol7.HeaderText = "序号";
            newcol7.Name = "Number";
            newcol7.DataPropertyName = "Number";
            newcol7.Width = 50;
            newcol7.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol7.Tag = 0;
            this.dataGridViewX3.Columns.Insert(0, newcol7);

            DataGridViewTextBoxColumn newcol34 = new DataGridViewTextBoxColumn();
            newcol34.HeaderText = "电池类型";
            newcol34.Name = "BatteryType";
            newcol34.DataPropertyName = "BatteryType";
            newcol34.Width = 100;
            newcol34.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol34.Tag = 1;
            this.dataGridViewX3.Columns.Insert(1, newcol34);

            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "二维码/条码";
            newcol.Name = "QRCode";
            newcol.DataPropertyName = "QRCode";
            newcol.Width = 120;
            newcol.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol.Tag = 2;
            this.dataGridViewX3.Columns.Insert(2, newcol);

            DataGridViewTextBoxColumn newcol9 = new DataGridViewTextBoxColumn();
            newcol9.HeaderText = "OCV";
            newcol9.Name = "OCV";
            newcol9.DataPropertyName = "OCV";
            newcol9.Width = 50;
            newcol9.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol9.Tag = 3;
            this.dataGridViewX3.Columns.Insert(3, newcol9);

            DataGridViewTextBoxColumn newcol10 = new DataGridViewTextBoxColumn();
            newcol10.HeaderText = "开路电压";
            newcol10.Name = "vol_open";
            newcol10.DataPropertyName = "vol_open";
            newcol10.Width = 100;
            newcol10.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol10.Tag = 4;
            //newcol10.Visible = false;
            this.dataGridViewX3.Columns.Insert(4, newcol10);

            DataGridViewTextBoxColumn newcol3 = new DataGridViewTextBoxColumn();
            newcol3.HeaderText = "OCR";
            newcol3.Name = "OCR";
            newcol3.DataPropertyName = "OCR";
            newcol3.Width = 50;
            newcol3.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol3.Tag = 5;
            this.dataGridViewX3.Columns.Insert(5, newcol3);

            DataGridViewTextBoxColumn newcol11 = new DataGridViewTextBoxColumn();
            newcol11.HeaderText = "内阻";
            newcol11.Name = "imp";
            newcol11.DataPropertyName = "imp";
            newcol11.Width = 100;
            newcol11.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol11.Tag = 6;
            //newcol10.Visible = false;
            this.dataGridViewX3.Columns.Insert(6, newcol11);

            DataGridViewTextBoxColumn newco6 = new DataGridViewTextBoxColumn();
            newco6.HeaderText = "K值";
            newco6.Name = "Kvlues";
            newco6.DataPropertyName = "Kvlues";
            newco6.Width = 100;
            newco6.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco6.Tag = 7;
            this.dataGridViewX3.Columns.Insert(7, newco6);

            DataGridViewTextBoxColumn newcol16 = new DataGridViewTextBoxColumn();
            newcol16.HeaderText = "测试时间";
            newcol16.Name = "Time";
            newcol16.DataPropertyName = "Time";
            newcol16.Width = 200;
            newcol16.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol16.Tag = 8;
            newcol16.Visible = true;
            this.dataGridViewX3.Columns.Insert(8, newcol16);

            DataGridViewTextBoxColumn newcol5 = new DataGridViewTextBoxColumn();
            newcol5.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewX3.Columns.Insert(9, newcol5);
        }

        private void cbe_ProductionClass2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SetBatteryTypeBox(MainBll.SelectBatteryType(BatteryTypeList, cbe_ProductionClass2.SelectedItem.ToString()), 2);
        }

        private void 用户注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelItem2.Text = "";

            UsersHelp.Cancellation();
        }

        private void 系统ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (UsersHelp.Login)
            {
                用户注销ToolStripMenuItem.Enabled = true;
                用户登录ToolStripMenuItem.Text = "切换用户";
            }
            else
            {
                用户注销ToolStripMenuItem.Enabled = false;
                用户登录ToolStripMenuItem.Text = "用户登录";
            }
        }

        private void 控制设备连接ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(UsersHelp.ChangingEquipment || UsersHelp.Admini))
            {
                MessageBox.Show("你没有修改设备参数的权限!");
                return;
            }

            ControlEquipment ce = new ControlEquipment(this);
            ce.ShowDialog();
        }

        public void StartControlEquipment(string com, string com_baudtate, string databits, string par, string stop)
        {
            Cec = new ControlEquipmentComport(com, com_baudtate, databits, par, stop,this);
            Cec.SendEventStringX += Cec_SendEventStringX;
        }

        void Cec_SendEventStringX(string msg)
        {
            if(msg!="")
            {

            }
        }

        private void checkBoxItem1_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
        {
            MainBll.Cover = checkBoxItem1.Checked;
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!UsersHelp.Login)
            {
                UsersForm uf = new UsersForm(this);
                uf.ShowDialog();
                return;
            }

            UserManagement Um = new UserManagement();
            Um.ShowDialog();
        }

        public void SetlabelItem2(string str)
        {
            labelItem2.Text = str;
        }

        private void tabItem2_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedTabChanging(object sender, TabStripTabChangingEventArgs e)
        {
            if (e.NewTab.Name == "tabItem2" || e.NewTab.Name == "tabItem2")
            {
                if (!(UsersHelp.ChangingEquipment || UsersHelp.Admini))
                {
                    MessageBox.Show("你没有修改设备参数的权限!");
                    e.Cancel = true;
                }
            }
        }

        BatteryTestSystem_UnidentitiesTest GetBatteryTestSystem_UnidentitiesTest(TestMessage data, bool electricCoreOrFinishedProduct, bool bl)
        {
            try
            {
                BatteryTestSystem_UnidentitiesTest Rdata = new BatteryTestSystem_UnidentitiesTest
                {
                    BatteryType = data.BatteryType,
                    IDimp1 = electricCoreOrFinishedProduct ? Convert.ToDouble(data.r2) : 0,
                    imp1 = Convert.ToDouble(data.imp),
                    Kvalue1 = Convert.ToDouble(data.Kvlues),
                    vol_open1 = Convert.ToDouble(data.vol_open),
                    vol_load1 = Convert.ToDouble(data.vol_load),
                    ovc1 = electricCoreOrFinishedProduct ? Convert.ToDouble(data.ovc) : 0,
                    TestFrequency = Convert.ToInt32(data.TestFrequency),
                    ElectricCoreOrFinishedProduct = electricCoreOrFinishedProduct,
                    ErorrText = data.ERR_FLAG,
                    Result = bl
                };

                return Rdata;
            }
            catch
            {
                throw;
            }
        }

        private void 清空二维码条码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_QRCode.Text = "";
        }

        private void 二清空二维码条码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txt_QRCode2.Text = "";
        }
    }
}
