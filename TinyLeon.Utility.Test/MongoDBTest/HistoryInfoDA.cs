using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyLeon.Component.MongoDB;
using TinyLeon.Component.MongoDB.Collections;

namespace TinyLeon.Test.MongoDBTest
{
    public class HistoryInfoDA: BaseCollection<MessageWithId, Message>
    {
        internal HistoryInfoDA(BaseMongoDB mongoDB, string collectionName)
            : base(mongoDB, collectionName)
        {
        }
    }
}
