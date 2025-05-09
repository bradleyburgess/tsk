using Bogus;
using Moq;
using Tsk.CLI.Application.Commands;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Domain.Entities;
using Tsk.CLI.Presentation;

namespace Tsk.CLI.Tests.Application.Commands;

public class ListCommandTests
{
    private static readonly Faker f = new();

    [Fact]
    public void Execute_ListsTodos_ReturnsZero()
    {
        var fileName = f.System.FileName();
        var mockRepo = new Mock<ITodoRepository>();
        mockRepo.Setup(r => r.GetAll()).Returns(new List<TodoItem>());

        var mockRenderer = new Mock<SpectreRenderer>();
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
}
