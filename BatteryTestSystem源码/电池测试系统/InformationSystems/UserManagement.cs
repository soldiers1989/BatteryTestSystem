using BatteryTestSystem_Bll;
using BatteryTestSystem_Comm;
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

namespace 电池测试系统.InformationSystems
{
    public partial class UserManagement : Office2007Form
    {

        BatteryTestSystem_UserBll UserBll = new BatteryTestSystem_UserBll();

        List<BatteryTestSystem_Permissions> TreeList = new List<BatteryTestSystem_Permissions>();
        public UserManagement()
        {
            InitializeComponent();
        }

        private void btn_AddUserSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_AddPassword.Text != txt_AddPassword1.Text)
                {
                    MessageBox.Show("密码与确认密码不一致！");
                    return;
                }
                if (UserBll.selectCountUserNameBll("selectCountUserName", txt_AddUserName.Text.Trim()))
                {
                    MessageBox.Show("用户已存在!");
                    return;
                }

                if (UserBll.AddUserBll("AddUser", txt_AddUserName.Text.Trim(), txt_AddPassword.Text))
                {
                    MessageBox.Show("成功！");
                    txt_AddUserName.Text = "";
                    txt_AddPassword.Text = "";
                    txt_AddPassword1.Text = "";
                }
                else
                    MessageBox.Show("失败！");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_OldPassword.Text != UsersHelp.Password)
                {
                    MessageBox.Show("密码错误！");
                    txt_OldPassword.Focus();
                    txt_OldPassword.SelectAll();
                    return;
                }
                if (txt_NewPassword.Text != txt_NewPassword1.Text)
                {
                    MessageBox.Show("新密码与确认密码不一致！");
                    txt_NewPassword.Focus();
                    txt_NewPassword.SelectAll();
                    return;
                }

                if (UserBll.updateBatteryTestSystem_UserPasswordBll(UsersHelp.UserName, txt_NewPassword.Text, "updateBatteryTestSystem_UserPassword"))
                {
                    MessageBox.Show("修改成功!");
                    UsersHelp.Password = txt_NewPassword.Text;
                    txt_OldPassword.Text = "";
                    txt_NewPassword.Text = "";
                    txt_NewPassword1.Text = "";
                }
                else
                {
                    MessageBox.Show("修改失败!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SetTreeView(int number)
        {
            TreeList.Clear();
            treeView1.Nodes.Clear();
            try
            {
                TreeList = UserBll.selectBatteryTestSystem_PermissionsBll(UsersHelp.UserName, "selectBatteryTestSystem_Permissions");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            TreeNode tn = new TreeNode();
            tn.Text = "用户";
            treeView1.Nodes.Add(tn);
            for (int i = 0; i < TreeList.Count; i++)
            {
                TreeNode tnChild = new TreeNode();
                tnChild.Text = TreeList[i].UserName;
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

        private void btni_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (treeView1.SelectedNode == treeView1.Nodes[0])
                {
                    return;
                }
                if (UserBll.updateBatteryTestSystem_PermissionsBll(GetBatteryTestSystem_Permissions(), "updateBatteryTestSystem_Permissions"))
                {
                    MessageToolscs.Tools("修改成功!");
                    TreeList[Convert.ToInt32(treeView1.SelectedNode.Tag)] = GetBatteryTestSystem_Permissions();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        BatteryTestSystem_Permissions GetBatteryTestSystem_Permissions()
        {
            BatteryTestSystem_Permissions data = new BatteryTestSystem_Permissions()
            {
                UserName=treeView1.SelectedNode.Text.Trim(),
                Admini=ck_Admini.Checked,
                AddUser=ck_AddUser.Checked,
                ChangingEquipment=ck_ChangingEquipment.Checked,
                ServerConnection=ck_ServerConnection.Checked,
                DataDelete=ck_DataDelete.Checked,
                InformationSystem=ck_InformationSystem.Checked,
                ModifyPermissions=ck_ModifyPermissions.Checked
            };
            return data;
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeView1.SelectedNode==treeView1.Nodes[0])
            {
                return;
            }
            lab_UserName.Text = e.Node.Text;

            SetPermissions(TreeList[Convert.ToInt32(e.Node.Tag)]);
        }
        /// <summary>
        /// 给权限控件赋值
        /// </summary>
        /// <param name="data"></param>
        void SetPermissions(BatteryTestSystem_Permissions data)
        {
            try
            {
            ck_AddUser.Checked = data.AddUser;
            ck_Admini.Checked = data.Admini;
            ck_ChangingEquipment.Checked = data.ChangingEquipment;
            ck_DataDelete.Checked = data.DataDelete;
            ck_InformationSystem.Checked = data.InformationSystem;
            ck_ModifyPermissions.Checked = data.ModifyPermissions;
            ck_ServerConnection.Checked = data.ServerConnection;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tabItem3_Click(object sender, EventArgs e)
        {
            this.SetTreeView(-1);
        }

        private void textBoxItem1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar==13)
            {
                foreach (TreeNode item in treeView1.Nodes[0].Nodes)
                {
                    if (item.Text == textBoxItem1.Text.Trim())
                    {
                        this.treeView1.Focus();
                        this.treeView1.SelectedNode = item;
                    }
                }
            }
        }

        private void tabControl1_SelectedTabChanging(object sender, TabStripTabChangingEventArgs e)
        {
            if (e.NewTab.Name == "tabItem3")
            {
                if (!(UsersHelp.ModifyPermissions || UsersHelp.Admini))
                {
                    MessageBox.Show("你没有权限!");
                    e.Cancel = true;
                }
            }
            if (e.NewTab.Name == "tabItem3")
            {
                if (!(UsersHelp.AddUser || UsersHelp.Admini))
                {
                    MessageBox.Show("你没有权限!");
                    e.Cancel = true;
                }
            }
        }

        private void btni_Delete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("确定删除此用户？","系统提示",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                MessageBox.Show("成功删除 "+UserBll.deleteUsersBll(treeView1.SelectedNode.Text.Trim(), "deleteUsers").ToString()+" 条用户记录");
                this.SetTreeView(-1);
            }
        }
    }
}
