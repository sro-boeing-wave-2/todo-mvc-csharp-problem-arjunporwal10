using GenFu;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoNotes.Models;

namespace ToDoNotes.Services
{
    //Interface created for Notes
    public interface INoteService
    {
        Task<IEnumerable<ToDo>> GetAll();
        Task<ToDo> Get(int id);
        Task<ToDo> Add(ToDo note);
        Task Update(int id, ToDo note);
        Task Delete(int id);
    }
    // This class is implementing the methods of interface INoteService
    // Need to upgrade these methods 
    public class NoteService : INoteService
    {
        private List<ToDo> Notes { get; set; }

        public NoteService()
        {
            var i = 0;
            // Using GenFu to populate 
          Notes = A.ListOf<ToDo>(50);
            Notes.ForEach(note =>
            {
                i++;
                note.Id = i;
            });
        }

        public Task<IEnumerable<ToDo>> GetAll()
        {
            var result = Notes.Select(x => x);
            return Task.FromResult(result);
        }

        public Task<ToDo> Get(int id)
        {
            var result = Notes.First(_ => _.Id == id);
            return Task.FromResult(result);
        }

        public Task<ToDo> Add(ToDo note)
        {
            var newid = Notes.OrderBy(_ => _.Id).Last().Id + 1;
            note.Id = newid;

            Notes.Add(note);

            return Task.FromResult(note);
        }

        public Task Update(int id, ToDo note)
        {
            var existing = Notes.First(_ => _.Id == id);
            existing.Text = note.Text;
            existing.Title = note.Title;
            existing.IsPinned = note.IsPinned;
            existing.Labels = note.Labels;
            existing.CheckLists = note.CheckLists;
            return Task.CompletedTask;
        }

        public Task Delete(int id)
        {
            var existing = Notes.First(_ => _.Id == id);
            Notes.Remove(existing);
            return Task.CompletedTask;
        }

    }

}
