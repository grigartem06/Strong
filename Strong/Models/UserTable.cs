using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strong.Models
{
    public class UserTable
    {
        [PrimaryKey, AutoIncrement]
        public int user_id { get; set; } //1    2
        public string user_name { get; set; }// student trener
        public string user_password { get; set; } // student trener

        [ForeignKey(nameof(RoleTable))]
        public int role_id { get; set; }  //2 = ученик  1 = тренер




}
}
