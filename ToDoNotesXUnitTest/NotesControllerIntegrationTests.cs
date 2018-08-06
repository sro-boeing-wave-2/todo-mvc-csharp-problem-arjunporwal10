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

namespace ToDoNotesXUnitTest
{
    public class NotesControllerIntegrationTests
    {
        //private readonly PrototypeController _controller;
       // private readonly TestServer _server;
        private HttpClient _client;
        public NotesControllerIntegrationTests()
        {
            //var dbOptionBuilder = new DbContextOptionsBuilder<PrototypeContext>();
            //dbOptionBuilder.UseInMemoryDatabase("InMemoryDataBaseString");
            //PrototypeContext prototypeContext = new PrototypeContext(dbOptionBuilder.Options);
            //_controller = new PrototypeController(prototypeContext);
            //CreateData(prototypeContext);

            // Arrange
           var _server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Testing")
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(
            // 
        }
        public void CreateData(PrototypeContext prototypeContext)
        {
            var NotesToAdd = new List<ToDo>
            {new ToDo()
            {
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
            prototypeContext.SaveChanges();


        }
        

        //public NotesControllerIntegrationTests()
        //{
        //    // Arrange
        //    _server = new TestServer(new WebHostBuilder()
        //        .UseEnvironment("Testing")
        //        .UseStartup<Startup>());
        //    _client = _server.CreateClient();
        //    //_client.DefaultRequestHeaders.Accept.Clear();
        //    //_client.DefaultRequestHeaders.Accept.Add(
        //    //    new MediaTypeWithQualityHeaderValue("application/json"));
        //}

        //[Fact]
        //public async Task Notes_Get_All()
        //{
        //    // Act
        //    var response = await _client.GetAsync("/api/prototype");

        //    // Assert
        //    //response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //   // Console.WriteLine(responseString);
        //    var notes = JsonConvert.DeserializeObject<List<ToDo>>(responseString);
        //    notes.Count().Should().Be(2);
        //}

        //[Fact]
        //public async Task Notes_Get_Specific()
        //{
            
            
        //       var response = await _client.GetAsync("/api/prototype/1");

        //    // Assert
        //   // response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    //Console.WriteLine(responseString);
        //    var notes = JsonConvert.DeserializeObject<ToDo>(responseString);
        //    notes.Id.Should().Be(1);
        //}

        //[Fact]
        //public async Task Notes_Post_Specific()
        //{
        //    // Arrange
        //    var NoteToAdd = new ToDo
        //    {
        //        Text = "John",
        //        IsPinned = true,
        //        Title = "FooBar"
        //    };
        //    var content = JsonConvert.SerializeObject(NoteToAdd);
        //    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PostAsync("/api/prototype", stringContent);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    var note = JsonConvert.DeserializeObject<ToDo>(responseString);
        //    note.Id.Should().Be(1006);
        //}

        //[Fact]
        //public async Task Notes_Post_Specific_Invalid()
        //{
        //    // Arrange
        //    var noteToAdd = new ToDo { Text = "John" };
        //    var content = JsonConvert.SerializeObject(noteToAdd);
        //    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PostAsync("/api/prototype", stringContent);

        //    // Assert
        //    response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        //    //var responseString = await response.Content.ReadAsStringAsync();
        //    //responseString.Should().Contain("The Email field is required")
        //    //    .And.Contain("The LastName field is required")
        //    //    .And.Contain("The Phone field is required");
        //}

        //[Fact]
        //public async Task Notes_Put_Specific()
        //{
        //    // Arrange
        //    var noteToChange = new ToDo
        //    {
        //        Id = 1,
        //        Text = "John",
        //        IsPinned = true,
        //        Title = "FooBar",

        //    };
        //    var content = JsonConvert.SerializeObject(noteToChange);
        //    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PutAsync("/api/prototype/1", stringContent);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    responseString.Should().Be(String.Empty);
        //}


        //[Fact]
        //public async Task Notes_Put_Specific_Invalid()
        //{
        //    // Arrange
        //    var noteToChange = new ToDo { Text = "John" };
        //    var content = JsonConvert.SerializeObject(noteToChange);
        //    var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

        //    // Act
        //    var response = await _client.PutAsync("/api/Prototype/1", stringContent);

        //    // Assert
        //    response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    //responseString.Should().Contain("The Email field is required")
        //    //    .And.Contain("The LastName field is required")
        //    //    .And.Contain("The Phone field is required");
        //}

        //[Fact]
        //public async Task Notes_Delete_Specific()
        //{
        //    // Arrange

        //    // Act
        //    var response = await _client.DeleteAsync("/api/Prototype/1");

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    var responseString = await response.Content.ReadAsStringAsync();
        //    responseString.Should().Be(String.Empty);
        //}
    }
}
