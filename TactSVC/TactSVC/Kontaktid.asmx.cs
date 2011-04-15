using System;
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
        public string Loo_Konto()
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Logi_Sisse()
        {
            string kasutajanimi, parool;

            return "Tere maailm!";
        }

        [WebMethod]
        public string Logi_Valja()
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Lisa_Kontakt()
        {
            string eesnimi, perenimi;
            return "Tere maailm!";
        }

        [WebMethod]
        public string Muuda_Kontakt()
        {
            string eesnimi, perenimi;
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kuva_Kontakt()
        {
            string eesnimi, perenimi;
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kustuta_Kontakt()
        {
            return "Tere maailm!";
        }

        [WebMethod]
        public string Kontaktiraamat()
        {
            string eesnimi, perenimi;
            return "Tere maailm!";
        }
    }
}
