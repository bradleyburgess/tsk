using Moq;
using Tsk.Domain.Entities;
using Tsk.Domain.Serialization;
using Tsk.Infrastructure.FileIO;
using Tsk.Infrastructure.Repositories;
using Tsk.Tests.TestSupport;
using Xunit.Sdk;

namespace Tsk.Tests.Repositories
{
    public class FlatFileTodoRepositoryTests
    {
        public TodoItemFlatFileSerializer serializer = new();

        [Fact]
        public void ShouldLoadTodosFromMockedString()
        {
            var path = "somefile";
            var todo1 = new TodoItem(id: 1, description: "buy milk");
            todo1.MarkComplete();
            var todo2 = new TodoItem(id: 2, description: "go for a run");
            var todo3 = new TodoItem(id: 3, description: "win");
            List<TodoItem> todos = new() { todo1, todo2, todo3 };
            var lines = new TodoItem[] { todo1, todo2, todo3 }
                .Select(u => serializer.Serialize(u));


            var mockFile = new Mock<IFileReaderWriter>();
            mockFile.Setup(f => f.ReadAllLines(path)).Returns(lines);

            var fileRepository = new FlatFileTodoRepository(path, mockFile.Object);
            fileRepository.LoadTodos();

            var result = fileRepository.GetAll().ToList();

            foreach (var t in todos)
            {
                var r = result.Find(u => u.Id == t.Id);
                Assert.Equal(t.Description, r!.Description);
                Assert.Equal(t.Completed, r!.Completed);
            }
        }

        [Fact]
        public void ShouldLoad_With_Full_Example()
        {
            var path = "somefile.txt";
            string[] lines =
                [
                    "[ ] 20250425 \"buy milk\" loc:\"Cape Town\" tags:shopping,errands",
                    "[X] 20250426 \"go for a walk\" loc:Alphen tags:fitness,out-and-about",
                    "[ ] \"complete assignment\""
                ];
            var mockFile = new Mock<IFileReaderWriter>();
            mockFile.Setup(f => f.ReadAllLines(path)).Returns(lines);
            var fileRepository = new FlatFileTodoRepository(path, mockFile.Object);
            fileRepository.LoadTodos();
            var result = fileRepository.GetAll();
            var r1 = result.ToList().Find(u => u.Id == 1);
            var r2 = result.ToList().Find(u => u.Id == 2);
            var r3 = result.ToList().Find(u => u.Id == 3);

            Assert.Equal("buy milk", r1!.Description);
            Assert.Equal("Cape Town", r1.Location);
            Assert.Equal("shopping", r1.Tags[0].Name);
            Assert.Equal("errands", r1.Tags[1].Name);
            Assert.Equivalent(new DateOnly(2025, 04, 25), r1.DueDate);
            Assert.False(r1.Completed);

            Assert.Equal("go for a walk", r2!.Description);
            Assert.Equal("Alphen", r2.Location);
            Assert.Equal("fitness", r2.Tags[0].Name);
            Assert.Equal("out-and-about", r2.Tags[1].Name);
            Assert.Equivalent(new DateOnly(2025, 04, 26), r2.DueDate);
            Assert.True(r2.Completed);

            Assert.Equal("complete assignment", r3!.Description);
            Assert.Null(r3.Location);
            Assert.Empty(r3.Tags);
            Assert.Null(r3.DueDate);
            Assert.False(r3.Completed);
        }

        [Fact]
        public void ShouldError_With_Bad_Input()
        {
            string path = "somefile.txt";
            var line = @"[ ] 12342 asdf asdf";
            var mockFile = new Mock<IFileReaderWriter>();
            mockFile.Setup(f => f.ReadAllLines(path)).Returns([line]);
            var fileRepository = new FlatFileTodoRepository(path, mockFile.Object);
            Assert.Throws<FormatException>(() => fileRepository.LoadTodos());
        }

        [Fact]
        public void ShouldLoadFromFile()
        {
            string path = TestHelpers.ResolveFromSlnRoot("tests", "Tsk.Tests", "TestData", "TestInputFile1.txt");

            var fileRepository = new FlatFileTodoRepository(path, new FileReaderWriter());
            fileRepository.LoadTodos();
            var result = fileRepository.GetAll().ToList();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ShouldGetTags()
        {
            var path = "somefile.txt";
            string[] lines =
                [
                    "[ ] 20250425 \"buy milk\" loc:\"Cape Town\" tags:shopping,errands",
                    "[X] 20250426 \"go for a walk\" loc:Alphen tags:fitness,out-and-about",
                    "[ ] \"complete assignment\""
                ];
            var mockFile = new Mock<IFileReaderWriter>();
            mockFile.Setup(f => f.ReadAllLines(path)).Returns(lines);
            var fileRepository = new FlatFileTodoRepository(path, mockFile.Object);
            fileRepository.LoadTodos();
            var result = fileRepository.GetAllTags().ToList();
            Assert.Equal(4, result.Count());
            string[] tags = ["shopping", "errands", "fitness", "out-and-about"];
            foreach (var t in result)
            {
                Assert.Contains(t.Name, tags);
            }
            Assert.Equal(4, result.Count());
        }

        [Fact]
        public void ShouldGetOneTagById()
        {
            var path = "somefile.txt";
            string[] lines =
                [
                    "[ ] 20250425 \"buy milk\" loc:\"Cape Town\" tags:shopping,errands",
                    "[X] 20250426 \"go for a walk\" loc:Alphen tags:fitness,out-and-about",
                    "[ ] \"complete assignment\""
                ];
            var mockFile = new Mock<IFileReaderWriter>();
            mockFile.Setup(f => f.ReadAllLines(path)).Returns(lines);
            var fileRepository = new FlatFileTodoRepository(path, mockFile.Object);
            fileRepository.LoadTodos();
            var result = fileRepository.GetTodosByTag("fitness")?.ToList();
            Assert.Equal(1, result?.Count);
            Assert.Equal(2, result?[0].Id);
            Assert.Equal("go for a walk", result?[0].Description);
        }
    }
}