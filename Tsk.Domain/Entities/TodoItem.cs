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
                Input.ValidateDescription(value);
                _description = value;
            }
        }
        public DateOnly? DueDate { get; private set; }
        public bool Completed { get; set; }
        public string? Location { get; set; } = string.Empty;

        private List<Tag> _tags = new();
        public IReadOnlyList<Tag> Tags { get => _tags; }

        public void MarkComplete() => Completed = true;
        public void MarkIncomplete() => Completed = false;

        public void UpdateDescription(string input)
        {
            Input.ValidateDescription(input);
            Description = input;
        }

        public void UpdateDueDate(DateOnly dueDate) => DueDate = dueDate;

        public void RemoveDueDate() => DueDate = null;

        public void UpdateLocation(string? location)
        {
            Input.ValidateLocation(location ?? "");
            Location = location;
        }

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
            DateOnly? dueDate = null
        )
        {
            Id = id;
            UpdateDescription(description);
            if (dueDate is not null) UpdateDueDate(dueDate.Value);
            if (location is not null) UpdateLocation(location);
        }

        public override string ToString() => Description;
    }
}