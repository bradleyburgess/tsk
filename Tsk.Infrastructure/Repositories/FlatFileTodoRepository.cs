using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tsk.Domain.Entities;
using Tsk.Domain.Repositories;
using Tsk.Domain.Serialization;
using Tsk.Infrastructure.FileIO;

namespace Tsk.Infrastructure.Repositories
{
    public class FlatFileTodoRepository : ITodoRepository
    {
        private string _filepath;
        private IFileReaderWriter _fileIO;
        private List<TodoItem>? _list = null;
        private List<Tag> _tags = new();
        private TodoItemFlatFileSerializer _serializer = new();


        public FlatFileTodoRepository(string filePath, IFileReaderWriter fileIO)
        {
            _filepath = filePath;
            _fileIO = fileIO;
        }

        public void LoadTodos()
        {
            _list = new();
            var lines = _fileIO.ReadAllLines(_filepath);
            var lineNum = 1;
            foreach (var line in lines)
            {
                TodoItem todo = _serializer.Deserialize(line, lineNum++);
                _list.Add(todo);
            }
            foreach (var todo in _list)
                foreach (var tag in todo.Tags)
                    if (!_tags.Any(t => t.Name == tag.Name)) _tags.Add(tag);
        }

        public void Add(TodoItem todoItem) => _list?.Add(todoItem);

        public void Delete(TodoItem todoItem)
        {
            _list?.RemoveAll(t => t.Id == todoItem.Id);
        }

        public IEnumerable<TodoItem> GetAll()
        {
            if (_list is null)
                throw new NullReferenceException("List is null; did you for get to `LoadTodos`?");
            return _list;
        }

        public IEnumerable<Tag> GetAllTags() => _tags;

        public TodoItem? GetById(int id) => _list?.Find(t => t.Id == id);

        public IEnumerable<TodoItem>? GetTodosByTag(string tag) =>
            _list?.FindAll(t => t.Tags.Any(u => u.Name == tag));

        public void Save(TodoItem todoItem)
        {
            if (_list is null) throw new ArgumentNullException("List is not initialized!");
            string[] data = _list.Select(u => _serializer.Serialize(u)).ToArray();
            _fileIO.WriteAllLines(_filepath, data);
        }
    }
}