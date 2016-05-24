using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TinyLeon.Component.MongoDB.Configuration
{
    /// <summary>
    /// MongoDB服务器节点配置
    /// </summary>
    public class MongoDBServerConfigurationCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 创建新的MongoDB服务器节点
        /// </summary>
        /// <returns>新的MongoDB服务器节点</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new MongoDBServerConfiguration();
        }
        /// <summary>
        /// 获取服务器节点的服务器名
        /// </summary>
        /// <param name="element">服务器节点</param>
        /// <returns>服务器节点的服务器名</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MongoDBServerConfiguration)element).Name;
        }

        /// <summary>
        /// 获取MongoDB主机节点的类型名
        /// </summary>
        protected override string ElementName { get { return "server"; } }
        /// <summary>
        /// 获取集合的类型
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType { get { return ConfigurationElementCollectionType.BasicMap; } }
    }
}
