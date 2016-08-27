using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions.Sql;

namespace TinyLeon.Component.DataAccess
{
    public class SqlClient : BaseClient
    {
        public SqlClient(string configName)
        {
            string connectionStr = ConfigurationManager.AppSettings[configName];
            base.SetConnection(new SqlConnection(connectionStr));
            DapperExtensions.DapperExtensions.SqlDialect = new SqlServerDialect();
        }
    }
}
