using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace MongoDBConnection
{
    /// <summary>
    /// 官方驱动
    /// </summary>
    class Program
    {
        //数据库连接字符串
        const string strconn = "mongodb://127.0.0.1:27017";
        //数据库名称
        const string dbName = "test";
        static void Main(string[] args)
        {

            Insert();
           
        }
               
        public static void Insert()
        {
            MongoClient client = new MongoClient(strconn);
            client.GetServer();
            //创建数据库链接
            MongoServer server = client.GetServer();// MongoDB.Driver.MongoServer.Create(strconn);
            //获得数据库
            MongoDatabase db = server.GetDatabase(dbName);

            Persons person = new Persons();
            person.name = "44444";
            person.age = 24;
            MongoCollection col = db.GetCollection("person");
            col.Insert<Persons>(person);
        }
    }
}
