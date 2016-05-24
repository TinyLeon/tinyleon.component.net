using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TinyLeon.Component.MongoDB.Configuration
{
    /// <summary>
    /// MongoDB服务器节点
    /// </summary>
    public class MongoDBServerConfiguration : ConfigurationElement
    {
        /// <summary>
        /// 主机名节点名
        /// </summary>
        public const string SectionName = "name";
        /// <summary>
        /// 主机地址节点名
        /// </summary>
        public const string SectionConnectionString = "connectionString";

        /// <summary>
        /// 设置或获取主机名
        /// </summary>
        [ConfigurationProperty(SectionName, IsRequired = true)]
        public string Name
        {
            get { return this[SectionName].ToString(); }
        }

        /// <summary>
        /// 设置或获取主机地址
        /// </summary>
        [ConfigurationProperty(SectionConnectionString, IsRequired = true)]
        public string ConnectionString
        {
            get { return this[SectionConnectionString].ToString(); }
        }
    }
}
