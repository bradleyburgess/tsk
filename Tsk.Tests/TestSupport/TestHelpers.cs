using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tsk.Tests.TestSupport
{
    public class TestHelpers
    {
        public static string Enq(string input) =>
            input.Contains(" ") ? $"\"{input}\"" : input;

        public static string ResolveFromSlnRoot(params string[] relativeSegments)
        {
            var dir = new DirectoryInfo(AppContext.BaseDirectory);

            while (dir != null && !File.Exists(Path.Combine(dir.FullName, "Tsk.slnx")))
            {
                dir = dir.Parent;
            }

            if (dir == null)
                throw new DirectoryNotFoundException("Could not find the solution root containing Tsk.slnx");

            return Path.Combine(new[] { dir.FullName }.Concat(relativeSegments).ToArray());
        }
    }
}