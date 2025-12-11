using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Strong.ViewModels
{
    public partial class AllTrainingsPageViewModel : ObservableObject
    {
        [ObservableProperty] public ObservableCollection<TrainingTable> traininsList;
        public int stId;
        private readonly ToDoDataBase _database;

        public AllTrainingsPageViewModel()
            {
                _database = new ToDoDataBase();
                LoadTrainings();
            }

        [RelayCommand]
        public  async Task LoadTrainings()
        {
            await GetSecret();
            var trainings = await _database.GetAllTrainings(stId);
            TraininsList = new ObservableCollection<TrainingTable>(trainings);
        }

        private async Task GetSecret()
        {
            string x = await SecureStorage.Default.GetAsync("studentID");
            stId = Convert.ToInt32(x);
        }

        

        [RelayCommand]
        private async Task Click( int trainingID)
        {
            await SecureStorage.Default.SetAsync("TrainingID", trainingID.ToString());
            await Shell.Current.GoToAsync("//Pages/StudentsPages/AboutTrainingPage");
        }

        [RelayCommand]
        public async Task Back() => Shell.Current.GoToAsync("//Pages/StudentsPages/StudentMainPage");



    }
}
