using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TinyLeon.Test.MongoDBTest;
using TinyLeon.Utility;

namespace TinyLeon.Utility.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadDataFromMongo();
            //DistinctTest();
            EntityToDataTable();
        }

        private static void Jsonserialize()
        {
            Person p = new Person
            {
                Name = "张三",
                Age = 1,
                BirthDate = DateTime.Now
            };

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("name", "张三");
            dic.Add("age", "2");


            string dicStr = JsonHelper.JsonSerializer(dic);


            dic = JsonHelper.JsonDeserialize<Dictionary<string, string>>(dicStr);

            string jsonStr = JsonHelper.JsonSerializer<Person>(p);
            string jsonString = "{\"Age\":2,\"Name\":\"李四\",\"BirthDate\":\"2011-01-09 00:30:00\"}";
            p = JsonHelper.JsonDeserialize<Person>(jsonString);
            Console.WriteLine(jsonStr);
        }

        private static void ReadDataFromMongo()
        {
            var historyList = HistoryInfoInstance.Instance.historyInfoDA.Find(
                    MongoDB.Driver.Builders.Query.And(
                    MongoDB.Driver.Builders.Query.EQ("ToId.Uid", "52aabc9bbd70fda20f4a49cbbbaada72")
                    )).SetLimit(2).ToList();
            Console.WriteLine(string.Format("读出{0}条数据", historyList.Count));
        }

        private static IList<Person> DistinctTest()
        {
            List<Person> personList = new List<Person>();
            personList.Add(new Person { Name = "1", Age = 1 });
            personList.Add(new Person { Name = "2", Age = 1 });
            personList.Add(new Person { Name = "3", Age = 1 });
            personList.Add(new Person { Name = "1", Age = 2 });
            var noDuplicatedList = personList.Distinct(p => p.Age).ToList();
            return noDuplicatedList;
        }

        private static void EntityToDataTable()
        {
            List<Person> personList = new List<Person>();
            personList.Add(new Person { Name = "1", Age = 1 });
            personList.Add(new Person { Name = "2", Age = 1 });
            personList.Add(new Person { Name = "3", Age = 1 });
            personList.Add(new Person { Name = "1", Age = 2 });
            ExcelHelper eh = new ExcelHelper(@"E:\myExcel.xlsx");
            DataTable dt = eh.EntityToDataTable<Person>(personList);
            eh.DataTableToExcel(dt, "Person", true);
        }
    }

    [DataContractAttribute]
    class Person
    {
        [DataMemberAttribute]
        public string Name { get; set; }
        [DataMemberAttribute]
        public int Age { get; set; }
        [DataMemberAttribute]
        public DateTime BirthDate { get; set; }
        public List<string> ClotheColor { get; set; }
    }
}
