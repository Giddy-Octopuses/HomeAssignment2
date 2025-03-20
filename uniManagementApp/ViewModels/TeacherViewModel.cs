using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using System.Linq;
using uniManagementApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace uniManagementApp.ViewModels
{
    public partial class TeacherViewModel : ViewModelBase
    {
        public Teacher Teacher { get; }
        private readonly DataRepository _dataRepository;

        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
            _dataRepository = new DataRepository();
            SubjectAll = TurnSubjectIdIntoAll(Teacher.Subjects ?? new List<int>()) ?? new ObservableCollection<Subject>();
        }

        public ObservableCollection<Subject> SubjectAll { get; } = new();

        public ObservableCollection<Subject>? TurnSubjectIdIntoAll(List<int> subjectIds)
        {
            var subjectAll = new ObservableCollection<Subject>();
            foreach (int subjectId in subjectIds)
            {
                var subject = _dataRepository.FindSubject(subjectId);
                if (subject != null)
                {
                    subjectAll.Add(subject);
                }
            }
            return subjectAll;
        }

        [ObservableProperty] private Subject? selectedSubject;
        [ObservableProperty] private bool popupOpen;
        [ObservableProperty] private bool popup2Open;
        [ObservableProperty] private string message = string.Empty;
        [ObservableProperty] private string colour = string.Empty;
        [ObservableProperty] private bool popup3Open;
        [ObservableProperty] private string message3 = string.Empty;
        [ObservableProperty] private string colour3 = string.Empty;
        [ObservableProperty] private bool editSubjectStackPanelVisible;
        [ObservableProperty] private string? newSubjectName;
        [ObservableProperty] private string? newSubjectDescription;
        [ObservableProperty] private bool newSubjectStackPanelVisible;

        private async Task ShowPopup(string message, string colour, int duration = 3000)
        {
            Message3 = message;
            Colour3 = colour;
            Popup3Open = true;
            await Task.Delay(duration);
            Popup3Open = false;
        }

        [RelayCommand]
        public void EditSubject()
        {
            EditSubjectStackPanelVisible = true;
        }

        [RelayCommand]
        public async Task Save2() // Saving the edited subject.
        {
            if (SelectedSubject == null || string.IsNullOrWhiteSpace(SelectedSubject.Name) || string.IsNullOrWhiteSpace(SelectedSubject.Description))
            {
                await ShowPopup("Error: Fill out both Name and Description.", "Red");
                return;
            }

            var subjectInAll = SubjectAll.FirstOrDefault(s => s.Id == SelectedSubject.Id);
            if (subjectInAll != null)
            {
                // Update the subject in SubjectAll.
                subjectInAll.Name = SelectedSubject.Name;
                subjectInAll.Description = SelectedSubject.Description;

                // Update the subject in _dataRepository.Subjects.
                var repoSubject = _dataRepository.Subjects?.FirstOrDefault(s => s.Id == SelectedSubject.Id);
                if (repoSubject != null)
                {
                    repoSubject.Name = SelectedSubject.Name;
                    repoSubject.Description = SelectedSubject.Description;
                }

                _dataRepository.SaveData("data.json");

                EditSubjectStackPanelVisible = false;
                await ShowPopup("The subject was edited successfully.", "Green");
            }
        }

        [RelayCommand]
        public async Task DeleteSubject()
        {
            if (SelectedSubject != null)
            {
                int IdToDelete = SelectedSubject.Id;

                // Remove from view
                SubjectAll.Remove(SelectedSubject);

                // Remove from teacher's subjects (with null check)
                if (Teacher.Subjects != null)
                {
                    Teacher.Subjects.Remove(IdToDelete);
                }

                // Remove from data repository (with null check)
                if (_dataRepository.Subjects != null)
                {
                    var subjectToRemove = _dataRepository.Subjects.FirstOrDefault(s => s.Id == IdToDelete);
                    if (subjectToRemove != null)
                    {
                        _dataRepository.Subjects.Remove(subjectToRemove);
                    }
                }

                // Save changes
                SelectedSubject = null;
                _dataRepository.SaveData("data.json");

                await ShowPopup("Subject deleted successfully.", "Green");
            }
        }


        [RelayCommand]
        public void AddSubject()
        {
            NewSubjectStackPanelVisible = true;
        }

        [RelayCommand]
        public async Task Save()
        {
            if (string.IsNullOrWhiteSpace(NewSubjectName) || string.IsNullOrWhiteSpace(NewSubjectDescription))
            {
                await ShowPopup("Error: Fill out both Name and Description.", "Red");
                return;
            }

            // Find the maximum existing ID and add 1 to it.
            int newId = _dataRepository.Subjects?.Any() == true ? _dataRepository.Subjects.Max(s => s.Id) + 1 : 1;

            var newSubject = new Subject(newId, NewSubjectName!, NewSubjectDescription!, Teacher.Id);
            SubjectAll.Add(newSubject);
            _dataRepository.Subjects?.Add(newSubject);
            Teacher.Subjects?.Add(newId);

            var teacher = _dataRepository.Teachers?.FirstOrDefault(t => t.Id == Teacher.Id);
            teacher?.Subjects?.Add(newId);

            _dataRepository.SaveData("data.json");

            NewSubjectName = NewSubjectDescription = null;
            NewSubjectStackPanelVisible = false;

            await ShowPopup("The subject was saved successfully.", "Green");
        }
    }

    // Used to determine whether a subject was selected from the listbox.
    public class NullToBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // Used for the Edit method.
    public static class ObservableCollectionExtensions
    {
        public static void Replace<T>(this ObservableCollection<T> collection, T oldItem, T newItem)
        {
            int index = collection.IndexOf(oldItem);
            if (index >= 0)
            {
                collection[index] = newItem;
            }
        }
    }
}
