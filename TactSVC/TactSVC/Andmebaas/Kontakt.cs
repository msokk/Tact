using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Andmebaas
{
    public class Kontakt
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int KasutajaId { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }
        public string Riik { get; set; }
        public string Maakond { get; set; }
        public string Asula { get; set; }
        public string Tanav { get; set; }
        public string MajaNr { get; set; }
        public string WindowsLiveMessenger { get; set; }
        public string Facebook { get; set; }
        public string Orkut { get; set; }
        public string Skype { get; set; }
        public string Twitter { get; set; }
        public string Pilt { get; set; }
    }
}