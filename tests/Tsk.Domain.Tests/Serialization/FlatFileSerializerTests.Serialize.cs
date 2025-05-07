using Tsk.Domain.Entities;
using Tsk.Domain.Serialization;
using Tsk.TestSupport;
using Bogus;

namespace Tsk.Domain.Tests.Serialization;

public class TodoItemFlatFile_SerializeTests
{
    private readonly TodoTestDataGenerator z = new();

    private readonly TodoItemFlatFileSerializer serializer = new();

    public static IEnumerable<object[]> TestData(TodoFields[]? fields) =>
        new TodoTestDataGenerator().GetTodoTestData(fields);


    [Theory]
    [MemberData(nameof(TestData), null)]
    public void Incomplete_And_Description(int id, string desc)
    {
        TodoItem t = new(
            id: id,
            description: desc
        );
        var result = serializer.Serialize(t);
        var expected = $"[ ] {TestHelpers.Enq(desc)}";
        Assert.Equal(result, expected);
    }

    [Theory]
    [MemberData(nameof(TestData), null)]
    public void Complete_And_Description(int id, string desc)
    {
        TodoItem t = new(
            id: id,
            description: desc
        );
        t.MarkComplete();
        var result = serializer.Serialize(t);
        var expected = $"[X] {TestHelpers.Enq(desc)}";
        Assert.Equal(result, expected);
    }

    [Theory]
    [MemberData(nameof(TestData), new TodoFields[] { TodoFields.DueDate })]
    public void Status_DueDate_Description(int id, string desc, DateTime date)
    {
        TodoItem t = new(
            id: id,
            description: desc,
            dueDate: DateOnly.FromDateTime(date)
        );
        t.MarkComplete();
        var result = serializer.Serialize(t);
        var expected = $"[X] {date.ToString("yyyyMMdd")} {TestHelpers.Enq(desc)}";
        Assert.Equal(result, expected);
    }

    [Theory]
    [MemberData(nameof(TestData), new TodoFields[] { TodoFields.DueDate, TodoFields.Location })]
    public void Status_DueDate_Description_Location(int id, string desc, DateTime date, string loc)
    {
        TodoItem t = new(
            id: id,
            description: desc,
            dueDate: DateOnly.FromDateTime(date),
            location: loc
        );
        t.MarkComplete();
        var result = serializer.Serialize(t);
        var expected = $"[X] {date.ToString("yyyyMMdd")} {TestHelpers.Enq(desc)} loc:{TestHelpers.Enq(loc)}";
        Assert.Equal(result, expected);
    }

    [Theory]
    [MemberData(nameof(TestData), new TodoFields[] { TodoFields.DueDate, TodoFields.Location, TodoFields.Tags })]
    public void Status_DueDate_Description_Location_Tags(int id, string desc, DateTime date, string loc, string[] _tags)
    {
        var tags = string.Join(",", _tags).ToLower();
        TodoItem t = new(
            id: id,
            description: desc,
            dueDate: DateOnly.FromDateTime(date),
            location: loc
        );
        t.MarkComplete();
        foreach (var u in _tags) t.AddTag(new Tag(u));
        var result = serializer.Serialize(t);
        var expected = $"[X] {date.ToString("yyyyMMdd")} {TestHelpers.Enq(desc)} loc:{TestHelpers.Enq(loc)} tags:{tags}";
        Assert.Equal(result, expected);
    }
}
