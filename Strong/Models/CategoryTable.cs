using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strong.Models
{
    public class CategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int category_id { get; set; }
        public string category_name{ get; set; }
    }
}
