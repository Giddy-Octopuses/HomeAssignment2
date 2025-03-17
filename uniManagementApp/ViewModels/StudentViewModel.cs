using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;
using System.Collections.Generic;
using uniManagementApp.Models;
using System.Diagnostics;

namespace uniManagementApp.ViewModels
{
    public partial class StudentViewModel : ViewModelBase
    {
        public DataRepository dataRepo = new DataRepository();
        private Student _student;
        public ObservableCollection<Subject> EnrolledSubjects { get; }
        public ObservableCollection<Subject> AvailableSubjects { get; }

        private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                SetProperty(ref _selectedSubject, value);
                OnPropertyChanged(nameof(SubjectDetails));
            }
        }

        [ObservableProperty]
        private string? confirmationMessage;

        public string SubjectDetails => SelectedSubject != null ? $"ID: {SelectedSubject.Id}\nName: {SelectedSubject.Name}\nDescription: {SelectedSubject.Description}" : string.Empty;

        public StudentViewModel(Student student)
        {
            _student = student;

            EnrolledSubjects = new ObservableCollection<Subject>(
                _student.EnrolledSubjects
                    .Select(id => dataRepo.FindSubject(id))
                    .Where(subject => subject != null)
                    .Cast<Subject>()
            );

            AvailableSubjects = new ObservableCollection<Subject>(
                dataRepo.Subjects
                    .Where(s => !_student.EnrolledSubjects.Contains(s.Id))
            );

            Debug.WriteLine($"Loaded {AvailableSubjects.Count} available subjects.");
        }

        public StudentViewModel() : this(new Student(1, "John Doe", "johndoe", "password123", new List<int>()))
        {
        }

        [RelayCommand]
        public void EnrollSubject()
        {
            if (SelectedSubject != null && !EnrolledSubjects.Contains(SelectedSubject))
            {
                EnrolledSubjects.Add(SelectedSubject);
                _student.EnrolledSubjects.Add(SelectedSubject.Id);
                AvailableSubjects.Remove(SelectedSubject);
                dataRepo.SaveData();
                ConfirmationMessage = $"Successfully enrolled in {SelectedSubject.Name}.";
                SelectedSubject = null;
            }
        }

        [RelayCommand]
        public void DropSubject()
        {
            if (SelectedSubject != null && EnrolledSubjects.Contains(SelectedSubject))
            {
                EnrolledSubjects.Remove(SelectedSubject);
                _student.EnrolledSubjects.Remove(SelectedSubject.Id);
                AvailableSubjects.Add(SelectedSubject);
                dataRepo.SaveData();
                ConfirmationMessage = $"Successfully dropped {SelectedSubject.Name}.";
                SelectedSubject = null;
            }
        }
    }
}
