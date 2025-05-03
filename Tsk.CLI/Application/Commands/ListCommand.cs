using Spectre.Console.Cli;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;
using Tsk.CLI.Application.Settings;
using Spectre.Console;

namespace Tsk.CLI.Application.Commands
{
    public class ListCommand(ITodoRepositoryFactory factory) : BaseCommand<ListCommand.Settings>
    {
        private readonly ITodoRepositoryFactory _factory = factory;

        public sealed class Settings : BaseCommandSettings { }

        public override int Execute(CommandContext context, Settings settings)
        {
            Init(settings.FileName, _factory);
            var list = _repo!.GetAll().ToList();
            if (list.Count == 0)
            {
                AnsiConsole.WriteLine("Your list is empty.");
                return 0;
            }
            var grid = new Grid();
            Style headingStyle = new Style(foreground: Color.Yellow, decoration: Decoration.Underline);
            Text[] headings = {
                new Text("Status", headingStyle).LeftJustified(),
                new Text("Description", headingStyle).LeftJustified(),
                new Text("Due Date", headingStyle).LeftJustified(),
                new Text("Tags", headingStyle).LeftJustified(),
                new Text("Location", headingStyle).LeftJustified()
            };
            for (var i = 1; i <= 5; i++) grid.AddColumn();
            grid.AddRow(
                [.. headings.Select(h => new Padder(h)
                    .PadTop(0)
                    .PadRight(2)
                    .PadBottom(0)
                    .PadLeft(0)
                )]
            );
            foreach (var t in list)
            {
                grid.AddRow(new Text[]{
                    new Text(t.Completed ? "X" : "").LeftJustified(),
                    new Text(t.Description, t.Completed ? new Style(decoration: Decoration.Strikethrough): null).LeftJustified(),
                    new Text(t.DueDate is not null? t.DueDate!.Value.ToString("yyyy-MM-dd") : "").LeftJustified(),
                    new Text(t.Tags.Count > 0 ? string.Join("\n", t.Tags.Select(u => u.Name)) : "").LeftJustified(),
                    new Text(t.Location ?? "").LeftJustified(),
                });
            }
            AnsiConsole.Write(grid);
            return 0;


        }
    }
}