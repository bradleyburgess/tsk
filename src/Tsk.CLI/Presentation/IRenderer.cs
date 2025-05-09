using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsk.Domain.Entities;

namespace Tsk.CLI.Presentation
{
    public interface IRenderer
    {
        public void RenderTodoList(IEnumerable<TodoItem> list);
        public void RenderError(string message);
        public void RenderSuccess(string message);
    }
}