using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // Для IAsyncRelayCommand, если нужно
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks; // Добавить

namespace Strong.ViewModels
{
    public partial class AboutTrainingPageViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;
        private int trainingId;

        [ObservableProperty]
        private ObservableCollection<SetsTable> setsList = new(); // Инициализировать сразу

        public AboutTrainingPageViewModel() // Конструктор остаётся синхронным
        {
            _database = new ToDoDataBase();
            // Убираем LoadSets из конструктора
        }

        // Метод для инициализации, вызываемый извне
        public async Task InitializeAsync()
        {
            await GetSecret();
            await LoadSetsAsync(); // Вызываем асинхронно
        }

        public async Task GetSecret()
        {
            var trainingIdStr = await SecureStorage.Default.GetAsync("TrainingID");
            if (!string.IsNullOrEmpty(trainingIdStr) && int.TryParse(trainingIdStr, out int id))
            {
                trainingId = id;
            }
            else
            {
                //trainingId = -1; // Или другое значение по умолчанию
            }
        }

        [RelayCommand] // Команда может быть асинхронной
        public async Task LoadSetsAsync() // Сделаем метод асинхронным
        {
            if (trainingId == 0) // Проверяем, был ли уже получен ID
            {
                await GetSecret();
            }
            if (trainingId == -1) return; // Не грузим, если ID не валидный

            var sets = await _database.GetSetsByTrainingId(trainingId);
            SetsList.Clear(); // Очищаем старые данные
            foreach (var set in sets)
            {
                set.ValueChanged += OnSetValueChanged;

                SetsList.Add(set); // Добавляем в существующую ObservableCollection
            }
        }

        private async void OnSetValueChanged(SetsTable changedSet)
        {
            try
            {
                _database.UpdateAsync(changedSet);
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Ошибка при сохранении сета {changedSet.sets_id}: {ex.Message}");
                // Можно добавить уведомление пользователю
            }
        }


    }
}