using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Component.MongoDB
{
    /// <summary>
    /// 基础mangoDB类
    /// </summary>
    public class BaseMongoDB
    {
        /// <summary>
        /// 根据MongoDB名从配置文件中读取信息创建一个通用的MongoDB访问器
        /// </summary>
        /// <param name="dbName">MongoDB名</param>
        public BaseMongoDB(string dbName)
        {
            this.DBName = dbName;
        }
        private MongoServer mongoServer;

        protected virtual MongoServer MongoServer
        {
            get
            {
                if (this.mongoServer == null)
                {
                    var connectionString = string.Empty;
                    foreach (Configuration.MongoDBServerConfiguration confServer in Configuration.MongoDBConfigurationCenter.Instance.Configuration.Servers.List)
                    {
                        if (confServer.Name.ToLower() == this.DBName.ToLower())
                        {
                            connectionString = confServer.ConnectionString;
                            break;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        throw new System.Configuration.ConfigurationErrorsException("没有有效的MongoDB地址");
                    }
                    var client = new MongoClient(connectionString);
                    this.mongoServer = client.GetServer();
                    if (this.mongoServer == null)
                    {
                        throw new Exception("初始化MongoDB错误");
                    }
                }
                return this.mongoServer;
            }
        }
        /// <summary>
        /// 获取MongoDB名
        /// </summary>
        public string DBName { private set; get; }
        /// <summary>
        /// 获取MongoDB数据集
        /// </summary>
        /// <returns>MongoDB数据集</returns>
        public MongoDatabase GetDatabase()
        {
            return this.MongoServer.GetDatabase(this.DBName);
        }
    }
}
