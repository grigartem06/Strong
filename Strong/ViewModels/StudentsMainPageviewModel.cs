using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strong.ViewModels
{
    public partial class StudentsMainPageviewModel : ObservableObject
    {
        [ObservableProperty]
        private StudentTable student;

        [ObservableProperty]
        public string test;
        public StudentsMainPageviewModel() => Get();

        public async void Get()
        {
            string x = await SecureStorage.Default.GetAsync("userID");
            test = x;
        }

        [RelayCommand]
        public async Task Back() => await Shell.Current.GoToAsync("..");

        [RelayCommand]
        public async Task GoToExercisePage() => await Shell.Current.GoToAsync("//Pages/StudentsPages/ExercisePage");

        [RelayCommand]
        public async Task GoToMeasurementsPage() => await Shell.Current.GoToAsync("//Pages/StudentsPages/MeasurementsPage");

        [RelayCommand]
        public async Task GoToNewTrainingPage() => await Shell.Current.GoToAsync("//Pages/StudentsPages/NewTrainingPage");
    }
}
