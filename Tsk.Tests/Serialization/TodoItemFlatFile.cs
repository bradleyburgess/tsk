using Tsk.Domain.Entities;
using Tsk.Domain.Serialization;

namespace Tsk.Tests.Serialization
{
    public class TodoItemFlatFile_Serialize
    {
        private readonly TodoItemFlatFileSerializer serializer = new();

        [Fact]
        public void Incomplete_And_Description()
        {
            TodoItem t = new(
                id: 1,
                description: "buy milk"
            );
            var result = serializer.Serialize(t);
            var expected = "[ ] \"buy milk\"";
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Complete_And_Description()
        {
            TodoItem t = new(
                id: 1,
                description: "buy milk"
            );
            t.MarkComplete();
            var result = serializer.Serialize(t);
            var expected = "[X] \"buy milk\"";
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Status_DueDate_Description()
        {
            TodoItem t = new(
                id: 1,
                description: "buy milk",
                dueDate: new DateOnly(2000, 12, 31)
            );
            t.MarkComplete();
            var result = serializer.Serialize(t);
            var expected = "[X] 20001231 \"buy milk\"";
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Status_DueDate_Description_Location()
        {
            TodoItem t = new(
                id: 1,
                description: "buy milk",
                dueDate: new DateOnly(2000, 12, 31),
                location: "New York"
            );
            t.MarkComplete();
            var result = serializer.Serialize(t);
            var expected = "[X] 20001231 \"buy milk\" loc:\"New York\"";
            Assert.Equal(result, expected);
        }

        [Fact]
        public void Status_DueDate_Description_Location_Tags()
        {
            TodoItem t = new(
                id: 1,
                description: "buy milk",
                dueDate: new DateOnly(2000, 12, 31),
                location: "New York"
            );
            t.MarkComplete();
            t.AddTag(new Tag("shopping"));
            t.AddTag(new Tag("out-and-about"));
            var result = serializer.Serialize(t);
            var expected = "[X] 20001231 \"buy milk\" loc:\"New York\" tags:shopping,out-and-about";
            Assert.Equal(result, expected);
        }
    }
}