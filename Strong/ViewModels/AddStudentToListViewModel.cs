using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Strong.DataBase.ToDoDataBase;

namespace Strong.ViewModels
{
    public partial class AddStudentToListViewModel : ObservableObject
    {
        private readonly ToDoDataBase _database;
        public int id;
        public int stID;
        [ObservableProperty] public ObservableCollection<StudentWithUserName> studentsList;
        [ObservableProperty] private TrainerTable trainer;

        
        public AddStudentToListViewModel()
        {
             _database = new ToDoDataBase();
            LoadStudents();
        }

        [RelayCommand] 
        public async Task Back() => await Shell.Current.GoToAsync("..");


        [RelayCommand]
        public async Task LoadStudents() 
        {
            var students = await _database.GetStudentsWithoutTrainerWithUserNames();
            StudentsList = new ObservableCollection<StudentWithUserName>(students);
        }

        [RelayCommand]
        public async Task AddStudentForTrainer(int studentId)
        {
            stID = studentId;
            await GetSecret();

            var student =  await _database.GetStudentByUserId(stID);
            if (student == null) return;

            var studentToUpdate = new StudentTable
            {
                student_id = student.student_id,
                student_weight = student.student_weight,
                student_height = student.student_height,
                user_id = student.user_id,
                trainer_id = id
            };

            await _database.UpdateStudent(studentToUpdate);
            await LoadStudents();
        }

        public async Task GetSecret() 
        {
            string x = await SecureStorage.Default.GetAsync("trenerID") ;
            id = Convert.ToInt32(x);
        }
    }
}
