using System.Collections.ObjectModel;
using Moq;
using uniManagementApp.Models;
using uniManagementApp.ViewModels;
using Xunit;

namespace uniManagementApp.Tests
{
    public class LoginViewModelTests
    {
        private readonly Mock<IDataRepository> _mockRepository;
        private readonly LoginViewModel _loginViewModel;

        // Constructor should not have a return type (void is implied for constructors)
        public LoginViewModelTests()
        {
            // Setup mock data
            _mockRepository = new Mock<IDataRepository>();

            var students = new ObservableCollection<Student>
            {
                new Student( 1, "Alice", "alice123", "pass123" ) 
            };

            var teachers = new ObservableCollection<Teacher>
            {
                new Teacher ( 1, "Mr. Smith", "smith", "teach123" )
            };

            // Setup mocked repository to return the test data
            _mockRepository.Setup(repo => repo.Students).Returns(students);
            _mockRepository.Setup(repo => repo.Teachers).Returns(teachers);

            // Create instance of LoginViewModel with the mock repository
            _loginViewModel = new LoginViewModel();
            _loginViewModel.dataRepository = _mockRepository.Object;
        }

        [Fact]
        public void Login_WithValidStudentCredentials_ShouldSucceed()
        {
            // Arrange
            _loginViewModel.Username = "alice123";
            _loginViewModel.Password = "pass123";
            _loginViewModel.IsStudent = true;

            // Act
            _loginViewModel.Login();

            // Assert
            Assert.Equal("Welcome, Alice!", _loginViewModel.ErrorMessage);
        }

        [Fact]
        public void Login_WithValidTeacherCredentials_ShouldSucceed()
        {
            // Arrange
            _loginViewModel.Username = "smith";
            _loginViewModel.Password = "teach123";
            _loginViewModel.IsTeacher = true;

            // Act
            _loginViewModel.Login();

            // Assert
            Assert.Equal("Welcome, Mr. Smith!", _loginViewModel.ErrorMessage);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ShouldFail()
        {
            // Arrange
            _loginViewModel.Username = "invalid_user";
            _loginViewModel.Password = "wrong_pass";
            _loginViewModel.IsStudent = true;

            // Act
            _loginViewModel.Login();

            // Assert
            Assert.Equal("Invalid username or password!", _loginViewModel.ErrorMessage);
        }

        [Fact]
        public void Login_WithEmptyFields_ShouldFail()
        {
            // Arrange
            _loginViewModel.Username = "";
            _loginViewModel.Password = "";
            _loginViewModel.IsStudent = true;

            // Act
            _loginViewModel.Login();

            // Assert
            Assert.Equal("Please fill in all fields.", _loginViewModel.ErrorMessage);
        }
    }
}