using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Strong.DataBase;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strong.ViewModels
{
    public  partial class InputPageViewModel: ObservableObject
    {
        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string password;

        [RelayCommand]
        private async Task Input()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Введите имя пользователя", "ОК");
                return;
            }

            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Введите пароль", "ОК");
                return;
            }

            var database = new ToDoDataBase(); // или используйте DI, если подключено
            var user = await database.GetUserByIdAndPassword(UserName, Password);

            if (user != null)
            {
                //if (user.role_id == 2) { await Application.Current.MainPage.DisplayAlertAsync("Успех", $"Вход выполнен. ID: {user.user_id} ученик = {user.role_id}", "ОК"); }
                //else if (user.role_id == 1)
                //{
                //    await Application.Current.MainPage.DisplayAlertAsync("Успех", $"Вход выполнен. ID: {user.user_id} тренер ={user.role_id} ", "ОК");
                //    await Shell.Current.GoToAsync("//Pages/TrenersPages/TrenersMainPage");
                //}


                if (user.role_id == 1) // тренер
                {
                    var trainer = await database.GetTrainerByUserId(user.user_id);
                    if (trainer != null)
                    {
                        await Shell.Current.GoToAsync("//TrainerPage", new Dictionary<string, object>
                        {
                            ["Trainer"] = trainer
                        });
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Данные тренера не найдены", "ОК");
                    }
                }
                else if (user.user_id == 2) //ученик
                {
                    var student = await database.GetStudentByUserId(user.user_id);
                    if (student != null)
                    {
                        await Shell.Current.GoToAsync("//StudentPage", new Dictionary<string, object>
                        {
                            ["Student"] = student
                        });
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Неверное имя пользователя или пароль", "ОК");
                }
            }
        }

        [RelayCommand]
        private async Task Registration() { await Shell.Current.GoToAsync("//RegistrationPage"); }
    }
}
