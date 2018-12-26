using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 激活码获取工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = GetMd5(GetMacAddress());
        }

        /// <summary>
        /// 获取MD5值
        /// </summary>
        /// <param name="str">需要转换MD5的字符串</param>
        /// <returns>string</returns>
        public string GetMd5(string str)
        {
            StringBuilder sb = new StringBuilder();
            //创建一个计算MD5值对象
            using (MD5 md = MD5.Create())
            {
                //把字符串转成字节数组
                byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
                //调用该对象的方法进行MD5计算
                byte[] md5bytes = md.ComputeHash(bytes);

                for (int i = 0; i < md5bytes.Length; i++)
                {
                    sb.Append(md5bytes[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }

        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetMacAddress()
        {
            try
            {
                string strMac = string.Empty;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        strMac = mo["MacAddress"].ToString();
                    }
                }
                moc = null;
                mc = null;
                return strMac;
            }
            catch
            {
                return "unknown";
            }
        }
    }
}
