using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class ExerciseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string exercise_name { get; set; }

        [ForeignKey(nameof(MuscleGroupTable))]
        public int muscleGroup_id { get; set; }

        [ForeignKey(nameof(CategoryTable))]
        public int category_id { get; set; }

        [ForeignKey(nameof(StudentTable))]
        public int? student_id { get; set; }

    }
}
