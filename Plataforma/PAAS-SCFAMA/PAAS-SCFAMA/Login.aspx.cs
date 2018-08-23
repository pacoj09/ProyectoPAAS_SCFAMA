using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAAS_SCFAMA
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            if (IniciarSesion())
            {
                Response.Redirect("Project.aspx");
            }
        }


        private bool IniciarSesion()
        {
            bool exito = false;
            WS_SCFAMA.ArrayOfString array_fields = new WS_SCFAMA.ArrayOfString();
            array_fields.Add(txtEmpresa.Text);
            array_fields.Add(txtUser.Text);
            array_fields.Add(txtPassword.Text);
            WS_SCFAMA.WSPAASSCFAMASoapClient ws_Method = new WS_SCFAMA.WSPAASSCFAMASoapClient();
            var Campos = ws_Method.UserAuthentication(array_fields).ToList();


            if (!string.IsNullOrEmpty(Campos.FirstOrDefault()) && Campos.Count == 12)
            {
                if (Convert.ToInt32(Campos.ElementAt(11)) == 0)
                {
                    clsEmpresa objEmpresa = new clsEmpresa(Convert.ToInt32(Campos.ElementAt(0)), Campos.ElementAt(1), Campos.ElementAt(2), Campos.ElementAt(3), Campos.ElementAt(4), Campos.ElementAt(5));
                    clsUsuario objUsuario = new clsUsuario(Convert.ToInt32(Campos.ElementAt(6)), Campos.ElementAt(8), Campos.ElementAt(9), Campos.ElementAt(10), objEmpresa, false);
                    Session["objUsuario"] = objUsuario;
                    exito = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "User_False", "alert('Esta cuenta esta inhabilitada');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_False", "alert('Error de Conexion');", true);
            }
            return exito;
        }
    }
}