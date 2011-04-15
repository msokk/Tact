using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Database
{



    public class Andmebaas : SQLiteConnection
    {
        public Andmebaas(string path)
            : base(path)
        {
            //CreateTable<Stock>();
            //CreateTable<Valuation>();
        }
    }
}