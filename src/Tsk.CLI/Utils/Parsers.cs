using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsk.Domain.Entities;

namespace Tsk.CLI.Utils
{
    public class Parsers
    {
        public static DateOnly ParseDateFromString(string dateString) =>
            new(
                int.Parse(dateString.Substring(0, 4)),
                int.Parse(dateString.Substring(4, 2)),
                int.Parse(dateString.Substring(6, 2))
            );

        public static IEnumerable<Tag> ParseTagsFromString(string tagsString) =>
            tagsString.Split(",").Select(t => new Tag(t.Trim()));
    }
}