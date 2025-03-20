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
using Avalonia.Controls.Primitives;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace uniManagementApp.ViewModels
{
    public partial class TeacherViewModel : ViewModelBase
    {
        public Teacher Teacher { get; }
        private readonly DataRepository _dataRepository;
        private Popup _popup;

        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
            _dataRepository = new DataRepository();
            SubjectAll = TurnSubjectIdIntoAll(Teacher.Subjects);
        }

        // These store the subjects with all their attributes.
        public ObservableCollection<Subject> SubjectAll { get; }

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

        [ObservableProperty]
        private Subject? selectedSubject;

        [ObservableProperty]
        private bool popupOpen;

        [ObservableProperty]
        private bool popup2Open;

        [ObservableProperty]
        private string message;

        [ObservableProperty]
        private string colour;

        [ObservableProperty]
        private bool popup3Open;

        [ObservableProperty]
        private string message3;

        [ObservableProperty]
        private string colour3;

        public void SetPopup(Popup popup)
        {
            _popup = popup;
        }

        [ObservableProperty]
        private bool editSubjectStackPanelVisible;

        [RelayCommand]
        public void EditSubject()
        {
            EditSubjectStackPanelVisible = true;
        }

        [RelayCommand]
        public async void Save2() // Saving the edited subject.
        {
            if (string.IsNullOrWhiteSpace(SelectedSubject.Name) || string.IsNullOrWhiteSpace(SelectedSubject.Description))
            {
                Message3 = "Error: Fill out both Name and Description.";
                Colour3 = "Red";
                Popup3Open = true;
                await Task.Delay(3000);
                Popup3Open = false;
                return;
            }

            // Find the subject in the SubjectAll collection and replace it with the edited data.
            var subject = SubjectAll.FirstOrDefault(s => s.Id == SelectedSubject.Id);
            if (subject != null)
            {
                var updatedSubject = new Subject(subject.Id, SelectedSubject.Name, SelectedSubject.Description, subject.TeacherId, subject.StudentsEnrolled);
                SubjectAll.Replace(subject, updatedSubject);
            }

            // Delete every subject from _dataRepository.Subjects that are in SubjectAll.
            foreach (var subj in SubjectAll)
            {
                var repoSubject = _dataRepository.Subjects.FirstOrDefault(s => s.Id == subj.Id);
                if (repoSubject != null)
                {
                    _dataRepository.Subjects.Remove(repoSubject);
                }
            }

            // Put every subject of SubjectAll back to _dataRepository.Subjects --> the updated subjects will show in datarepository.
            foreach (var subj in SubjectAll)
            {
                _dataRepository.Subjects.Add(subj);
            }

            // Order the subjects by id, so they are in the original order.
            var orderedSubjects = new ObservableCollection<Subject>(_dataRepository.Subjects.OrderBy(s => s.Id));
            _dataRepository.Subjects = orderedSubjects;

            _dataRepository.SaveData();

            EditSubjectStackPanelVisible = false;

            Message3 = "The subject was edited successfully.";
            Colour3 = "Green";
            Popup3Open = true;
            await Task.Delay(3000);
            Popup3Open = false;
        }

        [RelayCommand]
        public async void DeleteSubject()
        {
            if (SelectedSubject != null)
            {
                int IdToDelete = SelectedSubject.Id;
                Subject SubjectToDelete = SelectedSubject;

                SubjectAll.Remove(SelectedSubject);
                Teacher.Subjects.Remove(IdToDelete);
                _dataRepository.Subjects.Remove(SubjectToDelete);
                var teacher = _dataRepository.Teachers.FirstOrDefault(t => t.Id == Teacher.Id);
                if (teacher != null)
                {
                    teacher.Subjects.Remove(IdToDelete);
                }
                SelectedSubject = null;

                _dataRepository.SaveData();

                // Show the popup for 3 seconds
                PopupOpen = true;
                await Task.Delay(3000);
                PopupOpen = false;
            }
        }

        [ObservableProperty]
        private string? newSubjectName;

        [ObservableProperty]
        private string? newSubjectDescription;

        [ObservableProperty]
        private bool newSubjectStackPanelVisible;

        [RelayCommand]
        public void AddSubject()
        {
            NewSubjectStackPanelVisible = true;
        }

        [RelayCommand]
        public async void Save()
        {
            if (string.IsNullOrWhiteSpace(NewSubjectName) || string.IsNullOrWhiteSpace(NewSubjectDescription))
            {
                Message = "Error: Fill out both Name and Description.";
                Colour = "Red";
                Popup2Open = true;
                await Task.Delay(3000);
                Popup2Open = false;
                return;
            }

            // Find the maximum existing ID and add 1 to it.
            int newId = _dataRepository.Subjects.Any() ? _dataRepository.Subjects.Max(s => s.Id) + 1 : 1;

            SubjectAll.Add(new Subject(newId, NewSubjectName!, NewSubjectDescription!, Teacher.Id));
            _dataRepository.Subjects.Add(new Subject(newId, NewSubjectName!, NewSubjectDescription!, Teacher.Id));
            Teacher.Subjects.Add(newId); 
            var teacher = _dataRepository.Teachers.FirstOrDefault(t => t.Id == Teacher.Id);
            if (teacher != null)
            {
                teacher.Subjects.Add(newId);
            }

            NewSubjectName = NewSubjectDescription = null;

            _dataRepository.SaveData();

            NewSubjectStackPanelVisible = false;

            Message = "The subject was saved successfully.";
            Colour = "Green";
            Popup2Open = true;
            await Task.Delay(3000);
            Popup2Open = false;
        }
    }

    // Used to determine whether a subject was selected from the listbox.
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
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