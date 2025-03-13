using System;
using uniManagementApp.Models;
using Xunit;

namespace uniManagementApp.Tests
{
    public class DataRepositoryTests
    {
        // redo with IDataRepository
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
                _output.WriteLine($" - {teacher.Subjects[0]}");
            }
        }

        [Fact]
        public void DataRepository_SaveData()
        {
            // Arrange
            var dataRepository = new DataRepository();
            var subject = new Subject(1, "Math", "Mathematics", 1);
            dataRepository.Subjects.Add(subject);

            // Act
            dataRepository.SaveData();

            // Assert
            Assert.Contains(subject, dataRepository.Subjects);
        }

        [Fact]
        public void DataRepository_FindTeacher()
        {
            // Arrange
            var dataRepository = new DataRepository();
            var teacher = new Teacher(1, "John Doe", "johndoe", "password", []);
            dataRepository.Teachers.Add(teacher);

            // Act
            var foundTeacher = dataRepository.FindTeacher("johndoe", "password");

            // Assert
            Assert.Equal(teacher, foundTeacher);
        }   

        [Fact]
        public void DataRepository_FindStudent()
        {
            // Arrange
            var dataRepository = new DataRepository();

            // Act
            var foundStudent = dataRepository.FindStudent("john123", "john123");

            // Assert
            Assert.Equal(dataRepository.Students[0], foundStudent);
        }

        [Fact]
        public void DataRepository_FindSubject()
        {
            // Arrange
            var dataRepository = new DataRepository();

            // Act
            var foundSubject = dataRepository.FindSubject(1);

            // Assert
            Assert.Equal(foundSubject, dataRepository.Subjects[0]);
        }

        [Fact]
        public void DataRepository_CreateSubject()
        {
            // Arrange
            // Act
            // Assert
        }
        
    }
}
