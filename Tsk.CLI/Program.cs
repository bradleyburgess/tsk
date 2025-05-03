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
    static void Main(string[] args)
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
                .WithDescription("List tasks");
        });
        app.Run(args);
    }
}
