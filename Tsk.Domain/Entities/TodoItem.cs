using Tsk.Domain.Validators;

namespace Tsk.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            protected set
            {
                InputValidators.ValidateDescription(value);
                _description = value;
            }
        }
        public void UpdateDescription(string input)
        {
            InputValidators.ValidateDescription(input);
            Description = input;
        }

        public DateOnly? DueDate { get; private set; } = null;
        public void UpdateDueDate(DateOnly? dueDate) => DueDate = dueDate;

        public bool Completed { get; private set; } = false;
        public void MarkComplete() => Completed = true;
        public void MarkIncomplete() => Completed = false;

        public string? Location { get; private set; } = null;
        public void UpdateLocation(string? location)
        {
            InputValidators.ValidateLocation(location ?? "");
            Location = location;
        }

        private List<Tag> _tags = new();
        public IReadOnlyList<Tag> Tags { get => _tags; }
        public void AddTag(Tag tag)
        {
            if (!Tags.Any(t => t.Name == tag.Name)) _tags.Add(tag);
        }
        public void RemoveTag(Tag tag) =>
            _tags.RemoveAll(t => t.Name == tag.Name);

        public TodoItem(
            int id,
            string description,
            string? location = null,
            DateOnly? dueDate = null,
            IEnumerable<Tag>? tags = null
        )
        {
            Id = id;
            UpdateDescription(description);
            if (dueDate is not null) UpdateDueDate(dueDate.Value);
            if (location is not null) UpdateLocation(location);
            if (tags is not null)
            {
                foreach (var tag in tags)
                {
                    AddTag(tag);
                }
            }
        }

        public override string ToString() => Description;
    }
}