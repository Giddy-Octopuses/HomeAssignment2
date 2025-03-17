using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.Generic;
using uniManagementApp.Models;

namespace uniManagementApp.ViewModels
{
    public partial class StudentViewModel : ViewModelBase
    {
        public Student Student { get; }
        private Student _student;
        public ObservableCollection<Subject> EnrolledSubjects { get; }

        [ObservableProperty]
        private string? newSubjectName;

        [ObservableProperty]
        private string? newSubjectDescription;

        private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set => SetProperty(ref _selectedSubject, value);
        }

        [ObservableProperty]
        private bool isCreatingNewSubject;

        public StudentViewModel(Student student)
        {
            _student = student;
            var dataRepo = new DataRepository();

            // Load enrolled subjects from the student
            EnrolledSubjects = new ObservableCollection<Subject>(
                _student.EnrolledSubjects.Select(id => dataRepo.LoadSubjectById(id))
            );
        }

        public StudentViewModel() : this(new Student(1, "John Doe", "johndoe", "password123", new List<int>())) // Default for preview purposes
        {
        }

        [RelayCommand]
        public void EnrollSubject()
        {
            // Logic to enroll in a new subject (could be selecting from a list of available subjects)
            if (!string.IsNullOrWhiteSpace(NewSubjectName) && !string.IsNullOrWhiteSpace(NewSubjectDescription))
            {
                // Generate a new subject ID (or retrieve it from some logic)
                var id = EnrolledSubjects.Count + 1; // Example ID, adjust logic as needed
                var name = NewSubjectName;
                var description = NewSubjectDescription;

                // Assuming teacherId is required, fetch it from DataRepository or set a placeholder value
                var teacherId = 1; // You can fetch the teacherId dynamically here (e.g., from a selected teacher)

                var enrolledStudents = new List<int> { _student.Id }; // Initially, the student is enrolled

                // Create the new subject and add it to the list
                var newSubject = new Subject(id, name, description, teacherId, enrolledStudents);

                EnrolledSubjects.Add(newSubject);
                _student.EnrolledSubjects.Add(newSubject.Id);  // Update student enrolled subjects list

                NewSubjectName = NewSubjectDescription = null; // Reset fields
            }
        }

        [RelayCommand]
        public void DropSubject()
        {
            if (SelectedSubject != null)
            {
                EnrolledSubjects.Remove(SelectedSubject);
                _student.EnrolledSubjects.Remove(SelectedSubject.Id); // Remove from student's enrolled subjects
                SelectedSubject = null; // Clear selection
            }
        }
    }
}
