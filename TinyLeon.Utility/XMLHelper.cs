using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TinyLeon.Component.Utility
{
    public class XMLHelper<T> where T : new()
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

        #region entity to xml
        /// <summary>
        /// 对象实例转成xml
        /// </summary>
        /// <param name="item">对象实例</param>
        /// <returns></returns>
        public static string EntityToXml(T item)
        {
            IList<T> items = new List<T>();
            items.Add(item);
            return EntityToXml(items);
        }

        /// <summary>
        /// 对象实例集转成xml
        /// </summary>
        /// <param name="items">对象实例集</param>
        /// <returns></returns>
        public static string EntityToXml(IList<T> items)
        {
            //创建XmlDocument文档
            XmlDocument doc = new XmlDocument();
            //创建根元素
            XmlElement root = doc.CreateElement(typeof(T).Name + "s");
            //添加根元素的子元素集
            foreach (T item in items)
            {
                EntityToXml(doc, root, item);
            }
            //向XmlDocument文档添加根元素
            doc.AppendChild(root);
            return doc.InnerXml;
        }

        private static void EntityToXml(XmlDocument doc, XmlElement root, T item)
        {
            //创建元素
            XmlElement xmlItem = doc.CreateElement(typeof(T).Name);
            //对象的属性集

            System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

            foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
            {
                if (pinfo != null)
                {
                    //对象属性名称
                    string name = pinfo.Name;
                    //对象属性值
                    string value = String.Empty;

                    if (pinfo.GetValue(item, null) != null)
                        value = pinfo.GetValue(item, null).ToString();//获取对象属性值
                    //设置元素的属性值
                    xmlItem.SetAttribute(name, value);
                }
            }
            //向根添加子元素
            root.AppendChild(xmlItem);
        }

        #endregion

        #region xml to entity

        /// <summary>
        /// Xml转成对象实例
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static T XmlToEntity(string xml)
        {
            IList<T> items = XmlToEntityList(xml);
            if (items != null && items.Count > 0)
                return items[0];
            else return default(T);
        }

        /// <summary>
        /// Xml转成对象实例集
        /// </summary>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        public static IList<T> XmlToEntityList(string xml)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch
            {
                return null;
            }
            if (doc.ChildNodes.Count != 1)
                return null;
            if (doc.ChildNodes[0].Name.ToLower() != typeof(T).Name.ToLower() + "s")
                return null;

            XmlNode node = doc.ChildNodes[0];

            IList<T> items = new List<T>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == typeof(T).Name.ToLower())
                    items.Add(XmlNodeToEntity(child));
            }

            return items;
        }

        private static T XmlNodeToEntity(XmlNode node)
        {
            T item = new T();

            if (node.NodeType == XmlNodeType.Element)
            {
                XmlElement element = (XmlElement)node;

                System.Reflection.PropertyInfo[] propertyInfo =
            typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

                foreach (XmlAttribute attr in element.Attributes)
                {
                    string attrName = attr.Name.ToLower();
                    string attrValue = attr.Value.ToString();
                    foreach (System.Reflection.PropertyInfo pinfo in propertyInfo)
                    {
                        if (pinfo != null)
                        {
                            string name = pinfo.Name.ToLower();
                            Type dbType = pinfo.PropertyType;
                            if (name == attrName)
                            {
                                if (String.IsNullOrEmpty(attrValue))
                                    continue;
                                switch (dbType.ToString())
                                {
                                    case "System.Int32":
                                        pinfo.SetValue(item, Convert.ToInt32(attrValue), null);
                                        break;
                                    case "System.Boolean":
                                        pinfo.SetValue(item, Convert.ToBoolean(attrValue), null);
                                        break;
                                    case "System.DateTime":
                                        pinfo.SetValue(item, Convert.ToDateTime(attrValue), null);
                                        break;
                                    case "System.Decimal":
                                        pinfo.SetValue(item, Convert.ToDecimal(attrValue), null);
                                        break;
                                    case "System.Double":
                                        pinfo.SetValue(item, Convert.ToDouble(attrValue), null);
                                        break;
                                    default:
                                        pinfo.SetValue(item, attrValue, null);
                                        break;
                                }
                                continue;
                            }
                        }
                    }
                }
            }
            return item;
        }

        #endregion
    }
}
