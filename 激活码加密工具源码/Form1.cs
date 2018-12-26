using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 激活码加密工具
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if(e.KeyValue == 13)
            {
                textBox2.Text = GetMd5(textBox1.Text.Trim() + "刘润辉");
            }
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
    }
}
