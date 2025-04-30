using Tsk.Domain.Entities;

namespace Tsk.Domain.Serialization
{
    public interface ITodoItemSerializer
    {
        string Serialize(TodoItem todoItem);
        TodoItem Deserialize(string line);
    }
}