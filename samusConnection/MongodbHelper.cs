using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Configuration;
using MongoDB.Linq;
using MongoDB.Driver;


namespace samusConnection
{
    /// <summary>
    /// 第三方的驱动
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongodbHelper<T> where T:class
    {
        string connectionString = string.Empty;
        string databaseName = string.Empty;
        string collectionName = string.Empty;
        static MongodbHelper<T> mongodb;

        public MongodbHelper()
        {
            connectionString = "Server=127.0.0.1:27017";
            databaseName = "test";
            collectionName = "person";
        }


        #region 实现linq查询的映射配置
        /// <summary>
        /// 实现linq查询的映射配置
        /// </summary>
        public MongoConfiguration configuration
        {
            get
            {
                var config = new MongoConfigurationBuilder();

                config.Mapping(mapping =>
                {
                    mapping.DefaultProfile(profile =>
                    {
                        profile.SubClassesAre(t => t.IsSubclassOf(typeof(T)));
                    });
                    mapping.Map<T>();
                    mapping.Map<T>();
                });

                config.ConnectionString(connectionString);

                return config.BuildConfiguration();
            }
        }
        #endregion

        #region 插入操作
        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Insert(T t)
        {
            using (MongoDB.Mongo mongo = new MongoDB.Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(collectionName);

                    collection.Insert(t, true);

                    mongo.Disconnect();

                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }
        #endregion

        #region 更新操作
        /// <summary>
        /// 更新操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Update(T t, Expression<Func<T, bool>> func)
        {
            using (MongoDB.Mongo mongo = new MongoDB.Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(collectionName);

                    collection.Update<T>(t, func, true);

                    mongo.Disconnect();

                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }
        #endregion

        #region 获取集合
        /// <summary>
        ///获取集合
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public List<T> List(int pageIndex, int pageSize, Expression<Func<T, bool>> func, out int pageCount)
        {
            pageCount = 0;

            using (MongoDB.Mongo mongo = new MongoDB.Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(collectionName);

                    pageCount = Convert.ToInt32(collection.Count());

                    var personList = collection.Linq().Where(func).Skip(pageSize * (pageIndex - 1))
                                                   .Take(pageSize).Select(i => i).ToList();

                    mongo.Disconnect();

                    return personList;

                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }
        #endregion

        #region 读取单条记录
        /// <summary>
        ///读取单条记录
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> func)
        {
            using (MongoDB.Mongo mongo = new MongoDB.Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(collectionName);

                    var single = collection.Linq().FirstOrDefault(func);

                    mongo.Disconnect();

                    return single;

                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }
        #endregion

        #region 删除操作
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public void Delete(Expression<Func<T, bool>> func)
        {
            using (MongoDB.Mongo mongo = new MongoDB.Mongo(configuration))
            {
                try
                {
                    mongo.Connect();

                    var db = mongo.GetDatabase(databaseName);

                    var collection = db.GetCollection<T>(collectionName);

                    //这个地方要注意，一定要加上T参数，否则会当作object类型处理
                    //导致删除失败
                    collection.Remove<T>(func);

                    mongo.Disconnect();

                }
                catch (Exception)
                {
                    mongo.Disconnect();
                    throw;
                }
            }
        }
        #endregion

    }
}
