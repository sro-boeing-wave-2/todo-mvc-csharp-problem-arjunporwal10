using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class ToDo
    {
        public ObjectId Id { get; set; }
        [BsonElement("NotesId")]
        public int NotesId { get; set; }
        [BsonElement("Text")]
        public string Text { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("IsPinned")]
        public bool? IsPinned { get; set; }
        [BsonElement("Labels")]
        public List<Label> Labels { get; set; }
        [BsonElement("CheckLists")]
        public List<Checklist> CheckLists { get; set; }
        public bool IsEquals(ToDo n)
        {

            if (Title == n.Title && Text == n.Text && IsPinned == n.IsPinned && Labels.All(x => n.Labels.Exists(y => y.LabelName == x.LabelName)) && CheckLists.All(x => n.CheckLists.Exists(y => (y.ChecklistData == x.ChecklistData && y.IsChecked == x.IsChecked))))
                return true;

            return false;


        }

    }
    public class Label
    {
        [BsonElement("Id")]
        public int Id { get; set; }
        [BsonElement("LabelName")]
        public string LabelName { get; set; }
    }
    public class Checklist
    {
        [BsonElement("Id")]
        public int Id { get; set; }
        [BsonElement("ChecklistData")]
        public string ChecklistData { get; set; }
        [BsonElement("IsChecked")]
        public bool IsChecked { get; set; }
    }
}