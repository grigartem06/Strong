using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Strong.ViewModels
{
    public partial class NewTrainingViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;
        private string _studentIdStr; // хранить строку, а не int

        [ObservableProperty] private TrainingTable trainingTable;
        [ObservableProperty] private SetsTable sets;
        [ObservableProperty] public List<string> exerciseList;
        [ObservableProperty] public ObservableCollection<SetsTable> exerciseSections;

        public TrainingTable nowtraining; // можно сделать ObservableProperty, если нужно обновлять UI

        public NewTrainingViewModel()
        {
            _database = new ToDoDataBase(); InitializeAsync(); 
            // Не делаем ничего асинхронного в конструкторе!
        }

        public async Task InitializeAsync()
        {
            try
            {
                _studentIdStr = await SecureStorage.Default.GetAsync("studentID");

                if (string.IsNullOrEmpty(_studentIdStr))
                {
                    throw new InvalidOperationException("Student ID not found in secure storage.");
                }

                int studentId = Convert.ToInt32(_studentIdStr);

                trainingTable = new TrainingTable()
                {
                    training_name = "Name",
                    training_start = DateTime.Now,
                    student_id = studentId,
                    training_end = DateTime.Now,
                    IsPattern = false
                };

                // Добавляем тренировку и ждём завершения
                await AddNewTraining();

                // Получаем только что созданную тренировку по её данным
                nowtraining = await _database.GetNowTraining(trainingTable);
                await SecureStorage.Default.SetAsync("trainingID",  nowtraining.training_id.ToString());
            }
            catch (Exception ex)
            {
                // Логируем или показываем пользователю
                Console.WriteLine($"Ошибка инициализации: {ex.Message}");
            }
            await Load();
        }

        public async Task AddNewTraining()
        {
            await _database.AddTraining(trainingTable);
        }



        public async Task AddSets() => await _database.AddSets(sets);

        [RelayCommand]
        public async Task AddExerciseToTraining()
        {
            if (nowtraining == null)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Тренировка не создана.", "OK");
                return;
            }

            var exercises = await _database.GetExercise(_studentIdStr); // передаём строку, а не int

            if (exercises == null || !exercises.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Нет упражнений для выбора.", "OK");
                return;
            }

            ExerciseList = exercises.Select(e => e.exercise_name).ToList();

            string selectedName = await Application.Current.MainPage.DisplayActionSheet("Выберите упражнение", "Отмена", null, ExerciseList.ToArray());

            if (string.IsNullOrEmpty(selectedName)) return;

            var selectedExercise = exercises.FirstOrDefault(e => e.exercise_name == selectedName);

            if (selectedExercise == null) return;

            sets = new SetsTable()
            {
                exercise_id = selectedExercise.Id,
                exercise_weight = 0,
                exercise_reps = 0,
                rest_time = 3,
                training_id = nowtraining.training_id
            };

            await AddSets();
            await Load();
        }

        // Если нужно перезагрузить текущую тренировку
        public async Task GetNow()
        {
            if (trainingTable != null)
            {
                nowtraining = await _database.GetNowTraining(trainingTable);
            }
        }

        public async Task Load()
        {
            GetNow();
            var training = await _database.GetSetsByStudentId(nowtraining.training_id);
            exerciseSections = new ObservableCollection<SetsTable>();
        }
    }
}