using System;
using uniManagementApp.Models;
using Xunit;

namespace uniManagementApp.Tests
{
    public class DataRepositoryTests
    {
        private readonly ITestOutputHelper _output;

        public DataRepositoryTests(ITestOutputHelper output)
        {
            _output = output; // Inject xUnit output helper
        }

        [Fact]
        public void DataRepository_Constructor()
        {
            // Arrange
            var dataRepository = new DataRepository();
            
            // Assert
            Assert.NotNull(dataRepository.Subjects);
            Assert.NotNull(dataRepository.Students);
            Assert.NotNull(dataRepository.Teachers);

            // Display the lists
            _output.WriteLine("1");
            _output.WriteLine("Subjects:");
            foreach (var subject in dataRepository.Subjects)
            {
                _output.WriteLine($" - {subject.Name}");
            }

            _output.WriteLine("Students:");
            foreach (var student in dataRepository.Students)
            {
                _output.WriteLine($" - {student.Name}");
            }

            _output.WriteLine("Teachers:");
            foreach (var teacher in dataRepository.Teachers)
            {
                _output.WriteLine($" - {teacher.Name}");
            }
        }
    }
}
