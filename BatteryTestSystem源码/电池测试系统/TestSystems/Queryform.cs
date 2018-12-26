using BatteryTestSystem_Model;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 电池测试系统.TestSystems
{
    public partial class Queryform : Office2007Form
    {
        BatteryTypeManagement BT;
        List<BatteryTestSystem_BatteryType> Datalist;

        public Queryform(List<BatteryTestSystem_BatteryType> datalist, BatteryTypeManagement bt)
        {
            InitializeComponent();
            this.Datalist = datalist;
            this.BT = bt;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (cbe_ProductionClass.Text.Trim()=="")
            {
                return;
            }
            BT.SelectedTreeviewNode(cbe_ProductionClass.Text.Trim());
            this.Close();
        }

        private void setcbe_ProductionClassDataSource(List<BatteryTestSystem_BatteryType> list)
        {
            List<string> cbelist = new List<string>();
            foreach (BatteryTestSystem_BatteryType item in list)
            {
                cbelist.Add(item.BatteryType);
            }
            this.cbe_ProductionClass.DataSource = cbelist;
        }

        private void Queryform_Load(object sender, EventArgs e)
        {
            setcbe_ProductionClassDataSource(Datalist);
        }
    }
}
