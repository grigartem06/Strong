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
        public ExerciseTable newExercise;

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

        [RelayCommand]
        public async Task AddNewExersice()
        {
            var name = await Application.Current.MainPage.DisplayPromptAsync("новое упражнение", "введите имя" ,"Добавить", "Отмена" );
            if(name != null)
            {
                var muscleGroupList = await _database.GetMuscleGroup();
                var muscleGroup = await Application.Current.MainPage.DisplayActionSheet("выбретире группу мышц", "отмена", null, muscleGroupList.Select(m=> m.muscleGroup_name).ToList().ToArray());
                var selectedmuscleGroup = muscleGroupList.FirstOrDefault(m => m.muscleGroup_name == muscleGroup);

                if (selectedmuscleGroup != null) 
                {
                    var categoryList = await _database.GetCategory();
                    var category = await Application.Current.MainPage.DisplayActionSheet("выберите категорию", "отмена", null, categoryList.Select(c => c.category_name).ToList().ToArray());
                    var selectedcategory = categoryList.FirstOrDefault(c => c.category_name == category);

                    if (selectedcategory != null) 
                    {
                        newExercise = new ExerciseTable()
                        {
                            exercise_name = name,
                            muscleGroup_id = selectedmuscleGroup.muscleGroup_id,
                            category_id = selectedcategory.category_id,
                            student_id = Convert.ToInt32(await SecureStorage.Default.GetAsync("studentID"))
                        };
                        await _database.AddExercise(newExercise);
                    }
                }
            }
            LoadExercise();
        }

        [RelayCommand]
        public async Task Back() => Shell.Current.GoToAsync("//Pages/StudentsPages/StudentMainPage");
    }


}
