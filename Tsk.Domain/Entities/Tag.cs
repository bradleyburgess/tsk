using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsk.Domain.Validators;

namespace Tsk.Domain.Entities
{
    public class Tag
    {
        private string _name = string.Empty;
        public string Name
        {
            get => _name; set
            {
                Input.ValidateTag(value);
                _name = value;
            }
        }

        public Tag(string name)
        {
            Name = name;
        }
    }
}