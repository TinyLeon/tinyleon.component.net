using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TinyLeon.Component.Utility
{
    public class XMLHelper
    {
        /// <summary>
        /// 创建包含指定数据的XML节点
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>包含指定数据的XML节点</returns>
        public static XmlNode CreateXmlCDataNode(string data)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNode nodeCData = xmldoc.CreateCDataSection(data);
            return nodeCData;
        }

        /// <summary>
        /// 获取指定节点的文本
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childNodeName">子节点</param>
        /// <returns>节点文本</returns>
        public static string GetNodeText(XmlNode parentNode, string childNodeName)
        {
            if (parentNode == null)
            {
                return string.Empty;
            }
            XmlNode node = parentNode.SelectSingleNode(childNodeName);
            if (node == null)
            {
                return string.Empty;
            }
            else
            {
                return node.InnerText;
            }
        }

        /// <summary>
        /// 获取指定节点的文本
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="nodeName">二级节点</param>
        /// <param name="childNodeName">子节点</param>
        /// <returns>节点文本</returns>
        public static string GetNodeText(XmlNode parentNode, string nodeName, string childNodeName)
        {
            if (parentNode == null)
            {
                return string.Empty;
            }
            XmlNode node = parentNode.SelectSingleNode(nodeName + "/" + childNodeName);
            if (node == null)
            {
                return string.Empty;
            }
            else
            {
                return node.InnerText;
            }
        }

        /// <summary>
        /// 获取指定节点的属性值
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childNodeName">子节点</param>
        /// <param name="attributeName">子节点的属性名称</param>
        /// <returns>节点的属性值</returns>
        public static string GetNodeAttribute(XmlNode parentNode, string childNodeName, string attributeName)
        {
            if (parentNode == null)
            {
                return string.Empty;
            }
            XmlNode node = parentNode.SelectSingleNode(childNodeName);
            if (node == null)
            {
                return string.Empty;
            }
            XmlAttribute attr = node.Attributes[attributeName];
            if (attr == null)
            {
                return string.Empty;
            }
            else
            {
                return attr.Value;
            }
        }

        /// <summary>
        /// 获取指定节点的属性值
        /// </summary>
        /// <param name="node">节点</param>
        /// <param name="attributeName">属性值</param>
        /// <returns>节点的属性值</returns>
        public static string GetNodeAttribute(XmlNode node, string attributeName)
        {
            if (node == null)
            {
                return string.Empty;
            }
            XmlAttribute attr = node.Attributes[attributeName];
            if (attr == null)
            {
                return string.Empty;
            }
            else
            {
                return attr.Value;
            }
        }

        /// <summary>
        /// 获取节点及其所有子节点的标记
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <param name="childNodeName">子节点</param>
        /// <returns>节点及其子节点标记字符串</returns>
        public static string GetNodeXml(XmlNode parentNode, string childNodeName)
        {
            if (parentNode == null)
            {
                return string.Empty;
            }
            XmlNode node = parentNode.SelectSingleNode(childNodeName);
            if (node == null)
            {
                return "<" + childNodeName + "/>";
            }
            else
            {
                return node.OuterXml;
            } 
        }

        /// <summary>
        /// 修剪CDATA
        /// </summary>
        /// <param name="data">传入的数据</param>
        /// <returns>修剪后的CDATA</returns>
        public static string TrimCData(string data)
        {
            string xmlass = string.Format("<data>{0}</data>", data);

            XmlDocument xmlDoc = new XmlDocument();
            if (!IsXmlValid(xmlass, ref xmlDoc))
            {
                return string.Empty;
            }
            return xmlDoc.SelectSingleNode("data").InnerText;
        }

        /// <summary>
        /// 验证Xml是否合法
        /// </summary>
        /// <param name="data">xml字符串</param>
        /// <param name="xmlDoc">返回的xml文档</param>
        /// <returns>是否合法，true：有效的xml文档，false：无效的xml文档</returns>
        public static bool IsXmlValid(string data, ref XmlDocument xmlDoc)
        {
            bool isValidData = false;
            if (String.IsNullOrWhiteSpace(data))
            {
                return false;
            }

            try
            {
                xmlDoc.LoadXml(data);
                isValidData = true;
            }
            catch
            {
                isValidData = false;
            }
            if (xmlDoc == null || xmlDoc.ChildNodes.Count < 1)
            {
                isValidData = false;
            }
            return isValidData;
        }
    }
}
