using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class TrainingTable
    {
        [PrimaryKey, AutoIncrement]
        public int training_id { get; set; }

        public string training_name {get;set;}
        public DateTime training_start {get;set;}
        public DateTime training_end {get;set;}

        public bool IsPattern { get;set;}

        [ForeignKey(nameof(StudentTable))]
        public int student_id { get; set; }
    }
}
