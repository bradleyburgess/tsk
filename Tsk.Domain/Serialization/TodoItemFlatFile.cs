using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Tsk.Domain.Entities;

namespace Tsk.Domain.Serialization
{
    public class TodoItemFlatFileSerializer : ITodoItemSerializer
    {
        private static readonly Regex FlatLineMainRegex = new(
            @"^\[(?<status>X|\s|\d)\]\s+((?:(?<date>\d{8})?)\s+)?""(?<desc>[^""]*)""\s?(?<extras>.*)?$",
            RegexOptions.Compiled
        );
        private static readonly Regex FlatLineExtrasRegex = new(
            @"(?<key>tags|loc):(?<value>(""[^""]*"")|[^ ]+)",
            RegexOptions.Compiled
        );

        public TodoItem Deserialize(string line, int lineNum)
        {
            var match = FlatLineMainRegex.Match(line);
            if (!match.Success)
            {
                var message = $"Bad syntax in line {lineNum.ToString()}";
                throw new FormatException(message);
            }
            string status = match.Groups["status"].Value;
            string _date = match.Groups["date"].Value;
            DateOnly? date = null;
            if (!string.IsNullOrEmpty(_date))
            {
                try
                {
                    date = new DateOnly(
                        int.Parse(_date.Substring(0, 4)),
                        int.Parse(_date.Substring(4, 2)),
                        int.Parse(_date.Substring(6, 2))
                    );
                }
                catch (ArgumentOutOfRangeException)
                {
                    throw new FormatException($"Bad date in line {lineNum}");
                }
            }
            string desc = match.Groups["desc"].Value;
            string? location = null;
            List<Tag>? tags = new();
            string _extras = match.Groups["extras"].Value;
            if (_extras.Length > 0)
            {
                var extrasMatches = FlatLineExtrasRegex.Matches(_extras);
                if (!extrasMatches.All(m => m.Success))
                {
                    var message = "Bad syntax";
                    message += $" in line {lineNum.ToString()}";
                    throw new FormatException(message);
                }
                foreach (Match m in extrasMatches)
                {
                    var key = m.Groups["key"].Value;
                    var value = m.Groups["value"].Value.Trim('"');

                    if (key == "loc") location = value;
                    if (key == "tags")
                    {
                        string[] _tags = value.Split(",");
                        foreach (var t in _tags)
                        {
                            tags.Add(new Tag(t));
                        }
                    }

                }
            }
            TodoItem result = new TodoItem(
                id: lineNum,
                description: desc,
                dueDate: date,
                location: location
            );
            if (status == "X") result.MarkComplete();
            foreach (var tag in tags) result.AddTag(tag);
            return result;
        }

        public string Serialize(TodoItem t)
        {
            List<string> r = new();
            r.Add($"[{(t.Completed ? "X" : " ")}]");
            if (t.DueDate is not null)
                r.Add(t.DueDate.Value.ToString("yyyyMMdd"));
            r.Add($"\"{t.Description}\"");
            if (!string.IsNullOrEmpty(t.Location))
                r.Add($"loc:{(t.Location.Contains(" ") ? $"\"{t.Location}\"" : t.Location)}");
            if (t.Tags is not null && t.Tags?.Count > 0)
                r.Add($"tags:{string.Join(",", t.Tags.Select(u => u.Name))}");
            return string.Join(" ", r);

        }
    }
}