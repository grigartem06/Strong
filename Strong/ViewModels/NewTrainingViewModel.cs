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
        public int stID;
        public int id;
        private readonly ToDoDataBase _database;
        public TrainingTable nowtraining;
        public TrainingTable now;

        [ObservableProperty] public List<string> exerciseList;
        [ObservableProperty] private SetsTable sets;
        [ObservableProperty] private ObservableCollection<SetsTable> exerciseSections;

        // при загрузке
        // получаем studentID 
        // 1. создаётся тренировка 
        // 2. сохраняется тренировка
        // 3. получаем training_id только что созданной тренировки

        // 4. создём запись setsTable c training_id
        // 5. сохраняем 
        // 6. обновляем вид

        public NewTrainingViewModel() 
        {
            _database = new ToDoDataBase();
             GetSecret();
        }

        public async Task GetSecret()
        {
            stID = Convert.ToInt32(await SecureStorage.Default.GetAsync("studentID"));
            nowtraining = new TrainingTable()
            {
                student_id = stID,
                training_name = "name",
                training_start = DateTime.Now,
                training_end = DateTime.Now,
                IsPattern = false
            };

            var addedTraining = await _database.AddTraining(nowtraining);
            now = addedTraining;
            id = now.training_id;
        }

        public async Task GetInf()
        {
            now = await _database.GetNowTraining(nowtraining);
            int id = now.training_id;
        }

        [RelayCommand]
        public async Task AddExerciseToTraining()
        {
            var exercises = await _database.GetExercise(stID.ToString()); 
            exerciseList = exercises.Select(e => e.exercise_name).ToList();

            string selectedName = await Application.Current.MainPage.DisplayActionSheet("Выберите упражнение", "Отмена", null, exerciseList.ToArray());

            if (string.IsNullOrEmpty(selectedName)) return;

            var selectedExercise = exercises.FirstOrDefault(e => e.exercise_name == selectedName);

            if (selectedExercise == null) return;

            sets = new SetsTable()
            {
                exercise_id = selectedExercise.Id,
                exercise_weight = 0,
                exercise_reps = 0,
                rest_time = 3,
                training_id = id
            };

            await _database.AddSets(sets);

            var e = await _database.GetSetsByTrainingId(id);
            ExerciseSections = new ObservableCollection<SetsTable>(e);


            InitializeAsync();
        }

        [RelayCommand]
        public async Task SaveTraining()
        { 
            
            if (ExerciseSections == null) 
            {
                CancelTraining();
            } 
            else { await Shell.Current.GoToAsync("//Pages/StudentsPages/StudentMainPage"); }
        }

        [RelayCommand]
        public async Task CancelTraining ()
        {
             _database.DellTraining(id);
            
            if(ExerciseSections!= null) 
                ExerciseSections.Clear();
            
            Shell.Current.GoToAsync("//Pages/StudentsPages/StudentMainPage");

        }


        //моментальное изменение значений
        public async Task InitializeAsync()
        {
            await LoadSetsAsync();
        }

        [RelayCommand]
        public async Task LoadSetsAsync()
        {
            var sets = await _database.GetSetsByTrainingId(id);
            ExerciseSections.Clear();
            foreach (var set in sets) 
            {
                set.ValueChanged += OnSetValueChanged;
                ExerciseSections.Add(set);
            }
        }

        private async void OnSetValueChanged(SetsTable changedSet)
        {
            try
            {
                _database.UpdateAsync(changedSet);
            }
            catch (Exception ex) { }
        }

    }
}