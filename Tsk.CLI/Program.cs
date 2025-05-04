using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using Tsk.CLI.Infrastructure.DI;
using Tsk.Domain.Factories;
using Tsk.Infrastructure.Factories;
using Tsk.CLI.Application.Commands;
using Tsk.CLI.Utils;


namespace Tsk.CLI;

class Program
{
    static int Main(string[] args)
    {
        var registrations = new ServiceCollection();
        registrations.AddScoped<ITodoRepositoryFactory, FlatFileTodoRepositoryFactory>();
        var registrar = new TypeRegistrar(registrations);

        var app = new CommandApp(registrar);
        app.Configure(config =>
        {
            config.SetApplicationName("tsk");
            config.SetApplicationVersion($"v{AppData.TskVersion}");

            config.AddCommand<ListCommand>("list")
                .WithDescription("List tasks")
                .WithAlias("l")
                .WithExample("list")
                .WithExample("l");

            config.AddCommand<AddCommand>("add")
                .WithAlias("a")
                .WithDescription("Add a todo")
                .WithExample("add \"update resume\"")
                .WithExample([$"add \"buy milk\" --date {DateTime.Now.ToString("yyyyMMdd")} --loc \"Shoprite\" --tags shopping,errands"]);

            config.AddCommand<DeleteCommand>("delete")
                .WithAlias("del")
                .WithAlias("d")
                .WithDescription("Delete a todo")
                .WithExample("delete 2")
                .WithExample("del 2");

            config.AddCommand<CompleteCommand>("complete")
                .WithDescription("Mark a todo as completed")
                .WithAlias("c")
                .WithAlias("check")
                .WithAlias("x")
                .WithExample("complete 1")
                .WithExample("x 1")
                .WithExample("check 1");

            config.AddCommand<IncompleteCommand>("incomplete")
                .WithDescription("Mark a todo as not completed")
                .WithAlias("i")
                .WithAlias("uncheck")
                .WithAlias("o")
                .WithExample("incomplete 1")
                .WithExample("o 1")
                .WithExample("uncheck 1");

            config.AddCommand<UpdateCommand>("update")
                .WithDescription("Update a todo")
                .WithAlias("u")
                .WithExample("update 1 --desc \"fix typos in resume\" --loc home");
        });
        return app.Run(args);
    }
}
