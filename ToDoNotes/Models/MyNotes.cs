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
        public bool? IsPinned { get; set; }
        public List<Label> Labels { get; set; }
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