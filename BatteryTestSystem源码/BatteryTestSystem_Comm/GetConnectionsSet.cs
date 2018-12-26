using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BatteryTestSystem_Comm
{
    public static class GetConnectionsSet
    {
        public static string StrConnection(string Path)
        {
            StringBuilder str = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            if (File.Exists(Path))
            {
                doc.Load(Path);
                XmlNodeList list = doc.SelectSingleNode("Connectionset").ChildNodes;//获得根节点user下的所有子节点
                foreach (XmlNode node in list)
                {
                    if (node.Name == "IPText")
                        str.Append("Data Source="+node.InnerText+";");
                    if (node.Name == "DataBaseName")
                        str.Append("Initial Catalog=" + node.InnerText + ";User ID=szjimi;PassWord=1234;");
                }
            }

            return str.ToString();
        }
    }
}
