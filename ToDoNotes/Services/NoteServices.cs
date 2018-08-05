using GenFu;
using Microsoft.EntityFrameworkCore;
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
        Task<List<ToDo>> GetAll();
        Task<ToDo> Get(int id);
        Task<List<ToDo>> GetByQuery(bool? Ispinned = null,string title = "", string labelName = "");
        Task<ToDo> Add(ToDo note);
        Task Update(int id, ToDo note);
        Task Delete(int id);
        Task DeleteAll();
    }
    // This class is implementing the methods of interface INoteService 
    public class NoteService : INoteService
    {
        private readonly PrototypeContext _context;
        public NoteService(PrototypeContext context)
        {
            _context = context;
        }
        public Task<List<ToDo>> GetAll()
        {
            return _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels).ToListAsync();
           // return Task.FromResult(result);
        }
        public Task<ToDo> Get(int id)
        {
            var result = _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels).First(_ => _.Id == id);
            return Task.FromResult(result);
        }
        public Task<List<ToDo>> GetByQuery(bool? Ispinned = null, string title = "", string labelName = "")
        {
            var toDo =  _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels).Where(
               m => ((title == "") || (m.Title == title)) && ((!Ispinned.HasValue) || (m.IsPinned == Ispinned)) && ((labelName == "") || (m.Labels).Any(b => b.LabelName == labelName))).ToList();
            return Task.FromResult(toDo);
        }
        public Task<ToDo> Add(ToDo note)
        {
            _context.ToDo.Add(note);
            _context.SaveChanges();
            return Task.FromResult(note);
        }
        public Task Update(int id, ToDo note)
        {
            _context.ToDo.Update(note);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public Task Delete(int id)
        {
            var existing = _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels).First(_ => _.Id == id);
            _context.ToDo.Remove(existing);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public Task DeleteAll()
        {
            var notes = _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels);
            _context.ToDo.RemoveRange(notes);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }

    }

}
