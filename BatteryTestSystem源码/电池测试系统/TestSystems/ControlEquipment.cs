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
    public partial class ControlEquipment : Office2007Form
    {
        private TestSystem TS;

        public ControlEquipment(TestSystem ts)
        {
            InitializeComponent();
            this.TS = ts;
        }

        private void btn_cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_connection_Click(object sender, EventArgs e)
        {
            try
            {
                TS.StartControlEquipment(com_PortMainBoard.Text.Trim(), com_baudRateMainBoard.Text.Trim(), comb_Databits.Text.Trim(), comb_Parity.Text.Trim(), comb_StopBits.Text.Trim());
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_sava_Click(object sender, EventArgs e)
        {
            try
            {
                string path = System.Windows.Forms.Application.StartupPath + "\\XML\\ParameterConfigurationControlEquipment.xml";
                savaSetup(com_PortMainBoard.Text.Trim(), com_baudRateMainBoard.Text.Trim(), comb_Databits.Text.Trim(), comb_Parity.Text.Trim(), comb_StopBits.Text.Trim(), "equipment1", path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void savaSetup(string com, string com_baudtate, string databits, string par, string stop, string Element, string path)
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

                    XmlElement root = doc.CreateElement("ParameterConfiguration");//创建根节点          
                    doc.AppendChild(root);

                    XmlElement root1 = doc.CreateElement("equipment1");//创建根节点          
                    root.AppendChild(root1);

                    CreateNode(doc, root1, "com", com);
                    CreateNode(doc, root1, "com_baudtate", com_baudtate);
                    CreateNode(doc, root1, "databits", databits);
                    CreateNode(doc, root1, "par", par);
                    CreateNode(doc, root1, "stop", stop);

                    doc.Save(path);
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
                    doc.Load(path);
                    XmlNodeList list = doc.SelectSingleNode("ParameterConfiguration").ChildNodes;//获得根节点user下的所有子节点
                    foreach (XmlNode item in list)
                    {
                        if (item.Name == Element)
                        {
                            foreach (XmlNode node in item.ChildNodes)
                            {
                                if (node.Name == "com")
                                    node.InnerText = com;
                                if (node.Name == "com_baudtate")
                                    node.InnerText = com_baudtate;
                                if (node.Name == "databits")
                                    node.InnerText = databits;
                                if (node.Name == "par")
                                    node.InnerText = par;
                                if (node.Name == "stop")
                                    node.InnerText = stop;
                            }
                        }
                    }


                    doc.Save(path);
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

        private void ControlEquipment_Load(object sender, EventArgs e)
        {
            GetPortName();
            LoadXML(System.Windows.Forms.Application.StartupPath + "\\XML\\ParameterConfigurationControlEquipment.xml");//加载串口参数XML
        }

        /// <summary>
        /// 获得当前计算机的端口号
        /// </summary>
        void GetPortName()
        {
            List<string> list = new List<string>();

            foreach (string item in System.IO.Ports.SerialPort.GetPortNames())
            {
                list.Add(item);
            }
            com_PortMainBoard.DataSource = list;
        }

        void LoadXML(string path)
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
                XmlNodeList list = doc.SelectSingleNode("ParameterConfiguration").ChildNodes;//获得根节点user下的所有子节点
                foreach (XmlNode item in list)
                {
                    if (item.Name == "equipment1")
                    {
                        foreach (XmlNode node in item.ChildNodes)
                        {
                            if (node.Name == "com")
                                com_PortMainBoard.Text = node.InnerText;
                            if (node.Name == "com_baudtate")
                                com_baudRateMainBoard.Text = node.InnerText;
                            if (node.Name == "databits")
                                comb_Databits.Text = node.InnerText;
                            if (node.Name == "par")
                                comb_Parity.Text = node.InnerText;
                            if (node.Name == "stop")
                                comb_StopBits.Text = node.InnerText;
                        }
                    }
                }
            }
        }
    }
}
