using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyLeon.Component.MongoDB;

namespace TinyLeon.Test.MongoDBTest
{
    public class HistoryInfoInstance : BaseMongoDB
    {
        private static HistoryInfoInstance _instance = null;
        private static object syncLock = new object();
        public static HistoryInfoInstance Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new HistoryInfoInstance();
                        }
                    }
                }
                return _instance;
            }
        }

        private HistoryInfoInstance()
            : base("TinyLeonHistoryInfo")
        {
            this.historyInfoDA = new HistoryInfoDA(this, "TinyLeonHistoryInfo");
        }

        public HistoryInfoDA historyInfoDA { private set; get; }
    }
}
