using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using ToDoNotes.Controllers;
using ToDoNotes.Services;
using Notes.Models;
using ToDoNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoNotesXUnitTest
{
    public class NotesWebAPITests
    {
        public PrototypeController GetController()
        {
           // Console.WriteLine("Get Controller");
            var optionsBuilder = new DbContextOptionsBuilder<PrototypeContext>();
            optionsBuilder.UseInMemoryDatabase<PrototypeContext>(Guid.NewGuid().ToString());
            PrototypeContext prototypeContext = new PrototypeContext(optionsBuilder.Options);
            CreateData(optionsBuilder.Options);
            return new PrototypeController(prototypeContext);
        }

        public void CreateData(DbContextOptions<PrototypeContext> options)
        {
            using (var prototypeContext = new PrototypeContext(options))
            {
                var NotesToAdd = new List<ToDo>
            {
                new ToDo()
                {
                    Id = 1,
                    Text = "John",
                    IsPinned = true,
                    Title = "FooBar",
                    Labels = new List<Label>
                    {
                        new Label{LabelName="red label"},
                        new Label{LabelName="black label"}
                    },
                    CheckLists=new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=true},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
                },
                new ToDo()
                {
                    Id = 2,
                    Text = "Ronnie",
                    IsPinned = false,
                    Title = "SooBar",
                    Labels = new List<Label>
                    {
                        new Label{LabelName="blue label"},
                        new Label{LabelName="juice"}
                    },
                    CheckLists=new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=false},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
                }
            };
                prototypeContext.ToDo.AddRange(NotesToAdd);
                var CountOfEntitiesBeingTracked = prototypeContext.ChangeTracker.Entries().Count();
                prototypeContext.SaveChanges();
            }
        }

        [Fact]
        public void TestGetByPinnedAndLabel()
        {
            var _controller = GetController();
            var result = _controller.GetByQuery(true,"","");
            var objectresult = result.Result as OkObjectResult;
            var notes = objectresult.Value as List<ToDo>;
            Assert.Equal(1, notes.Count());
        }
        [Fact]
        public async Task TestGetById()
        {
            var _controller = GetController();
            var result = await _controller.GetToDo(1);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as ToDo;
            Assert.Equal(1, notes.Id);
        }

        [Fact]
        public async Task TestGetAll()
        {
            var _controller = GetController();
            var result = await _controller.Get();
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as List<ToDo>;
            Assert.Equal(2, notes.Count());
        }

        [Fact]
        public async Task TestPutMethod()
        {
            var TestNotePut = new ToDo()
            {
                Id = 1,
                Text = "Johnny",
                IsPinned = true,
                Title = "FooBar",
                Labels = new List<Label>
                    {
                        new Label{LabelName="red label"},
                        new Label{LabelName="black label"}
                    },
                CheckLists = new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=true},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
            };
            var _controller = GetController();
            var result = await _controller.PutToDo(1, TestNotePut);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as ToDo;
            Assert.Equal(1, notes.Id);
        }
        [Fact]
        public void TestPostMethod()
        {
            var TestNotePost = new ToDo()
            {
                
                Text = "Rohnny",
                IsPinned = true,
                Title = "FooBar",
                Labels = new List<Label>
                    {
                        new Label{LabelName="red label"},
                        new Label{LabelName="black label"}
                    },
                CheckLists = new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=true},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
            };
            var _controller = GetController();
            var result = _controller.PostToDo(TestNotePost);
            var objectresult = result.IsCompleted;
            Assert.True(objectresult);
        }

        [Fact]
        public void TestDeleteMethod()
        {
            var _controller = GetController();
            var result = _controller.DeleteToDo(1);
            Assert.True(result.IsCompletedSuccessfully);
        }
        [Fact]
        public void TestDeleteAllMethod()
        {
            var _controller = GetController();
            var result = _controller.DeleteAll();
            Assert.True(result.IsCompletedSuccessfully);
        }
       
    }
}
