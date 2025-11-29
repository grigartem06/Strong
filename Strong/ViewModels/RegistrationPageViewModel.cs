using CommunityToolkit.Mvvm.ComponentModel;
using Strong.DataBase;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Strong.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace Strong.ViewModels
{
    public partial class RegistrationPageViewModel: ObservableObject
    {
        private readonly ToDoDataBase _database;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string firstPassword;

        [ObservableProperty]
        private string secondPassword;

        [ObservableProperty]
        public ObservableCollection<RoleTable> roles;

        [ObservableProperty]
        public ObservableCollection<UserTable> users;

        [ObservableProperty]
        private RoleTable selectedRole;

        public  RegistrationPageViewModel()
        {
            _database = new ToDoDataBase();
            LoadRoles();
            SecureStorage.Default.RemoveAll();
        }

        private async void LoadRoles() => Roles = new ObservableCollection<RoleTable>(await _database.GetRoles());

        [RelayCommand]
        private async Task Registration()
        {
            bool check = false;
       
            if (string.IsNullOrEmpty(UserName)) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "введите имя пользователя", "ОК"); }
            else if (string.IsNullOrEmpty(FirstPassword)) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "введите пароль", "ОК"); }
            else if (string.IsNullOrEmpty(SecondPassword)) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "повторите пароль", "ОК"); }
            else if (FirstPassword != SecondPassword) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "пароли не сходятся", "ОК"); }
            else { check = true; }

            if(!check) return; //блок при неправильных полях

            bool IsExests = await _database.IsUserNameExists(UserName);
            if (IsExests)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Пользователь с таким именем уже существует", "ОК");
                return;
            }
            
            // Создание пользователя
            var newUser = new UserTable
            {
                user_name = UserName,
                user_password = FirstPassword,
                role_id = SelectedRole.role_id
            };
            // Сохраняем пользователя
            await _database.AddUser(newUser);

            var u = await _database.GetUserByUserNamaAndPassword(userName, FirstPassword);
            // Создаём связанные записи в TrainerTable или StudentTable
            if (newUser.role_id == 1) // Тренер
            {
                var trainer = new TrainerTable
                {
                    user_id = (int)u.user_id 
                };
                await _database.AddTrainer(trainer);
            }
            else if (newUser.role_id == 2) // Ученик
            {
                var student = new StudentTable
                {
                    user_id = (int)u.user_id,
                    student_weight = 0.0, 
                    student_height = 0.0,
                    trainer_id = null

                };
                await _database.AddStudent(student);
            }

            await Application.Current.MainPage.DisplayAlertAsync("Успех", "Регистрация успешна!", "ОК");

        }

        [RelayCommand]
        private async Task Input() => await Shell.Current.GoToAsync("//Pages/InputPage");
    }
}
