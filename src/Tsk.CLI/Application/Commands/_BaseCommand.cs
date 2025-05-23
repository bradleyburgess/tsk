using Tsk.CLI.Presentation;
using Spectre.Console.Cli;
using Tsk.Infrastructure.Utils;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;

namespace Tsk.CLI.Application.Commands
{
    public abstract class BaseCommand<TSettings>(IRenderer renderer)
        : Command<TSettings> where TSettings : CommandSettings
    {
        private ITodoRepository? _repo;
        protected ITodoRepository Repo => _repo ?? throw new InvalidOperationException("Repository is not initialized!");
        protected readonly IRenderer _renderer = renderer;

        protected void InitRepository(string? path, ITodoRepositoryFactory factory)
        {
            string resolvedPath = Helpers.GetTskPath(path);

            try
            {

                _repo = factory.Create(resolvedPath);
            }
            catch (DirectoryNotFoundException)
            {
                var message = $"Directory {Path.GetDirectoryName(resolvedPath)} does not exist!";
                if (path is not null && path.Contains('$'))
                    message += " It seems like you're trying to use a $variable; please make sure it is resolving correctly.";
                _renderer.RenderError(message);
                Environment.Exit(1);
            }
            catch (UnauthorizedAccessException)
            {
                _renderer.RenderError($"You do not have permission to write to {path}");
                Environment.Exit(1);
            }


        }
    }
}