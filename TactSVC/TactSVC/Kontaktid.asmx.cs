﻿using System;
using System.Collections;
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
using System.Web.SessionState;
using System.Web.Script.Services;


namespace TactSVC
{
    /// <summary>
    /// Summary description for Kontaktid
    /// </summary>
    ///     
    [WebService(Namespace = "http://kontaktid.sokk.ee/")]
    [ScriptService]
    public class Kontaktid : System.Web.Services.WebService
    {
        Andmebaas.Andmebaas ab = new Andmebaas.Andmebaas("andmebaas.db");


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Staatus LooKasutaja(String eesnimi, String perenimi, String kasutajanimi, String parool, String facebookId = "")
        {

            if (ab.tagastaKasutaja(kasutajanimi) == null)
            {
                ArrayList error = new ArrayList();
                if (eesnimi == "")
                {
                    error.Add("Eesnimi");
                }

                if (perenimi == "")
                {
                    error.Add("Perenimi");
                }

                if (kasutajanimi == "")
                {
                    error.Add("Kasutajanimi");
                }

                if (parool == "")
                {
                    error.Add("Parool");
                }
                string viga = String.Join(", ", error.ToArray());
                if (viga == "")
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
                else {
                    return new Staatus()
                    {
                        Tyyp = "Viga",
                        Sonum = "Palun täitke järgmised väljad: \n" + viga
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
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Staatus LogiSisse(String kasutajanimi, String parool, String voti)
        {

            parool = ComputeHash(parool);
            Kasutaja k = ab.tagastaKasutaja(kasutajanimi);
            string domeen = API.votaDomeen(voti, ab);

            if (domeen == null)
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Vigane võti!"
                };
            }

            if (domeen == HttpContext.Current.Request.ServerVariables["HTTP_HOST"].Split(':')[0])
            {
                if (k != null)
                {
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
            else
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Päring tuli valelt domeenilt!"
                };
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Staatus LogiValja()
        {

            Session.Clear();
            return new Staatus() { 
                Tyyp = "OK",
                Sonum = "Välja logitud!"
            };
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
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
            k = ab.tagastaKasutaja(k.Kasutajanimi);

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
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
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

            if (eesnimi == "")
            {
                return new Staatus()
                {
                    Tyyp = "Viga",
                    Sonum = "Eesnimi on nõutud!"
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
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Staatus MuudaKontakt(int kontakt_id, String eesnimi, String perenimi, String telefonKodu, String telefonToo,
            String telefonMob, String emailKodu, String emailToo, String riik, String maakond, String asula, String tanav,
            String maja_nr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {

            if(Session["kasutaja"] == null) {
                return new Staatus() {
                    Tyyp = "Viga",
                    Sonum = "Autentimata"
                };
            }
            Kasutaja k = (Kasutaja)Session["kasutaja"];

            Kontakt kontakt = k.otsiKontaktid(new Kontakt() {
                Id = kontakt_id
            }, ab)[0];

            kontakt.KasutajaId = k.Id;
            if(eesnimi != "") kontakt.Eesnimi = eesnimi;
            if(perenimi != "") kontakt.Perenimi = perenimi;
            if(telefonKodu != "") kontakt.TelefonKodu = telefonKodu;
            if(telefonToo != "") kontakt.TelefonToo = telefonToo;
            if(telefonMob != "") kontakt.TelefonMob = telefonMob;
            if(emailKodu != "") kontakt.EmailKodu = emailKodu;
            if(emailToo != "") kontakt.EmailToo = emailToo;
            if(riik != "") kontakt.Riik = riik;
            if(maakond != "") kontakt.Maakond = maakond;
            if(asula != "") kontakt.Asula = asula;
            if(tanav != "") kontakt.Tanav = tanav;
            if(maja_nr != "") kontakt.MajaNr = maja_nr;
            if(wlm != "") kontakt.WindowsLiveMessenger = wlm;
            if(facebook != "") kontakt.Facebook = facebook;
            if(orkut != "") kontakt.Orkut = orkut;
            if(skype != "") kontakt.Skype = skype;
            if(twitter != "") kontakt.Twitter = twitter;
            if(pilt != "") kontakt.Pilt = pilt;
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
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Kontakt[] KuvaKontakt(String kontakt_id, String eesnimi, String perenimi, String telefonKodu, String telefonToo,
            String telefonMob, String emailKodu, String emailToo, String riik, String maakond, String asula, String tanav,
            String maja_nr, String wlm, String facebook, String orkut, String skype, String twitter, String pilt)
        {

            int id = 0;
            Int32.TryParse(kontakt_id, out id);

            if (Session["kasutaja"] == null)
            {
                return null;
            }

            Kasutaja k = (Kasutaja)Session["kasutaja"];

            Kontakt kontakt = new Kontakt();
            kontakt.Id = id;
            if (eesnimi != "") kontakt.Eesnimi = eesnimi;
            if(eesnimi != "") kontakt.Eesnimi = eesnimi;
            if(perenimi != "") kontakt.Perenimi = perenimi;
            if(telefonKodu != "") kontakt.TelefonKodu = telefonKodu;
            if(telefonToo != "") kontakt.TelefonToo = telefonToo;
            if(telefonMob != "") kontakt.TelefonMob = telefonMob;
            if(emailKodu != "") kontakt.EmailKodu = emailKodu;
            if(emailToo != "") kontakt.EmailToo = emailToo;
            if(riik != "") kontakt.Riik = riik;
            if(maakond != "") kontakt.Maakond = maakond;
            if(asula != "") kontakt.Asula = asula;
            if(tanav != "") kontakt.Tanav = tanav;
            if(maja_nr != "") kontakt.MajaNr = maja_nr;
            if(wlm != "") kontakt.WindowsLiveMessenger = wlm;
            if(facebook != "") kontakt.Facebook = facebook;
            if(orkut != "") kontakt.Orkut = orkut;
            if(skype != "") kontakt.Skype = skype;
            if(twitter != "") kontakt.Twitter = twitter;
            if(pilt != "") kontakt.Pilt = pilt;
            Kontakt[] kontaktid = k.otsiKontaktid(kontakt, ab);

            return kontaktid;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Kontakt[] OtsiKontakt(string param)
        {
            if (Session["kasutaja"] == null)
            {
                return null;
            }

            Kasutaja k = (Kasutaja)Session["kasutaja"];
            return k.otsiKontaktid(param, ab);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Kasutaja KuvaKasutaja()
        {

            if (Session["kasutaja"] == null)
            {
                    return null;
            }

            Kasutaja k = (Kasutaja)Session["kasutaja"];
            k.Parool = "";
            return k;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
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
