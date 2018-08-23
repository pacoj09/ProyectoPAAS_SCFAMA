using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsTarea
    {
        private int id { get; set; }
        private string codigo { get; set; }
        private clsProyecto objProyecto { get; set; }
        private string descripcion { get; set; }
        private int tiempo { get; set; }
        private string costo { get; set; }

        public int getIdTarea()
        {
            return this.id;
        }

        public clsTarea() { }

        public clsTarea(int _id)
        {
            this.id = _id;
        }

        public clsTarea(int _id, string _codigo, clsProyecto _objProyecto, string _descripcion, int _tiempo, string _costo)
        {
            this.id = _id;
            this.codigo = _codigo;
            this.objProyecto = _objProyecto;
            this.descripcion = _descripcion;
            this.tiempo = _tiempo;
            this.costo = _costo;
        }

        public DataTable consultarTareas(int id_proyecto, int _id_empresa)
        {
            DataTable dtTareas = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                DataTable dtProyecto = new DataTable();
                string query = string.Format("select p.* from proyecto p inner join usuario u on p.id_usuario = u.id where u.id_empresa = {0} and p.id = {1};", _id_empresa, id_proyecto);
                dtProyecto = objConexion.consultar(query);
                if (dtProyecto.Rows.Count == 1)
                {
                    if (!Convert.ToBoolean(dtProyecto.Rows[0][8]))
                    {
                        query = string.Format("select * from tarea where id_proyecto = {0};", id_proyecto);
                        dtTareas = objConexion.consultar(query);
                    }
                    else
                    {
                        dtTareas = null;
                    }
                }
                else
                {
                    dtTareas = null;
                }
                objConexion.cerrarConexion();
            }
            return dtTareas;
        }

        public DataTable consultarTarea()
        {
            DataTable dtTarea = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string query = string.Format("select * from tarea where id = {0};", this.id);
                dtTarea = objConexion.consultar(query);
                if (dtTarea.Rows.Count != 1)
                {
                    dtTarea = null;
                }
                objConexion.cerrarConexion();
            }
            return dtTarea;
        }

        private bool proyectoHabilitado(int _id_proyecto)
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            DataTable dtProyecto = new DataTable();
            if (objConexion.abrirConexion())
            {
                string query = string.Format("select * from proyecto where id = {0};", _id_proyecto);
                dtProyecto = objConexion.consultar(query);
                if (Convert.ToBoolean(dtProyecto.Rows[0][8]))
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool crearTarea()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("insert into tarea values(null, {0}, '{1}', {2}, '{3}');", this.objProyecto.getId_Proyecto(), this.descripcion, this.tiempo, this.costo);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool actualizarTarea()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                if (objConexion.beginTransaction())
                {
                    DataTable dtTiempoTarea = new DataTable();
                    string query = string.Format("select tiempo from tarea where id = {0};", this.id);
                    dtTiempoTarea = objConexion.consultaTransaction(query);
                    if (dtTiempoTarea.Rows.Count == 1)
                    {
                        string statement = string.Format("update tarea set descripcion = '{0}', costo = '{1}', tiempo = {2} where id = {3};", this.descripcion, this.costo, this.tiempo, this.id);
                        if (objConexion.gestionTransaction(statement) == 1)
                        {
                            DataTable dtTiempoProyecto = new DataTable();
                            query = string.Format("select tiempo_total from proyecto where id = {0};", this.objProyecto.getId_Proyecto());
                            dtTiempoProyecto = objConexion.consultaTransaction(query);
                            if (dtTiempoProyecto.Rows.Count == 1)
                            {
                                int TiempoProyecto = Convert.ToInt32(dtTiempoProyecto.Rows[0][0]) - Convert.ToInt32(dtTiempoTarea.Rows[0][0]);
                                if (TiempoProyecto < 0)
                                {
                                    TiempoProyecto = 0;
                                }
                                TiempoProyecto += this.tiempo;
                                statement = string.Format("update proyecto set tiempo_total = {0} where id = {1};", TiempoProyecto, this.objProyecto.getId_Proyecto());
                                if (objConexion.gestionTransaction(statement) == 1)
                                {
                                    objConexion.commit();
                                    exito = true;
                                }
                                else
                                {
                                    objConexion.rollback();
                                }
                            }
                            else
                            {
                                objConexion.rollback();
                            }
                        }
                        else
                        {
                            objConexion.rollback();
                        }
                    }
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool eliminarTarea(int _id_proyecto, string _costo, int _tiempo)
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                if (objConexion.beginTransaction())
                {
                    string statement = string.Format("delete tarea where id = {0};", this.id);
                    if (objConexion.gestionTransaction(statement) == 1)
                    {
                        DataTable dtProyecto = new DataTable();
                        string query = string.Format("select costo_total, tiempo_total from proyecto where id = {0};", _id_proyecto);
                        dtProyecto = objConexion.consultaTransaction(query);
                        if (dtProyecto.Rows.Count == 1)
                        {
                            string costofinal = "" + (Convert.ToDouble(dtProyecto.Rows[0][0]) - Convert.ToDouble(_costo));
                            int tiempofinal = (Convert.ToInt32(dtProyecto.Rows[0][1]) - _tiempo);
                            if (Convert.ToDouble(costofinal) < 0)
                            {
                                costofinal = "0";
                            }
                            if (tiempofinal < 0)
                            {
                                tiempofinal = 0;
                            }
                            statement = string.Format("update proyecto set costo_total = '{0}', tiempo_total = {1} where id = {2};", costofinal, tiempofinal, _id_proyecto);
                            if (objConexion.gestionTransaction(statement) == 1)
                            {
                                objConexion.commit();
                                exito = true;
                            }
                            else
                            {
                                objConexion.rollback();
                            }
                        }
                        else
                        {
                            objConexion.rollback();
                        }
                    }
                    else
                    {
                        objConexion.rollback();
                    }
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }
    }
}
