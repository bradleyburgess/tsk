using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Infrastructure.FileIO;
using Tsk.Infrastructure.Repositories;

namespace Tsk.Infrastructure.Factories
{
    public class FlatFileTodoRepositoryFactory : ITodoRepositoryFactory
    {
        public ITodoRepository Create(string path)
        {
            var fileIO = new FileReaderWriter();
            var repo = new FlatFileTodoRepository(path, fileIO);
            repo.LoadTodos();
            return repo;
        }
    }
}