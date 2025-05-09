using Spectre.Console.Cli;
using Tsk.Domain.Factories;
using Tsk.CLI.Application.Settings;
using Tsk.CLI.Presentation;
using System.ComponentModel;
using Tsk.Domain.Entities;
using Tsk.Domain.Validators;
using Tsk.CLI.Utils;

namespace Tsk.CLI.Application.Commands
{
    public class UpdateCommand(ITodoRepositoryFactory factory) : BaseCommand<UpdateCommand.Settings>
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings
        {
            [Description("ID of todo to update")]
            [CommandArgument(0, "<id>")]
            public string Id { get; set; } = string.Empty;

            [Description("Update the todo description")]
            // [CommandOption("--desc")]
            [CommandArgument(1, "[description]")]
            public string Description { get; set; } = string.Empty;

            [Description($"Update the todo due date in yyyyMMdd format, e.g. 20250501")]
            [CommandOption("-d|--date")]
            public string? DueDate { get; set; }

            [Description("Update the todo location")]
            [CommandOption("-l|--loc")]
            public string? Location { get; set; }

            [Description("Update the todo tags in a comma-separated list")]
            [CommandOption("-t|--tags")]
            public string? UpdateTags { get; set; }

            [Description("Add tag(s) in a comma-separated list")]
            [CommandOption("--add-tags|--add-tag")]
            public string? AddTags { get; set; }

            [Description("Remove tag(s) in a comma-separated list")]
            [CommandOption("--remove-tag|--remove-tags")]
            public string? RemoveTags { get; set; }
        }

        public override int Execute(CommandContext context, Settings settings)
        {
            InitRepository(settings.FileName, _factory);
            try
            {
                InputValidators.ValidateIdString(settings.Id);
                CommandValidators.ValidateTagOptions(settings);
                var todo = Repo.GetById(int.Parse(settings.Id))!;

                UpdateDescription(settings, todo);
                UpdateLocation(settings, todo);
                UpdateTags(settings, todo);
                AddTags(settings, todo);
                RemoveTags(settings, todo);
                UpdateDueDate(settings, todo);

                Repo.Save(todo);
                Renderers.RenderSuccess($":floppy_disk: Updated todo \"{todo.Id}\"");
                return 0;
            }
            catch (Exception ex)
            {
                Renderers.RenderError(ex.Message);
                return 1;
            }
        }

        private static void UpdateDescription(Settings settings, TodoItem todo)
        {
            if (!string.IsNullOrEmpty(settings.Description))
            {
                InputValidators.ValidateDescription(settings.Description);
                todo.UpdateDescription(settings.Description);
            }
        }

        private static void UpdateLocation(Settings settings, TodoItem todo)
        {
            if (settings.Location is not null)
            {
                if (settings.Location == string.Empty)
                {
                    todo.UpdateLocation(null);
                }
                else
                {
                    InputValidators.ValidateLocation(settings.Location);
                    todo.UpdateLocation(settings.Location);
                }
            }
        }

        private static void UpdateTags(Settings settings, TodoItem todo)
        {
            if (settings.UpdateTags is not null)
            {
                if (settings.UpdateTags == string.Empty)
                {
                    var allTags = todo.Tags.ToList();
                    foreach (var t in allTags)
                    {
                        todo.RemoveTag(t);
                    }
                }
                else if (!string.IsNullOrEmpty(settings.UpdateTags))
                {
                    var updatedTags = Parsers.ParseTagsFromString(settings.UpdateTags);
                    foreach (var t in updatedTags)
                        todo.AddTag(t);
                    List<Tag> tagsToRemove = new();
                    foreach (var t in todo.Tags)
                        if (!updatedTags.Any(u => u.Name == t.Name))
                            tagsToRemove.Add(t);
                    foreach (var t in tagsToRemove)
                        todo.RemoveTag(t);
                }
            }
        }

        private static void AddTags(Settings settings, TodoItem todo)
        {
            if (!string.IsNullOrEmpty(settings.AddTags))
            {
                var newTags = Parsers.ParseTagsFromString(settings.AddTags);
                foreach (var t in newTags)
                    todo.AddTag(t);
            }
        }

        private static void RemoveTags(Settings settings, TodoItem todo)
        {
            if (!string.IsNullOrEmpty(settings.RemoveTags))
            {
                var removeTags = Parsers.ParseTagsFromString(settings.RemoveTags);
                List<Tag> tagsToRemove = new();
                foreach (var t in todo.Tags)
                {
                    if (removeTags.Any(u => u.Name == t.Name))
                        tagsToRemove.Add(t);
                }
                foreach (var t in tagsToRemove)
                {
                    todo.RemoveTag(t);
                }
            }
        }

        private static void UpdateDueDate(Settings settings, TodoItem todo)
        {
            if (settings.DueDate == string.Empty)
            {
                todo.UpdateDueDate(null);
            }
            else if (settings.DueDate is not null)
            {
                InputValidators.ValidateDateString(settings.DueDate);
                todo.UpdateDueDate(Parsers.ParseDateFromString(settings.DueDate));
            }
        }
    }
}