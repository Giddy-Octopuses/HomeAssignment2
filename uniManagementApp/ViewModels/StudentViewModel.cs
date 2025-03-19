using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using uniManagementApp.Models;

namespace uniManagementApp.ViewModels
{
    public partial class StudentViewModel : ViewModelBase
    {
        private readonly DataRepository dataRepo = new DataRepository();
        private readonly Student _student;

        public ObservableCollection<Subject> EnrolledSubjects { get; }
        public ObservableCollection<Subject> AvailableSubjects { get; }

        private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                _selectedSubject = value;
                IsSubjectDetailsVisible = _selectedSubject != null;
                OnPropertyChanged(nameof(SelectedSubject));
                OnPropertyChanged(nameof(SubjectDetails));
            }
        }

        [ObservableProperty]
        private bool isSubjectDetailsVisible;

        [ObservableProperty]
        private string? confirmationMessage;

        [ObservableProperty]
        private int selectedTabIndex;

        public string SubjectDetails => SelectedSubject != null
            ? $"ID: {SelectedSubject.Id}\n" +
              $"Name: {SelectedSubject.Name}\n" +
              $"Description: {SelectedSubject.Description}\n" +
              $"Teacher: {GetTeacherName(SelectedSubject.TeacherId)}"
            : string.Empty;

        public StudentViewModel(Student student)
        {
            _student = student;

            // Ensure EnrolledSubjects is not null
            EnrolledSubjects = new ObservableCollection<Subject>(
                dataRepo.Subjects.Where(s => _student.EnrolledSubjects?.Contains(s.Id) ?? false)
            );

            AvailableSubjects = new ObservableCollection<Subject>();
            RefreshAvailableSubjects();
        }

        public StudentViewModel()
        {
        }

        partial void OnSelectedTabIndexChanged(int value)
        {
            if (value == 1)
            {
                RefreshAvailableSubjects();
            }
        }

        public void RefreshAvailableSubjects()
        {
            AvailableSubjects.Clear();
            foreach (var subject in dataRepo.Subjects)
            {
                if (_student.EnrolledSubjects?.Contains(subject.Id) == false)
                {
                    AvailableSubjects.Add(subject);
                }
            }
            OnPropertyChanged(nameof(AvailableSubjects));
        }

        [RelayCommand]
public void EnrollSubject()
{
    Console.WriteLine("EnrollSubject called");
    if (SelectedSubject == null || EnrolledSubjects.Any(s => s.Id == SelectedSubject.Id))
    {
        ConfirmationMessage = "No subject selected or already enrolled.";
        OnPropertyChanged(nameof(ConfirmationMessage));
        return;
    }

    Console.WriteLine($"Enrolling subject: {SelectedSubject.Name}");

    // Add subject to the student's enrolled subjects
    _student.EnrolledSubjects ??= new List<int>();
    _student.EnrolledSubjects.Add(SelectedSubject.Id);
    EnrolledSubjects.Add(SelectedSubject);

    // Update the global student list in dataRepo
    var studentInRepo = dataRepo.Students.FirstOrDefault(s => s.Id == _student.Id);
    if (studentInRepo != null)
    {
        studentInRepo.EnrolledSubjects = new List<int>(_student.EnrolledSubjects);
    }

    // Update the subject's list of enrolled students
    var subject = dataRepo.FindSubject(SelectedSubject.Id);
    subject?.StudentsEnrolled?.Add(_student.Id);

    // Save the updated data to the JSON file
    dataRepo.SaveData();
    Console.WriteLine("Data saved");

    // Refresh available subjects
    RefreshAvailableSubjects();
    ConfirmationMessage = "Subject successfully enrolled!";
    OnPropertyChanged(nameof(ConfirmationMessage));

    // Notify UI of changes
    OnPropertyChanged(nameof(EnrolledSubjects));
    OnPropertyChanged(nameof(AvailableSubjects));
    Console.WriteLine("UI updated");
}


        [RelayCommand]
public void DropSubject()
{
    Console.WriteLine("DropSubject called");
    if (SelectedSubject == null || !EnrolledSubjects.Any(s => s.Id == SelectedSubject.Id))
    {
        ConfirmationMessage = "No subject selected or not enrolled.";
        OnPropertyChanged(nameof(ConfirmationMessage));
        return;
    }

    Console.WriteLine($"Dropping subject: {SelectedSubject.Name}");

    var id = SelectedSubject.Id;

    // Remove the subject from the student's enrolled subjects
    _student.EnrolledSubjects?.Remove(id);
    EnrolledSubjects.Remove(SelectedSubject);

    // Update the global student list in dataRepo
    var studentInRepo = dataRepo.Students.FirstOrDefault(s => s.Id == _student.Id);
    if (studentInRepo != null)
    {
        studentInRepo.EnrolledSubjects = new List<int>(_student.EnrolledSubjects);
    }

    // Update the subject's list of enrolled students
    var subject = dataRepo.FindSubject(id);
    if (subject != null)
    {
        subject.StudentsEnrolled?.Remove(_student.Id);
        Console.WriteLine($"Removed student {_student.Id} from subject {subject.Id}");
    }
    else
    {
        Console.WriteLine($"Subject {id} not found");
    }

    // Save the updated data to the JSON file
    dataRepo.SaveData();
    Console.WriteLine("Data saved");

    // Refresh available subjects
    RefreshAvailableSubjects();
    ConfirmationMessage = "Subject successfully dropped!";
    OnPropertyChanged(nameof(ConfirmationMessage));

    // Notify UI of changes
    OnPropertyChanged(nameof(EnrolledSubjects));
    OnPropertyChanged(nameof(AvailableSubjects));
    Console.WriteLine("UI updated");
}


       private string GetTeacherName(int teacherId)
{
    var teacher = dataRepo.Teachers.FirstOrDefault(t => t.Subjects.Contains(teacherId));
    return teacher?.Name ?? "Unknown Teacher";
}

    }
}
