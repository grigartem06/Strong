
using CommunityToolkit.Mvvm.ComponentModel;
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Strong.ViewModels
{
    public partial class AboutTrainingPageViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;
        public int trainingId;

        [ObservableProperty] public ObservableCollection<SetsTable> setsList;



        public AboutTrainingPageViewModel()
        {
            _database = new ToDoDataBase();
            GetSecret();
            LoadSets();
        }

        public async Task GetSecret()
        {
            trainingId = Convert.ToInt32(await SecureStorage.Default.GetAsync("TrainingID"));
        }

        public async Task LoadSets() 
        {
            await  GetSecret();
            var sets =await  _database.GetSetsByTrainingId(trainingId);
            SetsList = new ObservableCollection<SetsTable>(sets);

        }

    }
}
