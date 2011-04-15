﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite;

namespace TactSVC.Andmebaas
{
    public class Kasutaja
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Kasutajanimi { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }
        public string FacebookId { get; set; }
    }

}