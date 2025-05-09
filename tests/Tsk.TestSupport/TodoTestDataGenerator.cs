using Bogus;
using Tsk.Domain.Entities;

namespace Tsk.TestSupport;

public enum TodoFields
{
    Location,
    DueDate,
    Tags
}

public class TodoTestDataGenerator()
{
    public Faker f = new();

    public IEnumerable<object[]> GetTodoTestData(TodoFields[]? fields)
    {
        List<object[]> data = new();
        for (var i = 0; i < 5; i++)
        {
            List<object> testItem = new() {
                        int.Abs(f.Random.Int()),
                        string.Join(" ", f.Lorem.Words(3)),
                    };
            if (fields is not null)
            {
                if (fields.Contains(TodoFields.DueDate)) testItem.Add(f.Date.Soon());
                if (fields.Contains(TodoFields.Location)) testItem.Add(f.Address.City());
                if (fields.Contains(TodoFields.Tags))
                    testItem.Add(new string[] { f.Lorem.Word(), string.Join("-", f.Lorem.Words(2)) });
            }
            data.Add(testItem.ToArray());
        }
        return data;
    }

    public TodoItem GetFakeTodoItem() =>
        new TodoItem(
            id: f.Random.Int(1, 1000),
            description: string.Join(" ", f.Lorem.Words(3)),
            dueDate: f.Date.SoonDateOnly(),
            location: f.Address.City(),
            tags: [new Tag(f.Lorem.Word()), new Tag(string.Join("-", f.Lorem.Words(2)))]
        );

}
