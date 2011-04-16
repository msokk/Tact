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
            CreateTable<TelefonTyyp>();
            CreateTable<TelefonRida>();
            CreateTable<EmailTyyp>();
            CreateTable<EmailRida>();
        }
    }
}