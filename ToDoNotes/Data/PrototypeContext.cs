using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Models;

namespace ToDoNotes.Models
{
    public class PrototypeContext : DbContext
    {
        public PrototypeContext (DbContextOptions<PrototypeContext> options)
            : base(options)
        {
        }

        public DbSet<Notes.Models.ToDo> ToDo { get; set; }
    }
}
