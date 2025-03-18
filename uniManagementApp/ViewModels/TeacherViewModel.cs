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
                Message = "Error: Fill out both Name and Description.";
                Colour = "Red";
                Popup2Open = true;
                await Task.Delay(3000);
                Popup2Open = false;
                return;
            }

            // Update the subject in the SubjectAll collection
            var subject = SubjectAll.FirstOrDefault(s => s.Id == SelectedSubject.Id);
            if (subject != null)
            {
                subject.Name = SelectedSubject.Name;
                subject.Description = SelectedSubject.Description;
            }

            // MISSING: also edit in AvailableSubjects for students

            // Update the Subjects collection in the DataRepository
            // ERROR: this won't work when there're multiply teachers
            _dataRepository.Subjects.Clear();
            foreach (var subj in SubjectAll)
            {
                _dataRepository.Subjects.Add(subj);
            }

            _dataRepository.SaveData(); // ERROR: doesn't save to the json

            EditSubjectStackPanelVisible = false;

            Message = "The subject was edited successfully.";
            Colour = "Green";
            Popup2Open = true;
            await Task.Delay(3000);
            Popup2Open = false;

        }

        [RelayCommand] 
        public async void DeleteSubject()
        {
            if (SelectedSubject != null)
            {
                SubjectAll.Remove(SelectedSubject);
                // MISSING: also remove from AvailableSubjects for students
                SelectedSubject = null; 

                _dataRepository.SaveData(); // ERROR: doesn't save to the json

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
            int newId = SubjectAll.Any() ? SubjectAll.Max(s => s.Id) + 1 : 1;

            SubjectAll.Add(new Subject(newId, NewSubjectName!, NewSubjectDescription!, Teacher.Id));
            // MISSING: also add to AvailableSubjects for students
            NewSubjectName = NewSubjectDescription = null;

            _dataRepository.SaveData(); // ERROR: doesn't save to the json

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
}