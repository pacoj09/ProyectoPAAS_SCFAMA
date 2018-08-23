using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAAS_SCFAMA
{
    public partial class Resource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                if (!(Session["objUsuario"] as clsUsuario).getTipoUsuario().Equals("Gerente"))
                {
                    Session["UserDeny"] = true;
                    Response.Redirect("Project.aspx");
                }
            }

            cargarRecursos();
        }

        protected void btnNuevoRecurso_Click(object sender, EventArgs e)
        {
            clsRecurso objRecurso = new clsRecurso(0, (Session["objUsuario"] as clsUsuario).getObjEmpresa(), "Recurso Nuevo", "Recurso sin actualizar", 0, false);
            if (objRecurso.crearRecurso())
            {
                cargarRecursos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear el recurso');", true);
            }
        }

        private void cargarRecursos()
        {
            clsRecurso objRecurso = new clsRecurso();
            gvRecursos.DataSource = objRecurso.consultarRecurso(0, (Session["objUsuario"] as clsUsuario).getObjEmpresa().getIdEmpresa());
            gvRecursos.DataBind();
        }

        protected void btnActualizarRecurso_Click(object sender, EventArgs e)
        {
            clsRecurso objRecurso = new clsRecurso(Convert.ToInt32(txtIdRecurso.Text), (Session["objUsuario"] as clsUsuario).getObjEmpresa(), txtTipo.Text, txtDescripcion.Text, Convert.ToInt32(txtStock.Text), false);
            if (objRecurso.actualizarRecurso())
            {
                cargarRecursos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de actualizar el recurso');", true);
            }
        }

        protected void btnInhabilitarRecurso_Click(object sender, EventArgs e)
        {
            clsRecurso objRecurso = new clsRecurso(Convert.ToInt32(txtIdRecurso.Text), true);
            if (objRecurso.inhabilitarRecurso())
            {
                cargarRecursos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear el recurso');", true);
            }
        }

        protected void gvRecursos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtIdRecurso.Text = gvRecursos.Rows[gvRecursos.SelectedIndex].Cells[1].Text;
                txtTipo.Text = gvRecursos.Rows[gvRecursos.SelectedIndex].Cells[3].Text;
                txtDescripcion.Text = gvRecursos.Rows[gvRecursos.SelectedIndex].Cells[4].Text;
                txtStock.Text = gvRecursos.Rows[gvRecursos.SelectedIndex].Cells[5].Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Selection_Error", "alert('Error al seleccionar la fila');", true);
            }
        }

        protected void gvRecursos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[6].Visible = false;
        }
    }
}