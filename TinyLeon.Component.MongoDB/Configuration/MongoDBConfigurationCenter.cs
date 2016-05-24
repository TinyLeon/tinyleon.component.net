using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.MongoDB.Configuration
{
    class MongoDBConfigurationCenter
    {
        private static object syncLock = new object();
        private static MongoDBConfigurationCenter _Instance = null;
        /// <summary>
        /// 获取单例实例
        /// </summary>
        public static MongoDBConfigurationCenter Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (syncLock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new MongoDBConfigurationCenter();
                        }
                    }
                }
                return _Instance;
            }
        }

        private MongoDBConfigurationCenter()
        {
            try
            {
                this.Configuration = (MongoDBConfiguration)ConfigurationManager.GetSection("mongodb");
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("初始化MongoDB配置出错", ex);
            }
        }
        /// <summary>
        /// 获取系统加载器配置节点
        /// </summary>
        public MongoDBConfiguration Configuration { private set; get; }
    }
}
