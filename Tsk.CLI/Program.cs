using Tsk.CLI.Utils;

namespace Tsk.CLI;

class Program
{
    static void Main(string[] args)
    {
        System.Console.WriteLine(AppData.GetDefaultTskPath());
    }
}