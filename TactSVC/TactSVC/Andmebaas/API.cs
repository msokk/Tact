using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Andmebaas
{
    public class API
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Nimi { get; set; }
        public string Voti { get; set; }
        public string Domeen { get; set; }

        public static string votaDomeen(string voti, SQLiteConnection c)
        {
            var q = from k in c.Table<API>()
                    where k.Voti == voti
                    select k.Domeen;
            return (q.ToArray().Length != 0) ? q.ToArray()[0]: null;
        }

        public static string votaNimi(string voti, SQLiteConnection c)
        {
            var q = from k in c.Table<API>()
                    where k.Voti == voti
                    select k.Nimi;
            return (q.ToArray().Length != 0) ? q.ToArray()[0] : null;
        }
    }
}