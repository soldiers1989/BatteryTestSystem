using BatteryTestSystem_Bll;
using BatteryTestSystem_Comm;
using BatteryTestSystem_Model;
using BatteryTestSystem_UI.InformationSystems.InformationSystemsClass;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 电池测试系统.InformationSystems;
using 电池测试系统.TestSystems;
using System.Data;

namespace 电池测试系统
{
    public partial class InformationSystem : Office2007Form
    {

        SynchronizationContext My_cotext = null;

        MainForm MF;

        BatteryTestSystem_DayAnalysisBll BatteryBll = new BatteryTestSystem_DayAnalysisBll();

        private Dictionary<string, string> tablItemDit = new Dictionary<string, string>();

        private System.Timers.Timer timer = new System.Timers.Timer();

        DataTable Dt = new DataTable();

        public InformationSystem(MainForm mf)
        {
            InitializeComponent();
            this.MF = mf;
        }


        /// <summary>
        /// 多文档实现
        /// </summary>
        /// <param name="caption">窗体名称</param>
        /// <param name="formType">窗体类型</param>
        public void SetMdiForm(string caption, Type formType)
        {
            bool IsOpened = false;
            //
            foreach (SuperTabItem tabitem in superTabControl1.Tabs)
            {
                if (tabitem.Name == caption)
                {
                    superTabControl1.SelectedTab = tabitem;
                    IsOpened = true;
                    break;
                }
            }
            //
            if (!IsOpened)
            {
                Office2007Form form = ChildWinManagement.LoadMdiForm(this, formType)
                    as Office2007Form;

                SuperTabItem tabitem = superTabControl1.CreateTab(caption);
                tabitem.Name = caption;
                tabitem.Text = caption;

                form.FormBorderStyle = FormBorderStyle.None;
                form.TopLevel = false;
                form.Visible = true;
                form.Dock = DockStyle.Fill;

                tabitem.AttachedControl.Controls.Add(form);

                superTabControl1.SelectedTab = tabitem;

                if (!tablItemDit.ContainsKey(tabitem.Text))
                {
                    tablItemDit.Add(tabitem.Text, form.Name);
                }

            }
        }

        private void superTabControl1_TabItemClose(object sender, SuperTabStripTabItemCloseEventArgs e)
        {
            try
            {
                string selectedTab = e.Tab.Text;//获取当前TabItem的显示文本

                string controlName = null;

                tablItemDit.TryGetValue(selectedTab, out controlName);//获取当前TabItem中内嵌的Form的Name属性值

                Form frm = this.superTabControl1.Controls.Find(controlName, true)[0] as Form;//获取内嵌的Form对象

                frm.Close(); //调用form的close事件，即触发了内嵌窗体的关闭事件
            }
            catch { }
        }

        private void InformationSystem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否退出系统？", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InformationSystem_Load(object sender, EventArgs e)
        {
            MF.Visible = false;//隐藏主窗体
            this.labelItem2.Text = UsersHelp.UserName;
            My_cotext = SynchronizationContext.Current;
            SetDatagridviewShow(dataGridViewX1, false);
            SetDatagridviewShow(dataGridViewX2, true);
            timer.Elapsed += timer_Elapsed;
            timer.Interval = 300000;
            timer.Enabled = true;
            InitializationDataTable();
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            GetStatistics(dt);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DateTime dt=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,0,0,0);
            GetStatistics(dt);
        }

        private void InformationSystem_FormClosed(object sender, FormClosedEventArgs e)
        {
            MF.Close();
        }

        private void 连接配置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(!(UsersHelp.ServerConnection||UsersHelp.Admini))
            {
                MessageBox.Show("你没有修改连接配置的权限！");
                return;
            }

            connectionsSet cs = new connectionsSet();
            cs.ShowDialog();
        }

        private void 用户登录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersForm uf = new UsersForm(this);
            uf.ShowDialog();
        }

        private void btn_FinancialInputERP_Click(object sender, EventArgs e)
        {
            DayAnalysis da = new DayAnalysis();
            da.MdiParent = this;
            SetMdiForm(da.Text, typeof(DayAnalysis));
        }

        private void InformationSystem_Resize(object sender, EventArgs e)
        {
            groupBox1.Width = (this.Width-groupBox2.Width) / 2;
        }

        void SetDatagridviewShow(DataGridViewX datagridviewx1,bool bl)
        {
            datagridviewx1.Columns.Clear();

            DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
            newcol.HeaderText = "二维码/条码";
            newcol.Name = "QRCode";
            newcol.DataPropertyName = "QRCode";
            newcol.Width = 120;
            newcol.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol.Tag = 0;
            datagridviewx1.Columns.Insert(0, newcol);

            DataGridViewTextBoxColumn newcol34 = new DataGridViewTextBoxColumn();
            newcol34.HeaderText = "电池类型";
            newcol34.Name = "BatteryType";
            newcol34.DataPropertyName = "BatteryType";
            newcol34.Width = 100;
            newcol34.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol34.Tag = 1;
            datagridviewx1.Columns.Insert(1, newcol34);

            DataGridViewTextBoxColumn newcol10 = new DataGridViewTextBoxColumn();
            newcol10.HeaderText = "开路电压";
            newcol10.Name = "vol_open1";
            newcol10.DataPropertyName = "vol_open1";
            newcol10.Width = 100;
            newcol10.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol10.Tag = 2;
            newcol10.Visible = true;
            datagridviewx1.Columns.Insert(2, newcol10);

            DataGridViewTextBoxColumn newco5 = new DataGridViewTextBoxColumn();
            newco5.HeaderText = "测试时间";
            newco5.Name = "Time1";
            newco5.DataPropertyName = "Time1";
            newco5.Width = 200;
            newco5.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco5.Tag = 3;
            datagridviewx1.Columns.Insert(3, newco5);

            DataGridViewTextBoxColumn newcol11 = new DataGridViewTextBoxColumn();
            newcol11.HeaderText = "内阻";
            newcol11.Name = "imp1";
            newcol11.DataPropertyName = "imp1";
            newcol11.Width = 100;
            newcol11.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol11.Tag = 4;
            datagridviewx1.Columns.Insert(4, newcol11);

            DataGridViewTextBoxColumn newcol16 = new DataGridViewTextBoxColumn();
            newcol16.HeaderText = "过流保护值";
            newcol16.Name = "ovc1";
            newcol16.DataPropertyName = "ovc1";
            newcol16.Width = 100;
            newcol16.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol16.Tag = 5;
            newcol16.Visible = bl;
            datagridviewx1.Columns.Insert(5, newcol16);

            DataGridViewTextBoxColumn newcol2 = new DataGridViewTextBoxColumn();
            newcol2.HeaderText = "电阻1阻值";
            newcol2.Name = "r1";
            newcol2.DataPropertyName = "r1";
            newcol2.Width = 100;
            newcol2.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol2.Tag = 6;
            newcol2.Visible = false;
            datagridviewx1.Columns.Insert(6, newcol2);

            DataGridViewTextBoxColumn newcol6 = new DataGridViewTextBoxColumn();
            newcol6.HeaderText = "ID电阻";
            newcol6.Name = "IDimp1";
            newcol6.DataPropertyName = "IDimp1";
            newcol6.Width = 100;
            newcol6.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol6.Tag = 7;
            newcol6.Visible = bl;
            datagridviewx1.Columns.Insert(7, newcol6);

            DataGridViewTextBoxColumn newcol3 = new DataGridViewTextBoxColumn();
            newcol3.HeaderText = "测试次数";
            newcol3.Name = "TestFrequency";
            newcol3.DataPropertyName = "TestFrequency";
            newcol3.Width = 80;
            newcol3.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol3.Tag = 8;
            datagridviewx1.Columns.Insert(8, newcol3);

            DataGridViewTextBoxColumn newco6 = new DataGridViewTextBoxColumn();
            newco6.HeaderText = "K值";
            newco6.Name = "Kvalue1";
            newco6.DataPropertyName = "Kvalue1";
            newco6.Width = 100;
            newco6.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco6.Tag = 9;
            datagridviewx1.Columns.Insert(9, newco6);

            DataGridViewTextBoxColumn newcol4 = new DataGridViewTextBoxColumn();
            newcol4.HeaderText = "测试结果";
            newcol4.Name = "ErorrText";
            newcol4.DataPropertyName = "ErorrText";
            newcol4.Width = 200;
            newcol4.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol4.Tag = 10;
            datagridviewx1.Columns.Insert(10, newcol4);

            DataGridViewTextBoxColumn newcol1 = new DataGridViewTextBoxColumn();
            newcol1.HeaderText = "负载电压";
            newcol1.Name = "vol_load1";
            newcol1.DataPropertyName = "vol_load1";
            newcol1.Width = 100;
            newcol1.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol1.Tag = 11;
            datagridviewx1.Columns.Insert(11, newcol1);

            DataGridViewTextBoxColumn newco25 = new DataGridViewTextBoxColumn();
            newco25.HeaderText = "Result";
            newco25.Name = "Result";
            newco25.DataPropertyName = "Result";
            newco25.Width = 50;
            newco25.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco25.Tag = 12;
            newco25.Visible = false;
            datagridviewx1.Columns.Insert(12, newco25);

            DataGridViewTextBoxColumn newco26 = new DataGridViewTextBoxColumn();
            newco26.HeaderText = "是否成品";
            newco26.Name = "ElectricCoreOrFinishedProduct";
            newco26.DataPropertyName = "ElectricCoreOrFinishedProduct";
            newco26.Width = 50;
            newco26.SortMode = DataGridViewColumnSortMode.NotSortable;
            newco26.Tag = 13;
            newco26.Visible = false;
            datagridviewx1.Columns.Insert(13, newco26);

            DataGridViewTextBoxColumn newcol5 = new DataGridViewTextBoxColumn();
            newcol5.SortMode = DataGridViewColumnSortMode.NotSortable;
            newcol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            datagridviewx1.Columns.Insert(14, newcol5);
        }

        void GetStatistics(DateTime now)
        {
            try
            {
                Dt.Rows.Clear();

                My_cotext.Post(SetStatistics, BatteryBll.selectBatteryTestSystem_DayAnalysisStatisticsBll(now, "selectBatteryTestSystem_DayAnalysisStatistics"));//统计
                My_cotext.Post(SetFinishedProduct, BatteryBll.selectBatteryTestSystem_DayAnalysisStatisticsBll(now, "selectBatteryTestSystem_DayAnalysisFinishedProduct"));//成品
                My_cotext.Post(SetElectricCore, BatteryBll.selectBatteryTestSystem_DayAnalysisStatisticsBll(now, "selectBatteryTestSystem_DayAnalysisElectricCore"));//电芯
                My_cotext.Post(SetDataGridView2, BatteryBll.selectBatteryTestSystem_DayAnalysisFinishedProduct1Bll(now, "selectBatteryTestSystem_DayAnalysisFinishedProduct1"));//成品
                My_cotext.Post(SetDataGridView, BatteryBll.selectBatteryTestSystem_DayAnalysisFinishedProduct1Bll(now, "selectBatteryTestSystem_DayAnalysisElectricCore1"));//电芯
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SetStatistics(object o)
        {
            try
            {
                Statistics_DayAnalysis data = o as Statistics_DayAnalysis;

                this.txt_StatisticsNember.Text = data.Number.ToString();
                this.txt_StatisticsMaxvol_open1.Text = data.Maxvol_open1.ToString();
                this.txt_StatisticsMinvol_open1.Text = data.Minvol_open1.ToString();
                this.txt_StatisticsAvgvol_open1.Text = data.Avgvol_open1.ToString("0.000");
                this.txt_StatisticsMaximp1.Text = data.Maximp1.ToString();
                this.txt_StatisticsMinimp1.Text = data.Minimp1.ToString();
                this.txt_StatisticsAvgimp1.Text = data.Avgimp1.ToString("0.000");
                this.txt_StatisticsDirectRate.Text = (data.DirectRate * 100).ToString() + "%";

                //3.通过行框架创建并赋值
                DataRow dr = Dt.NewRow();
                dr["电池规格"] = "统计";//Add里面参数的数据顺序要和dt中的列的顺序对应
                dr["总数"] = data.Number;
                dr["电压最小值"] = data.Minvol_open1;
                dr["电压最大值"] = data.Maxvol_open1;
                dr["电压平均值"] = data.Avgvol_open1;
                dr["内阻最小值"] = data.Minimp1;
                dr["内阻最大值"] = data.Maximp1;
                dr["内阻平均值"] = data.Avgimp1;
                dr["良率"] = (data.DirectRate * 100).ToString() + "%";

                Dt.Rows.Add(dr);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SetElectricCore(object o)
        {
            try
            {
                Statistics_DayAnalysis data = o as Statistics_DayAnalysis;

                this.txt_ElectricCoreNember.Text = data.Number.ToString();
                this.txt_ElectricCoreMaxvol_open1.Text = data.Maxvol_open1.ToString();
                this.txt_ElectricCoreMinvol_open1.Text = data.Minvol_open1.ToString();
                this.txt_ElectricCoreAvgvol_open1.Text = data.Avgvol_open1.ToString("0.000");
                this.txt_ElectricCoreMaximp1.Text = data.Maximp1.ToString();
                this.txt_ElectricCoreMinimp1.Text = data.Minimp1.ToString();
                this.txt_ElectricCoreAvgimp1.Text = data.Avgimp1.ToString("0.000");
                this.txt_ElectricCoreDirectRate.Text = (data.DirectRate * 100).ToString() + "%";

                //3.通过行框架创建并赋值
                DataRow dr = Dt.NewRow();
                dr["电池规格"] = "电芯";//Add里面参数的数据顺序要和dt中的列的顺序对应
                dr["总数"] = data.Number;
                dr["电压最小值"] = data.Minvol_open1;
                dr["电压最大值"] = data.Maxvol_open1;
                dr["电压平均值"] = data.Avgvol_open1;
                dr["内阻最小值"] = data.Minimp1;
                dr["内阻最大值"] = data.Maximp1;
                dr["内阻平均值"] = data.Avgimp1;
                dr["良率"] = (data.DirectRate * 100).ToString() + "%";

                Dt.Rows.Add(dr);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SetFinishedProduct(object o)
        {
            try
            {
                Statistics_DayAnalysis data = o as Statistics_DayAnalysis;

                this.txt_FinishedProductNember.Text = data.Number.ToString();
                this.txt_FinishedProductMaxvol_open1.Text = data.Maxvol_open1.ToString();
                this.txt_FinishedProductMinvol_open1.Text = data.Minvol_open1.ToString();
                this.txt_FinishedProductAvgvol_open1.Text = data.Avgvol_open1.ToString("0.000");
                this.txt_FinishedProductMaximp1.Text = data.Maximp1.ToString();
                this.txt_FinishedProductMinimp1.Text = data.Minimp1.ToString();
                this.txt_FinishedProductAvgimp1.Text = data.Avgimp1.ToString("0.000");
                this.txt_FinishedProductDirectRate.Text = (data.DirectRate * 100).ToString() + "%";

                //3.通过行框架创建并赋值
                DataRow dr = Dt.NewRow();
                dr["电池规格"] = "成品";//Add里面参数的数据顺序要和dt中的列的顺序对应
                dr["总数"] = data.Number;
                dr["电压最小值"] = data.Minvol_open1;
                dr["电压最大值"] = data.Maxvol_open1;
                dr["电压平均值"] = data.Avgvol_open1;
                dr["内阻最小值"] = data.Minimp1;
                dr["内阻最大值"] = data.Maximp1;
                dr["内阻平均值"] = data.Avgimp1;
                dr["良率"] = (data.DirectRate * 100).ToString() + "%";

                Dt.Rows.Add(dr);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.dataGridViewX1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        private void dataGridViewX2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.dataGridViewX2.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        void SetDataGridView(object o)
        {
            List<BatteryTestSystem_DayAnalysis> datalist = o as List<BatteryTestSystem_DayAnalysis>;

            dataGridViewX1.DataSource = datalist;
        }

        void SetDataGridView2(object o)
        {
            List<BatteryTestSystem_DayAnalysis> datalist = o as List<BatteryTestSystem_DayAnalysis>;

            dataGridViewX2.DataSource = datalist;
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            GetStatistics(dt);
        }

        private void btn_IncomingExamine_Click(object sender, EventArgs e)
        {
            if(!(UsersHelp.DataDelete||UsersHelp.Admini))
            {
                MessageBox.Show("你没有删除数据的权限!");
                return;
            }

            try
            {
                dataManagement da = new dataManagement();
                da.MdiParent = this;
                SetMdiForm(da.Text, typeof(dataManagement));
            }
            catch { }
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewX1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void dataGridViewX2_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewX2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!UsersHelp.Login)
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

        private void 导出数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(ExcelHelperForCs.ExportToExcel(Dt)!=null)
                MessageBox.Show("导出完成");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitializationDataTable()
        {
            //3.通过列架构添加列
            Dt.Columns.Add("电池规格", typeof(String));
            Dt.Columns.Add("总数", typeof(int));
            Dt.Columns.Add("电压最小值", typeof(double));
            Dt.Columns.Add("电压最大值", typeof(double));
            Dt.Columns.Add("电压平均值", typeof(double));
            Dt.Columns.Add("内阻最小值", typeof(double));
            Dt.Columns.Add("内阻最大值", typeof(double));
            Dt.Columns.Add("内阻平均值", typeof(double));
            Dt.Columns.Add("良率", typeof(String));

        }
    }
}
