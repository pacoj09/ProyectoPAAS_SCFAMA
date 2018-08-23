using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAAS_SCFAMA
{
    public partial class PAAS : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblEmpresa.Text = (Session["objUsuario"] as clsUsuario).getObjEmpresa().getNombre();
                lblUsuario.Text = (Session["objUsuario"] as clsUsuario).getNombre() + " | " + (Session["objUsuario"] as clsUsuario).getTipoUsuario();
            }
        }

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            Session["UserDeny"] = null;
            Session["objUsuario"] = null;
            Response.Redirect("Home.aspx");
        }
    }
}