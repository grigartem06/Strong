
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
    public partial class AboutTrainingPageViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;
        public int trainingId;

        [ObservableProperty] public ObservableCollection<SetsTable> setsList;

        [ObservableProperty]
        private ObservableCollection<Grouping<string, SetsTable>> _groupedSets;



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

            // Группируем по exercise_id (или другому полю)
            var grouped = sets
                .GroupBy(s => s.exercise_id.ToString()) // или название упражнения
                .Select(g => new Grouping<string, SetsTable>(g.Key, g))
                .ToList();

            GroupedSets = new ObservableCollection<Grouping<string, SetsTable>>(grouped);
        }

        public class Grouping<TKey, TElement> : ObservableCollection<TElement>
        {
            public TKey Key { get; private set; }

            public Grouping(TKey key, IEnumerable<TElement> items)
            {
                Key = key;
                foreach (var item in items)
                    this.Add(item);
            }
        }

        [RelayCommand]
        public async Task TextChanged()
        {
            await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Введите пароль", "ОК");
        }

    }
}
