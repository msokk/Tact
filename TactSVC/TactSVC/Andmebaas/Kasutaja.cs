using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.SqlClient;
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

       public Kontakt[] otsiKontaktid(Kontakt otsing, SQLiteConnection c)
        {
            var q = from k in c.Table<Kontakt>()
                    where k.KasutajaId == Id
                    select k;

            if (otsing.Id != 0)
                q = q.Where(k => k.Id == otsing.Id);

            if (otsing.Eesnimi != null)
                q = q.Where(k => SqlMethods.Like(k.Eesnimi, otsing.Eesnimi));

            if(otsing.Perenimi != null)
                q = q.Where(k => k.Perenimi.Contains(otsing.Perenimi));

            if(otsing.Asula != null) 
                q = q.Where(k => k.Asula.Contains(otsing.Asula));

            if(otsing.Facebook != null)
                q = q.Where(k => k.Facebook.Contains(otsing.Facebook));

            if(otsing.Maakond != null)
                q = q.Where(k => k.Maakond.Contains(otsing.Maakond));

            if(otsing.MajaNr != null)
                q = q.Where(k => k.MajaNr.Contains(otsing.MajaNr));

            if(otsing.Orkut != null)
                q = q.Where(k => k.Orkut.Contains(otsing.Orkut));

            if(otsing.Riik != null)
                q = q.Where(k => k.Riik.Contains(otsing.Riik));

            if(otsing.Skype != null)
                q = q.Where(k => k.Skype.Contains(otsing.Skype));

            if(otsing.Tanav != null)
                q = q.Where(k => k.Tanav.Contains(otsing.Tanav));

            if(otsing.Twitter != null)
                q = q.Where(k => k.Twitter.Contains(otsing.Twitter));

            if(otsing.WindowsLiveMessenger != null)
                q = q.Where(k => k.WindowsLiveMessenger.Contains(otsing.WindowsLiveMessenger));

            if(otsing.EmailKodu != null)
                q = q.Where(k => k.EmailKodu.Contains(otsing.EmailKodu));

            if(otsing.EmailToo != null)
                q = q.Where(k => k.EmailToo.Contains(otsing.EmailToo));

            if(otsing.TelefonKodu != null)
                q = q.Where(k => k.TelefonKodu.Contains(otsing.TelefonKodu));

            if(otsing.TelefonMob != null)
                q = q.Where(k => k.TelefonMob.Contains(otsing.TelefonMob));

            if(otsing.TelefonToo != null)
                q = q.Where(k => k.TelefonToo.Contains(otsing.TelefonToo));

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