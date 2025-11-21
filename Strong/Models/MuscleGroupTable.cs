using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strong.Models
{
    public class MuscleGroupTable
    {
        [PrimaryKey, AutoIncrement]
        public int muscleGroup_id { get; set; }
        public string muscleGroup_name { get; set; }

    }
}
