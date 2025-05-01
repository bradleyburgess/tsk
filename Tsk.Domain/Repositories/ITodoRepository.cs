using Tsk.Domain.Entities;

namespace Tsk.Domain.Repositories
{
    public interface ITodoRepository
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem? GetById(int id);
        void Add(TodoItem todoItem);
        void Save(TodoItem todoItem); // not needed for flat file
        void Delete(TodoItem todoItem);
    }
}