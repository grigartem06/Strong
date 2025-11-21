using CommunityToolkit.Mvvm.ComponentModel;
using Strong.DataBase;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using Strong.Models;
using System.Collections.ObjectModel;

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
        private RoleTable selectedRole;

        public RegistrationPageViewModel()
        {
            _database = new ToDoDataBase();
            LoadRoles();

        }

        private async void LoadRoles()
        {
            var roles = await _database.GetRoles();
            Roles = new ObservableCollection<RoleTable>(roles);
        }

        [RelayCommand]
        private async Task Registration()
        {
            bool check = false;

            if (string.IsNullOrEmpty(UserName)) 
            {
                await Application.Current.MainPage.DisplayAlertAsync("ошибка", "введите имя пользователя" , "ОК"); 
            }
            else {check =true;}

            if (string.IsNullOrEmpty(FirstPassword))
            {
                await Application.Current.MainPage.DisplayAlertAsync("ошибка", "введите пароль", "ОК");
            }
            else { check = true; }

            if (string.IsNullOrEmpty(SecondPassword))
            {
                await Application.Current.MainPage.DisplayAlertAsync("ошибка", "повторите пароль", "ОК");
            }
            else { check = true; }

            if (FirstPassword == SecondPassword)
            {
                await Application.Current.MainPage.DisplayAlertAsync("ошибка", "пароли не сходятся", "ОК");
            }
            else { check = true; }






        }

        [RelayCommand]
        private async Task Input() { }

    }
}
