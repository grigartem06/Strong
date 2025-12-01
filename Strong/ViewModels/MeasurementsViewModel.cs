using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Strong.ViewModels
{
    public partial class MeasurementsViewModel: ObservableObject
    {
        [ObservableProperty] public List<string> measurements;
        [ObservableProperty] public double value;
        [ObservableProperty] public string selectedType;
        [ObservableProperty] public ObservableCollection<MeasurementsTable> measurementsList;

        

        private readonly ToDoDataBase _database;
        public int stId;


        public  MeasurementsViewModel()
        {
            _database = new ToDoDataBase();
            measurements = new List<string>() {"замер1","замер2","замер3" };
            GetSecret();
            Load();

            


        }
        [RelayCommand]
        public async Task Load()
        {
            
        await  GetSecret();
            int x = stId;
            var measurements = await _database.GetMeasurements(SelectedType, stId);
            MeasurementsList = new ObservableCollection<MeasurementsTable>(measurements);
        }

        public async Task GetSecret() 
        {
            string x  = await SecureStorage.Default.GetAsync("studentID"); 
            stId = Convert.ToInt32(x); 
        }


        [RelayCommand]
        public async Task Save()
        {
            GetSecret();
            string name = selectedType;
            double saveValue = value;
            await _database.SaveMeasurement(selectedType, value , stId);
        }


        partial  void OnSelectedTypeChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                Load();
            }
        }


    }
}
