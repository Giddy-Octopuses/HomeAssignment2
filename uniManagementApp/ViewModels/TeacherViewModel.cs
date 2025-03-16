using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Avalonia.Data.Converters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Avalonia.Controls;
using uniManagementApp.Models;

namespace uniManagementApp.ViewModels
{

    public partial class TeacherViewModel : ViewModelBase
    {
        public Teacher Teacher { get; }
        public IDataRepository dataRepository = new DataRepository(); // need to create datarepository in this class
        public TeacherViewModel(Teacher teacher)
        {
            Teacher = teacher;
        }
 
        /*    

        [ObservableProperty]
        private string? newSubjectName;

        [ObservableProperty]
        private string? newSubjectDescription; */

        // I TRIED MAKING THE STACKPANEL VISIBLE ONLY AFTER A SUBJECT WAS CHOSEN, BUT IT DIDN'T WORK
        /* private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set
            {
                if (SetProperty(ref _selectedSubject, value))
                {
                    IsSelected = (value != null);
                    Console.WriteLine($"SelectedSubject: {SelectedSubject?.Name}, IsSelected: {IsSelected}");
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        } */

        private Subject? _selectedSubject;
        public Subject? SelectedSubject
        {
            get => _selectedSubject;
            set => SetProperty(ref _selectedSubject, value);
        }  

        /* [ObservableProperty]
        private Subject? selectedSubject; */

        /* 
        [RelayCommand]
        public void AddSubject()
        {
            if (string.IsNullOrWhiteSpace(NewSubjectName) || string.IsNullOrWhiteSpace(NewSubjectDescription))
            {
                return;
            }

            MySubjects.Add(new Subject { Name = NewSubjectName!, Description = NewSubjectDescription! });
            NewSubjectName = NewSubjectDescription = null;
        }

        [RelayCommand] 
        public void DeleteSubject()
        {
            if (SelectedItem != null)
            {
                MySubjects.Remove(SelectedItem);
                // MISSING: also remove from AvailableSubjects for students
                SelectedItem = null; // Clear the selection
            }
        } */
    }
    public class SubjectsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is List<int> subjects)
            {
                return string.Join(", ", subjects);
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

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
