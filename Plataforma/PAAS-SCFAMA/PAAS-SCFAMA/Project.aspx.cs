using Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PAAS_SCFAMA
{
    public partial class Project : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["objUsuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (Session["UserDeny"] != null)
            {
                if (Convert.ToBoolean(Session["UserDeny"]))
                {
                    Session["UserDeny"] = false;
                    ScriptManager.RegisterStartupScript(this, GetType(), "User_Deny", "alert('No posee acceso a este recurso');", true);
                }
            }

            cargarProyectos();
        }

        private void limpiarProyecto()
        {
            txtIdProyecto.Text = string.Empty;
            txtCodgioProyecto.Text = string.Empty;
            txtUsuarioCreador.Text = string.Empty;
            txtUsuarioModificador.Text = string.Empty;
            txtDescripcionProyecto.Text = string.Empty;
            txtCostoProyecto.Text = string.Empty;
            txtTiempoTotal.Text = string.Empty;
            txtEstado.Text = string.Empty;
        }

        private void limpiarTarea()
        {
            DataTable dt = new DataTable();
            txtIdTarea.Text = string.Empty;
            txtCodigoTarea.Text = string.Empty;
            txtProyecto.Text = string.Empty;
            txtDescripcionTarea.Text = string.Empty;
            txtTiempo.Text = string.Empty;
            txtCostoTarea.Text = string.Empty;
            gvDetallesTarea.DataSource = dt;
            gvDetallesTarea.DataBind();
        }

        private void cargarRecurso()
        {
            clsRecurso objRecurso = new clsRecurso();
            ddlRecursos.DataSource = objRecurso.consultarRecurso(1, (Session["objUsuario"] as clsUsuario).getObjEmpresa().getIdEmpresa());
            ddlRecursos.DataTextField = "descripcion";
            ddlRecursos.DataValueField = "id";
            ddlRecursos.DataBind();
        }

        private void cargarProyectos()
        {
            clsProyecto objProyecto = new clsProyecto();
            gvProyectos.DataSource = objProyecto.consultaProyectos((Session["objUsuario"] as clsUsuario).getObjEmpresa().getIdEmpresa());
            gvProyectos.DataBind();
        }

        protected void btnNuevoProyecto_Click(object sender, EventArgs e)
        {
            clsProyecto objProyecto = new clsProyecto(0, string.Empty, (Session["objUsuario"] as clsUsuario), (Session["objUsuario"] as clsUsuario), "Proyecto Nuevo", "0", 0, "Empezando", false);
            if (objProyecto.crearProyecto())
            {
                cargarProyectos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear el proyecto');", true);
            }
        }

        protected void gvProyectos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[9].Visible = false;
        }

        protected void gvProyectos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtIdProyecto.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[1].Text;
                txtCodgioProyecto.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[2].Text;
                txtUsuarioCreador.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[3].Text;
                txtUsuarioModificador.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[4].Text;
                txtDescripcionProyecto.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[5].Text;
                txtCostoProyecto.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[6].Text;
                txtTiempoTotal.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[7].Text + "|Dias";
                txtEstado.Text = gvProyectos.Rows[gvProyectos.SelectedIndex].Cells[8].Text;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Selection_Error", "alert('Error al seleccionar la fila');", true);
            }
        }

        protected void btnActualizarProyecto_Click(object sender, EventArgs e)
        {
            string[] Tiempo = txtTiempoTotal.Text.Split('|');
            clsUsuario objUsuario = new clsUsuario(Convert.ToInt32(txtUsuarioCreador.Text));
            clsProyecto objProyecto = new clsProyecto(Convert.ToInt32(txtIdProyecto.Text), txtCodgioProyecto.Text, objUsuario, (Session["objUsuario"] as clsUsuario), txtDescripcionProyecto.Text, txtCostoProyecto.Text, Convert.ToInt32(Tiempo[0]), txtEstado.Text, false);
            if (objProyecto.actualizarProyecto())
            {
                cargarProyectos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear el proyecto');", true);
            }
        }

        protected void btnInhabilitarProyecto_Click(object sender, EventArgs e)
        {
            clsProyecto objProyecto = new clsProyecto(Convert.ToInt32(txtIdProyecto.Text), true);
            if (objProyecto.inhabilitarProyecto())
            {
                limpiarProyecto();
                cargarProyectos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear el proyecto');", true);
            }
        }

        private void cargarTarea()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            clsTarea objTarea = new clsTarea();
            if (objTarea.consultarTareas(Convert.ToInt32(Session["idProyecto"]), (Session["objUsuario"] as clsUsuario).getObjEmpresa().getIdEmpresa()) != null)
            {
                gvTareas.DataSource = objTarea.consultarTareas(Convert.ToInt32(Session["idProyecto"]), (Session["objUsuario"] as clsUsuario).getObjEmpresa().getIdEmpresa());
                gvTareas.DataBind();
                btnNuevaTarea.Enabled = true;
                btnActualizarTarea.Enabled = true;
                btnEliminarTarea.Enabled = true;
                btnAgregarDetalle.Enabled = true;
                cargarRecurso();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Proyecto_Inhabilitado_Error", "alert('Error al cargar las tareas, pueda que el proyecto se encuentre inhabilitado o el id sea incorrecto');", true);
            }
        }

        protected void btnBuscarTareas_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            if (!string.IsNullOrEmpty(txtIdProyecto_Tarea.Text))
            {
                try
                {
                    Session["idProyecto"] = txtIdProyecto_Tarea.Text;
                    cargarTarea();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Tarea_Error", "alert('Error al cargar las tareas');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Proyecto_ID_Error", "alert('Debe de dijitar un ID de proyecto');", true);
            }
        }

        private void cargarDetallesTarea()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            clsDetallesTarea objDetalleTarea = new clsDetallesTarea();
            gvDetallesTarea.DataSource = objDetalleTarea.consultarDetallesTarea(Convert.ToInt32(txtIdTarea.Text));
            gvDetallesTarea.DataBind();
        }

        protected void gvTareas_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            try
            {
                txtIdTarea.Text = gvTareas.Rows[gvTareas.SelectedIndex].Cells[1].Text;
                txtCodigoTarea.Text = gvTareas.Rows[gvTareas.SelectedIndex].Cells[2].Text;
                txtProyecto.Text = gvTareas.Rows[gvTareas.SelectedIndex].Cells[3].Text;
                txtDescripcionTarea.Text = gvTareas.Rows[gvTareas.SelectedIndex].Cells[4].Text;
                txtTiempo.Text = gvTareas.Rows[gvTareas.SelectedIndex].Cells[5].Text + "|Dias";
                txtCostoTarea.Text = gvTareas.Rows[gvTareas.SelectedIndex].Cells[6].Text;
                cargarDetallesTarea();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Selection_Error", "alert('Error al seleccionar la fila');", true);
            }
        }

        protected void btnNuevaTarea_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            clsProyecto objProyecto = new clsProyecto(Convert.ToInt32(Session["idProyecto"]));
            clsTarea objTarea = new clsTarea(0, string.Empty, objProyecto, "Nueva Tarea", 0, "0");
            if (objTarea.crearTarea())
            {
                cargarTarea();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear la tarea');", true);
            }
        }

        protected void btnActualizarTarea_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            string[] Tiempo = txtTiempo.Text.Split('|');
            clsProyecto objProyecto = new clsProyecto(Convert.ToInt32(Session["idProyecto"]));
            clsTarea objTarea = new clsTarea(Convert.ToInt32(txtIdTarea.Text), txtCodigoTarea.Text, objProyecto, txtDescripcionTarea.Text, Convert.ToInt32(Tiempo[0]), txtCostoTarea.Text);
            if (objTarea.actualizarTarea())
            {
                cargarProyectos();
                cargarTarea();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de actualizar la tarea');", true);
            }
        }

        protected void btnEliminarTarea_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            string[] Tiempo = txtTiempo.Text.Split('|');
            clsTarea objTarea = new clsTarea(Convert.ToInt32(txtIdTarea.Text));
            if (objTarea.eliminarTarea(Convert.ToInt32(txtIdProyecto_Tarea.Text), txtCostoTarea.Text, Convert.ToInt32(Tiempo[0])))
            {
                cargarProyectos();
                cargarTarea();
                limpiarTarea();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de eliminar la tarea');", true);
            }
        }

        protected void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            clsRecurso objRecurso = new clsRecurso(Convert.ToInt32(ddlRecursos.SelectedValue));
            if (Convert.ToInt32(objRecurso.obtenerCantidad().Rows[0][0]) >= Convert.ToInt32(txtCantidadRecurso.Text))
            {
                clsTarea objTarea = new clsTarea(Convert.ToInt32(txtIdTarea.Text));
                clsDetallesTarea objDetalleTarea = new clsDetallesTarea(0, objTarea, objRecurso, txtMontoRecurso.Text, Convert.ToInt32(txtCantidadRecurso.Text));
                if (objDetalleTarea.nuevoDetalleTarea(Convert.ToInt32(txtIdProyecto_Tarea.Text), txtCostoTarea.Text))
                {
                    cargarProyectos();
                    cargarTarea();
                    cargarRecurso();
                    ActualizarCampos();
                    limpiarCamporDetalles();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de crear el detalle');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('No ha suficientes recursos, actualizar el stock');", true);
            }
        }

        protected void gvDetallesTarea_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            clsTarea objTarea = new clsTarea(Convert.ToInt32(txtIdTarea.Text));
            clsDetallesTarea objDetalleTarea = new clsDetallesTarea(Convert.ToInt32(gvDetallesTarea.Rows[gvDetallesTarea.SelectedIndex].Cells[1].Text), objTarea);
            if (objDetalleTarea.eliminarDetalle(Convert.ToInt32(txtIdProyecto_Tarea.Text), txtCostoTarea.Text))
            {
                cargarProyectos();
                cargarTarea();
                ActualizarCampos();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Conexion_Error", "alert('Error al tratar de eliminar el detalle');", true);
            }
        }

        private void limpiarCamporDetalles()
        {
            txtMontoRecurso.Text = string.Empty;
            txtCantidadRecurso.Text = string.Empty;
        }

        private void ActualizarCampos()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Stay_Tab_Error", "stayTabWork();", true);
            clsTarea objTarea = new clsTarea(Convert.ToInt32(txtIdTarea.Text));
            DataTable dtTarea = new DataTable();
            dtTarea = objTarea.consultarTarea();
            if (dtTarea != null)
            {
                txtDescripcionTarea.Text = dtTarea.Rows[0][3].ToString();
                txtTiempo.Text = dtTarea.Rows[0][4].ToString() + "|Dias";
                txtCostoTarea.Text = dtTarea.Rows[0][5].ToString();
                cargarDetallesTarea();
            }
            else
            {
                txtIdTarea.Text = string.Empty;
                txtCodigoTarea.Text = string.Empty;
                txtProyecto.Text = string.Empty;
                txtDescripcionTarea.Text = string.Empty;
                txtTiempo.Text = string.Empty;
                txtCostoTarea.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, GetType(), "Selection_Error", "alert('Error al recargar los campos');", true);
            }
        }
    }
}