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
    public partial class AddStudentToListViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;

        [ObservableProperty]
        private ObservableCollection<StudentTable> studentsList;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private TrainerTable trainer;


        [RelayCommand]
        public async Task Back()
        {
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Trainer", out var trainerObj))
            {
                Trainer = (TrainerTable)trainerObj;
            }
        }

        public AddStudentToListViewModel()
        {
            LoadStudents();
        }

        [RelayCommand]
        public async Task LoadStudents() 
        {
            var students = await _database.GetStudentWithoutTraeiners();
            studentsList = new ObservableCollection<StudentTable>((IEnumerable<StudentTable>)students);
        }





    }
}
