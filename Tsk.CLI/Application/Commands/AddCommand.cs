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
    public class AddCommand(ITodoRepositoryFactory factory) : BaseCommand<AddCommand.Settings>
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings
        {
            [Description("Set the todo description")]
            [CommandArgument(0, "<description>")]
            public string Description { get; set; } = string.Empty;

            [Description($"Set the todo due date in yyyyMMdd format, e.g. 20250501")]
            [CommandOption("-d|--date")]
            public string? DueDate { get; set; }

            [Description("Set the todo location")]
            [CommandOption("-l|--loc")]
            public string? Location { get; set; }

            [Description("Set the todo tags in a comma-separated list")]
            [CommandOption("-t|--tags")]
            public string? Tags { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            InitRepository(settings.FileName, _factory);
            try
            {
                var id = Repo.GetAll().Count() + 1;
                InputValidators.ValidateDescription(settings.Description);
                var todo = new TodoItem(
                    id: id,
                    description: settings.Description
                );
                if (!string.IsNullOrEmpty(settings.Location))
                {
                    InputValidators.ValidateLocation(settings.Location);
                    todo.UpdateLocation(settings.Location);
                }
                if (!string.IsNullOrEmpty(settings.Tags))
                {

                    var tags = settings.Tags.Split(",").Select(t => new Tag(t.Trim()));
                    foreach (var t in tags)
                        todo.AddTag(t);
                }
                if (settings.DueDate is not null)
                {
                    InputValidators.ValidateDateString(settings.DueDate);
                    var yyyy = int.Parse(settings.DueDate.Substring(0, 4));
                    var MM = int.Parse(settings.DueDate.Substring(4, 2));
                    var dd = int.Parse(settings.DueDate.Substring(6, 2));
                    todo.UpdateDueDate(new DateOnly(yyyy, MM, dd));
                }

                Repo.Add(todo);
                Renderers.RenderSuccess($":plus: Added \"{todo.Description}\"");
                return 0;
            }
            catch (Exception ex)
            {

                Renderers.RenderError(ex.Message);
                return 1;
            }
        }
    }
}