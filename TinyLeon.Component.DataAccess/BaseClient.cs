using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using DapperExtensions;
using DapperExtensions.Sql;

namespace TinyLeon.Component.DataAccess
{
    public abstract class BaseClient
    {
        private IDbConnection conn = null;

        protected void SetConnection(IDbConnection conn)
        {
            this.conn = conn;
        }

        public IDbConnection GetConnection()
        {
            if (conn == null)
                throw new Exception("尚未连接数据库，请检查连接字符串");
            return conn;
        }

        public BaseClient()
        {

        }

        public List<T> Query<T>(string sql, object obj = null, IDbTransaction transaction = null) where T : class
        {
            return conn.Query<T>(sql, obj, transaction).ToList();
        }

        public List<T> QueryByPage<T>(string sql, int pageIndex, int pageSize, object obj = null, IList<ISort> sortRules = null, IDbTransaction transaction = null) where T : class
        {
            return DapperExtensions.DapperExtensions.GetPage<T>(conn, sql, obj, sortRules, pageIndex, pageSize, transaction).ToList();
        }

        public List<T> QueryByPage<T>(int pageIndex, int pageSize, object obj = null, IList<ISort> sortRules = null, IDbTransaction transaction = null) where T : class
        {
            return DapperExtensions.DapperExtensions.GetPage<T>(conn, obj, sortRules, pageIndex, pageSize, transaction).ToList();
        }

        public T GetEntityById<T>(object id, IDbTransaction transaction = null) where T : class
        {
            return DapperExtensions.DapperExtensions.Get<T>(conn, id, transaction);
        }

        public int Insert<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            return conn.Insert<T>(entity, transaction);
        }
        public bool Insert<T>(List<T> entities, IDbTransaction transaction = null) where T : class
        {
            DapperExtensions.DapperExtensions.Insert<T>(conn, entities, transaction);
            return true;
        }

        public bool Update<T>(T entity, IDbTransaction transaction = null) where T : class
        {
            return DapperExtensions.DapperExtensions.Update<T>(conn, entity, transaction);
        }

        public int ExcuteNonQuery(string sql, object obj = null, IDbTransaction transaction = null)
        {
            return conn.Execute(sql, obj, transaction);
        }
    }
}
