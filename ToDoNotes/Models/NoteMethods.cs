using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoNotes.Models;

namespace ToDoNotes.Models
{
    //Interface created for Notes
    public interface INoteService
    {
        IEnumerable<ToDo> GetAll();
        ToDo Get(int id);
        ToDo Add(ToDo note);
        void Update(int id, ToDo note);
        void Delete(int id);
    }
    // This class is implementing the methods of interface INoteService
    // Need to upgrade these methods 
    public class NoteService : INoteService
    {
        private List<ToDo> Notes { get; set; }

        public NoteService()
        {
            //var i = 0;
            //Notes = A.ListOf<ToDo>(50);
            //Notes.ForEach(note =>
            //{
            //    i++;
            //    note.Id = i;
            //});
        }

        public IEnumerable<ToDo> GetAll()
        {
            return Notes;
        }

        public ToDo Get(int id)
        {
            return Notes.First(_ => _.Id == id);
        }

        public ToDo Add(ToDo note)
        {
            var newid = Notes.OrderBy(_ => _.Id).Last().Id + 1;
            note.Id = newid;

            Notes.Add(note);

            return note;
        }

        public void Update(int id, ToDo note)
        {
            var existing = Notes.First(_ => _.Id == id);
            existing.Text = note.Text;
            existing.Title = note.Title;
            existing.IsPinned = note.IsPinned;
            existing.Labels = note.Labels;
            existing.CheckLists = note.CheckLists;
            
        }

        public void Delete(int id)
        {
            var existing = Notes.First(_ => _.Id == id);
            Notes.Remove(existing);
        }
       
    }

    
}
