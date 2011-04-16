using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace TactSVC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Logitud"] != null) && (Convert.ToBoolean(Session["Logitud"]) == true))
            {
                Response.Redirect("test.aspx");
            }
        }

        protected void Logi_sisse(object sender, EventArgs e)
        {
            //kasutaja autentimine
            String kasutajanimi = kasutajanimi_input.Text;
            String parool = ComputeHash(parool_input.Text);
            
            Session["Kasutaja"] = "kasutaja_objekt_siia";
            
        }

        public string ComputeHash(String input)
        {
            SHA256Managed sha256 = new SHA256Managed();
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = sha256.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }

    }
}