using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Security.Cryptography;
using TactSVC.Andmebaas;
using System.Text;


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
            var result = ab.Insert(new Kasutaja() { 
                Kasutajanimi = kasutajanimi,
                Parool = parool,
                Eesnimi = eesnimi,
                Perenimi = perenimi,
                FacebookId = facebookId
            });

            if (IsNumeric(result))
            {
                return new Staatus() { 
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

        [WebMethod (EnableSession = true)]
        public Staatus Logi_Sisse(String kasutajanimi, String parool)
        {
            parool = ComputeHash(parool);
            Kasutaja k = ab.tagastaKasutaja(kasutajanimi);
            
            if(k != null) {
                if (k.Parool == parool)
                {

                    Session["kasutaja"] = k;
                    //sessioon luua mis sisaldaks kasutaja id-d
                    return new Staatus()
                    {
                        Tyyp = "OK",
                        Sonum = Session["kasutaja"].ToString()
                    };
                }
                else
                {
                    return new Staatus()
                    {
                        Tyyp = "Viga",
                        Sonum = "Vale parool!"
                    };
                }
            } 
            else
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Kasutajat ei eksisteeri!"
                };
            }

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

        public string ComputeHash(String input)
        {
            SHA256Managed sha256 = new SHA256Managed();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = sha256.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

    }
}
