using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Andmebaas
{
    public class TelefonRida
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int KontaktId { get; set; }
        public int TelefonTyypId { get; set; }
        public string Telefon { get; set; }
    }
}