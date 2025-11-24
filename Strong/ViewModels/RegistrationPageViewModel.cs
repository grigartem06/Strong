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

        public RegistrationPageViewModel()
        {
            _database = new ToDoDataBase();
            LoadRoles();

        }

        private async void LoadRoles()
        {
            var rolelist = await _database.GetRoles();
            Roles = new ObservableCollection<RoleTable>(rolelist);
        }

        [RelayCommand]
        private async Task Registration()
        {
            bool check = false;
       




            if (string.IsNullOrEmpty(UserName)) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "введите имя пользователя", "ОК"); }
            else if (string.IsNullOrEmpty(FirstPassword)) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "введите пароль", "ОК"); }
            else if (string.IsNullOrEmpty(SecondPassword)) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "повторите пароль", "ОК"); }
            else if (FirstPassword != SecondPassword) { await Application.Current.MainPage.DisplayAlertAsync("ошибка", "пароли не сходятся", "ОК"); }
            else { check = true; }




            //if (check)
            //{
            //    var newUser = new UserTable
            //    {
            //        user_name = UserName, // исправлено: UserName, а не userName
            //        user_password = FirstPassword, // исправлено: FirstPassword
            //        role_id = SelectedRole.role_id
            //    };

            //    if (newUser.role_id == 1) //тренер
            //    {
            //        var newTrainer = new TrainerTable { };
            //    }
            //    else if (newUser.role_id == 2)//ученик 
            //    {
                    
            //    }

            //    await NewUser(newUser);
            //}

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
            var userId = await _database.AddUser(newUser);

            // Создаём связанные записи в TrainerTable или StudentTable
            if (newUser.role_id == 1) // Тренер
            {
                var trainer = new TrainerTable
                {
                    user_id = (int)userId // user_id из вставленного пользователя
                };
                await _database.AddTrainer(trainer);
            }
            else if (newUser.role_id == 2) // Ученик
            {
                var student = new StudentTable
                {
                    user_id = (int)userId,
                    student_weight = 0.0, 
                    student_height = 0.0
                };
                await _database.AddStudent(student);
            }

            await Application.Current.MainPage.DisplayAlertAsync("Успех", "Регистрация успешна!", "ОК");










        }

        [RelayCommand]
        private async Task Input() 
        {
            await Shell.Current.GoToAsync("//Pages/InputPage");
        }

        public async Task NewUser(UserTable user)
        {
            // Проверка на уникальность имени
            bool isExists = await _database.IsUserNameExists(user.user_name);
            if (isExists)
            {
                await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Пользователь с таким именем уже существует", "ОК");
                return;
            }
            // Если имя уникально — добавляем пользователя
            await _database.AddUser(user);
        }
    }
}
