using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AutoParts
{
    public partial class admin_master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["isLogged"] == null || (String)Session["perfil"] != "admin")
            {
                Response.Redirect("login.aspx");
            }
        }
    }
}