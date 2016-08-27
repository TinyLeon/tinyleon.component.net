using MySql.Data.MySqlClient;
using System.Configuration;
using DapperExtensions.Sql;

namespace TinyLeon.Component.DataAccess
{
    public class MySqlClient : BaseClient
    {
        public MySqlClient(string configName)
        {
            string connectionStr = ConfigurationManager.AppSettings[configName];
            base.SetConnection(new MySqlConnection(connectionStr));
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
        }
    }
}
