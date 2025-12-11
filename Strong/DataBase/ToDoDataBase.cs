

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
                _connection.CreateTableAsync<MeasurementsTable>().Wait();

            //заполнение внутренних полей   
                MuscleGroup();
                Roles();
                Category();
                Exercise();
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

        //добавление групп мышщ
        public Task<int> AddMuscleGroup(MuscleGroupTable muscleGroup)
        {
            if(muscleGroup.muscleGroup_id == 0)
                return _connection.InsertAsync(muscleGroup);
            else
                return _connection.UpdateAsync(muscleGroup);
        }
        private async void MuscleGroup()
        {
            if(_connection.Table<MuscleGroupTable>().CountAsync().Result == 0)
            {
                var biceps = new MuscleGroupTable {muscleGroup_name= "бицепс" };
                var triceps= new MuscleGroupTable {muscleGroup_name= "трицепс" };
                var back = new MuscleGroupTable {muscleGroup_name= "спина" };
                var chest = new MuscleGroupTable {muscleGroup_name= "грудь" };
                var shoulders = new MuscleGroupTable {muscleGroup_name= "плечи" };
                var core = new MuscleGroupTable {muscleGroup_name= "пресс" };
                var legs = new MuscleGroupTable {muscleGroup_name= "ноги" };

                AddMuscleGroup(biceps);
                AddMuscleGroup(triceps);
                AddMuscleGroup(back);
                AddMuscleGroup(chest);
                AddMuscleGroup(shoulders);
                AddMuscleGroup(core);
                AddMuscleGroup(legs);

            }
        }

        //добавление категорий
        public Task<int> AddCategore(CategoryTable category)
        {
            if (category.category_id == 0)
                return _connection.InsertAsync(category);
            else
                return _connection.UpdateAsync(category);
        }
        public async void Category() 
        {
            if (_connection.Table<CategoryTable>().CountAsync().Result == 0)
            {
                var list = new List<string>() { "штанга", "гантели", "машина", "масса тела", "упражнение с опорой", "только повторения", "кардио", "длительность" };

                for(int i = 0; i < list.Count; i++)
                {
                    var category = new CategoryTable { category_name = list[i]};
                    AddCategore(category);
                }

            }
        }

        //добавление системных упражнений
        public Task<int> AddExercise(ExerciseTable exercise)
        {
            if (exercise.Id == 0)
                return _connection.InsertAsync(exercise);
            else
                return _connection.UpdateAsync(exercise);
        }
        private async void Exercise()
        {
            if(_connection.Table<ExerciseTable>().CountAsync().Result == 0) 
            {
                var list = new List<string>() { "жим лёжа штанги", "жим  лёжа на наклонной"  };
                
                for (int i = 0; i < list.Count; i++)
                {
                    var exercise = new ExerciseTable { exercise_name = list[i], category_id=2 , muscleGroup_id = 4 };
                    AddExercise(exercise);
                }
            }
        }

        

        public Task<List<RoleTable> > GetRoles () => _connection.Table<RoleTable>().ToListAsync();

        public Task<int> AddUser(UserTable user)=> _connection.InsertAsync(user);

        public async Task<List<UserTable>> GetUsers()=> await _connection.Table<UserTable>().ToListAsync();

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

        public async Task<UserTable> GetUserByUserNamaAndPassword(string  userName, string password)
        {
            var u = await _connection.Table<UserTable>().FirstOrDefaultAsync(z => z.user_name == userName && z.user_password == password);
            return u;
        }

        //ai
        public async Task<int> AddTrainer(TrainerTable trainer) => await _connection.InsertAsync(trainer);

        //ai
        public async Task<int> AddStudent(StudentTable student)=> await _connection.InsertAsync(student);
        
        //ai
        public async Task<TrainerTable> GetTrainerByUserId(int userId)=> await _connection.Table<TrainerTable>().FirstOrDefaultAsync(t => t.user_id == userId);

        public async Task<StudentTable> GetStudentByUserId(int userId) =>  await _connection.Table<StudentTable>().FirstOrDefaultAsync(s => s.user_id == userId);

        public async Task<List<StudentTable>> GetStudentWithoutTraeiners() => await _connection.Table<StudentTable>().Where(s => s.trainer_id == null).ToListAsync();

        //ai
        public async Task<List<StudentWithUserName>> GetStudentsWithoutTrainerWithUserNames()
        {
            var query = @"
            SELECT s.student_id, s.student_weight, s.student_height, s.user_id, s.trainer_id,
                   u.user_name
            FROM StudentTable s
            INNER JOIN UserTable u ON s.user_id = u.user_id
            WHERE s.trainer_id IS NULL";

            return await _connection.QueryAsync<StudentWithUserName>(query);
        }

        public class StudentWithUserName
        {
            public int student_id { get; set; }
            public double student_weight { get; set; }
            public double student_height { get; set; }
            public int user_id { get; set; }
            public int? trainer_id { get; set; }
            public string user_name { get; set; }
        }

        //ai
        public async Task<int> UpdateStudent(StudentTable student) => await _connection.UpdateAsync(student);

        public async Task<List<ExerciseTable>> GetExercise(string userId)
        {
            int z = Convert.ToInt32(userId);
            return await _connection.QueryAsync<ExerciseTable>("SELECT * FROM ExerciseTable WHERE student_id IS NULL OR student_id = ?", z);
        }

        public async Task<List<StudentTable>> GetStudentsWithTrener(int trenerID)
        {
            int x = Convert.ToInt32(trenerID);
            return await _connection.Table<StudentTable>().Where(s => s.trainer_id == x).ToListAsync();
        }

        
        public async Task SaveMeasurement(string name, double value, int st)
        {
            var meansure = new MeasurementsTable { measurements_name = name, measurements_value = value , student_id =st, measurements_date = DateTime.Now};
            await _connection.InsertAsync(meansure);
        }

        public async Task<List<MeasurementsTable>> GetMeasurements(string name, int id)
        {
            if (name != null)
                return await _connection.Table<MeasurementsTable>().Where(s => s.student_id == id && s.measurements_name == name).ToListAsync();
            else
                return await _connection.Table<MeasurementsTable>().Where(s => s.student_id == id).ToListAsync();
        }

        public async Task<TrainingTable> AddTraining(TrainingTable training)
        {
            await _connection.InsertAsync(training); 
            return training;
        }
        
        
        
        public async Task<TrainingTable> GetNowTraining(TrainingTable training)
        {
            return await _connection.Table<TrainingTable>().
                FirstOrDefaultAsync(t => t.training_name == training.training_name 
                && t.IsPattern == training.IsPattern 
                && t.training_start == training.training_start 
                && t.student_id == training.student_id);
            
        }


        public async Task AddSets(SetsTable sets) => await _connection.InsertAsync(sets);

        public async Task<List<SetsTable>> GetSetsByStudentId(int trId) => await _connection.Table<SetsTable>().Where(s=> s.training_id == trId).ToListAsync();

        public async Task <List<TrainingTable>> GetAllTrainings(int stId)
            => await _connection.Table<TrainingTable>().Where(s=> s.student_id == stId).ToListAsync();

        public async Task<List<SetsTable>> GetSetsByTrainingId(int trainingID)
            => await _connection.Table<SetsTable>().Where(s => s.training_id == trainingID).ToListAsync();



        public async Task<List<MuscleGroupTable>> GetMuscleGroup () => await _connection.Table<MuscleGroupTable>().ToListAsync();

        public async Task<List<CategoryTable>> GetCategory() => await _connection.Table<CategoryTable>().ToListAsync();

        public async Task DellTraining(int trId)
        {
            var setsDel = await _connection.Table<SetsTable>().Where(s => s.training_id == trId).ToListAsync();

            foreach (var set in setsDel)
            {
                await _connection.DeleteAsync(set);
            }

            var trDel = await _connection.Table<TrainingTable>().Where(t => t.training_id == trId).ToListAsync();

            foreach (var training in trDel)
            {
                await _connection.DeleteAsync(training);
            }
        }


    }
}
