using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TinyLeon.Test.MongoDBTest
{
    [Serializable]
    public class Message
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        [XmlArray("content")]
        [XmlArrayItem("roaming_message_item")]
        public List<RoamingMessageItem> Content { get; set; }

        /// <summary>
        /// 发送方
        /// </summary>
        [XmlElement("from_id")]
        public OpenImUser FromId { get; set; }

        /// <summary>
        /// 消息时间，UTC时间
        /// </summary>
        [XmlElement("time")]
        public long Time { get; set; }

        /// <summary>
        /// 接收方
        /// </summary>
        [XmlElement("to_id")]
        public OpenImUser ToId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [XmlElement("type")]
        public long Type { get; set; }

        /// <summary>
        /// 消息UUID
        /// </summary>
        [XmlElement("uuid")]
        public long Uuid { get; set; }
    }

    /// <summary>
    /// RoamingMessageItem Data Structure.
    /// </summary>
    [Serializable]
    public class RoamingMessageItem
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        [XmlElement("type")]
        public string Type { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [XmlElement("value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// OpenImUser Data Structure.
    /// </summary>
    [Serializable]
    public class OpenImUser
    {
        /// <summary>
        /// 账户appkey
        /// </summary>
        [XmlElement("app_key")]
        public string AppKey { get; set; }

        /// <summary>
        /// 是否为淘宝账号
        /// </summary>
        [XmlElement("taobao_account")]
        public bool TaobaoAccount { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        [XmlElement("uid")]
        public string Uid { get; set; }
    }
}
