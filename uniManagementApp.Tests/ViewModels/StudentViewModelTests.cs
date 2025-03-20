using System.Linq;
using System.Threading.Tasks;
using uniManagementApp.Models;
using uniManagementApp.ViewModels;
using Xunit;

namespace uniManagementApp.Tests
{
    public class StudentViewModelTests
    {
        private StudentViewModel CreateTestViewModel()
        {
            var student = new Student(1, "John", "john123", "password123", new List<int>());
            return new StudentViewModel(student);
        }

        [Fact]
        public async Task EnrollSubject_ShouldAppearInEnrolledSubjects()
        {
            // Arrange
            var viewModel = CreateTestViewModel();
            var subject = new Subject(1, "Mathematics", "Study of numbers", 1);
            viewModel.AvailableSubjects.Add(subject);
            viewModel.SelectedSubject = subject;

            // Act
            viewModel.EnrollSubject();

            // Assert
            Assert.Contains(viewModel.EnrolledSubjects, s => s.Name == "Mathematics" && s.Description == "Study of numbers");
            Assert.DoesNotContain(viewModel.AvailableSubjects, s => s.Name == "Mathematics");
        } // passes all 15

        [Fact]
        public async Task DropSubject_ShouldAppearInAvailableSubjects()
        {
            // Arrange
            var viewModel = CreateTestViewModel();
            var subject = new Subject(1, "Physics", "Study of matter and energy", 1);
            viewModel.EnrolledSubjects.Add(subject);
            viewModel.SelectedSubject = subject;

            // Act
            viewModel.DropSubject();

            // Assert
            Assert.Contains(viewModel.AvailableSubjects, s => s.Name == "Physics" && s.Description == "Study of matter and energy");
            Assert.DoesNotContain(viewModel.EnrolledSubjects, s => s.Name == "Physics");
        }
    }
}