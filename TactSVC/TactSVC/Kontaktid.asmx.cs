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
    [System.Web.Script.Services.ScriptService]
    public class Kontaktid : System.Web.Services.WebService
    {
        Andmebaas.Andmebaas ab = new Andmebaas.Andmebaas("andmebaas.db");

        [WebMethod]
        public Staatus LooKasutaja(String eesnimi, String perenimi, String kasutajanimi, String parool, String facebookId = "")
        {
            if (ab.tagastaKasutaja(kasutajanimi) == null)
            {
                var result = ab.Insert(new Kasutaja()
                {
                    Kasutajanimi = kasutajanimi,
                    Parool = ComputeHash(parool),
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
        public Staatus LogiSisse(String kasutajanimi, String parool)
        {
            parool = ComputeHash(parool);
            Kasutaja k = ab.tagastaKasutaja(kasutajanimi);
            
            if(k != null) {
                if (k.Parool == parool)
                {

                    Session["kasutaja"] = k;
                    return new Staatus()
                    {
                        Tyyp = "OK",
                        Sonum = "Sisse logitud!"
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
        public Staatus LogiValja()
        {
            Session.Clear();
            return new Staatus() { 
                Tyyp = "OK",
                Sonum = "Välja logitud!"
            };
        }

        [WebMethod(EnableSession = true)]
        public Staatus MuudaKasutaja(String parool, String eesnimi, String perenimi)
        {
            if (Session["kasutaja"] == null)
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Autentimata"
                };
            }

            Kasutaja k = (Kasutaja)Session["Kasutaja"];
            if (parool != "") k.Parool = ComputeHash(parool);
            if (eesnimi != "") k.Eesnimi = eesnimi;
            if (perenimi != "") k.Perenimi = perenimi;

            var rowsAffected = ab.Update(k);

            if (rowsAffected > 0)
            {
                return new Staatus()
                {
                    Tyyp = "OK",
                    Sonum = "Kasutaja muudetud!"
                };
            }
            else
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Muutmine ebaõnnestus!"
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public Staatus LisaKontakt(String eesnimi, String perenimi, String telefonKodu, String telefonToo, String telefonMob,
            String emailKodu, String emailToo, String riik, String maakond, String asula, String tanav,
            String maja_nr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {
            if (Session["kasutaja"] == null)
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Autentimata"
                };
            }

            Kasutaja k = (Kasutaja)Session["Kasutaja"];

            int rowsAffected = k.LisaKontakt(new Kontakt()
            {
                Eesnimi = eesnimi,
                Perenimi = perenimi,
                TelefonKodu = telefonKodu,
                TelefonToo = telefonToo,
                TelefonMob = telefonMob,
                EmailKodu = emailKodu,
                EmailToo = emailToo,
                Riik = riik,
                Maakond = maakond,
                Asula = asula,
                Tanav = tanav,
                MajaNr = maja_nr,
                WindowsLiveMessenger = wlm,
                Facebook = facebook,
                Orkut = orkut,
                Skype = skype,
                Twitter = twitter,
                Pilt = pilt
            }, ab);

            if (rowsAffected > 0)
            {
                return new Staatus()
                {
                    Tyyp = "OK",
                    Sonum = "Kontakt lisatud!"
                };
            }
            else
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Lisamine ebaõnnestus!"
                };
            }
        }

        [WebMethod(EnableSession = true)]
        public Staatus MuudaKontakt(int kontaktId, String eesnimi, String perenimi, String telefonKodu, String telefonToo,
            String telefonMob, String emailKodu, String emailToo, String riik, String maakond, String asula, String tanav,
            String majaNr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {
            if(Session["kasutaja"] == null) {
                return new Staatus() {
                    Tyyp = "Viga",
                    Sonum = "Autentimata"
                };
            }
            Kasutaja k = (Kasutaja)Session["kasutaja"];

            Kontakt kontakt = k.otsiKontaktid(new Kontakt() {
                Id = kontaktId
            }, ab)[0];

            kontakt.KasutajaId = k.Id;
            if(eesnimi != null) kontakt.Eesnimi = eesnimi;
            if(perenimi != null) kontakt.Perenimi = perenimi;
            if(telefonKodu != null) kontakt.TelefonKodu = telefonKodu;
            if(telefonToo != null) kontakt.TelefonToo = telefonToo;
            if(telefonMob != null) kontakt.TelefonMob = telefonMob;
            if(emailKodu != null) kontakt.EmailKodu = emailKodu;
            if(emailToo != null) kontakt.EmailToo = emailToo;
            if(riik != null) kontakt.Riik = riik;
            if(maakond != null) kontakt.Maakond = maakond;
            if(asula != null) kontakt.Asula = asula;
            if(tanav != null) kontakt.Tanav = tanav;
            if(majaNr != null) kontakt.MajaNr = majaNr;
            if(wlm != null) kontakt.WindowsLiveMessenger = wlm;
            if(facebook != null) kontakt.Facebook = facebook;
            if(orkut != null) kontakt.Orkut = orkut;
            if(skype != null) kontakt.Skype = skype;
            if(twitter != null) kontakt.Twitter = twitter;
            if(pilt != null) kontakt.Pilt = pilt;
            int affected = ab.Update(kontakt);
            if (affected > 0)
            {
                return new Staatus()
                {
                    Tyyp = "OK",
                    Sonum = "Kontakt muudetud!"
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

        [WebMethod(EnableSession = true)]
        public Kontakt[] KuvaKontakt(string kontakt_id)
        {
            int id = 0;
            Int32.TryParse(kontakt_id, out id);

            if (Session["kasutaja"] == null)
            {
                return null;
            }

            Kasutaja k = (Kasutaja)Session["kasutaja"];

            Kontakt[] kontaktid = k.otsiKontaktid(new Kontakt {
                    Id = id
            }, ab);
            return kontaktid;
        }

        [WebMethod(EnableSession = true)]
        public Staatus EemaldaKontakt(int kontakt_id)
        {
            if (Session["kasutaja"] == null)
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Autentimata"
                };
            }

            Kasutaja k = (Kasutaja)Session["kasutaja"];

            int rowsAffected = k.EemaldaKontakt(kontakt_id, ab);
            if (rowsAffected > 0)
            {
                return new Staatus()
                {
                    Tyyp = "OK",
                    Sonum = "Kustutamine õnnestus!"
                };
            }
            else
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Kustutamine ebaõnnestus!"
                };
            }
        }

        //Helper
        public string ComputeHash(String input)
        {
            SHA256Managed sha256 = new SHA256Managed();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = sha256.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }

    }
}
