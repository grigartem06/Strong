using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class StudentTable
    {
        [PrimaryKey, AutoIncrement]
        public int student_id { get; set; }

        public double student_weight { get; set; }
        public double student_height { get; set; }

        [ForeignKey(nameof(UserTable))]
        public int user_id { get; set; }

        [ForeignKey(nameof(TrainerTable))]
        public int? trainer_id { get;set; }
}
}
