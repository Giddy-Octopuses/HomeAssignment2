using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Threading;
using uniManagementApp.Models;
using uniManagementApp.ViewModels;
using Xunit;

namespace uniManagementApp.Tests
{
    public class StudentViewModelTests
    {
        private async Task<StudentViewModel> CreateTestViewModel()
        {
            var student = new Student(1, "John", "john123", "password123", new List<int>());
            StudentViewModel viewModel = null;

            // Wrap in UI thread
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                viewModel = new StudentViewModel(student);
            });

            return viewModel;
        }

        [Fact]
        public async Task EnrollSubject_ShouldAppearInEnrolledSubjects()
        {
            // Arrange
            var viewModel = await CreateTestViewModel();
            var subject = new Subject(1, "Mathematics", "Study of numbers", 1);
            viewModel.AvailableSubjects.Add(subject);
            viewModel.SelectedSubject = subject;

            // Act
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                viewModel.EnrollSubject();
            });

            // Assert
            Assert.Contains(viewModel.EnrolledSubjects, s => s.Name == "Mathematics" && s.Description == "Study of numbers");
            Assert.DoesNotContain(viewModel.AvailableSubjects, s => s.Name == "Mathematics");
        }

        [Fact]
        public async Task DropSubject_ShouldAppearInAvailableSubjects()
        {
            // Arrange
            var viewModel = await CreateTestViewModel();
            var subject = new Subject(1, "Physics", "Study of matter and energy", 1);
            viewModel.EnrolledSubjects.Add(subject);
            viewModel.SelectedSubject = subject;

            // Act
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                viewModel.DropSubject();
            });

            // Assert
            Assert.Contains(viewModel.AvailableSubjects, s => s.Name == "Physics" && s.Description == "Study of matter and energy");
            Assert.DoesNotContain(viewModel.EnrolledSubjects, s => s.Name == "Physics");
        }
    }
}
