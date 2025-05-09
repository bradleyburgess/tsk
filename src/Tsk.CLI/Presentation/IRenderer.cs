using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsk.Domain.Entities;

namespace Tsk.CLI.Presentation
{
    public interface IRenderer
    {
        public abstract static void RenderTodoList(IEnumerable<TodoItem> list);
        public abstract static void RenderError(string message);
    }
}