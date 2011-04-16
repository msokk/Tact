﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;

namespace TactSVC
{
    /// <summary>
    /// Summary description for Contacts
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Contacts : System.Web.Services.WebService
    {

        [WebMethod]
        public string Loo_Kasutaja(String kasutajanimi, String parool)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Logi_Sisse(String kasutajanimi, String parool)
        {
            //sessioon luua mis sisaldaks kasutaja id-d
            return "Tere maailm!";
        }

        [WebMethod]
        public string Logi_Valja()
        {
            //sessioon hävitada
            return "Tere maailm!";
        }

        [WebMethod]
        public string Muuda_Kasutaja(String parool, String eesnimi, String perenimi)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Lisa_Kontakt(String eesnimi, String perenimi, String[] telefon, String[] email, String Riik, String Maakond, String asula, String tanav,
            String maja_nr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Muuda_Kontakt(int kontakt_id, String eesnimi, String[] telefon, String[] email, String perenimi, String Riik, String Maakond, String asula, String tanav,
            String maja_nr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kuva_Kontakt(int kontakt_id)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kustuta_Kontakt(int kontakt_id)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kontaktiraamat()
        {
            // ??
            return "Tere maailm!";
        }

        [WebMethod]
        public string Lisa_Email_Tyyp(String tyyp)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Lisa_Telefon_Tyyp(String tyyp)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Muuda_Email_Tyyp(int email_tyyp_id, String tyyp)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Muuda_Telefon_Tyyp(int telefon_tyyp_id, String tyyp)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kustuta_Email_Tyyp(int email_tyyp_id)
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kustuta_Telefon_Tyyp(int telefon_tyyp_id)
        {
            return "Tere maailm!";
        }


    }
}
