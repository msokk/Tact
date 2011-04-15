using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Andmebaas
{
    public class TelefonTyyp
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Tyyp { get; set; }
    }
}