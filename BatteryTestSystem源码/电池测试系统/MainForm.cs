using BatteryTestSystem_Bll;
using BatteryTestSystem_Comm;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using 电池测试系统.TestSystems;

namespace 电池测试系统
{
    public partial class MainForm : Office2007Form
    {
        BatteryTestSystem_DayAnalysisBll DayAnalysisBll = new BatteryTestSystem_DayAnalysisBll();
        public MainForm()
        {
            InitializeComponent();
        }

        public string LoadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
                XmlNodeList list = doc.SelectSingleNode("Important").ChildNodes;//获得根节点user下的所有子节点
                foreach (XmlNode item in list)
                {
                    if (item.Name == "X")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "XXX")
                                return node.InnerText;
                        }
                    }
                }
            }
            return "";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            GetXML();
            this.DeleteDayAnalysis();
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

        void GetXML()
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\important.xml";
                savaSetup(path);
                //UpdateSetup(GetMacAddress(), path);
                if (LoadXML(path) != GetMd5(GetMd5(GetMacAddress())+ "刘润辉"))
                {
                    Activation at = new Activation(this, GetMd5(GetMd5(GetMacAddress()) + "刘润辉"));
                    at.ShowDialog();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

         void savaSetup(string path)
        {

            XmlDocument doc = new XmlDocument();
            if (!File.Exists(path))
            {
                try
                {
                    //创建类型声明节点    
                    XmlNode node = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                    doc.AppendChild(node);

                    //Add the new node to the document.

                    XmlElement root = doc.CreateElement("Important");//创建根节点          
                    doc.AppendChild(root);

                    XmlElement root1 = doc.CreateElement("X");//创建根节点          
                    root.AppendChild(root1);

                    CreateNode(doc, root1, "XXX", "");

                    doc.Save(path);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void UpdateSetup(string Mac, string path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                try
                {
                    doc.Load(path);
                    XmlNodeList list = doc.SelectSingleNode("Important").ChildNodes;//获得根节点user下的所有子节点
                    foreach (XmlNode item in list)
                    {
                        if (item.Name == "X")
                        {
                            foreach (XmlNode node in item.ChildNodes)
                            {
                                if (node.Name == "XXX")
                                    node.InnerText = Mac;
                            }
                        }
                    }


                    doc.Save(path);
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>      
        /// 创建节点      
        /// </summary>      
        /// <param name="xmldoc">xml文档 </param>     
        /// <param name="parentnode">父节点  </param>    
        /// <param name="name">节点名</param>      
        /// <param name="value">节点值 </param>     
        ///     
        public void CreateNode(XmlDocument xmlDoc, XmlNode parentNode, string name, string value)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, null);
            node.InnerText = value;
            parentNode.AppendChild(node);
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            UsersForm uf = new UsersForm();
            uf.ShowDialog();

            if (UsersHelp.Login)
            {
                TestSystem ts = new TestSystem(this);
                ts.ShowDialog();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                UsersForm uf = new UsersForm();
                uf.ShowDialog();

                if (UsersHelp.Login)
                {
                    if(UsersHelp.InformationSystem||UsersHelp.Admini)
                    {
                        InformationSystem ifs = new InformationSystem(this);
                        ifs.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("你没有登录信息系统的权限!");
                    }
                }
            }
            catch
            {
            }
        }

        void DeleteDayAnalysis()
        {
            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);

            Thread th = new Thread(delegate()
            {
                deleteThread(dt);
            });
            th.IsBackground = true;
            th.Start();


        }

        void deleteThread(DateTime data)
        {
            try
            {
                DayAnalysisBll.deleteBatteryTestSystem_DayAnalysisBll(data, "deleteBatteryTestSystem_DayAnalysis");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
