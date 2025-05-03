using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console.Cli;

namespace Tsk.CLI.Application.Settings
{
    public abstract class BaseCommandSettings : CommandSettings
    {
        [CommandOption("-f|--file <FILE_NAME>")]
        public string? FileName { get; set; }
    }
}