using Bogus;
using Moq;
using Tsk.CLI.Application.Commands;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Domain.Entities;
using Tsk.TestSupport;
using Tsk.CLI.Presentation;

namespace Tsk.CLI.Tests.Application.Commands;

public class DeleteCommandTests
{
    private static readonly Faker f = new();
    private static readonly TodoTestDataGenerator z = new();

    [Fact]
    public void Execute_DeletesTodo_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var todo = z.GetFakeTodoItem();

        var mockRenderer = new Mock<IRenderer>();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == todo.Id))).Returns(todo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new DeleteCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new DeleteCommand.Settings
        {
            Id = todo.Id.ToString(),
            FileName = fileName,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Delete(It.Is<TodoItem>(s => s.Id == todo.Id)), Times.Once);
        mockRenderer.Verify(f => f.RenderSuccess(It.IsAny<string>()), Times.Once);
    }
}
