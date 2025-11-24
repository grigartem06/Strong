using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using System.Collections.ObjectModel;


namespace Strong.ViewModels
{
    public partial class TrenersMainPageViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;

        [ObservableProperty]
        private TrainerTable trainer;

        [ObservableProperty]
        private ObservableCollection<StudentTable> studentsList;

        public TrenersMainPageViewModel()
        {
            
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("Trainer", out var trainerObj))
            {
                Trainer = (TrainerTable)trainerObj;
            }
        }

    }
}
