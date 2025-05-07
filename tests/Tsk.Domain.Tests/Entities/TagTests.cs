using Bogus;
using Tsk.Domain.Entities;

namespace Tsk.Domain.Tests.Entities;

public class TagTests
{
    [Fact]
    public void ShouldThrow_With_Comma()
    {
        var faker = new Faker<Tag>()
            .CustomInstantiator(
                f => new Tag(name: f.Random.String(4) + "," + f.Random.String())
            );
        Assert.Throws<ArgumentException>(() => faker.Generate());
    }

    [Fact]
    public void ShouldThrow_With_LineBreak()
    {
        var faker = new Faker<Tag>()
            .CustomInstantiator(
                f => new Tag(name: f.Random.String(4) + "\r")
            );
        Assert.Throws<ArgumentException>(() => faker.Generate());
    }

    [Fact]
    public void ShouldThrow_With_TooLong()
    {
        var faker = new Faker<Tag>()
            .CustomInstantiator(
                f => new Tag(name: f.Random.String(50))
            );
        Assert.Throws<ArgumentException>(() => faker.Generate());
    }

    [Fact]
    public void ShouldThrow_Blank()
    {
        var faker = new Faker<Tag>()
            .CustomInstantiator(
                f => new Tag(name: string.Empty)
            );
        Assert.Throws<ArgumentNullException>(() => faker.Generate());
    }
}