using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Notes.Controllers;
using ToDoNotes.Controllers;
using ToDoNotes.Services;
using Notes.Models;
using ToDoNotes.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoNotesXUnitTest
{
    public class NotesWebAPITests
    {
        //private readonly PrototypeContext _context;
        private readonly PrototypeController _controller;
        public NotesWebAPITests()
        {
            var dbOptionBuilder = new DbContextOptionsBuilder<PrototypeContext>();
            dbOptionBuilder.UseInMemoryDatabase("InMemoryDataBaseString");
            PrototypeContext prototypeContext = new PrototypeContext(dbOptionBuilder.Options);
            _controller = new PrototypeController(prototypeContext);
            CreateData(prototypeContext);
        }
        public async void CreateData(PrototypeContext prototypeContext)
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
            await prototypeContext.SaveChangesAsync();


        }
        //[Fact]
        //public void TestGetByPinnedAndLabel()
        //{
        //    //string label = "";
        //    //bool Ispinned = true;
        //    //string title = "";
        //    var result = _controller.GetByQuery(true);
        //    var objectresult = result.Result as OkObjectResult;
        //    var notes = objectresult.Value as List<ToDo>;
        //   // Console.WriteLine(notes.Count());
        //     Assert.Equal(1, notes.Count());
        //}
        [Fact]
        public async void TestGetById()
        {
            var result = await _controller.GetToDo(2);

           // Console.Write(result.Result);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as ToDo;
            Assert.Equal("SooBar", notes.Title);
        }
        [Fact]
        public async void TestGetAll()
        {
            var result = await _controller.Get();
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as List<ToDo>;
            Assert.Equal(2, notes.Count());
        }

        [Fact]
        public void TestPostMethod()
        {
            var noteToAdd = new ToDo()
            {
                Text = "Sam",
                IsPinned = true,
                Title = "MyBaar",
                Labels = new List<Label>
                {
                    new Label{LabelName="label1"},
                    new Label{LabelName="label2"}
                },
                CheckLists = new List<Checklist>
                {
                    new Checklist{ChecklistData="juice",IsChecked=false},
                    new Checklist {ChecklistData="water",IsChecked=false}

                }
            };
            var result = _controller.PostToDo(noteToAdd);
            var objectresult = result.IsCompleted;
            //var notes = objectresult.Value as CreatedAtActionResult;
            ////Console.WriteLine(notes.Count());
            Assert.True(objectresult);
        }
        [Fact]
        public async void TestPutMethod()
        {
            var noteToAdd = new ToDo()
            {
                Id=2,
                Text = "Sonnie",
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
            var result = await _controller.PutToDo(2,noteToAdd);
            var objectresult = result as OkObjectResult;
            var notes = objectresult.Value as ToDo;
            Assert.Equal("Sonnie", notes.Text);
        }
        //[Fact]
        //public void TestDeleteMethod()
        //{
        //    var result = _controller.DeleteToDo(1);
        //    var objectresult = result;
        //    //Console.WriteLine(objectresult);
        //    //var notes = objectresult.Value as int;
        //    //Assert.Equal("RanToCompletion", objectresult);
        //}
        //[Fact]
        //public void TestDeleteAllMethod()
        //{
        //    var result = _controller.DeleteAll();
        //    var objectresult = result;
        //    //Console.WriteLine(objectresult);
        //    //var notes = objectresult.Value as int;
        //    //Assert.Equal("RanToCompletion", objectresult);
        //}
        //[Fact]
        //public async Task Notes_Get_All()
        //{
        //    List<ToDo> note = new List<ToDo>();
        //    var _context = new Mock<PrototypeContext>();
        //    // _context.Setup(x => x.ToDo.Find(It.IsAny<int>())).Returns(note);
        //    _context.Setup(x => x.ToDo).As(note);
        //    // Arrange
        //    var controller = new PrototypeController(_context.Object);



        //    // Act
        //    var result = await controller.Get();

        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var notes = okResult.Value.Should().BeAssignableTo<IEnumerable<ToDo>>().Subject;

        //    notes.Count().Should().Be(50);
        //}

        //[Fact]
        //public async Task Notes_Get_From_Moq()
        //{
        //    // Arrange
        //    var serviceMock = new Mock<INoteService>();
        //    IEnumerable<ToDo> notes = new List<ToDo>
        //    {
        //        new ToDo{Id=1, Title="Foo", Text="Bar"},
        //        new ToDo{Id=2, Title="John", Text="Doe"},
        //        new ToDo{Id=3, Title="Juergen", Text="Gutsch"},
        //    };
        //    serviceMock.Setup(x => x.GetAll()).Returns(() => Task.FromResult(notes));
        //    var controller = new PrototypeController(serviceMock.Object);

        //    // Act
        //    var result = await controller.Get();

        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var actual = okResult.Value.Should().BeAssignableTo<IEnumerable<ToDo>>().Subject;

        //    notes.Count().Should().Be(3);
        //}

        //[Fact]
        //public async Task Notes_Get_Specific()
        //{
        //    // Arrange
        //    var controller = new PrototypeController(new NoteService());

        //    // Act
        //    var result = await controller.GetToDo(16);

        //    // Assert
        //    var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        //    var note = okResult.Value.Should().BeAssignableTo<ToDo>().Subject;
        //    note.Id.Should().Be(16);
        //}

        //[Fact]
        //public async Task Notes_Add()
        //{
        //    // Arrange
        //    var controller = new PrototypeController(new NoteService());
        //    var newNote = new ToDo
        //    {
        //        Text = "John",
        //        Title = "FooBar",
        //        IsPinned = true

        //    };

        //    // Act
        //    var result = await controller.PostToDo(newNote);

        //    // Assert
        //    var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
        //    var note = okResult.Value.Should().BeAssignableTo<ToDo>().Subject;
        //    note.Id.Should().Be(51);
        //}

        //[Fact]
        //public async Task Notes_Change()
        //{
        //    // Arrange
        //    var service = new NoteService();
        //    var controller = new PrototypeController(service);
        //    var newNote = new ToDo
        //    {
        //        Text = "John",
        //        Title = "FooBar",
        //        IsPinned = false

        //    };

        //    // Act
        //    var result = await controller.PutToDo(1, newNote);

        //    // Assert
        //    var okResult = result.Should().BeOfType<NoContentResult>().Subject;

        //    var note = await service.Get(1);
        //    note.Id.Should().Be(20);
        //    note.Text.Should().Be("John");
        //    note.Title.Should().Be("FooBar");
        //    note.IsPinned.Should().Be(false);

        //}

        //[Fact]
        //public async Task Notes_Delete()
        //{
        //    // Arrange
        //    var service = new NoteService();
        //    var controller = new PrototypeController(service);

        //    // Act
        //    var result = await controller.DeleteToDo(20);

        //    // Assert
        //    var okResult = result.Should().BeOfType<NoContentResult>().Subject;
        //    // should throw an eception, 
        //    // because the person with id==20 doesn't exist enymore
        //    //AssertionExtensions.ShouldThrow<InvalidOperationException>(
        //    //     () => service.Get(20));
        //}


        //[Fact]
        //public async Task Persons_Delete_Fail()
        //{
        //    // Arrange
        //    var service = new NoteService();
        //    var controller = new PrototypeController(service);

        //    // Act
        //    var result = await controller.DeleteToDo(20);

        //    // Assert
        //    var okResult = result.Should().BeOfType<NoContentResult>().Subject;
        //    // should throw an eception, 
        //    // because the person with id==20 doesn't exist enymore
        //    //AssertionExtensions.ShouldThrow<InvalidOperationException>(
        //    //    () => service.Get(15));
        //}
    }
}
