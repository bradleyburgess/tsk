using Bogus;
using Tsk.Domain.Entities;

namespace Tsk.Domain.Tests.Entities;

public class TodoItemTests
{
    [Fact]
    public void ShouldThrow_Description_Blank()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(id: f.Random.Int(), description: string.Empty)
            );
        Assert.Throws<ArgumentNullException>(
            () => new TodoItem(id: 1, description: "")
        );
    }

    [Fact]
    public void ShouldThrow_Description_TooLong()
    {
        Faker<TodoItem> faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(id: f.Random.Int(), description: f.Random.String(100))
            );
        Assert.Throws<ArgumentException>(
            () => faker.Generate()
        );
    }

    [Fact]
    public void ShouldThrow_Description_LineBreaks()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(id: f.Random.Int(), description: f.Random.String(10) + "\n")
            );
        Assert.Throws<ArgumentException>(
            () => faker.Generate()
        );
    }

    [Fact]
    public void ShouldThrow_Location_Commas()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(
                    id: f.Random.Int(),
                    description: f.Random.String(10),
                    location: "Rondebosch,Common"
                )
            );
        Assert.Throws<ArgumentException>(
            () => faker.Generate()
        );
    }

    [Fact]
    public void ShouldThrow_Location_LineBreaks()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(
                    id: f.Random.Int(),
                    description: f.Random.String(10),
                    location: "Rondebosch\n"
                )
            );
        Assert.Throws<ArgumentException>(
            () => faker.Generate()
        );
    }

    [Fact]
    public void ShouldThrow_Location_TooLong()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(
                    id: f.Random.Int(),
                    description: f.Random.String(10),
                    location: f.Random.String(50)
                )
            );
        Assert.Throws<ArgumentException>(
            () => faker.Generate()
        );
    }

    [Fact]
    public void ShoulNotThrow_Without_Location()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(
                    id: f.Random.Int(),
                    description: f.Random.String(20),
                    location: null
                )
            );
        var exception = Record.Exception(() => faker.Generate());
        Assert.Null(exception);
    }

    [Fact]
    public void ShoulNotThrow_With_Location()
    {
        var faker = new Faker<TodoItem>()
            .CustomInstantiator(
                f => new TodoItem(
                    id: f.Random.Int(),
                    description: f.Random.String(20),
                    location: f.Address.City()
                )
            );
        var exception = Record.Exception(() => faker.Generate());
        Assert.Null(exception);
    }
}
