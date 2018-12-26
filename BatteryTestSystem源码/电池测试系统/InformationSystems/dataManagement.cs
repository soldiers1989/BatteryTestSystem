using BatteryTestSystem_Bll;
using BatteryTestSystem_Comm;
using BatteryTestSystem_Model;
using DevComponents.DotNetBar;
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

namespace 电池测试系统.InformationSystems
{
    public partial class dataManagement : Office2007Form
    {

        SynchronizationContext My_cotext = null;

        dataManagementBll deleteBll = new dataManagementBll();
        TestSystemBll MainBll = new TestSystemBll();

        List<dataManagementModel> DataList = new List<dataManagementModel>();

        string RT = "";
        string QRCode ="";
        string ProductionClass = "";
        string ComboBo = "";
        DateTime BeginTime = new DateTime();
        DateTime EndTime = new DateTime();
        public dataManagement()
        {
            InitializeComponent();
        }

        private void btni_Query_Click(object sender, EventArgs e)
        {
            DataList.Clear();

            string rt=GetRT(RB_All.Checked, RB_ElectricCore.Checked, RB_FinishedProduct.Checked);
            string qrcode= txt_QRCode.Text.Trim();
            string productionclass=cbe_ProductionClass.Text.Trim();
            DateTime begintime=Dti_Begin.Value;
            DateTime endtime=Dti_End.Value;
            string comboBo=comboBoxEx1.Text.Trim();

            this.RT = rt;
            this.QRCode = qrcode;
            this.ProductionClass = productionclass;
            this.BeginTime = begintime;
            this.EndTime = endtime;
            this.ComboBo = comboBo;

            Thread th = new Thread(delegate()
            {
                Query(rt, qrcode, productionclass, begintime, endtime, comboBo);
            });
            th.IsBackground = true;
            th.Start();

        }

        private void dataManagement_Load(object sender, EventArgs e)
        {
            comboBoxEx1.SelectedIndex = 0;
            My_cotext = SynchronizationContext.Current;
            setDateTime();
            SetDataGridView();
            SetBatteryTypeList(MainBll.GetBatteryType());
        }

        public void SetBatteryTypeList(List<BatteryTestSystem_BatteryType> data)
        {
            cbe_ProductionClass.DataSource = MainBll.GetBatteryTypeStr(data);
        }
        private void setDateTime()
        {
            DateTime dt = DateTime.Now;
            DateTime startTime = new DateTime(dt.Year, dt.Month, dt.Day, 00, 00, 00);
            DateTime endTime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
            Dti_Begin.Value = startTime;
            Dti_End.Value = endTime;
        }

        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(this.dataGridViewX1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString(Convert.ToString(e.RowIndex + 1, CultureInfo.CurrentUICulture),
                e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 20, e.RowBounds.Location.Y + 4);
            }
        }

        void Query(string RT, string QRCode, string BatteryType, DateTime beginTime, DateTime endTime,string str)
        {
            try
            {
                if (str == "条码测试数据")
                {
                    switch (RT)
                    {
                        case "全部":
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_FinishedProductFuzzyQueryBll(QRCode, BatteryType, beginTime, endTime, "selectBatteryTestSystem_FinishedProductFuzzyQuery"));
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_ElectricCoreFuzzyQueryBll(QRCode, BatteryType, beginTime, endTime, "selectBatteryTestSystem_ElectricCoreFuzzyQuery"));
                            break;
                        case "电芯":
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_ElectricCoreFuzzyQueryBll(QRCode, BatteryType, beginTime, endTime, "selectBatteryTestSystem_ElectricCoreFuzzyQuery"));
                            break;
                        case "成品":
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_FinishedProductFuzzyQueryBll(QRCode, BatteryType, beginTime, endTime, "selectBatteryTestSystem_FinishedProductFuzzyQuery"));
                            break;
                    }
                }
                else
                {
                    switch (RT)
                    {
                        case "全部":
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_UnidentitiesTestFuzzyQueryBll(BatteryType, beginTime, endTime, "selectBatteryTestSystem_UnidentitiesTestFuzzyQuery"));                                                        
                            break;
                        case "电芯":
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_UnidentitiesTestFuzzyQueryBll(BatteryType, beginTime, endTime, "selectBatteryTestSystem_UnidentitiesTestFuzzyQuery0"));
                            break;
                        case "成品":
                            My_cotext.Post(SetDataGridView1, deleteBll.selectBatteryTestSystem_UnidentitiesTestFuzzyQueryBll(BatteryType, beginTime, endTime, "selectBatteryTestSystem_UnidentitiesTestFuzzyQuery1"));
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string GetRT(bool RTAll, bool RTElectricCore, bool RTFinishedProduct)
        {
            if (RTAll)
                return "全部";
            if (RTElectricCore)
                return "电芯";
            if (RTFinishedProduct)
                return "成品";

            return "";
        }

        void SetDataGridView1(object o)
        {
            try
            {
                List<dataManagementModel> list = o as List<dataManagementModel>;

                dataGridViewX1.DataSource = new List<dataManagementModel>();

                DataList.InsertRange(0, list);
                if (DataList.Count > 0)
                {
                    dataGridViewX1.DataSource = DataList;
                }
            }
            catch
            {
                throw;
            }
        }

        private void dataGridViewX1_SelectionChanged(object sender, EventArgs e)
        {
            this.dataGridViewX1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void btni_DeleteAll_Click(object sender, EventArgs e)
        {
            if(dataGridViewX1.Rows.Count==0)
            {
                MessageBox.Show("没有可删除的数据!");
                return;
            }
            if (MessageBox.Show("确定删除此条件的数据？", "系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            DeleteRT(RT,QRCode, ProductionClass, BeginTime, EndTime,comboBoxEx1.Text.Trim());
        }

        void DeleteRT(string rt, string qrcode, string batterytype, DateTime begintime, DateTime endtime, string str)
        {
            try
            {
                if (str == "条码测试数据")
                {
                    switch (rt)
                    {
                        case "全部":
                            MessageBox.Show("成功删除电芯表：" + deleteBll.deleteBatteryTestSystem_ElectricCoreConditionBll(qrcode, batterytype, begintime, endtime, "deleteBatteryTestSystem_ElectricCoreCondition").ToString() + "行 \r\n" + "成功删除成品表：" + deleteBll.deleteBatteryTestSystem_FinishedProductConditionBll(QRCode, ProductionClass, BeginTime, EndTime, "deleteBatteryTestSystem_FinishedProductCondition").ToString() + "行 \r\n" + "成功删除异常信息表：" + deleteBll.deleteBatteryTestSystem_ErorrMessageConditionBll(qrcode, batterytype, begintime, endtime, "deleteBatteryTestSystem_ErorrMessageCondition").ToString() + "行");
                            dataGridViewX1.DataSource = new List<dataManagementModel>();
                            break;
                        case "电芯":
                            MessageBox.Show("成功删除电芯表：" + deleteBll.deleteBatteryTestSystem_ElectricCoreConditionBll(qrcode, batterytype, begintime, endtime, "deleteBatteryTestSystem_ElectricCoreCondition").ToString() + "行 \r\n" + "成功删除异常信息表：" + deleteBll.deleteBatteryTestSystem_ErorrMessageConditionBll(qrcode, batterytype, begintime, endtime, "deleteBatteryTestSystem_ErorrMessageCondition").ToString() + "行");
                            dataGridViewX1.DataSource = new List<dataManagementModel>();
                            break;
                        case "成品":
                            MessageBox.Show("成功删除成品表：" + deleteBll.deleteBatteryTestSystem_FinishedProductConditionBll(qrcode, batterytype, begintime, endtime, "deleteBatteryTestSystem_FinishedProductCondition").ToString() + "行 \r\n" + "成功删除异常信息表：" + deleteBll.deleteBatteryTestSystem_ErorrMessageConditionBll(qrcode, batterytype, begintime, endtime, "deleteBatteryTestSystem_ErorrMessageCondition").ToString() + "行");
                            dataGridViewX1.DataSource = new List<dataManagementModel>();
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("成功删除无条码测试表：" + deleteBll.deleteBatteryTestSystem_UnidentitiesTestBll(batterytype, begintime, endtime, "deleteBatteryTestSystem_UnidentitiesTest") + "行 ");
                    dataGridViewX1.DataSource = new List<dataManagementModel>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       void SetDataGridView()
       {
           dataGridViewX1.Columns.Clear();

           DataGridViewTextBoxColumn newcol = new DataGridViewTextBoxColumn();
           newcol.HeaderText = "二维码/条码";
           newcol.Name = "QRCode";
           newcol.DataPropertyName = "QRCode";
           newcol.Width = 120;
           newcol.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol.Tag = 0;
           newcol.Visible = comboBoxEx1.Text == "条码测试数据" ? true : false;
           this.dataGridViewX1.Columns.Insert(0, newcol);

           DataGridViewTextBoxColumn newcol34 = new DataGridViewTextBoxColumn();
           newcol34.HeaderText = "电池类型";
           newcol34.Name = "BatteryType";
           newcol34.DataPropertyName = "BatteryType";
           newcol34.Width = 100;
           newcol34.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol34.Tag = 1;
           this.dataGridViewX1.Columns.Insert(1, newcol34);

           DataGridViewTextBoxColumn newcol35 = new DataGridViewTextBoxColumn();
           newcol35.HeaderText = "电池规格";
           newcol35.Name = "ElectricCoreOrFinishedProduct";
           newcol35.DataPropertyName = "ElectricCoreOrFinishedProduct";
           newcol35.Width = 100;
           newcol35.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol35.Tag = 2;
           this.dataGridViewX1.Columns.Insert(2, newcol35);

           DataGridViewTextBoxColumn newcol10 = new DataGridViewTextBoxColumn();
           newcol10.HeaderText = "开路电压1";
           newcol10.Name = "vol_open1";
           newcol10.DataPropertyName = "vol_open1";
           newcol10.Width = 100;
           newcol10.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol10.Tag = 3;
           newcol10.Visible = true;
           this.dataGridViewX1.Columns.Insert(3, newcol10);

           DataGridViewTextBoxColumn newcol1 = new DataGridViewTextBoxColumn();
           newcol1.HeaderText = "负载电压1";
           newcol1.Name = "vol_load1";
           newcol1.DataPropertyName = "vol_load1";
           newcol1.Width = 100;
           newcol1.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol1.Tag = 4;
           this.dataGridViewX1.Columns.Insert(4, newcol1);

           DataGridViewTextBoxColumn newcol11 = new DataGridViewTextBoxColumn();
           newcol11.HeaderText = "内阻1";
           newcol11.Name = "imp1";
           newcol11.DataPropertyName = "imp1";
           newcol11.Width = 100;
           newcol11.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol11.Tag = 5;
           this.dataGridViewX1.Columns.Insert(5, newcol11);

           DataGridViewTextBoxColumn newcol16 = new DataGridViewTextBoxColumn();
           newcol16.HeaderText = "过流保护值1";
           newcol16.Name = "ovc1";
           newcol16.DataPropertyName = "ovc1";
           newcol16.Width = 100;
           newcol16.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol16.Tag = 6;
           this.dataGridViewX1.Columns.Insert(6, newcol16);

           DataGridViewTextBoxColumn newcol2 = new DataGridViewTextBoxColumn();
           newcol2.HeaderText = "ID电阻1";
           newcol2.Name = "IDimp1";
           newcol2.DataPropertyName = "IDimp1";
           newcol2.Width = 100;
           newcol2.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol2.Tag = 7;
           this.dataGridViewX1.Columns.Insert(7, newcol2);

           DataGridViewTextBoxColumn newcol6 = new DataGridViewTextBoxColumn();
           newcol6.HeaderText = "K值1";
           newcol6.Name = "Kvalue1";
           newcol6.DataPropertyName = "Kvalue1";
           newcol6.Width = 100;
           newcol6.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol6.Tag = 8;
           this.dataGridViewX1.Columns.Insert(8, newcol6);

           DataGridViewTextBoxColumn newcol3 = new DataGridViewTextBoxColumn();
           newcol3.HeaderText = "第一次测试时间";
           newcol3.Name = "Time1";
           newcol3.DataPropertyName = "Time1";
           newcol3.Width = 120;
           newcol3.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol3.Tag = 9;
           this.dataGridViewX1.Columns.Insert(9, newcol3);

           DataGridViewTextBoxColumn newco6 = new DataGridViewTextBoxColumn();
           newco6.HeaderText = "开路电压2";
           newco6.Name = "vol_open2";
           newco6.DataPropertyName = "vol_open2";
           newco6.Width = 100;
           newco6.SortMode = DataGridViewColumnSortMode.NotSortable;
           newco6.Tag = 10;
           this.dataGridViewX1.Columns.Insert(10, newco6);

           DataGridViewTextBoxColumn newcol4 = new DataGridViewTextBoxColumn();
           newcol4.HeaderText = "负载电压2";
           newcol4.Name = "vol_load2";
           newcol4.DataPropertyName = "vol_load2";
           newcol4.Width = 200;
           newcol4.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol4.Tag = 11;
           this.dataGridViewX1.Columns.Insert(11, newcol4);

           DataGridViewTextBoxColumn newco5 = new DataGridViewTextBoxColumn();
           newco5.HeaderText = "内阻2";
           newco5.Name = "imp2";
           newco5.DataPropertyName = "imp2";
           newco5.Width = 200;
           newco5.SortMode = DataGridViewColumnSortMode.NotSortable;
           newco5.Tag = 12;
           this.dataGridViewX1.Columns.Insert(12, newco5);

           DataGridViewTextBoxColumn newco25 = new DataGridViewTextBoxColumn();
           newco25.HeaderText = "过电流保护值2";
           newco25.Name = "ovc2";
           newco25.DataPropertyName = "ovc2";
           newco25.Width = 50;
           newco25.SortMode = DataGridViewColumnSortMode.NotSortable;
           newco25.Tag = 13;
           this.dataGridViewX1.Columns.Insert(13, newco25);

           DataGridViewTextBoxColumn newco26 = new DataGridViewTextBoxColumn();
           newco26.HeaderText = "ID电阻2";
           newco26.Name = "IDimp2";
           newco26.DataPropertyName = "IDimp2";
           newco26.Width = 200;
           newco26.SortMode = DataGridViewColumnSortMode.NotSortable;
           newco26.Tag = 14;
           this.dataGridViewX1.Columns.Insert(14, newco26);


           DataGridViewTextBoxColumn newco36 = new DataGridViewTextBoxColumn();
           newco36.HeaderText = "K值2";
           newco36.Name = "Kvalue2";
           newco36.DataPropertyName = "Kvalue2";
           newco36.Width = 200;
           newco36.SortMode = DataGridViewColumnSortMode.NotSortable;
           newco36.Tag = 15;
           this.dataGridViewX1.Columns.Insert(15, newco36);

           DataGridViewTextBoxColumn newco38 = new DataGridViewTextBoxColumn();
           newco38.HeaderText = "第二次测试时间";
           newco38.Name = "Time2";
           newco38.DataPropertyName = "Time2";
           newco38.Width = 120;
           newco38.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol3.Tag = 16;
           this.dataGridViewX1.Columns.Insert(16, newco38);

           DataGridViewTextBoxColumn newcol46 = new DataGridViewTextBoxColumn();
           newcol46.HeaderText = "第一次与第二次测试时间实际间距";
           newcol46.Name = "timeInterval2";
           newcol46.DataPropertyName = "timeInterval2";
           newcol46.Width = 120;
           newcol46.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol46.Tag = 17;
           this.dataGridViewX1.Columns.Insert(17, newcol46);

           DataGridViewTextBoxColumn newcol39 = new DataGridViewTextBoxColumn();
           newcol39.HeaderText = "开路电压3";
           newcol39.Name = "vol_open3";
           newcol39.DataPropertyName = "vol_open3";
           newcol39.Width = 100;
           newcol39.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol39.Tag = 18;
           newcol39.Visible = true;
           this.dataGridViewX1.Columns.Insert(18, newcol39);

           DataGridViewTextBoxColumn newcol40 = new DataGridViewTextBoxColumn();
           newcol40.HeaderText = "负载电压3";
           newcol40.Name = "vol_load3";
           newcol40.DataPropertyName = "vol_load3";
           newcol40.Width = 100;
           newcol40.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol40.Tag = 19;
           this.dataGridViewX1.Columns.Insert(19, newcol40);

           DataGridViewTextBoxColumn newcol41 = new DataGridViewTextBoxColumn();
           newcol41.HeaderText = "内阻3";
           newcol41.Name = "imp3";
           newcol41.DataPropertyName = "imp3";
           newcol41.Width = 100;
           newcol41.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol41.Tag = 20;
           this.dataGridViewX1.Columns.Insert(20, newcol41);

           DataGridViewTextBoxColumn newcol42 = new DataGridViewTextBoxColumn();
           newcol42.HeaderText = "过流保护值3";
           newcol42.Name = "ovc3";
           newcol42.DataPropertyName = "ovc3";
           newcol42.Width = 100;
           newcol42.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol42.Tag = 21;
           this.dataGridViewX1.Columns.Insert(21, newcol42);

           DataGridViewTextBoxColumn newcol43 = new DataGridViewTextBoxColumn();
           newcol43.HeaderText = "ID电阻3";
           newcol43.Name = "IDimp3";
           newcol43.DataPropertyName = "IDimp3";
           newcol43.Width = 100;
           newcol43.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol43.Tag = 22;
           this.dataGridViewX1.Columns.Insert(22, newcol43);

           DataGridViewTextBoxColumn newcol44 = new DataGridViewTextBoxColumn();
           newcol44.HeaderText = "K值3";
           newcol44.Name = "Kvalue3";
           newcol44.DataPropertyName = "Kvalue3";
           newcol44.Width = 100;
           newcol44.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol44.Tag = 23;
           this.dataGridViewX1.Columns.Insert(23, newcol44);

           DataGridViewTextBoxColumn newcol45 = new DataGridViewTextBoxColumn();
           newcol45.HeaderText = "第三次测试时间";
           newcol45.Name = "Time3";
           newcol45.DataPropertyName = "Time3";
           newcol45.Width = 120;
           newcol45.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol45.Tag = 24;
           this.dataGridViewX1.Columns.Insert(24, newcol45);


           DataGridViewTextBoxColumn newcol47 = new DataGridViewTextBoxColumn();
           newcol47.HeaderText = "第二次与第三次测试时间实际间距";
           newcol47.Name = "timeInterval3";
           newcol47.DataPropertyName = "timeInterval3";
           newcol47.Width = 120;
           newcol47.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol47.Tag = 25;
           this.dataGridViewX1.Columns.Insert(25, newcol47);

           DataGridViewTextBoxColumn newcol48 = new DataGridViewTextBoxColumn();
           newcol48.HeaderText = "测试次数";
           newcol48.Name = "TestFrequency";
           newcol48.DataPropertyName = "TestFrequency";
           newcol48.Width = 120;
           newcol48.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol48.Tag = 26;
           this.dataGridViewX1.Columns.Insert(26, newcol48);

           DataGridViewTextBoxColumn newcol5 = new DataGridViewTextBoxColumn();
           newcol5.SortMode = DataGridViewColumnSortMode.NotSortable;
           newcol5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
           this.dataGridViewX1.Columns.Insert(27, newcol5);
       }

       private void btni_DeleteSelected_Click(object sender, EventArgs e)
       {
           try
           {
               if (dataGridViewX1.Rows.Count == 0)
               {
                   MessageBox.Show("没有可删除的数据!");
                   return;
               }
               if (MessageBox.Show("确定删除二维码/条码为：'" + DataList[dataGridViewX1.CurrentRow.Index].QRCode + "' 的数据？", "系统提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
               this.deleteQRCode(DataList[dataGridViewX1.CurrentRow.Index].QRCode, DataList[dataGridViewX1.CurrentRow.Index].ElectricCoreOrFinishedProduct, dataGridViewX1.CurrentRow.Index);
           }
           catch(Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

       void deleteQRCode(string QRCode, string ElectricCoreOrFinishedProduct, int count)
       {
           try
           {
               switch (ElectricCoreOrFinishedProduct)
               {
                   case "电芯":
                       MessageBox.Show("成功删除电芯表：" + deleteBll.deleteBatteryTestSystem_ElectricCoreQRCodeBll(QRCode, "deleteBatteryTestSystem_ElectricCoreQRCode").ToString() + "行 \r\n" + "成功删除异常信息表：" + deleteBll.deleteBatteryTestSystem_ElectricCoreQRCodeBll(QRCode, "deleteBatteryTestSystem_ErorrMessageQRCode").ToString() + "行");
                       dataGridViewX1.DataSource = new List<dataManagementModel>();
                       DataList.RemoveAt(count);
                       dataGridViewX1.DataSource = DataList;
                       break;
                   case "成品":
                       MessageBox.Show("成功删除电芯表：" + deleteBll.deleteBatteryTestSystem_ElectricCoreQRCodeBll(QRCode, "deleteBatteryTestSystem_FinishedProductQRCode").ToString() + "行 \r\n" + "成功删除异常信息表：" + deleteBll.deleteBatteryTestSystem_ElectricCoreQRCodeBll(QRCode, "deleteBatteryTestSystem_ErorrMessageQRCode").ToString() + "行");                       
                       dataGridViewX1.DataSource = new List<dataManagementModel>();
                       DataList.RemoveAt(count);
                       dataGridViewX1.DataSource = DataList;
                       break;
               }
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

       private void btni_Condition_Click(object sender, EventArgs e)
       {
           if(Dti_Begin.Text=="")
           {
               MessageBox.Show("开始时间不能不空！");
               return;
           }
           if (Dti_End.Text == "")
           {
               MessageBox.Show("结束时间不能不空！");
               return;
           }
           if(MessageBox.Show("确定删除此条件的数据？","系统提示",MessageBoxButtons.OKCancel)==DialogResult.OK)
               DeleteRT(GetRT(RB_All.Checked, RB_ElectricCore.Checked, RB_FinishedProduct.Checked), txt_QRCode.Text.Trim(), cbe_ProductionClass.Text.Trim(), Dti_Begin.Value, Dti_End.Value,comboBoxEx1.Text.Trim());
       }

       private void btni_Delete_Click(object sender, EventArgs e)
       {

       }

       private void btni_DataOut_Click(object sender, EventArgs e)
       {
           if (this.dataGridViewX1.CurrentRow == null)
           {
               MessageBox.Show("没有可导出的数据!");
               return;
           }

           try
           {
               if (ExcelHelperForCs.ExportToExcel(this.dataGridViewX1) != null)
                   MessageBox.Show("导出完成");
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
       }

       private void comboBoxEx1_SelectionChangeCommitted(object sender, EventArgs e)
       {
           SetDataGridView();
           if (comboBoxEx1.Text == "条码测试数据")
           {
               删除选中的数据ToolStripMenuItem.Enabled = true;
               btni_DeleteSelected.Enabled = true;
               txt_QRCode.Enabled = true;
           }
           else
           {
               删除选中的数据ToolStripMenuItem.Enabled = false;
               btni_DeleteSelected.Enabled = false;
               txt_QRCode.Enabled = false;
           }
              
       }
    }
}
