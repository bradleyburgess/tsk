using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Cli;
using Tsk.CLI.Application.Settings;
using Tsk.CLI.Utils;
using Tsk.Domain.Factories;
using Tsk.Domain.Repositories;

namespace Tsk.CLI.Application.Commands
{
    public abstract class BaseCommand<TSettings>
        : Command<TSettings> where TSettings : CommandSettings
    {
        protected ITodoRepository? _repo;

        protected void Init(string? path, ITodoRepositoryFactory factory)
        {
            string _path = AppData.GetTskPath(path);

            try
            {
                Helpers.EnsureTskFile(_path);
                _repo = factory.Create(_path);
            }
            catch (DirectoryNotFoundException)
            {
                var message = $"Directory {Path.GetDirectoryName(_path)} does not exist!";
                if (path is not null && path.Contains("$"))
                    message += " It seems like you're trying to use a $variable; please make sure it is resolving correctly.";
                System.Console.WriteLine(message);
                Environment.Exit(1);
            }
            catch (UnauthorizedAccessException)
            {
                System.Console.WriteLine($"You do not have permission to write to {path}");
                Environment.Exit(1);
            }

        }
    }
}