using Spectre.Console;
using Spectre.Console.Rendering;
using Tsk.Domain.Entities;

namespace Tsk.CLI.Presentation
{
    public static class Renderers
    {
        public static void RenderTodoList(IEnumerable<TodoItem> list)
        {
            if (!list.Any())
            {
                AnsiConsole.WriteLine("Your list is empty.");
                return;
            }
            var grid = new Grid();
            Style headingStyle = new Style(foreground: Color.Yellow, decoration: Decoration.Underline);
            Text[] headings = {
                new Text("Id", headingStyle).LeftJustified(),
                new Text("Status", headingStyle).LeftJustified(),
                new Text("Description", headingStyle).LeftJustified(),
                new Text("Due Date", headingStyle).LeftJustified(),
                new Text("Tags", headingStyle).LeftJustified(),
                new Text("Location", headingStyle).LeftJustified()
            };
            for (var i = 0; i < headings.Length; i++) grid.AddColumn();
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
                var row = new IRenderable[]{
                    new Text(t.Id.ToString()).LeftJustified(),
                    new Markup(t.Completed ? ":check_mark_button:" : ":hourglass_not_done:").LeftJustified(),
                    new Text(t.Description, t.Completed ? new Style(decoration: Decoration.Strikethrough): null).LeftJustified(),
                    new Text(t.DueDate is not null? t.DueDate!.Value.ToString("yyyy-MM-dd") : "").LeftJustified(),
                    new Text(t.Tags.Count > 0 ? string.Join("\n", t.Tags.Select(u => u.Name)) : "").LeftJustified(),
                    new Text(t.Location ?? "").LeftJustified(),
                };
                grid.AddRow([.. row.Select(r => new Padder(r).PadBottom(1).PadTop(0).PadRight(1).PadLeft(0))]);
            }
            AnsiConsole.Write(grid);
        }

        public static void RenderError(string message) =>
            AnsiConsole.Write(new Markup($"[red][bold]Error:[/][/] {message}\n"));
        
        public static void RenderSuccess(string message) =>
            AnsiConsole.Write(new Markup($"[green]Success![/] {message}\n"));
    }
}