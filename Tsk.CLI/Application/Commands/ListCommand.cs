using Spectre.Console.Cli;
using Tsk.Domain.Factories;
using Tsk.CLI.Application.Settings;
using Tsk.CLI.Presentation;

namespace Tsk.CLI.Application.Commands
{
    public class ListCommand(ITodoRepositoryFactory factory) : BaseCommand<ListCommand.Settings>
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings { }

        public override int Execute(CommandContext context, Settings settings)
        {
            InitRepository(settings.FileName, _factory);
            try
            {
                TodoListRenderer.Render(Repo.GetAll());
                return 0;
            }
            catch (Exception ex)
            {

                System.Console.WriteLine(ex.Message);
                return 1;
            }
        }
    }
}