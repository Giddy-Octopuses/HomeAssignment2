using System.Linq;
using System.Threading.Tasks;
using uniManagementApp.Models;
using uniManagementApp.ViewModels;
using Xunit;

namespace uniManagementApp.Tests
{
    public class TeacherViewModelTests
    {
        private TeacherViewModel CreateTestViewModel()
        {
            var teacher = new Teacher(1, "Mr. Smith", "smith123", "password123", new List<int>());
            return new TeacherViewModel(teacher);
        }

        [Fact]
        public async Task CreateSubject_ShouldAppearInMySubjectsAndAvailableSubjects()
        {
            // Arrange
            var viewModel = CreateTestViewModel();
            viewModel.NewSubjectName = "Physics";
            viewModel.NewSubjectDescription = "Study of matter and energy";

            // Act
            viewModel.Save();

            // Assert
            Assert.Contains(viewModel.SubjectAll, s => s.Name == "Physics" && s.Description == "Study of matter and energy");
            Assert.Contains(viewModel.Teacher.Subjects, id => viewModel.SubjectAll.Any(s => s.Id == id));
        }

        [Fact]
        public async Task DeleteSubject_ShouldRemoveFromMySubjectsAndAvailableSubjects()
        {
            // Arrange
            var viewModel = CreateTestViewModel();
            viewModel.NewSubjectName = "Chemistry";
            viewModel.NewSubjectDescription = "Study of substances";
            viewModel.Save();

            var createdSubject = viewModel.SubjectAll.FirstOrDefault(s => s.Name == "Chemistry");
            viewModel.SelectedSubject = createdSubject;

            // Act
            viewModel.DeleteSubject();

            // Assert
            Assert.DoesNotContain(viewModel.SubjectAll, s => s.Name == "Chemistry");
            Assert.DoesNotContain(viewModel.Teacher.Subjects, id => id == createdSubject.Id);
        }
    }
}
