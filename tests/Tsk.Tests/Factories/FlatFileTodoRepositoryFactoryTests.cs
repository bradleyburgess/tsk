using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tsk.Domain.Entities;
using Tsk.Infrastructure.Factories;
using Tsk.Tests.TestSupport;
using Xunit;

namespace Tsk.Tests.Factories
{
    public class FlatFileTodoRepositoryFactoryTests
    {
        [Fact]
        public void ShouldCreate_FlatFileTodoRepository()
        {
            var fileName = TestHelpers.ResolveFromSlnRoot("tests", "Tsk.Tests", "TestData", "TestInputFile1.txt");
            var repo = new FlatFileTodoRepositoryFactory().Create(fileName);
            var list = repo.GetAll();
            Assert.Equal(3, list.Count());
            Assert.True(list.All(u => u is TodoItem));
        }
    }
}