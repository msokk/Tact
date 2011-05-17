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
            string query = "SELECT * FROM Kontakt WHERE ";

            if (otsing.Id != 0)
            {
                query += "Id == " + otsing.Id;
                return c.Query<Kontakt>(query, string.Empty).ToArray();
            }

            if (otsing.Eesnimi != null)
                query += "Eesnimi LIKE '%" + otsing.Eesnimi + "%' AND ";

            if(otsing.Perenimi != null)
                query += "Perenimi LIKE '%" + otsing.Perenimi + "%' AND ";

            if(otsing.Asula != null)
                query += "Asula LIKE '%" + otsing.Asula + "%' AND ";

            if(otsing.Facebook != null)
                query += "Facebook LIKE '%" + otsing.Facebook + "%' AND ";

            if(otsing.Maakond != null)
                query += "Maakond LIKE '%" + otsing.Maakond + "%' AND ";

            if(otsing.MajaNr != null)
                query += "MajaNr LIKE '%" + otsing.MajaNr + "%' AND ";

            if(otsing.Orkut != null)
                query += "Orkut LIKE '%" + otsing.Orkut + "%' AND ";

            if(otsing.Riik != null)
                query += "Riik LIKE '%" + otsing.Riik + "%' AND ";

            if(otsing.Skype != null)
                query += "Skype LIKE '%" + otsing.Skype + "%' AND ";

            if(otsing.Tanav != null)
                query += "Tanav LIKE '%" + otsing.Tanav + "%' AND ";

            if(otsing.Twitter != null)
                query += "Twitter LIKE '%" + otsing.Twitter + "%' AND ";

            if(otsing.WindowsLiveMessenger != null)
                query += "WindowsLiveMessenger LIKE '%" + otsing.WindowsLiveMessenger + "%' AND ";

            if(otsing.EmailKodu != null)
                query += "EmailKodu LIKE '%" + otsing.EmailKodu + "%' AND ";

            if(otsing.EmailToo != null)
                query += "EmailToo LIKE '%" + otsing.EmailToo + "%' AND ";

            if(otsing.TelefonKodu != null)
                query += "TelefonKodu LIKE '%" + otsing.TelefonKodu + "%' AND ";

            if(otsing.TelefonMob != null)
                query += "TelefonMob LIKE '%" + otsing.TelefonMob + "%' AND ";

            if(otsing.TelefonToo != null)
                query += "TelefonToo LIKE '%" + otsing.TelefonToo + "%' AND ";

            if (query.Substring(query.Length - 5) == " AND ")
            {
                query = query.Substring(0, query.Length - 5);
            }
            else if (query.Substring(query.Length - 7) == " WHERE ")
            {
                query = query.Substring(0, query.Length - 7);
            }

            return c.Query<Kontakt>(query, string.Empty).ToArray();
        }

       public Kontakt[] otsiKontaktid(string param, SQLiteConnection c)
       {
           string query = "SELECT * FROM Kontakt WHERE ";
            query += "Eesnimi LIKE '%" + param + "%' OR ";
            query += "Perenimi LIKE '%" + param + "%' OR ";
            query += "Asula LIKE '%" + param + "%' OR ";
            query += "Facebook LIKE '%" + param + "%' OR ";
            query += "Maakond LIKE '%" + param + "%' OR ";
            query += "MajaNr LIKE '%" + param + "%' OR ";
            query += "Orkut LIKE '%" + param + "%' OR ";
            query += "Riik LIKE '%" + param + "%' OR ";
            query += "Skype LIKE '%" + param + "%' OR ";
            query += "Tanav LIKE '%" + param + "%' OR ";
            query += "Twitter LIKE '%" + param + "%' OR ";
            query += "WindowsLiveMessenger LIKE '%" + param + "%' OR ";
            query += "EmailKodu LIKE '%" + param + "%' OR ";
            query += "EmailToo LIKE '%" + param + "%' OR ";
            query += "TelefonKodu LIKE '%" + param + "%' OR ";
            query += "TelefonMob LIKE '%" + param + "%' OR ";
            query += "TelefonToo LIKE '%" + param + "%'";

           return c.Query<Kontakt>(query, string.Empty).ToArray();
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