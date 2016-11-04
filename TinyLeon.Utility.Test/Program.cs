using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TinyLeon.Test.MongoDBTest;
using TinyLeon.Component.Utility;
using TinyLeon.Component.DataAccess;
using System.Data.SqlClient;
using DapperExtensions;
using System.IO;

namespace TinyLeon.Utility.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReadDataFromMongo();
            //DistinctTest();
            //EntityToDataTable();
            //var list = TestEnum.Apple.GetSelectItemList();
            //string des = EnumHelper.GetEnumByDescription<TestEnum>("梨子").GetEnumDes();

            EncryptionTest();
            //DataAccessTest();
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

        public static void EncryptionTest()
        {
            string plainText = "pp47026791";
            string cipherText = EncryptHelper.AESEncrypt(plainText);
            plainText = EncryptHelper.AESDecrypt(cipherText);
        }

        private static void DataAccessTest()
        {
            //SqlClient client = new SqlClient("SqlServerConnectionStr");

            //IList<ISort> sortRules = new List<ISort> { new Sort { PropertyName = "IMUMemberId", Ascending = true } };

            //string sql = "SELECT IMUId,IMUMemberId FROM IMUserInfo WITH(NOLOCK) WHERE IMUId<@IMUId";
            //List<IMUserInfo> ressult = client.QueryByPage<IMUserInfo>(sql, 0, 10, new { IMUId = 3 }, sortRules);
            //IMUserInfo entity = client.GetEntityById<IMUserInfo>(3);

            BaseClient myClient = new MySqlClient("MySqlConnectionStr");
            //IList<ISort> mySortRules = new List<ISort> { new Sort { PropertyName = "Id", Ascending = false } };
            string sql = "DELETE FROM PersonInfo WHERE Id=@Id";
            //var list = myClient.QueryByPage<PersonInfo>(sql, 0, 5, new { Id = 5 }, mySortRules);
            //var entity = myClient.GetEntityById<PersonInfo>(5);
            //PersonInfo pi = new PersonInfo { Id = 10, Name = "Leon100" };
            //int isSuccess = myClient.ExcuteNonQuery(sql, new { Id = 9 });

            //transaction
            myClient.GetConnection().Open();
            IDbTransaction tran = myClient.GetConnection().BeginTransaction();
            try
            {
                int isSuccess1 = myClient.ExcuteNonQuery(sql, new { Id = 1 }, tran);
                int isSuccess2 = myClient.ExcuteNonQuery(sql, new { Id = 2 }, tran);
                int isSuccess3 = myClient.ExcuteNonQuery(sql, new { Id = 3 }, tran);
                throw new Exception("执行不下去了");
            }
            catch (Exception e)
            {
                tran.Rollback();
                myClient.GetConnection().Close();
                return;
            }
            tran.Commit();
            myClient.GetConnection().Close();
        }

        private static void IOHelperTest()
        {
            Stream stream = IOHelper.FileToStream(@"E:\allTravelConsultant.txt");
            IOHelper.StreamToFile(stream, @"E:\test.txt");
            byte[] bytes = IOHelper.ConvertStreamToBytes(stream);
            string base64Str = IOHelper.ConvertBytesToBase64Str(bytes);
            IOHelper.WriteContentIntoTxt(new List<string> { base64Str }, @"E:\test.txt", false);
            List<string> res = IOHelper.ReadContentFromTxt(@"E:\test.txt");
        }
    }

    class PersonInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
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

    enum TestEnum
    {
        [Description("苹果")]
        Apple = 1,
        [Description("橙子")]
        Orange = 2
    }

    /// <summary>
    /// 实体类IMUserInfo
    /// </summary>
    [Serializable]
    public class IMUserInfo
    {
        #region Field
        private long _imuid = 0;
        private string _imuuserid = "";
        private string _imupassword = "";
        private string _imuname = "";
        private string _imunick = "";
        private string _imuiconurl = "";
        private long _imumemberid = 0;
        private string _imumemberloginname = "";
        private Int32 _imuusertype = 0;
        private DateTime _imucreatetime = new DateTime(1900, 1, 1);
        private string _imucreatorid = "";
        private string _imucreatorname = "";
        private DateTime _imumodifytime = new DateTime(1900, 1, 1);
        private string _imumodifierid = "";
        private string _imumodifiername = "";
        private Int32 _imuisdel = 0;
        #endregion Field

        #region Construct
        public IMUserInfo()
        { }
        #endregion Construct

        #region Property
        /// <summary>
        /// 主键
        /// </summary>
        public long IMUId
        {
            set { _imuid = value; }
            get { return _imuid; }
        }
        /// <summary>
        /// 百川IM登录名
        /// </summary>
        public string IMUUserId
        {
            set { _imuuserid = value; }
            get { return _imuuserid; }
        }
        /// <summary>
        /// 百川IM密码
        /// </summary>
        public string IMUPassword
        {
            set { _imupassword = value; }
            get { return _imupassword; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string IMUName
        {
            set { _imuname = value; }
            get { return _imuname; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string IMUNick
        {
            set { _imunick = value; }
            get { return _imunick; }
        }
        /// <summary>
        /// 头像URL
        /// </summary>
        public string IMUIconUrl
        {
            set { _imuiconurl = value; }
            get { return _imuiconurl; }
        }
        /// <summary>
        /// 同程会员ID
        /// </summary>
        public long IMUMemberId
        {
            set { _imumemberid = value; }
            get { return _imumemberid; }
        }
        /// <summary>
        /// 同程登录账号
        /// </summary>
        public string IMUMemberLoginName
        {
            set { _imumemberloginname = value; }
            get { return _imumemberloginname; }
        }
        /// <summary>
        /// 会员类型：1.普通会员 2.旅游顾问
        /// </summary>
        public Int32 IMUUserType
        {
            set { _imuusertype = value; }
            get { return _imuusertype; }
        }
        /// <summary>
        /// 常驻地
        /// </summary>
        public string IMUResidence { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime IMUBirth { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string IMUMobile { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int IMUGender { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime IMUCreateTime
        {
            set { _imucreatetime = value; }
            get { return _imucreatetime; }
        }
        /// <summary>
        /// 创建人工号
        /// </summary>
        public string IMUCreatorId
        {
            set { _imucreatorid = value; }
            get { return _imucreatorid; }
        }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        public string IMUCreatorName
        {
            set { _imucreatorname = value; }
            get { return _imucreatorname; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime IMUModifyTime
        {
            set { _imumodifytime = value; }
            get { return _imumodifytime; }
        }
        /// <summary>
        /// 修改人工号
        /// </summary>
        public string IMUModifierId
        {
            set { _imumodifierid = value; }
            get { return _imumodifierid; }
        }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string IMUModifierName
        {
            set { _imumodifiername = value; }
            get { return _imumodifiername; }
        }
        /// <summary>
        /// 删除标记
        /// </summary>
        public Int32 IMUIsDel
        {
            set { _imuisdel = value; }
            get { return _imuisdel; }
        }
        #endregion Property

        #region Get Insert SqlParameter
        /// <summary>
        /// 获取将此实体插入数据库的SqlParameter参数
        /// </summary>
        public SqlParameter[] ToSqlParameters()
        {
            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@IMUId", IMUId));
            paramList.Add(new SqlParameter("@IMUUserId", IMUUserId));
            paramList.Add(new SqlParameter("@IMUPassword", IMUPassword));
            paramList.Add(new SqlParameter("@IMUName", IMUName));
            paramList.Add(new SqlParameter("@IMUNick", IMUNick));
            paramList.Add(new SqlParameter("@IMUIconUrl", IMUIconUrl));
            paramList.Add(new SqlParameter("@IMUMemberId", IMUMemberId));
            paramList.Add(new SqlParameter("@IMUMemberLoginName", IMUMemberLoginName));
            paramList.Add(new SqlParameter("@IMUUserType", IMUUserType));
            paramList.Add(new SqlParameter("@IMUCreateTime", IMUCreateTime));
            paramList.Add(new SqlParameter("@IMUCreatorId", IMUCreatorId));
            paramList.Add(new SqlParameter("@IMUCreatorName", IMUCreatorName));
            paramList.Add(new SqlParameter("@IMUModifyTime", IMUModifyTime));
            paramList.Add(new SqlParameter("@IMUModifierId", IMUModifierId));
            paramList.Add(new SqlParameter("@IMUModifierName", IMUModifierName));
            paramList.Add(new SqlParameter("@IMUIsDel", IMUIsDel));
            return paramList.ToArray();
        }
        #endregion Get Insert SqlParameter

    }
}
