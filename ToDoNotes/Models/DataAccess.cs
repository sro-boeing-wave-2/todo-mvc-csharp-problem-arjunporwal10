using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoNotes.Models
{
    public interface IDataAccess
    {
        IEnumerable<ToDo> GetNotes();
        IEnumerable<ToDo> GetNotesByQuery(bool? Ispinned = null, string title = "", string labelName = "");
        ToDo GetNote(ObjectId id);
        ToDo Create(ToDo p);
        void Update(ObjectId id, ToDo p);
        void Remove(ObjectId id);
    }
    public class DataAccess:IDataAccess
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;

        public DataAccess()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("ToDoNotes");
        }

        public IEnumerable<ToDo> GetNotes()
        {
            return _db.GetCollection<ToDo>("Notes").FindAll();
        }

        public IEnumerable<ToDo> GetNotesByQuery(bool? Ispinned = null, string title = "", string labelName = "")
        {
            return _db.GetCollection<ToDo>("Notes").FindAll().Where(
                m => ((title == "") || (m.Title == title)) && ((!Ispinned.HasValue) || (m.IsPinned == Ispinned)) && ((labelName == "") || (m.Labels).Any(b => b.LabelName == labelName)));
        }

        public ToDo GetNote(ObjectId id)
        {
            var res = Query<ToDo>.EQ(p => p.Id, id);
            return _db.GetCollection<ToDo>("Notes").FindOne(res);
        }

        public ToDo Create(ToDo p)
        {
            _db.GetCollection<ToDo>("Notes").Save(p);
            return p;
        }

        public void Update(ObjectId id, ToDo p)
        {
            p.Id = id;
            var res = Query<ToDo>.EQ(pd => pd.Id, id);
            var operation = Update<ToDo>.Replace(p);
            _db.GetCollection<ToDo>("Notes").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<ToDo>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<ToDo>("Notes").Remove(res);
        }
    }
}
