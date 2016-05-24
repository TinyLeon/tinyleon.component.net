using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.MongoDB.Configuration
{
    public class MongoDBConfiguration : ConfigurationSection
    {
        /// <summary>
        /// 服务器集合节点名
        /// </summary>
        public const string SectionServers = "servers";
        /// <summary>
        /// 获取服务器集合节点名
        /// </summary>
        [ConfigurationProperty(SectionServers, IsRequired = true)]
        public MongoDBServersConfiguration Servers
        {
            get { return (MongoDBServersConfiguration)this[SectionServers]; }
        }
    }
}
