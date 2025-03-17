using System;
using uniManagementApp.Models;
using Xunit;

namespace uniManagementApp.Tests
{
    public class DataRepositoryTests
    {

        private const string ValidJson = @"{ ""Subjects"": [{ ""Id"": 1, ""Name"": ""Math"", ""Description"": ""Mathematics is the study of numbers, shapes and patterns."", ""TeacherId"": 1, ""StudentsEnrolled"": [1] }], 
                                            ""Students"": [{ ""Id"": 1, ""Name"": ""John"", ""Username"": ""john123"", ""Password"": ""john123"", ""EnrolledSubjects"": [1] }, { ""Id"": 2, ""Name"": ""Jane"", ""Username"": ""jane123"", ""Password"": ""jane123"", ""EnrolledSubjects"": [] }], 
                                            ""Teachers"": [{ ""Id"": 1, ""Name"": ""Mr. Smith"", ""Username"": ""smith123"", ""Password"": ""smith123"", ""Subjects"": [1] }] }";

        [Fact]
        public void DataRepository_Constructor()
        {
            // Arrange
            var dataRepository = new DataRepository();
            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            dataRepository.LoadData(filePath);
            
            // Assert
            Assert.NotNull(dataRepository.Subjects);
            Assert.NotNull(dataRepository.Students);
            Assert.NotNull(dataRepository.Teachers);
        }

        [Fact]
        public void DataRepository_SaveData()
        {
            // Arrange
            var dataRepository = new DataRepository();
            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            dataRepository.LoadData(filePath);

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
            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            dataRepository.LoadData(filePath);

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
