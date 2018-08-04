using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public bool IsPinned { get; set; }
        public List<Label> Labels { get; set; }
        public List<Checklist> CheckLists { get; set; }

    }
    public class Label
    {
        public int Id { get; set; }
        public string LabelName { get; set; }
    }
    public class Checklist
    {
        public int Id { get; set; }
        public string ChecklistData { get; set; }
        public bool IsChecked { get; set; }
    }
}