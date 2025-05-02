using Tsk.Domain.Repositories;

namespace Tsk.Domain.Factories
{
    public interface ITodoRepositoryFactory
    {
        ITodoRepository Create(string path);
    }
}