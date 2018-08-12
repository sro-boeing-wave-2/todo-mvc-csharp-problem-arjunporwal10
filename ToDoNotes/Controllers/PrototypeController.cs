using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using Notes.Models;
using ToDoNotes.Models;
using ToDoNotes.Services;

namespace ToDoNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrototypeController : ControllerBase
    {
        DataAccess objds;

        public PrototypeController(DataAccess d)
        {
            objds = d;
        }

        [HttpGet]
        public IEnumerable<ToDo> Get()
        {
            return objds.GetNotes();
        }
        [HttpGet("{id:length(24)}")]
        public IActionResult Get(string id)
        {
            var note = objds.GetNote(new ObjectId(id));
            if (note == null)
            {
                return NotFound();
            }
            return new ObjectResult(note);
        }

        [HttpPost]
        public IActionResult Post([FromBody]ToDo p)
        {
            objds.Create(p);
            return new OkObjectResult(p);
        }
        [HttpPut("{id:length(24)}")]
        public IActionResult Put(string id, [FromBody]ToDo p)
        {
            var recId = new ObjectId(id);
            var note = objds.GetNote(recId);
            if (note == null)
            {
                return NotFound();
            }

            objds.Update(recId, p);
            return new OkResult();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var note = objds.GetNote(new ObjectId(id));
            if (note == null)
            {
                return NotFound();
            }

            objds.Remove(note.Id);
            return new OkResult();
        }
        //private INoteService _noteService;
        //public PrototypeController(PrototypeContext _context)
        //{
        //    _noteService = new NoteService(_context);
        //    //_noteService = noteService;
        //}
        //// GET: api/Prototype
        //[HttpGet]
        //public async Task<IActionResult> Get()
        //{
        //    var notes = await _noteService.GetAll();
        //    return Ok(notes);
        //}
        //// GET: api/Prototype/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetToDo(int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var notes = await _noteService.Get(id);
        //    if (notes==null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(notes);
        //}
        //[HttpGet]
        //[Route("query")]
        //public async Task<IActionResult> GetByQuery([FromQuery] bool? Ispinned = null, [FromQuery]string title = "", [FromQuery] string labelName = "")
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var notes = await _noteService.GetByQuery(Ispinned,title,labelName);
        //    if (notes.Count() == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(notes);
        //}
        //// PUT: api/Prototype/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutToDo(int id, [FromBody] ToDo toDo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    if (id != toDo.Id)
        //    {
        //        return NotFound();
        //    }

        //    await _noteService.Update(id, toDo);
        //    return Ok(toDo);
        //}
        //// POST: api/Prototype
        //[HttpPost]
        //public async Task<IActionResult> PostToDo([FromBody] ToDo toDo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var note = await _noteService.Add(toDo);
        //   // return StatusCode(201);
        //    // return Ok(toDo);
        //   return CreatedAtAction("GetToDo", new { id = note.Id }, note);
        //}
        //// DELETE: api/Prototype/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteToDo([FromRoute] int id)
        //{
        //    await _noteService.Delete(id);
        //    return NoContent();
        //}
        //[HttpDelete]
        //[Route("all")]
        //public async Task<IActionResult> DeleteAll()
        //{
        //    await _noteService.DeleteAll();

        //    return NoContent();
        //}
    }
}