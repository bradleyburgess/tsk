using Bogus;
using Moq;
using Tsk.CLI.Application.Commands;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Domain.Entities;

namespace Tsk.CLI.Tests.Application.Commands;

public class DeleteCommandTests
{
    private static readonly Faker f = new();

    [Fact]
    public void Execute_DeletesTodo_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var id = f.Random.Int(1, 1000);
        var description = string.Join(" ", f.Lorem.Words(3));
        var mockRepo = new Mock<ITodoRepository>();
        var fakeTodo = new TodoItem(id: id, description: description);
        mockRepo.Setup(f => f.GetById(It.Is<int>(s => s == id))).Returns(fakeTodo);

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);


        var cmd = new DeleteCommand(mockFactory.Object);

        var settings = new DeleteCommand.Settings
        {
            Id = id.ToString(),
            FileName = fileName,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Delete(It.Is<TodoItem>(s => s.Id == id)), Times.Once);
    }
}
