using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;


namespace TactSVC.Andmebaas
{

    public class Andmebaas : SQLiteConnection
    {
        public Andmebaas(string path)
            : base(path)
        {
            CreateTable<Kasutaja>();
            CreateTable<Kontakt>();
        }

        public IEnumerable<Kasutaja> tagastaKasutaja(string kasutajanimi)
        {
           return this.Table<Kasutaja> ().Where(k => k.Kasutajanimi == kasutajanimi);
        }

        public IEnumerable<Kasutaja> tagastaKasutajaFB(string facebookId)
        {
            return this.Table<Kasutaja>().Where(k => k.FacebookId == facebookId);
        }
    }
}