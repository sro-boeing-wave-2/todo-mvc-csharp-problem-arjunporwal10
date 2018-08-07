using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using FluentAssertions;
using System.Net.Http.Headers;
using ToDoNotes;
using Notes.Models;
using Microsoft.EntityFrameworkCore;
using ToDoNotes.Models;
using ToDoNotes.Controllers;
using ToDoNotes.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

namespace ToDoNotesXUnitTest
{
    public class NotesControllerIntegrationTests
    {
        private HttpClient _client;
        private PrototypeContext _context;
        public NotesControllerIntegrationTests()
        {
            var _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _context = _server.Host.Services.GetService(typeof(PrototypeContext)) as PrototypeContext;
            List<ToDo> TestNoteProper1 = new List<ToDo> { new ToDo()
            {
                Title = "Title-1-Deletable",
                Text = "Message-1-Deletable",
                CheckLists = new List<Checklist>()
                        {
                            new Checklist(){ ChecklistData = "checklist-1", IsChecked = true},
                            new Checklist(){ ChecklistData = "checklist-2", IsChecked = false}
                        },
                Labels = new List<Label>()
                        {
                            new Label(){LabelName = "Label-1-Deletable"},
                            new Label(){ LabelName = "Label-2-Deletable"}
                        },
                IsPinned = true
            } };
            _context.ToDo.AddRange(TestNoteProper1);
            _context.ToDo.AddRange(TestNoteDelete);
            _context.SaveChanges();
        }
        ToDo TestNoteProper = new ToDo()
        {
            Id = 1,
            Title = "Title-1-Updatable",
            Text = "Message-1-Updatable",
            CheckLists = new List<Checklist>()
                        {
                            new Checklist(){ ChecklistData = "checklist-1", IsChecked = true},
                            new Checklist(){ ChecklistData = "checklist-2", IsChecked = false}
                        },
            Labels = new List<Label>()
                        {
                            new Label(){LabelName = "Label-1-Deletable"},
                            new Label(){ LabelName = "Label-2-Updatable"}
                        },
            IsPinned = true
        };
        ToDo TestNotePut = new ToDo()
        {
            Id = 1,
            Title = "Title-1-Deletable",
            Text = "Message-1-deletable",
            CheckLists = new List<Checklist>()
                        {
                            new Checklist(){ ChecklistData = "checklist-1", IsChecked = true},
                            new Checklist(){ ChecklistData = "checklist-2", IsChecked = false}
                        },
            Labels = new List<Label>()
                        {
                            new Label(){LabelName = "Label-1-Deletable"},
                            new Label(){ LabelName = "Label-2-Updatable"}
                        },
            IsPinned = false
        };
        ToDo TestNotePost = new ToDo()
        {
            
            Title = "Title-2-Deletable",
            Text = "Message-1-deletable",
            CheckLists = new List<Checklist>()
                        {
                            new Checklist(){ ChecklistData = "checklist-1", IsChecked = true},
                            new Checklist(){ ChecklistData = "checklist-2", IsChecked = false}
                        },
            Labels = new List<Label>()
                        {
                            new Label(){LabelName = "Label-1-Deletable"},
                            new Label(){ LabelName = "Label-2-Updatable"}
                        },
            IsPinned = false
        };
        ToDo TestNoteDelete = new ToDo()
        {
            
            Title = "this is deleted title",
            Text = "Message-1-deletable",
            CheckLists = new List<Checklist>()
                        {
                            new Checklist(){ ChecklistData = "checklist-1", IsChecked = true},
                            new Checklist(){ ChecklistData = "checklist-2", IsChecked = false}
                        },
            Labels = new List<Label>()
                        {
                            new Label(){LabelName = "Label-1-Deletable"},
                            new Label(){ LabelName = "Label-2-Updatable"}
                        },
            IsPinned = false
        };
        [Fact]
        public async Task Notes_Post()
        {
            var content = JsonConvert.SerializeObject(TestNotePost);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/prototype", stringContent);
            var responseString = await response.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<ToDo>(responseString);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
        [Fact]
        public async Task Notes_Get_All()
        {
            var response = await _client.GetAsync("/api/prototype");
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<List<ToDo>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Notes_Get_Specific()
        {
            var response = await _client.GetAsync("/api/prototype/1");
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<ToDo>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Title-1-Deletable", notes.Title);
        }
        [Fact]
        public async Task Notes_Get_Query()
        {
            var response = await _client.GetAsync("/api/prototype/query?ispinned=true");
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<List<ToDo>>(responseString);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
           // Assert.Equal("Title-1-Deletable", notes.Title);
        }
        [Fact]
        public async Task Notes_Put()
        {
            var contentChannge = JsonConvert.SerializeObject(TestNoteProper);
            var stringContentChange = new StringContent(contentChannge, Encoding.UTF8, "application/json");
            var responseChanged = await _client.PutAsync("/api/prototype/1", stringContentChange);
            responseChanged.EnsureSuccessStatusCode();
            var responseString = await responseChanged.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<ToDo>(responseString);
            Assert.Equal(HttpStatusCode.OK, responseChanged.StatusCode);
        }
        [Fact]
        public async Task Notes_Delete_Specific()
        {
            var response = await _client.DeleteAsync("api/Prototype/1");
            var responsecode = response.StatusCode;
            Assert.Equal(HttpStatusCode.NoContent, responsecode);
        }
        [Fact]
        public async Task Notes_Delete_all()
        {
            var response = await _client.DeleteAsync("api/Prototype/all");
            var responsecode = response.StatusCode;
            Assert.Equal(HttpStatusCode.NoContent, responsecode);
        }
    }
}
