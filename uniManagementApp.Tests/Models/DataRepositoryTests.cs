using System;
using uniManagementApp.Models;
using Xunit;

namespace uniManagementApp.Tests
{
    public class DataRepositoryTests
    {

        private const string ValidJson = @"{ ""Subjects"": [{ ""Id"": 1, ""Name"": ""Math"", ""Description"": ""Mathematics is the study of numbers, shapes and patterns."", ""TeacherId"": 1, ""StudentsEnrolled"": [1] }], 
                                            ""Students"": [{ ""Id"": 1, ""Name"": ""John"", ""Username"": ""john123"", ""Password"": ""uGZCxIdmsEtg/SoUKbdMVg==:bj5u+2ojAhprGYwAAU2YPDtsbi/TjKztos3lBnzt3Ao="", ""EnrolledSubjects"": [1] }, { ""Id"": 2, ""Name"": ""Jane"", ""Username"": ""jane123"", ""Password"": ""3et5M7RFyxTbtdAEa6RwYA==:+gxdTzOWFHW5luBwmXdw0Si82GvjtE/IPUoH64i9ReY="", ""EnrolledSubjects"": [] }], 
                                            ""Teachers"": [{ ""Id"": 1, ""Name"": ""Mr. Smith"", ""Username"": ""smith123"", ""Password"": ""NIW+007DsVT+njlTpIx65Q==:gL+fCfpyB0MeKA119dGUsNsAEYImRCoKcD33ILC7tYk="", ""Subjects"": [1] }] }";

        [Fact]
        public void DataRepository_Constructor_ShouldSucceed()
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
        public void DataRepository_SaveData_ShouldSucceed()
        {
            // Arrange
            var dataRepository = new DataRepository();
            var filePath = "test.json";
            File.WriteAllText(filePath, ValidJson);
            dataRepository.LoadData(filePath);

            var newSubject = new Subject(2, "Physics", "Study of matter and energy", 2);
            dataRepository.Subjects.Add(newSubject);

            // Act
            dataRepository.SaveData(filePath);

            // Read the file again to verify changes
            var newDataRepository = new DataRepository();
            newDataRepository.LoadData(filePath);

            // Assert
            var savedSubject = newDataRepository.Subjects.FirstOrDefault(s => s.Id == 2);
            Assert.NotNull(savedSubject);
            Assert.Equal("Physics", savedSubject.Name);
            Assert.Equal("Study of matter and energy", savedSubject.Description);
            Assert.Equal(2, savedSubject.TeacherId);
            
        }

        [Fact]
        public void DataRepository_FindTeacher_ShouldSucceed()
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
        public void DataRepository_FindStudent_ShouldSucceed()
        {
            // Arrange
            var dataRepository = new DataRepository();

            // Act
            var foundStudent = dataRepository.FindStudent("john123", "john123");

            // Assert
            Assert.Equal(dataRepository.Students[0], foundStudent);
        }

        [Fact]
        public void DataRepository_FindSubject_ShouldSucceed()
        {
            // Arrange
            var dataRepository = new DataRepository();

            // Act
            var foundSubject = dataRepository.FindSubject(1);

            // Assert
            Assert.Equal(foundSubject, dataRepository.Subjects[0]);
        }


// Data persistence test
        [Fact]
        public void DataRepository_SaveData_PersistsToFile() 
        {
            // Arrange
            var dataRepository = new DataRepository();
            var subject = new Subject(2, "Physics", "Physics", 1);
            dataRepository.Subjects.Add(subject);

            // Act
            dataRepository.SaveData("test.json");
            var newRepository = new DataRepository();

            // Assert
            Assert.Contains(newRepository.Subjects, s => s.Id == subject.Id && s.Name == subject.Name);
        }
        
    }
}
