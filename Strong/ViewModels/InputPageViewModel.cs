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

        [ObservableProperty]
        private bool switchInf;

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
                await SecureStorage.Default.SetAsync("userID", user.user_id.ToString());
                if (user.role_id == 1) // тренер
                {
                    var trainer = await database.GetTrainerByUserId(user.user_id);
                    if (trainer != null)
                    {

                        await SecureStorage.Default.SetAsync("trenerID",trainer.trainer_id.ToString());


                        await Shell.Current.GoToAsync("//Pages/TrenersPages/TrenersMainPage");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Данные тренера не найдены", "ОК");
                    }
                }


                else if (user.role_id == 2) //ученик
                {
                    var student = await database.GetStudentByUserId(user.user_id);

                    if (student != null)
                    {
                       
                        await SecureStorage.Default.SetAsync("studentID", student.student_id.ToString());

                        await Shell.Current.GoToAsync("//Pages/StudentsPages/StudentMainPage");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlertAsync("Ошибка", "Неверное имя пользователя или пароль", "ОК");
                }

                await SecureStorage.SetAsync("zxc", "sec");
            }
        }

        [RelayCommand] private async Task Registration() { await Shell.Current.GoToAsync("//RegistrationPage"); }
    }
}
