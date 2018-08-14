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
using MongoDB.Bson;
using ToDoNotes.Wrappers;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ToDoNotesXUnitTest
{
    public class NotesWebAPITests
    {
        public ObjectId Id1 = ObjectId.GenerateNewId();
        public ObjectId Id2 = ObjectId.GenerateNewId();
        public ToDo NoteToPost =  new ToDo()
        {
            Id = new ObjectId("5b71298a6a2e663634872a65"),
            NotesId =3,
                    Text = "Ronnie",
                    IsPinned = false,
                    Title = "SooBar",
                    Labels = new List<Label>
                    {
                        new Label{LabelName="blue label"},
                        new Label{LabelName="juice"}
                    },
                    CheckLists = new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=false},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
                
        };
    public class FakeDataAccess : IDataAccess
        {
            MongoClient _client;
            MongoServer _server;
            MongoDatabase _db;

            public FakeDataAccess()
            {
                _client = new MongoClient("mongodb://localhost:27017");
                _server = _client.GetServer();
                _db = _server.GetDatabase("ToDoNotes");
            }

            public IEnumerable<ToDo> GetNotes()
            {
                var NotesToAdd = new List<ToDo>
            {
                new ToDo()
                {
                   Id = new ObjectId("5b71298a6a2e663634872a61"),
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
                    Id = new ObjectId("5b71298a6a2e663634872a62"),
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
                } };
                return NotesToAdd;
            }

            public IEnumerable<ToDo> GetNotesByQuery(bool? Ispinned = null, string title = "", string labelName = "")
            {
                List<ToDo> findNote = new List<ToDo>
                {new ToDo
                {
                    Id = new ObjectId("5b71298a6a2e663634872a62"),
                    Text = "Ronnie",
                    IsPinned = false,
                    Title = "SooBar",
                    Labels = new List<Label>
                    {
                        new Label{LabelName="blue label"},
                        new Label{LabelName="juice"}
                    },
                    CheckLists = new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=false},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
                }
                    
                };
                return findNote;
            }

            public ToDo GetNote(ObjectId id)
            {
                ToDo findNote =  new ToDo
                {
                    Id = new ObjectId("5b71298a6a2e663634872a62"),
                    Text = "Ronnie",
                    IsPinned = false,
                    Title = "SooBar",
                    Labels = new List<Label>
                    {
                        new Label{LabelName="blue label"},
                        new Label{LabelName="juice"}
                    },
                    CheckLists = new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=false},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
                };
                return findNote;
            }

            public ToDo Create(ToDo p)
            {
                ToDo findNote = new ToDo
                {
                    Id = new ObjectId("5b71298a6a2e663634872a65"),
                    Text = "Ronnie",
                    IsPinned = false,
                    Title = "SooBar",
                    Labels = new List<Label>
                    {
                        new Label{LabelName="blue label"},
                        new Label{LabelName="juice"}
                    },
                    CheckLists = new List<Checklist>
                    {
                        new Checklist{ChecklistData="soda",IsChecked=false},
                        new Checklist {ChecklistData="water",IsChecked=false}

                    }
                };
                return findNote;
            }

            public void Update(ObjectId id, ToDo p)
            {
                
            }
            public void Remove(ObjectId id)
            {
               
            }
        }

        //[Fact]
        //public void TestGetByPinnedAndLabel()
        //{
        //    var _controller = GetController();
        //    var result = _controller.GetNotesByQuery(true, "", "");
        //    //var objectresult = result.Result as OkObjectResult;
        //    //var notes = objectresult.Value as List<ToDo>;
        //    //Assert.Equal(1, notes.Count());
        //}
        

        [Fact]
        public void TestGetAll()
        {
            FakeDataAccess fakeData = new FakeDataAccess();
            PrototypeController _controller = new PrototypeController(fakeData);
            var result = _controller.Get();
            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void PostNote()
        {
            FakeDataAccess fakeData = new FakeDataAccess();
            PrototypeController _controller = new PrototypeController(fakeData);
            var result = _controller.Post(NoteToPost);
            var notePosted = result as OkObjectResult;
            var note = notePosted.Value as ToDo;
            Assert.Equal(3, note.NotesId);
        }
        [Fact]
        public void TestGetById()
        {
            var objId = new ObjectId("5b71298a6a2e663634872a62");
            FakeDataAccess fakeData = new FakeDataAccess();
            PrototypeController _controller = new PrototypeController(fakeData);
            var result = _controller.GetNotesById("5b71298a6a2e663634872a62");
            var notePosted = result as ObjectResult;
            var note = notePosted.Value as ToDo;
            Assert.Equal(objId, note.Id);
        }
        [Fact]
        public void TestGetByQuery()
        {
            var objId = new ObjectId("5b71298a6a2e663634872a61");
            FakeDataAccess fakeData = new FakeDataAccess();
            PrototypeController _controller = new PrototypeController(fakeData);
            var result = _controller.GetNotesByQuery(true, "", "");
            var notePosted = result as ObjectResult;
            var note = notePosted.Value as List<ToDo>;
            Assert.Equal(1,note.Count);
        }

        [Fact]
        public void TestPutMethod()
        {
            var TestNotePut = new ToDo()
            {
                Id = new ObjectId("5b71298a6a2e663634872a62"),
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
            var objId = new ObjectId("5b71298a6a2e663634872a62");
            FakeDataAccess fakeData = new FakeDataAccess();
            PrototypeController _controller = new PrototypeController(fakeData);
            var result = _controller.Put("5b71298a6a2e663634872a62",TestNotePut);
            var notePosted = result as OkResult;
            Assert.Equal(200, notePosted.StatusCode);
        }
        [Fact]
        public void TestDeleteMethod()
        {
            var objId = new ObjectId("5b71298a6a2e663634872a62");
            FakeDataAccess fakeData = new FakeDataAccess();
            PrototypeController _controller = new PrototypeController(fakeData);
            var result = _controller.Delete("5b71298a6a2e663634872a62");
            var notePosted = result as OkResult;
            Assert.Equal(200, notePosted.StatusCode);
        }
        

    }
    }

