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
    public partial class ExercisePageViewModel: ObservableObject
    {
        private readonly ToDoDataBase _database;
        public int userID;
        [ObservableProperty] private StudentTable student;
        [ObservableProperty] public ObservableCollection<ExerciseTable> exerciseList;

        public ExercisePageViewModel()
        {
            _database = new ToDoDataBase();
            GetSecret();
            LoadExercise();
        }

        public async void GetSecret()
        {
            string z = await SecureStorage.Default.GetAsync("studentID");
        }

        private async Task LoadExercise()
        {
            var exercise = await _database.GetExercise(await SecureStorage.Default.GetAsync("studentID"));
            ExerciseList = new ObservableCollection<ExerciseTable>(exercise);
        }
    }
}
