using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TactSVC
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["Logitud"] != null) && (Convert.ToBoolean(Session["Logitud"]) == true))
            {
                // If User is Authenticated then moved to a main page
                if (User.Identity.IsAuthenticated)
                {
                    Response.Redirect("test.aspx");
                    //TextBox3.Text = "autenditud";
                }
                else
                {
                    TextBox3.Text = "pole autenditud";
                }
            }
        }

        protected void Logi_sisse(object sender, EventArgs e)
        {
            //kasutaja autentimine
            Session["Logitud"] = true;
            Session["Kasutaja"] = "kasutaja_objekt_siia";
            Response.Redirect("test.aspx");
        }
    }
}