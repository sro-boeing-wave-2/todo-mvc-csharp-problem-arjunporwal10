//using System;
//using System.Collections.Generic;
//using System.Text;

//using Xunit;
//using NSuperTest;
//using ToDoNotes;

//namespace ToDoNotesXUnitTest
//{
//    public class NSupertestingUnit
//    {
//        static Server server;
//        [Fact]
//        public static void Init()
//        {
//            server = new Server("https://localhost:44310/api/prototype/");
//        }

//        [Fact]
//        public void TestGetNotFound()
//        {
//            server.Get("query?title=Arjun")
//            .Expect(404)
//            .End();
//        }

//        [Fact]
//        public void GetTestFound()
//        {
//            server.Get("query?title=Mumbai")
//            .Expect(200)
//            .End();
//        }

//        [Fact]
//        public void GetTestByPinnedPass()
//        {
//            server.Get("query?ispinned=true")
//            .Expect(200)
//            .End();
//        }

//        [Fact]
//        public void GetTestByPinnedFail()
//        {
//            server.Get("query?ispinned=false")
//            .Expect(200)
//            .End();
//        }

//        [Fact]
//        public void GetTestByLabelsPass()
//        {
//            server.Get("query?labelname=black deer")
//            .Expect(200)
//            .End();
//        }

//        [Fact]
//        public void GetTestByLabelsFail()
//        {
//            server.Get("query?labelname=black dee")
//            .Expect(404)
//            .End();
//        }

//        [Fact]
//        public void GetAll()
//        {
//            server.Get("")
//            .Expect(200)
//            .End();
//        }
//        [Fact]
//        public void GetByID()
//        {
//            server.Get("1")
//            .Expect(200)
//            .End();
//        }

//        [Fact]
//        public void GetTestByLablePinned()
//        {
//            server.Get("query?labelname=black deer&title=Delhi")
//            .Expect(200)
//            .End();
//        }
//    //    [Fact]
//    //    public void Post()
//    //    {
//    //        ToDo product = new ToDo()
//    //        {
//    //            Text = "Buy a Mobile",
//    //            Title = "Mobile",
//    //            IsPinned: false,
//    //          Labels = [{ Labelname = "samsung"},{ Labelname = "Redmi"}],
//    //         CheckLists = [{ Checklistdata = "check this list",Iischecked = true}]
//    //            };

//    //    server
//    //      .Post("")
//    //              .Send(product)
//    //              .Expect(201)
//    //              .End();
//    //}
//}
//}
