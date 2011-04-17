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
            CreateTable<API>();
        }

        public Kasutaja tagastaKasutaja(string kasutajanimi)
        {
            try
            {
                return this.Table<Kasutaja>().Where(k => k.Kasutajanimi == kasutajanimi).Single();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Kasutaja tagastaKasutajaFB(string facebookId)
        {
            try
            {
            return this.Table<Kasutaja>().Where(k => k.FacebookId == facebookId).Single();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}