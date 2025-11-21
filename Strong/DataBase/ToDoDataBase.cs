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

    }
}
