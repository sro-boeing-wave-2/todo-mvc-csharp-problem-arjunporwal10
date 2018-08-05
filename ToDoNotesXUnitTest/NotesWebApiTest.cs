//using System;
//using System.Threading.Tasks;
//using Xunit;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;
//using FluentAssertions;
//using Moq;
//using Notes.Controllers;
//using ToDoNotes.Controllers;
//using ToDoNotes.Services;
//using Notes.Models;

//namespace ToDoNotesXUnitTest
//{
//    public class NotesWebAPITests
//    {
//        [Fact]
//        public async Task Notes_Get_All()
//        {
//            // Arrange
//            var controller = new PrototypeController(new NoteService());

//            // Act
//            var result = await controller.Get();

//            // Assert
//            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
//            var notes = okResult.Value.Should().BeAssignableTo<IEnumerable<ToDo>>().Subject;

//            notes.Count().Should().Be(50);
//        }

//        [Fact]
//        public async Task Notes_Get_From_Moq()
//        {
//            // Arrange
//            var serviceMock = new Mock<INoteService>();
//            IEnumerable<ToDo> notes = new List<ToDo>
//            {
//                new ToDo{Id=1, Title="Foo", Text="Bar"},
//                new ToDo{Id=2, Title="John", Text="Doe"},
//                new ToDo{Id=3, Title="Juergen", Text="Gutsch"},
//            };
//            serviceMock.Setup(x => x.GetAll()).Returns(() => Task.FromResult(notes));
//            var controller = new PrototypeController(serviceMock.Object);

//            // Act
//            var result = await controller.Get();

//            // Assert
//            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
//            var actual = okResult.Value.Should().BeAssignableTo<IEnumerable<ToDo>>().Subject;

//            notes.Count().Should().Be(3);
//        }

//        [Fact]
//        public async Task Notes_Get_Specific()
//        {
//            // Arrange
//            var controller = new PrototypeController(new NoteService());

//            // Act
//            var result = await controller.GetToDo(16);

//            // Assert
//            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
//            var note = okResult.Value.Should().BeAssignableTo<ToDo>().Subject;
//            note.Id.Should().Be(16);
//        }

//        [Fact]
//        public async Task Notes_Add()
//        {
//            // Arrange
//            var controller = new PrototypeController(new NoteService());
//            var newNote = new ToDo
//            {
//                Text = "John",
//                Title = "FooBar",
//                IsPinned = true

//            };

//            // Act
//            var result = await controller.PostToDo(newNote);

//            // Assert
//            var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
//            var note = okResult.Value.Should().BeAssignableTo<ToDo>().Subject;
//            note.Id.Should().Be(51);
//        }

//        [Fact]
//        public async Task Notes_Change()
//        {
//            // Arrange
//            var service = new NoteService();
//            var controller = new PrototypeController(service);
//            var newNote = new ToDo
//            {
//                Text = "John",
//                Title = "FooBar",
//                IsPinned = false

//            };

//            // Act
//            var result = await controller.PutToDo(1, newNote);

//            // Assert
//            var okResult = result.Should().BeOfType<NoContentResult>().Subject;

//            var note = await service.Get(1);
//            note.Id.Should().Be(20);
//            note.Text.Should().Be("John");
//            note.Title.Should().Be("FooBar");
//            note.IsPinned.Should().Be(false);

//        }

//        [Fact]
//        public async Task Notes_Delete()
//        {
//            // Arrange
//            var service = new NoteService();
//            var controller = new PrototypeController(service);

//            // Act
//            var result = await controller.DeleteToDo(20);

//            // Assert
//            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
//            // should throw an eception, 
//            // because the person with id==20 doesn't exist enymore
//            //AssertionExtensions.ShouldThrow<InvalidOperationException>(
//            //     () => service.Get(20));
//        }


//        [Fact]
//        public async Task Persons_Delete_Fail()
//        {
//            // Arrange
//            var service = new NoteService();
//            var controller = new PrototypeController(service);

//            // Act
//            var result = await controller.DeleteToDo(20);

//            // Assert
//            var okResult = result.Should().BeOfType<NoContentResult>().Subject;
//            // should throw an eception, 
//            // because the person with id==20 doesn't exist enymore
//            //AssertionExtensions.ShouldThrow<InvalidOperationException>(
//            //    () => service.Get(15));
//        }
//    }
//}
