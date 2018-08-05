using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Models;
using ToDoNotes.Models;
using ToDoNotes.Services;

namespace ToDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeController : ControllerBase
    {
        private readonly PrototypeContext _context;
        private INoteService _noteService;

        public PrototypeController(INoteService noteService)
        {
            _noteService = noteService;
        }

        // GET: api/Prototype
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var notes = await _noteService.GetAll();
            return Ok(notes);
        }

        // GET: api/Prototype/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDo(int id)
        {
            var notes = await _noteService.Get(id);

            return Ok(notes);
        }

        // PUT: api/Prototype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDo( int id, [FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != toDo.Id)
            {
                return BadRequest();
            }
            await _noteService.Update(id, toDo);

          
            _context.Entry(toDo).State = EntityState.Modified;

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

            return NoContent();
        }

        // POST: api/Prototype
        [HttpPost]
        public async Task<IActionResult> PostToDo([FromBody] ToDo toDo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var note = await _noteService.Add(toDo);

            return CreatedAtAction("Get", new { id = toDo.Id }, note);
        }

        // DELETE: api/Prototype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        {
            await _noteService.Delete(id);
            return NoContent();
        }

        private bool ToDoExists(int id)
        {
            return _context.ToDo.Any(e => e.Id == id);
        }

        
    }
}