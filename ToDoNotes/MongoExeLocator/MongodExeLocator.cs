using MongoDB.Testing.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoNotes.MongoExeLocator
{
    public class MongodExeLocator : IMongoExeLocator
    {
        public string Locate()
        {
            return @"C:\Program Files\MongoDB\Server\4.0\bin\mongod.exe";
        }
    }
}
