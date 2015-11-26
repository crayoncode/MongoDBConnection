using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoDBConnection
{
    public class Persons
    {
        public ObjectId _id;
        public string name { get; set; }
        public int age { get; set; }

    }
}
