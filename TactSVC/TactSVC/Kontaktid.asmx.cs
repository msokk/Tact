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

        [WebMethod(EnableSession = true)]
        public string Logi_Valja()
        {
            //sessioon hävitada
            return "Tere maailm!";
        }

        [WebMethod(EnableSession = true)]
        public Staatus Muuda_Kasutaja(String parool, String eesnimi, String perenimi)
        {
            var result = ab.Update(new Kasutaja()
            {
                Parool = parool,
                Eesnimi = eesnimi,
                Perenimi = perenimi,
            });
            return new Staatus()
            {
                Tyyp = "OK",
                Sonum = "Kasutaja muudetud!"
            };
        }

        [WebMethod(EnableSession = true)]
        public string Lisa_Kontakt(String eesnimi, String perenimi, String[] telefon, String[] email, String Riik, String Maakond, String asula, String tanav,
            String maja_nr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {
            return "Tere maailm!";
        }

        [WebMethod(EnableSession = true)]
        public string Muuda_Kontakt(int kontaktId, String eesnimi, String perenimi, String telefonKodu, String telefonToo, String telefonMob,
            String emailKodu, String emailToo, String riik, String maakond, String asula, String tanav,
            String majaNr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {
            Kontakt kontakt = Kontakt.OtsiId(kontaktId, ab);
            kontakt.Eesnimi = eesnimi;
            kontakt.Perenimi = perenimi;
            kontakt.TelefonKodu = telefonKodu;
            kontakt.TelefonToo = telefonToo;
            kontakt.TelefonMob = telefonMob;
            kontakt.EmailKodu = emailKodu;
            kontakt.EmailToo = emailToo;
            kontakt.Riik = riik;
            kontakt.Maakond = maakond;
            kontakt.Asula = asula;
            kontakt.Tanav = tanav;
            kontakt.MajaNr = majaNr;
            kontakt.WindowsLiveMessenger = wlm;
            kontakt.Facebook = facebook;
            kontakt.Orkut = orkut;
            kontakt.Skype = skype;
            kontakt.Twitter = twitter;
            kontakt.Pilt = pilt;
            ab.Update(kontakt);
            return "test";
        }

        [WebMethod(EnableSession = true)]
        public Kontakt KuvaKontakt(int kontakt_id)
        {
            Kasutaja kasutaja = new Kasutaja();
            Kontakt[] kontaktid = kasutaja.otsiKontaktid(new Kontakt { Id = kontakt_id }, ab);
            return kontaktid[0];
        }

        [WebMethod(EnableSession = true)]
        public Staatus EemaldaKontakt(int kontakt_id)
        {
            Kasutaja kasutaja = new Kasutaja();
            kasutaja.EemaldaKontakt(kontakt_id, ab);
            Staatus staatus = new Staatus();
            staatus.Tyyp = "OK";
            staatus.Sonum = "Kustutamine õnnestus!";
            return staatus;
        }

        [WebMethod(EnableSession = true)]
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
