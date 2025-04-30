using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsk.Domain.Entities;

namespace Tsk.Domain.Serialization
{
    public class TodoItemFlatFileSerializer : ITodoItemSerializer
    {
        public TodoItem Deserialize(string line)
        {
            throw new NotImplementedException();
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