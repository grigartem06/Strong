using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class SetsTable
    {
        [PrimaryKey, AutoIncrement]
        public int sets_id { get; set; }

        [ForeignKey(nameof(ExerciseTable))]
        public int exercise_id { get; set; }

        public double exercise_weight { get; set; }
        public double exercise_reps { get; set; }

        [ForeignKey(nameof(TrainingTable))]
        public int training_id { get; set; }

        public double rest_time { get; set; }



    }
}
