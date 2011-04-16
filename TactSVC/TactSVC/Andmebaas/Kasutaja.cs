using System;
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
        public string Parool { get; set; }
        public string Eesnimi { get; set; }
        public string Perenimi { get; set; }
        public string FacebookId { get; set; }


        public Kontakt[] koikKontaktid(SQLiteConnection c)
        {
            var q = from k in c.Table<Kontakt>()
                    where k.KasutajaId == Id
                    select k;
            return q.ToArray();
        }

        public Kontakt[] otsiKontaktid(Kontakt otsing, SQLiteConnection c)
        {
            var q = from k in c.Table<Kontakt>()
                    where k.KasutajaId == Id &&
                    (otsing.Eesnimi != null) ? k.Eesnimi.Contains(otsing.Eesnimi) : true &&
                    (otsing.Perenimi != null) ? k.Perenimi.Contains(otsing.Perenimi) : true &&
                    (otsing.Asula != null) ? k.Asula.Contains(otsing.Asula) : true &&
                    (otsing.Facebook != null) ? k.Facebook.Contains(otsing.Facebook) : true &&
                    (otsing.Id != null) ? k.Id == otsing.Id : true &&
                    (otsing.Maakond != null) ? k.Maakond.Contains(otsing.Maakond) : true &&
                    (otsing.MajaNr != null) ? k.MajaNr.Contains(otsing.MajaNr) : true &&
                    (otsing.Orkut != null) ? k.Orkut.Contains(otsing.Orkut) : true &&
                    (otsing.Riik != null) ? k.Riik.Contains(otsing.Riik) : true &&
                    (otsing.Skype != null) ? k.Skype.Contains(otsing.Skype) : true &&
                    (otsing.Tanav != null) ? k.Tanav.Contains(otsing.Tanav) : true &&
                    (otsing.Twitter != null) ? k.Twitter.Contains(otsing.Twitter) : true &&
                    (otsing.WindowsLiveMessenger != null) ? k.WindowsLiveMessenger.Contains(otsing.WindowsLiveMessenger) : true &&
                    (otsing.EmailKodu != null) ? k.EmailKodu.Contains(otsing.EmailKodu) : true &&
                    (otsing.EmailToo != null) ? k.EmailToo.Contains(otsing.EmailToo) : true &&
                    (otsing.TelefonKodu != null) ? k.TelefonKodu.Contains(otsing.TelefonKodu) : true &&
                    (otsing.TelefonMob != null) ? k.TelefonMob.Contains(otsing.TelefonMob) : true &&
                    (otsing.TelefonToo != null) ? k.TelefonToo.Contains(otsing.TelefonToo) : true
                    select k;

            return q.ToArray();
        }

        public int LisaKontakt(Kontakt k, SQLiteConnection c)
        {
            k.KasutajaId = Id;
            return c.Insert(k);
        }

        public int EemaldaKontakt(int id, SQLiteConnection c)
        {
            Kontakt[] k = this.otsiKontaktid(new Kontakt() { Id = id }, c);
            if (k.Length > 0)
            {
                return c.Delete(k[0]);
            }
            return 0;
        }
    }

}