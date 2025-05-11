using Bogus;
using Moq;
using Tsk.CLI.Application.Commands;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Domain.Entities;
using Tsk.CLI.Presentation;

namespace Tsk.CLI.Tests.Application.Commands;

public class AddCommandTests
{
    private readonly static Faker f = new();

    [Fact]
    public void Execute_WithValidDescription_AddsTodo_ReturnsZero()
    {
        var id = f.Random.Int(1, 1000);
        var filename = f.System.FileName();
        var description = string.Join(" ", f.Lorem.Words(3));

        var mockRenderer = new Mock<IRenderer>();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>());

        var mockFactory = new Mock<ITodoRepositoryFactory>();
        mockFactory.Setup(f => f.Create(It.IsAny<string?>()!)).Returns(mockRepo.Object);

        var cmd = new AddCommand(mockFactory.Object, mockRenderer.Object);

        var settings = new AddCommand.Settings
        {
            Description = description,
            FileName = filename,
        };

        var result = cmd.Execute(null!, settings);

        Assert.Equal(0, result);
        mockRepo.Verify(r => r.Add(It.Is<TodoItem>(t => t.Description == description)), Times.Once);
        mockRenderer.Verify(f => f.RenderSuccess(It.IsAny<string>()), Times.Once);
    }
}
