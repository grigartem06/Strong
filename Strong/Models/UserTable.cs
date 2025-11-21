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
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }
        
        [ForeignKey(nameof(RoleTable))]
        public int role_id { get; set; }
}
}
