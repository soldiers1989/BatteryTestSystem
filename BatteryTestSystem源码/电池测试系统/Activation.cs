using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 电池测试系统
{
    public partial class Activation : Office2007Form
    {
        private MainForm MF;

        private string Mac;

        public Activation(MainForm mf,string mac)
        {
            InitializeComponent();
            this.MF = mf;
            this.Mac = mac;
        }

        private void textBoxX1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                if (textBoxX1.Text.Trim() == Mac)
                {
                    try
                    {
                        string path = System.Windows.Forms.Application.StartupPath + "\\important.xml";
                        MF.UpdateSetup(textBoxX1.Text.Trim(), path);
                        MessageBox.Show("激活成功！");
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("激活码错误！");
                }

            }
        }

        private void Activation_FormClosing(object sender, FormClosingEventArgs e)
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\important.xml";
            if (MF.LoadXML(path) != MF.GetMd5(MF.GetMd5(MainForm.GetMacAddress()) + "刘润辉"))
            {
                MF.Close();
            }
        }
    }
}
