using Tsk.Domain.Entities;
using Tsk.Infrastructure.Factories;
using Tsk.TestSupport;
using Xunit;

namespace Tsk.Infrastructure.Tests.Factories;

public class FlatFileTodoRepositoryFactoryTests
{
    [Fact]
    public void ShouldCreate_FlatFileTodoRepository()
    {
        var fileName = TestHelpers.ResolveFromSlnRoot("tests", "Tsk.TestSupport", "TestData", "TestInputFile1.txt");
        var repo = new FlatFileTodoRepositoryFactory().Create(fileName);
        var list = repo.GetAll();
        Assert.Equal(3, list.Count());
        Assert.True(list.All(u => u is TodoItem));
    }
}
