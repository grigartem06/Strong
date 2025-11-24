using SQLite;
using Strong.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Strong.DataBase
{
    
    public class ToDoDataBase
    {
        private readonly SQLiteAsyncConnection _connection;

        public ToDoDataBase()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "StrongDB.db");

                //созд файла бд
                _connection = new SQLiteAsyncConnection(dbPath);
                //создание таблиц
                _connection.CreateTableAsync<RoleTable>().Wait();
                _connection.CreateTableAsync<UserTable>().Wait();
                _connection.CreateTableAsync<TrainerTable>().Wait();
                _connection.CreateTableAsync<StudentTable>().Wait();
                _connection.CreateTableAsync<MuscleGroupTable>().Wait();
                _connection.CreateTableAsync<CategoryTable>().Wait();
                _connection.CreateTableAsync<ExerciseTable>().Wait();
                _connection.CreateTableAsync<TrainingTable>().Wait();
                _connection.CreateTableAsync<SetsTable>().Wait();
                //заполнение внутренних полей   
                Roles();
        }

        public  Task<int> AddRoles(RoleTable role)
        {
            if (role.role_id == 0)
                return _connection.InsertAsync(role);
            else
                return _connection.UpdateAsync(role);
        }

        private async void Roles()
        {
            var count = _connection.Table<RoleTable>().CountAsync().Result;
            if (count == 0)
            {
                var trainer = new RoleTable { role_name = "тренер" };
                var student = new RoleTable { role_name = "ученик" };

                await AddRoles(trainer);
                await AddRoles(student);
            }
        }

        public Task<List<RoleTable> > GetRoles ()
        {
            return _connection.Table<RoleTable>().ToListAsync();
        }

        public Task<int> AddUser(UserTable user)
        {
            if(user.user_id == 0)
                return _connection.InsertAsync(user);
            else
                return _connection.UpdateAsync(user);
        }

        public async Task<List<UserTable>> GetUsers()
        {
            return await _connection.Table<UserTable>().ToListAsync();
        }

        //ai
        public async Task<bool> IsUserNameExists(string userName)
        {
            var user = await _connection.Table<UserTable>()
                .FirstOrDefaultAsync(u => u.user_name == userName);
            return user != null;
        }

        //ai
        public async Task<UserTable> GetUserByIdAndPassword(string userName, string password)
        {
            var user = await _connection.Table<UserTable>()
                .FirstOrDefaultAsync(u => u.user_name == userName && u.user_password == password);
                return user;
        }

        //ai
        public async Task<int> AddTrainer(TrainerTable trainer)
        {
            return await _connection.InsertAsync(trainer);
        }

        //ai
        public async Task<int> AddStudent(StudentTable student)
        {
            return await _connection.InsertAsync(student);
        }

        

        //ai
        public async Task<TrainerTable> GetTrainerByUserId(int userId)
        {
            return await _connection.Table<TrainerTable>()
                .FirstOrDefaultAsync(t => t.user_id == userId);
        }

        public async Task<StudentTable> GetStudentByUserId(int userId)
        {
            return await _connection.Table<StudentTable>()
                .FirstOrDefaultAsync(s => s.user_id == userId);
        }

        public async Task<List<StudentTable>> GetStudentWithoutTraeiners()
        {
            //return await _connection.Table<StudentTable>()
            //    .FirstOrDefaultAsync(s=>s.trainer_id == null);

            return await _connection.Table<StudentTable>()
            .Where(s => s.trainer_id == null)
            .ToListAsync();
        }

    }
}
