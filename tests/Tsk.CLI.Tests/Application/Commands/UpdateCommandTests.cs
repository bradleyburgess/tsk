using Bogus;
using Moq;
using Tsk.CLI.Application.Commands;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Domain.Entities;
using Tsk.TestSupport;

namespace Tsk.CLI.Tests.Application.Commands;

public class UpdateCommandTests
{
    private static readonly Faker f = new();
    private static readonly TodoTestDataGenerator z = new();

    [Fact]
    public void Execute_WithDescription_UpdatesDescription_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var newDescription = string.Join(" ", f.Lorem.Words(3));
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            Description = newDescription,
            FileName = fileName,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.Description == newDescription)));
    }

    [Fact]
    public void Execute_WithLocation_UpdatesLocation_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var newLocation = f.Address.Country();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
            Location = newLocation,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.Location == newLocation)));
    }

    [Fact]
    public void Execute_WithBlankLocation_RemovesLocation_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
            Location = ""
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => string.IsNullOrEmpty(s.Location))));
    }

    [Fact]
    public void Execute_WithNoLocation_LeavesLocation_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.Location == todo.Location)));
    }

    [Fact]
    public void Execute_WithDueDate_UpdatesDate_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var newDate = f.Date.SoonDateOnly();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            DueDate = newDate.ToString("yyyyMMdd"),
            FileName = fileName,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.DueDate == newDate)));
    }

    [Fact]
    public void Execute_WithBlankDate_RemovesDate_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
            DueDate = "",
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.DueDate == null)));
    }

    [Fact]
    public void Execute_WithNoDate_KeepsDate_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.DueDate == todo.DueDate)));
    }

    [Fact]
    public void Execute_WithAddTag_AddsTag_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var newTag = f.Lorem.Word();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
            AddTags = newTag,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.Tags.Any(t => t.Name == newTag))));
    }

    [Fact]
    public void Execute_WithRemoveTag_RemovesTag_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var newTag = new Tag(f.Lorem.Word());
        var tagCount = todo.Tags.Count;
        var mockRepo = new Mock<ITodoRepository>();
        todo.AddTag(newTag);
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
            RemoveTags = newTag.Name,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.Tags.Count == tagCount && s.Tags.All(t => t.Name != newTag.Name))));
    }

    [Fact]
    public void Execute_WithBlankTags_RemovesTags_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();
        var tag1 = new Tag(f.Lorem.Word());
        var tag2 = new Tag(f.Lorem.Word());
        var mockRepo = new Mock<ITodoRepository>();
        todo.AddTag(tag1);
        todo.AddTag(tag2);
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new UpdateCommand(mockFactory.Object);

        var settings = new UpdateCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
            UpdateTags = "",
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Save(It.Is<TodoItem>(s => s.Tags.Count == 0)), Times.Once);
    }
}
