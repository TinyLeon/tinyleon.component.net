using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TinyLeon.Component.MongoDB.Collections
{
    /// <summary>
    /// 表示该对象是一个基础MongoDB集合
    /// </summary>
    /// <typeparam name="TDocument">集合文档的类型</typeparam>
    public interface IBaseCollection<TDocument>
    {
        /// <summary>
        /// 获取集合名
        /// </summary>
        string CollectionName { get; }
        /// <summary>
        /// 向该集合中插入一条文档
        /// </summary>
        /// <param name="document">要写入的文档</param>
        /// <returns>写入结果</returns>
        WriteConcernResult Insert(TDocument document);
        /// <summary>
        /// 指定插入选项向该集合中插入一条文档
        /// </summary>
        /// <param name="document">要写入的文档</param>
        /// <param name="options">插入选项</param>
        /// <returns>写入结果</returns>
        WriteConcernResult Insert(TDocument document, MongoInsertOptions options);
        /// <summary>
        /// 指定写入安全级别向该集合中插入一条文档
        /// </summary>
        /// <param name="document">要写入的文档</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>写入结果</returns>
        WriteConcernResult Insert(TDocument document, WriteConcern writeConcern);
        /// <summary>
        /// 向该集合中插入文档集合
        /// </summary>
        /// <param name="documents">要写入的文档集合</param>
        /// <returns>写入结果</returns>
        IEnumerable<WriteConcernResult> InsertBatch(IEnumerable<TDocument> documents);
        /// <summary>
        /// 指定插入选项向该集合中插入文档集合
        /// </summary>
        /// <param name="documents">要写入的文档集合</param>
        /// <param name="options">插入选项</param>
        /// <returns>写入结果</returns>
        IEnumerable<WriteConcernResult> InsertBatch(IEnumerable<TDocument> documents, MongoInsertOptions options);
        /// <summary>
        /// 指定写入安全级别向该集合中插入文档集合
        /// </summary>
        /// <param name="documents">要写入的文档集合</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>写入结果</returns>
        IEnumerable<WriteConcernResult> InsertBatch(IEnumerable<TDocument> documents, WriteConcern writeConcern);
        /// <summary>
        /// 根据指定查询条件从集合中移除一条数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>移除结果</returns>
        WriteConcernResult Remove(IMongoQuery query);
        /// <summary>
        /// 指定写入安全级别根据指定查询条件从集合中移除一条数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="writeConcern">写入安全级别</param>
        /// <returns>移除结果</returns>
        WriteConcernResult Remove(IMongoQuery query, WriteConcern writeConcern);
    }
}
