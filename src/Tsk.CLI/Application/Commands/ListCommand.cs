using Spectre.Console.Cli;
using Tsk.Domain.Factories;
using Tsk.CLI.Application.Settings;
using Tsk.CLI.Presentation;
using System.ComponentModel;

namespace Tsk.CLI.Application.Commands
{
    public class ListCommand(ITodoRepositoryFactory factory, IRenderer renderer)
        : BaseCommand<ListCommand.Settings>(renderer)
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings
        {
            [Description("Sort the list (`loc`, `date`)")]
            [CommandOption("--sort-by")]
            public string? SortBy { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            InitRepository(settings.FileName, _factory);
            try
            {
                var list = Repo.GetAll().OrderBy(s => s.Completed);
                if (!string.IsNullOrEmpty(settings.SortBy))
                {
                    switch (settings.SortBy.ToLower())
                    {
                        case "desc" or "description":
                            list = list.ThenBy(s => s.Description);
                            break;
                        case "loc" or "location":
                            list = list.ThenBy(s => s.Location);
                            break;
                        case "date" or "duedate":
                            list = list.ThenBy(s => s.DueDate);
                            break;
                        default:
                            throw new ArgumentException("Not a valid sort option!");
                    }
                }
                _renderer.RenderTodoList(list);
                return 0;
            }
            catch (Exception ex)
            {
                _renderer.RenderError(ex.Message);
                return 1;
            }
        }
    }
}