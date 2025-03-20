using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using uniManagementApp.Models;

using System.ComponentModel;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace uniManagementApp.ViewModels
{
    public partial class StudentViewModel : ViewModelBase
    {
        private readonly DataRepository dataRepo = new DataRepository();
        private readonly Student _student;
        private Popup _popup;

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
        private int selectedTabIndex;

        [ObservableProperty]
        private bool popupDropOpen;

        [ObservableProperty]
        private bool popupEnrollOpen;

        [ObservableProperty]
        private string messageDrop;

        [ObservableProperty]
        private string colourDrop;

        [ObservableProperty]
        private string messageEnroll;

        [ObservableProperty]
        private string colourEnroll;

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }

        public string SubjectDetails => SelectedSubject != null
            ? $"ID: {SelectedSubject.Id}\n" +
              $"Name: {SelectedSubject.Name}\n" +
              $"Description: {SelectedSubject.Description}\n" +
              $"Teacher: {GetTeacherName(SelectedSubject.TeacherId)}"
            : string.Empty;

        public StudentViewModel(Student student)
        {
            _student = student ?? throw new ArgumentNullException(nameof(student));

            EnrolledSubjects = new ObservableCollection<Subject>(
                dataRepo.Subjects?.Where(s => _student.EnrolledSubjects?.Contains(s.Id) ?? false) ?? Enumerable.Empty<Subject>()
            );

            AvailableSubjects = new ObservableCollection<Subject>();
            RefreshAvailableSubjects();
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
            foreach (var subject in dataRepo.Subjects ?? Enumerable.Empty<Subject>())
            {
                if (_student.EnrolledSubjects?.Contains(subject.Id) == false)
                {
                    AvailableSubjects.Add(subject);
                }
            }
            OnPropertyChanged(nameof(AvailableSubjects));
        }

        [RelayCommand]
        public async void EnrollSubject()
        {
            if (_student == null || SelectedSubject == null || EnrolledSubjects.Any(s => s.Id == SelectedSubject.Id))
            {
                MessageEnroll = "Error: Please choose a subject.";
                ColourEnroll = "Red";
                PopupEnrollOpen = true;
                await Task.Delay(3000);
                PopupEnrollOpen = false;
                return;
            }

            _student.EnrolledSubjects ??= new List<int>();
            _student.EnrolledSubjects.Add(SelectedSubject.Id);
            EnrolledSubjects.Add(SelectedSubject);

            var studentInRepo = dataRepo.Students?.FirstOrDefault(s => s.Id == _student.Id);
            if (studentInRepo != null)
            {
                studentInRepo.EnrolledSubjects = new List<int>(_student.EnrolledSubjects);
            }

            var subject = dataRepo.FindSubject(SelectedSubject.Id);
            if (subject != null)
            {
                subject.StudentsEnrolled ??= new List<int>();
                subject.StudentsEnrolled.Add(_student.Id);
            }

            dataRepo.SaveData(dataRepo.DataFilePath);

            RefreshAvailableSubjects();
            OnPropertyChanged(nameof(EnrolledSubjects));
            OnPropertyChanged(nameof(AvailableSubjects));

            MessageEnroll = "The enrollment was successful.";
            ColourEnroll = "Green";
            PopupEnrollOpen = true;
            await Task.Delay(3000);
            PopupEnrollOpen = false;
        }

        [RelayCommand]
        public async void DropSubject()
        {
            if (_student == null || SelectedSubject == null || !EnrolledSubjects.Any(s => s.Id == SelectedSubject.Id))
            {
                MessageDrop = "Error: Please choose a subject.";
                ColourDrop = "Red";
                PopupDropOpen = true;
                await Task.Delay(3000);
                PopupDropOpen = false;
                return;
            }

            var id = SelectedSubject.Id;
            _student.EnrolledSubjects?.Remove(id);
            EnrolledSubjects.Remove(SelectedSubject);

            var studentInRepo = dataRepo.Students?.FirstOrDefault(s => s.Id == _student.Id);
            if (studentInRepo != null)
            {
                studentInRepo.EnrolledSubjects = new List<int>(_student.EnrolledSubjects ?? Enumerable.Empty<int>());
            }

            var subject = dataRepo.FindSubject(id);
            if (subject?.StudentsEnrolled != null)
            {
                subject.StudentsEnrolled.Remove(_student.Id);
            }

            dataRepo.SaveData(dataRepo.DataFilePath);

            RefreshAvailableSubjects();
            OnPropertyChanged(nameof(EnrolledSubjects));
            OnPropertyChanged(nameof(AvailableSubjects));

            MessageDrop = "The subject was dropped successfully.";
            ColourDrop = "Green";
            PopupDropOpen = true;
            await Task.Delay(3000);
            PopupDropOpen = false;
        }

        private string GetTeacherName(int teacherId)
        {
            var teacher = dataRepo?.Teachers?.FirstOrDefault(t => t.Id == teacherId);
            return teacher?.Name ?? "Unknown Teacher";
        }
    }
}