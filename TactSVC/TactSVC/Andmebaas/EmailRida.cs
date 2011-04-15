using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Andmebaas
{
    public class EmailRida
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int KontaktId { get; set; }
        public string Email { get; set; }
    }
}