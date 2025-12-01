
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class MeasurementsTable
    {
        [PrimaryKey, AutoIncrement]
        public int measurements_id { get; set; }
        public string measurements_name { get; set; }

        public DateTime measurements_date { get; set; }

        public double measurements_value { get; set; }

        [ForeignKey(nameof(StudentTable))]
        public int student_id { get; set; }


    }
}
