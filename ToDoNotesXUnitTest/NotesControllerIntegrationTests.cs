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

namespace ToDoNotesXUnitTest
{
    public class NotesControllerIntegrationTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public NotesControllerIntegrationTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [Fact]
        public async Task Notes_Get_All()
        {
            // Act
            var response = await _client.GetAsync("/api/Prototype");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<IEnumerable<ToDo>>(responseString);
            notes.Count().Should().Be(50);
        }

        [Fact]
        public async Task Notes_Get_Specific()
        {
            // Act
            var response = await _client.GetAsync("/api/Prototype/16");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var notes = JsonConvert.DeserializeObject<ToDo>(responseString);
            notes.Id.Should().Be(16);
        }

        [Fact]
        public async Task Notes_Post_Specific()
        {
            // Arrange
            var NoteToAdd = new ToDo
            {
                Text = "John",
                IsPinned = true,
                Title = "FooBar"
            };
            var content = JsonConvert.SerializeObject(NoteToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Prototype", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var note = JsonConvert.DeserializeObject<ToDo>(responseString);
            note.Id.Should().Be(51);
        }

        [Fact]
        public async Task Notes_Post_Specific_Invalid()
        {
            // Arrange
            var noteToAdd = new ToDo { Text = "John" };
            var content = JsonConvert.SerializeObject(noteToAdd);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Prototype", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("The Email field is required")
                .And.Contain("The LastName field is required")
                .And.Contain("The Phone field is required");
        }

        [Fact]
        public async Task Notes_Put_Specific()
        {
            // Arrange
            var personToChange = new ToDo
            {
                Id = 16,
                Text = "John",
                IsPinned = true,   
                Title = "FooBar",
             
            };
            var content = JsonConvert.SerializeObject(personToChange);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Prototype/16", stringContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }


        [Fact]
        public async Task Notes_Put_Specific_Invalid()
        {
            // Arrange
            var noteToChange = new ToDo { Text = "John" };
            var content = JsonConvert.SerializeObject(noteToChange);
            var stringContent = new StringContent(content, Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync("/api/Prototype/16", stringContent);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("The Email field is required")
                .And.Contain("The LastName field is required")
                .And.Contain("The Phone field is required");
        }

        [Fact]
        public async Task Notes_Delete_Specific()
        {
            // Arrange

            // Act
            var response = await _client.DeleteAsync("/api/Prototype/16");

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Be(String.Empty);
        }
    }
}
