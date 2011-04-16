using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TactSVC.Andmebaas;
using System.Xml;
using System.Xml.Linq;

namespace TactSVC
{
    /// <summary>
    /// Summary description for Kontaktid
    /// </summary>
    [WebService(Namespace = "http://kontaktid.sokk.ee/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Kontaktid : System.Web.Services.WebService
    {
        Andmebaas.Andmebaas ab = new Andmebaas.Andmebaas("andmebaas.db");

        [WebMethod]
        public Staatus Loo_Kasutaja(String eesnimi, String perenimi, String kasutajanimi, String parool, String facebookId = "")
        {
            if (ab.tagastaKasutaja(kasutajanimi)==null)
            {
                var result = ab.Insert(new Kasutaja()
                {
                    Kasutajanimi = kasutajanimi,
                    Parool = parool,
                    Eesnimi = eesnimi,
                    Perenimi = perenimi,
                    FacebookId = facebookId
                });

                if (IsNumeric(result))
                {
                    return new Staatus()
                    {
                        Tyyp = "OK",
                        Sonum = "Kasutaja lisatud!"
                    };
                }
                else
                {
                    return new Staatus()
                    {
                        Tyyp = "Viga",
                        Sonum = "Süsteemi viga!"
                    };
                }
            }
            else
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Kasutajanimi on juba kasutusel!"
                };
            }
        }

        public static bool IsNumeric(object value)
        {
            try
            {
                int i = Convert.ToInt32(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
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
