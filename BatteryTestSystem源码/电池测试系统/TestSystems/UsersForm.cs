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

namespace 电池测试系统.TestSystems
{
    public partial class UsersForm : Office2007Form
    {
        InformationSystem Inform;
        TestSystem TestS;

        BatteryTestSystem_UserBll UserBll = new BatteryTestSystem_UserBll();

        public UsersForm()
        {
            InitializeComponent();
        }
        public UsersForm(TestSystem tests)
        {
            InitializeComponent();
            this.TestS = tests;
        }

        public UsersForm(InformationSystem inform)
        {
            InitializeComponent();
            this.Inform = inform;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_UserName.Text.Trim() == "")
                {
                    MessageBox.Show("用户名不能为空！");
                    return;
                }

                if (txt_Password.Text.Trim() == "")
                {
                    MessageBox.Show("密码不能为空！");
                    return;
                }
                
                if (!UserBll.selectCountBll("selectCount", txt_UserName.Text.Trim(),txt_Password.Text))
                {
                    MessageBox.Show("账号或密码不正确!");
                    return;
                }
                
                UsersHelp.Logining(UserBll.selectBatteryTestSystem_UserUserNameBll(txt_UserName.Text.Trim(), txt_Password.Text, "selectBatteryTestSystem_UserUserName"),UserBll.selectBatteryTestSystem_PermissionsUserNameDal(txt_UserName.Text.Trim(), "selectBatteryTestSystem_PermissionsUserName") );
                if (Inform != null)
                    Inform.SetlabelItem2(txt_UserName.Text.Trim());
                if (TestS != null)
                    TestS.SetlabelItem2(txt_UserName.Text.Trim());
                    
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
            {
                txt_Password.PasswordChar = new char();
            }
            else
                txt_Password.PasswordChar = '*';
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {

        }
    }
}
