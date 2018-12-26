using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace 电池测试系统.TestSystems
{
    public partial class connectionsSet : Office2007Form
    {
        public connectionsSet()
        {
            InitializeComponent();
        }

        private void txt_IP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8 && e.KeyChar.ToString() != ".")
            {
                e.Handled = true;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            string path = System.Windows.Forms.Application.StartupPath + "\\XML\\IPConnect.xml";
            XMLWrite(path);
        }

        void XMLWrite(string Path)
        {
            if (txt_InitialCatalog.Text.Trim() == "")
            {
                MessageToolscs.Tools("数据库名不能为空");
                return;
            }
            if (txt_DataSource.Text.Trim() == "")
            {
                MessageToolscs.Tools("IP地址不能为空");
                return;
            }

            XmlDocument doc = new XmlDocument();
            if(!File.Exists(Path))
            {
                try
                {
                    //创建类型声明节点    
                    XmlNode node = doc.CreateXmlDeclaration("1.0", "utf-8", "");
                    doc.AppendChild(node);

                    //Add the new node to the document.

                    XmlElement root = doc.CreateElement("Connectionset");//创建根节点          
                    doc.AppendChild(root);

                    CreateNode(doc, root, "IPText", txt_DataSource.Text.Trim());
                    CreateNode(doc, root, "DataBaseName", txt_InitialCatalog.Text.Trim());

                    doc.Save(Path);
                    MessageToolscs.Tools("保存成功！"); 
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    //using (FileStream stream = File.OpenRead(Path))
                    //{
                        doc.Load(Path);
                        XmlNodeList list = doc.SelectSingleNode("Connectionset").ChildNodes;//获得根节点user下的所有子节点
                        foreach (XmlNode node in list)
                        {
                            if (node.Name == "IPText")
                                node.InnerText = txt_DataSource.Text.Trim();
                            if (node.Name == "DataBaseName")
                                node.InnerText = txt_InitialCatalog.Text.Trim();
                        }

                        doc.Save(Path);
                        MessageToolscs.Tools("保存成功！");
               //     }
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

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void connectionsSet_Load(object sender, EventArgs e)
        {
            Loead(System.Windows.Forms.Application.StartupPath + "\\XML\\IPConnect.xml");
        }

        void Loead(string Path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(Path))
            {
                doc.Load(Path);
                XmlNodeList list = doc.SelectSingleNode("Connectionset").ChildNodes;//获得根节点user下的所有子节点
                foreach (XmlNode node in list)
                {
                    if (node.Name == "IPText")
                        txt_DataSource.Text = node.InnerText;
                    if (node.Name == "DataBaseName")
                        txt_InitialCatalog.Text = node.InnerText;
                }
            }
        }
    }
}
