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
    /// <summary>
    /// 模拟Tools消息提示
    /// </summary>
    public partial class MessageTools : Form
    {
        

        private string StrMessage = "";

        Timer time = new Timer();
        public MessageTools(string strMessage)
        {
            InitializeComponent();
            this.StrMessage = strMessage;
        }

        private void MessageTools_Load(object sender, EventArgs e)
        {

            lb_Message.Text = StrMessage;
            OnResize(e);
            time.Tick += time_Tick;
            time.Interval = 1000;
            time.Enabled = true;
        }

        void time_Tick(object sender, EventArgs e)
        {
            time.Enabled = false;
            this.Close();

        }

        void OnResize(EventArgs e)
        {
            //base.OnResize(e);
            int x = (int)(0.5 * (this.Width - lb_Message.Width));
            int y = lb_Message.Location.Y;
            lb_Message.Location = new System.Drawing.Point(x, y);
        }
    }
}
