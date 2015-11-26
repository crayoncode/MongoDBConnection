using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samusConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            //MongodbHelper<Person> helper = new MongodbHelper<Person>();

            ////插入1000条数据
            //for (int i = 0; i < 10; i++)
            //{
            //    helper.Insert(new Person()
            //    {
            //        ID = Guid.NewGuid().ToString(),
            //        Name = "jack" + i,
            //        Age = i,
            //        CreateTime = DateTime.Now
            //    });
            //}

            //Console.WriteLine("插入成功");



            MongodbHelper<Person> helper = new MongodbHelper<Person>();

            //修改jack941改成mary
            var single = helper.Single(i => i.Name == "jack9");
            single.Name = "mary";
            helper.Update(single, i => i.ID == single.ID);

            Console.WriteLine("修改成功");
           
            Console.Read();
        }
    }
}
