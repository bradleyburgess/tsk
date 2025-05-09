using Spectre.Console.Cli;
using Tsk.Domain.Factories;
using Tsk.CLI.Application.Settings;
using Tsk.CLI.Presentation;
using System.ComponentModel;
using Tsk.Domain.Entities;
using Tsk.Domain.Validators;
using Spectre.Console;

namespace Tsk.CLI.Application.Commands
{
    public class CompleteCommand(ITodoRepositoryFactory factory) : BaseCommand<CompleteCommand.Settings>
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings
        {
            [Description("ID of the todo to mark completed")]
            [CommandArgument(0, "<id>")]
            public string Id { get; set; } = string.Empty;
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            InitRepository(settings.FileName, _factory);
            try
            {
                InputValidators.ValidateIdString(settings.Id);
                var id = int.Parse(settings.Id);
                var todo = Repo.GetById(id);
                todo!.MarkComplete();
                Repo.Save(todo);

                Renderer.RenderSuccess($":check_mark: Marked todo {id} as completed.");
                return 0;
            }
            catch (Exception ex)
            {
                Renderer.RenderError(ex.Message);
                return 1;
            }
        }
    }
}