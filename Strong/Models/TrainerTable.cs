using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class TrainerTable
    {
        [PrimaryKey, AutoIncrement]
        public int trainer_id { get; set; }

        [ForeignKey(nameof(UserTable))]
        public int user_id { get; set; }
}
}
