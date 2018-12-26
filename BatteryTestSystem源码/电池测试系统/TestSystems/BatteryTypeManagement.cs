using BatteryTestSystem_Bll;
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
    public partial class BatteryTypeManagement : Office2007Form
    {
        TestSystem TS;

        BatteryTestSystem_BatteryTypeBll BatteryBll = new BatteryTestSystem_BatteryTypeBll();

        List<BatteryTestSystem_BatteryType> TreeList = new List<BatteryTestSystem_BatteryType>();
        

        //存储状态
        string state = "browse";

        public BatteryTypeManagement(TestSystem ts)
        {
            InitializeComponent();
            this.TS = ts;
        }

        private void txt_BarcodeEncoding_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar.ToString() != ".")
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 更新按钮控件
        /// </summary>
        void UpdateBtni()
        {
            if (state == "browse")
            {
                btni_new.Enabled = true;
                btni_save.Enabled = false;
                btni_cancle.Enabled = false;
                btni_modify.Enabled = true;
                btni_delete.Enabled = true;
                btni_lookup.Enabled = true;

                treeView1.Enabled = true;
                panelEx1.Enabled = false;
            }
            else
            {
                btni_new.Enabled = false;
                btni_save.Enabled = true;
                btni_cancle.Enabled = true;
                btni_modify.Enabled = false;
                btni_delete.Enabled = false;
                btni_lookup.Enabled = false;

                treeView1.Enabled = false;
                panelEx1.Enabled = true;
            }
        }

        private void btni_new_Click(object sender, EventArgs e)
        {
            state = "insert";

            UpdateBtni();
        }
        
        BatteryTestSystem_BatteryType GetBatteryTestSystem_BatteryType()
        {
            try
            {
                BatteryTestSystem_BatteryType data = new BatteryTestSystem_BatteryType()
                {
                    BatteryType = txt_BatteryType.Text.Trim(),
                    VoltageDifference = Convert.ToDouble(txt_VoltageDifference.Text.Trim()),
                    TimeInterval = Convert.ToDouble(txt_TimeInterval.Text.Trim()),
                    ElectricCore = ck_ElectricCore.Checked,
                    FinishedProduct = ck_FinishedProduct.Checked
                };
                return data;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return  new BatteryTestSystem_BatteryType();
            }
        }

        private void btni_save_Click(object sender, EventArgs e)
        {
            try
            {
                if(txt_BatteryType.Text.Trim()=="")
                {
                    MessageToolscs.Tools("电池类型不能为空！");
                    txt_BatteryType.Focus();
                    return;
                }
                if (txt_VoltageDifference.Text.Trim() == "")
                {
                    MessageToolscs.Tools("电池压差(每小时)不能为空！");
                    txt_VoltageDifference.Focus();
                    return;
                }
                if(txt_TimeInterval.Text.Trim()=="")
                {
                    MessageToolscs.Tools("电池压差(每小时)最小时间间距(小时)不能为空！");
                    txt_TimeInterval.Focus();
                    return;
                }
                float f;
                if (!float.TryParse(txt_VoltageDifference.Text.Trim(), out f))
                {
                    MessageToolscs.Tools("电池压差(每小时)不是一个数字！");
                    txt_VoltageDifference.Focus();
                    return;
                }
                if (!float.TryParse(txt_TimeInterval.Text.Trim(),out f))
                {
                    MessageToolscs.Tools("最小时间间距(小时)不是一个数字！");
                    txt_TimeInterval.Focus();
                    return;
                }



                if (state == "insert")
                {
                    if (!SelectedTreeviewNodeBool(txt_BatteryType.Text.Trim()))
                    {
                        if (BatteryBll.insertBatteryTestSystem_BatteryTypeBll(GetBatteryTestSystem_BatteryType(), "insertBatteryTestSystem_BatteryType"))
                        {
                            MessageToolscs.Tools("保存成功");
                            this.SetTreeView(-1);
                            this.state = "browse";
                            this.UpdateBtni();
                        }
                        else
                        {
                            MessageToolscs.Tools("保存失败");
                            return;
                        }
                    }
                    else
                    {
                        MessageToolscs.Tools("电池类型已存在！");
                        return;
                    }
                }
                else if (state == "modify")
                {
                    if (SelectedTreeviewNodeBool(txt_BatteryType.Text.Trim()))
                    {
                        if (BatteryBll.insertBatteryTestSystem_BatteryTypeBll(GetBatteryTestSystem_BatteryType(), "updateBatteryTestSystem_BatteryType"))
                        {
                            MessageToolscs.Tools("保存成功");
                            this.SetTreeView(treeView1.SelectedNode.Index);
                            this.state = "browse";
                            this.UpdateBtni();
                        }
                        else
                        {
                            MessageToolscs.Tools("保存失败");
                            return;
                        }
                    }
                    else
                    {
                        MessageToolscs.Tools("电池类型不存在！");
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
        }

        private void btni_cancle_Click(object sender, EventArgs e)
        {
            state = "browse";

            UpdateBtni();
        }

        private void btni_modify_Click(object sender, EventArgs e)
        {
            state = "modify";

            UpdateBtni();
        }

        private void BatteryTypeManagement_Load(object sender, EventArgs e)
        {
            UpdateBtni();
            SetTreeView(-1);
        }

        void SetTreeView(int number)
        {
            TreeList.Clear();
            treeView1.Nodes.Clear();
            try
            {
                TreeList = BatteryBll.selectBatteryTestSystem_BatteryTypeBll("selectBatteryTestSystem_BatteryType");
            }
            catch
            {                 
            }

            TreeNode tn = new TreeNode();
            tn.Text = "电池类型";
            treeView1.Nodes.Add(tn);
            for (int i = 0; i < TreeList.Count; i++)
            {
                TreeNode tnChild = new TreeNode();
                tnChild.Text = TreeList[i].BatteryType;
                tnChild.Tag = i;
                tn.Nodes.Add(tnChild);
            }
            if (number >= 0)
            {
                if (treeView1.Nodes[0].LastNode != null)
                {
                    treeView1.Nodes[0].Expand();
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[number];
                }

            }
            else
            {
                if (treeView1.Nodes[0].LastNode != null)
                {
                    treeView1.Nodes[0].Expand();
                    treeView1.SelectedNode = treeView1.Nodes[0].LastNode;
                }
            }
        }

        void SetControls(BatteryTestSystem_BatteryType list)
        {
            txt_BatteryType.Text = list.BatteryType;
            txt_VoltageDifference.Text = list.VoltageDifference.ToString();
            txt_TimeInterval.Text = list.TimeInterval.ToString();
            ck_ElectricCore.Checked = list.ElectricCore;
            ck_FinishedProduct.Checked = list.FinishedProduct;
        }

        private void ck_ElectricCore_CheckedChanged(object sender, EventArgs e)
        {
            ck_FinishedProduct.Checked = !ck_ElectricCore.Checked;           
        }

        private void ck_FinishedProduct_CheckedChanged(object sender, EventArgs e)
        {
            ck_ElectricCore.Checked = !ck_FinishedProduct.Checked;  
        }

        public void SelectedTreeviewNode(string Nodename)
        {
            foreach (TreeNode item in treeView1.Nodes[0].Nodes)
            {
                if (item.Text == Nodename)
                {
                    this.treeView1.SelectedNode = item;
                }
            }
        }

        bool SelectedTreeviewNodeBool(string Nodename)
        {
            foreach (TreeNode item in treeView1.Nodes[0].Nodes)
            {
                if (item.Text == Nodename)
                {
                    return true;
                }
            }

            return false;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == treeView1.Nodes[0])
            {
                btni_delete.Enabled = false;
                return;
            }
            btni_delete.Enabled = true;
            this.SetControls(TreeList[Convert.ToInt32(e.Node.Tag)]);
        }

        private void btni_delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (BatteryBll.deleteBatteryTestSystem_BatteryTypeBll(treeView1.SelectedNode.Text.Trim(), "deleteBatteryTestSystem_BatteryType"))
                {
                    MessageToolscs.Tools("删除成功");
                    this.SetTreeView(-1);
                }
                else
                {
                    MessageToolscs.Tools("删除失败");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btni_lookup_Click(object sender, EventArgs e)
        {
            Queryform qf = new Queryform(this.TreeList, this);
            qf.ShowDialog();
        }

        private void BatteryTypeManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            TS.SetBatteryTypeList(TreeList);
        }
    }
}
