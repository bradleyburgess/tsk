using Bogus;
using Tsk.Domain.Serialization;
using Tsk.Domain.Entities;
using Tsk.TestSupport;

namespace Tsk.Domain.Tests.Serialization;


public class TodoItemFlatFileDeserializeTests
{
    private readonly TodoItemFlatFileSerializer serializer = new();
    private readonly Faker f = new();
    public static IEnumerable<object[]> TestData(TodoFields[]? fields) =>
        new TodoTestDataGenerator().GetTodoTestData(fields);

    [Theory]
    [MemberData(nameof(TestData), null)]
    public void Complete_And_Description(int id, string desc)
    {
        var line = $"[X] \"{desc}\"";
        TodoItem result = serializer.Deserialize(line, id);

        Assert.Equal(desc, result.Description);
        Assert.True(result.Completed);
        Assert.Equal(id, result.Id);
    }

    [Theory]
    [MemberData(nameof(TestData), null)]
    public void Incomplete_And_Description(int id, string desc)
    {
        var line = $"[ ] \"{desc}\"";
        TodoItem result = serializer.Deserialize(line, id);

        Assert.Equal(desc, result.Description);
        Assert.False(result.Completed);
        Assert.Equal(id, result.Id);
    }

    [Theory]
    [MemberData(nameof(TestData), new TodoFields[] { TodoFields.DueDate })]
    public void With_DueDate(int id, string desc, DateTime _date)
    {
        var completed = f.Random.Bool();
        var date = DateOnly.FromDateTime(_date);
        var line = $"[{(completed ? "X" : " ")}] {date.ToString("yyyyMMdd")} \"{desc}\"";
        TodoItem result = serializer.Deserialize(line, id);

        Assert.Equal(desc, result.Description);
        Assert.Equal(completed, result.Completed);
        Assert.Equal(id, result.Id);
        Assert.Equal(date, result.DueDate);
    }

    [Theory]
    [MemberData(nameof(TestData), new TodoFields[] { TodoFields.DueDate, TodoFields.Location })]
    public void With_DueDate_And_Location(int id, string desc, DateTime _date, string location)
    {
        var completed = f.Random.Bool();
        var date = DateOnly.FromDateTime(_date);
        var line = $"[{(completed ? "X" : " ")}] {date.ToString("yyyyMMdd")} \"{desc}\" loc:\"{location}\"";
        TodoItem result = serializer.Deserialize(line, id);

        Assert.Equal(desc, result.Description);
        Assert.Equal(completed, result.Completed);
        Assert.Equal(id, result.Id);
        Assert.Equal(date, result.DueDate);
        Assert.Equal(location, result.Location);
    }

    [Theory]
    [MemberData(nameof(TestData), new TodoFields[] { TodoFields.DueDate, TodoFields.Location, TodoFields.Tags })]
    public void With_DueDate_And_Location_And_Tags(int id, string desc, DateTime _date, string location, string[] tags)
    {
        var completed = f.Random.Bool();
        var date = DateOnly.FromDateTime(_date);
        var line =
            $"[{(completed ? "X" : " ")}] " +
            $"{date.ToString("yyyyMMdd")} " +
            $"\"{desc}\" " +
            $"loc:\"{location}\"" +
            $"tags:{string.Join(",", tags)}";
        TodoItem result = serializer.Deserialize(line, id);

        Assert.Equal(desc, result.Description);
        Assert.Equal(completed, result.Completed);
        Assert.Equal(id, result.Id);
        Assert.Equal(date, result.DueDate);
        Assert.Equal(location, result.Location);
        Assert.All(tags, u => result.Tags.Any(t => t.Name == u));

    }

    [Fact]
    public void ShouldError_WithBadDate()
    {
        var line = @"[X] 20259999 ""buy milk""";
        Assert.Throws<FormatException>(() => serializer.Deserialize(line, 1));
    }
}
