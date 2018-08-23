using Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAAS_SCFAMA
{
    public partial class User : System.Web.UI.Page
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

            cargarUsuarios();
        }

        private void cargarUsuarios()
        {
            clsUsuario objUsuario = new clsUsuario();
            gvUsuarios.DataSource = objUsuario.consutarUsuarios((Session["objUsuario"] as clsUsuario).getObjEmpresa().getIdEmpresa());
            gvUsuarios.DataBind();
        }

        protected void btnGuardarUsuario_Click(object sender, EventArgs e)
        {
            if (txtIdUsuario.Text.Equals(""))
            {
                if (txtClave.Text.Equals(txtConfirmarClave.Text))
                {
                    clsUsuario objUsuario = new clsUsuario(0, txtNombre.Text, txtClave.Text, ddlTipoUsuario.Text, (Session["objUsuario"] as clsUsuario).getObjEmpresa(), false);
                    if (objUsuario.nuevoUsuario())
                    {
                        cargarUsuarios();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Usuario_False", "alert('Error al crear el nuevo usuario');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Clave_False", "alert('El campo clave y confirmacion deben de ser iguales');", true);
                }
            }
            else
            {
                if (txtClave.Text.Equals(txtConfirmarClave.Text))
                {
                    clsUsuario objUsuario = new clsUsuario(Convert.ToInt32(txtIdUsuario.Text), txtNombre.Text, txtClave.Text, ddlTipoUsuario.Text, (Session["objUsuario"] as clsUsuario).getObjEmpresa(), false);
                    if (objUsuario.actualizarUsuario())
                    {
                        cargarUsuarios();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Clave_False", "alert('Error al actualizar el usuario');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Clave_False", "alert('El campo clave y confirmacion deben de ser iguales');", true);
                }
            }
        }

        protected void btnInhabilitarUsuario_Click(object sender, EventArgs e)
        {
            clsUsuario objUsuario = new clsUsuario(Convert.ToInt32(txtIdUsuario.Text), true);
            if (objUsuario.inhabilitarUsuario())
            {
                cargarUsuarios();
                Limpiar();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Usuario_False", "alert('Error al inhabilitar el usuario');", true);
            }
        }

        protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtIdUsuario.Text = gvUsuarios.Rows[gvUsuarios.SelectedIndex].Cells[1].Text;
                txtNombre.Text = gvUsuarios.Rows[gvUsuarios.SelectedIndex].Cells[3].Text;
                txtClave.Text = gvUsuarios.Rows[gvUsuarios.SelectedIndex].Cells[4].Text;
                txtConfirmarClave.Text = gvUsuarios.Rows[gvUsuarios.SelectedIndex].Cells[4].Text;
                ddlTipoUsuario.SelectedValue = gvUsuarios.Rows[gvUsuarios.SelectedIndex].Cells[5].Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Selection_Error", "alert('Error al seleccionar la fila');", true);
            }
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[4].Visible = false;
        }

        private void Limpiar()
        {
            txtIdUsuario.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtClave.Text = string.Empty;
            txtConfirmarClave.Text = string.Empty;
        }
    }
}