using Bogus;
using Moq;
using Tsk.CLI.Application.Commands;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Domain.Entities;
using Tsk.CLI.Presentation;
using Tsk.TestSupport;

namespace Tsk.CLI.Tests.Application.Commands;

public class ListCommandTests
{
    private static readonly Faker f = new();
    private static readonly TodoTestDataGenerator z = new();

    [Fact]
    public void Execute_ListsTodos_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>());

        var mockRenderer = new Mock<IRenderer>();
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockFactory.Verify(f => f.Create(It.Is<string>(s => s.Contains(fileName))));
        mockRepo.Verify(r => r.GetAll(), Times.Once);
    }

    [Fact]
    public void Execute_ListsTodos_SortsByCompletedDefault_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo1 = new TodoItem(id: 1, description: "A");
        var todo2 = new TodoItem(id: 2, description: "C");
        var todo3 = new TodoItem(id: 3, description: "B");
        todo1.MarkComplete();

        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>([todo1, todo2, todo3]));

        var mockRenderer = new Mock<IRenderer>();
        mockRenderer.Setup(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()));
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockFactory.Verify(f => f.Create(It.Is<string>(s => s.Contains(fileName))));
        mockRepo.Verify(r => r.GetAll(), Times.Once);
        mockRenderer.Verify(f => f.RenderTodoList(It.Is<IEnumerable<TodoItem>>(s => s.ToList()[0].Id == 2)), Times.Once);
    }

    [Fact]
    public void Execute_ListsTodos_SortsByDescription_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo1 = new TodoItem(id: 1, description: "C", location: "Location B");
        var todo2 = new TodoItem(id: 2, description: "B", location: "Location A");
        var todo3 = new TodoItem(id: 3, description: "A", location: "Location C");
        todo1.MarkComplete();

        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>([todo1, todo2, todo3]));

        var mockRenderer = new Mock<IRenderer>();
        mockRenderer.Setup(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()));
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName,
            SortBy = "desc",
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockFactory.Verify(f => f.Create(It.Is<string>(s => s.Contains(fileName))));
        mockRepo.Verify(r => r.GetAll(), Times.Once);
        mockRenderer.Verify(f => f.RenderTodoList(It.Is<IEnumerable<TodoItem>>(s => s.ToList()[0].Id == 3)), Times.Once);
    }

    [Fact]
    public void Execute_ListsTodos_SortsByLocation_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo1 = new TodoItem(id: 1, description: "C", location: "Location B");
        var todo2 = new TodoItem(id: 2, description: "B", location: "Location A");
        var todo3 = new TodoItem(id: 3, description: "A", location: "Location C");
        todo1.MarkComplete();

        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>([todo1, todo2, todo3]));

        var mockRenderer = new Mock<IRenderer>();
        mockRenderer.Setup(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()));
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName,
            SortBy = "loc",
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockFactory.Verify(f => f.Create(It.Is<string>(s => s.Contains(fileName))));
        mockRepo.Verify(r => r.GetAll(), Times.Once);
        mockRenderer.Verify(f => f.RenderTodoList(It.Is<IEnumerable<TodoItem>>(s => s.ToList()[0].Id == 2)), Times.Once);
    }

    [Fact]
    public void Execute_ListsTodos_SortsByDate_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo1 = new TodoItem(id: 1, description: "C");
        var todo2 = new TodoItem(id: 2, description: "A", dueDate: new DateOnly(2000, 1, 1));
        var todo3 = new TodoItem(id: 3, description: "B", dueDate: new DateOnly(1999, 1, 1));
        todo1.MarkComplete();

        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>([todo1, todo2, todo3]));

        var mockRenderer = new Mock<IRenderer>();
        mockRenderer.Setup(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()));
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName,
            SortBy = "date",
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockFactory.Verify(f => f.Create(It.Is<string>(s => s.Contains(fileName))));
        mockRepo.Verify(r => r.GetAll(), Times.Once);
        mockRenderer.Verify(f => f.RenderTodoList(It.Is<IEnumerable<TodoItem>>(s => s.ToList()[0].Id == 3)), Times.Once);
    }

    [Theory]
    [InlineData("desc")]
    [InlineData("description")]
    [InlineData("loc")]
    [InlineData("location")]
    [InlineData("date")]
    [InlineData("duedate")]
    public void Execute_ListsTodos_DoesNotThrowForValidSortOption_ReturnsZero(string sortOption)
    {
        var fileName = f.System.FileName();

        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>());

        var mockRenderer = new Mock<IRenderer>();
        mockRenderer.Setup(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()));
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName,
            SortBy = sortOption,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockFactory.Verify(f => f.Create(It.Is<string>(s => s.Contains(fileName))));
        mockRepo.Verify(r => r.GetAll(), Times.Once);
        mockRenderer.Verify(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()), Times.Once);
    }

    [Fact]
    public void Execute_ListsTodos_RendersErrorForBadSortOption_ReturnsOne()
    {
        var fileName = f.System.FileName();

        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>());

        var mockRenderer = new Mock<IRenderer>();
        mockRenderer.Setup(f => f.RenderTodoList(It.IsAny<IEnumerable<TodoItem>>()));
        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string>())).Returns(mockRepo.Object);

        var cmd = new ListCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new ListCommand.Settings
        {
            FileName = fileName,
            SortBy = "asdf",
        };

        var result = cmd.Execute(null!, settings);
        Assert.Equal(1, result);
        mockRenderer.Verify(f => f.RenderError(It.IsAny<string>()), Times.Once);
    }
}