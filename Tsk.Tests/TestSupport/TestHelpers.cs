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
    }
}