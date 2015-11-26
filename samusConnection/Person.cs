using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Attributes;

namespace samusConnection
{
    public class Person
    {
        [MongoAlias("_id")]
        public string ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
