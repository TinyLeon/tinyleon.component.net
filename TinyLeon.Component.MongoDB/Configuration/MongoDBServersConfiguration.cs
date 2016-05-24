using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace TinyLeon.Component.MongoDB.Configuration
{
    public class MongoDBServersConfiguration : ConfigurationElement
    {
        /// <summary>
        /// 获取所有MongoDB服务器节点
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public MongoDBServerConfigurationCollection List
        {
            get { return (MongoDBServerConfigurationCollection)this[""]; }
        }
    }
}
