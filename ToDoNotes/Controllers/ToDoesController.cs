using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Models;
using ToDoNotes.Models;

namespace Notes.Controllers
{
    [Produces("application/json")]
    [Route("api/ToDoes")]
    public class ToDoesController : Controller
    {
        private readonly ToDoNotesContext _context;

        public ToDoesController(ToDoNotesContext context)
        {
            _context = context;
        }

        //GET: api/ToDoes
        //[HttpGet]
        //public IEnumerable<ToDo> GetToDo()
        //{
        //    return _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels);
        //}

        // GET: api/ToDoes/5
        [HttpGet]
        public async Task<IActionResult> GetToDo([FromQuery] bool? Ispinned = null, [FromQuery]string title = "", string labelName = "")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels).Where(
                m => ((title == "") || (m.Title == title)) && ((!Ispinned.HasValue) || (m.IsPinned == Ispinned)) && ((labelName == "") || (m.Labels).Any(b => b.LabelName == labelName))).ToListAsync();

            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }

        // PUT: api/ToDoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo([FromRoute] int id, [FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.Id)
            {
                return BadRequest();
            }
           
            _context.ToDo.Update(toDo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        // POST: api/ToDoes
        [HttpPost]
        public async Task<IActionResult> PostToDo([FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ToDo.Add(toDo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDo", new { id = toDo.Id }, toDo);
        }

        // DELETE: api/ToDoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var toDo = await _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels).SingleOrDefaultAsync(m => m.Id == id);
            if (toDo == null)
            {
                return NotFound();
            }

            _context.ToDo.Remove(toDo);

            await _context.SaveChangesAsync();

            return Ok(toDo);
        }
        // if you delete all you have to again migrate and update the database
        [HttpDelete]
        [Route("all")]
        public async Task<IActionResult> DeleteAll()
        {
            var notes = _context.ToDo.Include(x => x.CheckLists).Include(x => x.Labels);
            _context.ToDo.RemoveRange(notes);
            await _context.SaveChangesAsync();
            // await _context.Database.EnsureDeletedAsync();
            // if you delete all you have to again migrate and update the database using above commented statement

            return Ok();
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }
    }
}