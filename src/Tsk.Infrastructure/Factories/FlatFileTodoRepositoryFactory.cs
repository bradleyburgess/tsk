using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.Infrastructure.FileIO;
using Tsk.Infrastructure.Repositories;
using Tsk.Infrastructure.Utils;

namespace Tsk.Infrastructure.Factories
{
    public class FlatFileTodoRepositoryFactory : ITodoRepositoryFactory
    {
        public ITodoRepository Create(string path)
        {
            Helpers.EnsureTskFile(path);
            var fileIO = new FileReaderWriter();
            var repo = new FlatFileTodoRepository(path, fileIO);
            repo.LoadTodos();
            return repo;

        }
    }
}