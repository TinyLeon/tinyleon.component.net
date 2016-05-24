using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyLeon.Test.MongoDBTest
{
    public class MessageWithId : Message
    {
        public object _id { set; get; }
    }
}
