using Spectre.Console.Cli;
using Tsk.Domain.Factories;
using Tsk.CLI.Application.Settings;
using Tsk.CLI.Presentation;

namespace Tsk.CLI.Application.Commands
{
    public class ListCommand(ITodoRepositoryFactory factory, IRenderer renderer)
        : BaseCommand<ListCommand.Settings>(renderer)
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings { }

        public override int Execute(CommandContext context, Settings settings)
        {
            InitRepository(settings.FileName, _factory);
            try
            {
                _renderer.RenderTodoList(Repo.GetAll());
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}