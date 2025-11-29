using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strong.Models
{
    public class RoleTable
    {
        [PrimaryKey, AutoIncrement]
        public int role_id { get; set; }
        public string role_name { get; set; }
}
}
