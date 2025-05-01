using Tsk.Domain.Entities;

namespace Tsk.Domain.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        void Add(TodoItem todoItem);
        void Save(TodoItem todoItem);
        void Delete(TodoItem todoItem);
    }
}