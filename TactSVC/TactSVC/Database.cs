using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

    }

    public class Database : SQLiteConnection
    {
        public Database(string path)
            : base(path)
        {
            CreateTable<User>();
        }
    }
}