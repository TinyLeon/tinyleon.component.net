using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyLeon.Component.MongoDB.Collections
{
    /// <summary>
    /// 基础MongoDB集合类
    /// </summary>
    /// <typeparam name="TDocumentWithID">带有_id的集合文档类型</typeparam>
    /// <typeparam name="TDocument">不带_id的集合文档类型</typeparam>
    public abstract class BaseCollection<TDocumentWithID, TDocument> : IBaseCollection<TDocument>
        where TDocumentWithID : TDocument
    {
        /// <summary>
        /// MongoDB集合
        /// </summary>
        protected MongoCollection collection;
        /// <summary>
        /// 创建一个基础MongoDB集合
        /// </summary>
        /// <param name="mongoDB">基础MongoDB对象</param>
        /// <param name="collectionName">集合名</param>
        protected BaseCollection(BaseMongoDB mongoDB, string collectionName)
        {
            if (mongoDB == null)
            {
                throw new ArgumentNullException("必须指定一个有效的MongoServer");
            }
            this.CollectionName = collectionName;
            this.collection = mongoDB.GetDatabase().GetCollection<TDocumentWithID>(this.CollectionName);
        }
        /// <summary>
        /// 获取集合名
        /// </summary>
        public string CollectionName { private set; get; }
        /// <summary>
        /// 从集合中查询数据结果集
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询的结果集游标</returns>
        public virtual MongoCursor<TDocumentWithID> Find(IMongoQuery query)
        {
            return this.collection.FindAs<TDocumentWithID>(query);
        }
        /// <summary>
        /// 从集合中查询一条数据
        /// </summary>
        /// <returns>查询结果数据</returns>
        public virtual TDocumentWithID FindOne()
        {
            return this.collection.FindOneAs<TDocumentWithID>();
        }
        /// <summary>
        /// 根据指定查询条件从集合中查询一条数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询结果数据</returns>
        public virtual TDocumentWithID FindOne(IMongoQuery query)
        {
            return this.collection.FindOneAs<TDocumentWithID>(query);
        }
        /// <summary>
        /// 使用文档ID从集合中查询一条数据
        /// </summary>
        /// <param name="id">文档ID</param>
        /// <returns>查询结果数据</returns>
        public virtual TDocumentWithID FindOneById(BsonValue id)
        {
            return this.collection.FindOneByIdAs<TDocumentWithID>(id);
        }
        /// <summary>
        /// 向该集合中插入一条文档
        /// </summary>
        /// <param name="document">要写入的文档</param>
        /// <returns>写入结果</returns>
        public virtual WriteConcernResult Insert(TDocument document)
        {
            return this.collection.Insert(document);
        }
        /// <summary>
        /// 指定插入选项向该集合中插入一条文档
        /// </summary>
        /// <param name="document">要写入的文档</param>
        /// <param name="options">插入选项</param>
        /// <returns>写入结果</returns>
        public virtual WriteConcernResult Insert(TDocument document, MongoInsertOptions options)
        {
            return this.collection.Insert(document, options);
        }
        /// <summary>
        /// 指定写入安全级别向该集合中插入一条文档
        /// </summary>
        /// <param name="document">要写入的文档</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>写入结果</returns>
        public virtual WriteConcernResult Insert(TDocument document, WriteConcern writeConcern)
        {
            return this.collection.Insert(document, writeConcern);
        }
        /// <summary>
        /// 向该集合中插入文档集合
        /// </summary>
        /// <param name="documents">要写入的文档集合</param>
        /// <returns>写入结果</returns>
        public virtual IEnumerable<WriteConcernResult> InsertBatch(IEnumerable<TDocument> documents)
        {
            return this.collection.InsertBatch(documents);
        }
        /// <summary>
        /// 指定插入选项向该集合中插入文档集合
        /// </summary>
        /// <param name="documents">要写入的文档集合</param>
        /// <param name="options">插入选项</param>
        /// <returns>写入结果</returns>
        public virtual IEnumerable<WriteConcernResult> InsertBatch(IEnumerable<TDocument> documents, MongoInsertOptions options)
        {
            return this.collection.InsertBatch(documents, options);
        }
        /// <summary>
        /// 指定写入安全级别向该集合中插入文档集合
        /// </summary>
        /// <param name="documents">要写入的文档集合</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>写入结果</returns>
        public virtual IEnumerable<WriteConcernResult> InsertBatch(IEnumerable<TDocument> documents, WriteConcern writeConcern)
        {
            return this.collection.InsertBatch(documents, writeConcern);
        }
        /// <summary>
        /// 向该集合中保存一条文档
        /// </summary>
        /// <param name="document">要保存的文档</param>
        /// <returns>保存结果</returns>
        public virtual WriteConcernResult Save(TDocumentWithID document)
        {
            return this.collection.Save(document);
        }
        /// <summary>
        /// 指定插入选项向该集合中保存一条文档
        /// </summary>
        /// <param name="document">要保存的文档</param>
        /// <param name="options">插入选项</param>
        /// <returns>保存结果</returns>
        public virtual WriteConcernResult Save(TDocumentWithID document, MongoInsertOptions options)
        {
            return this.collection.Save(document, options);
        }
        /// <summary>
        /// 指定写入安全级别向该集合中保存一条文档
        /// </summary>
        /// <param name="document">要保存的文档</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>保存结果</returns>
        public virtual WriteConcernResult Save(TDocumentWithID document, WriteConcern writeConcern)
        {
            return this.collection.Save(document, writeConcern);
        }
        /// <summary>
        /// 根据指定查询条件从集合中移除一条数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>移除结果</returns>
        public virtual WriteConcernResult Remove(IMongoQuery query)
        {
            return this.collection.Remove(query);
        }
        /// <summary>
        /// 指定写入安全级别根据指定查询条件从集合中移除一条数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>移除结果</returns>
        public virtual WriteConcernResult Remove(IMongoQuery query, WriteConcern writeConcern)
        {
            return this.collection.Remove(query, writeConcern);
        }
    }
}
