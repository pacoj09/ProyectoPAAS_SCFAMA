using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsDetallesTarea
    {
        public int id { get; set; }
        public clsTarea objTarea { get; set; }
        public clsRecurso objRecurso { get; set; }
        public string monto { get; set; }
        public int cantidad { get; set; }

        public clsDetallesTarea() { }

        public clsDetallesTarea(int _id, clsTarea _objTarea)
        {
            this.id = _id;
            this.objTarea = _objTarea;
        }

        public clsDetallesTarea(int _id, clsTarea _objTarea, clsRecurso _objRecurso, string _monto, int _cantidad)
        {
            this.id = _id;
            this.objTarea = _objTarea;
            this.objRecurso = _objRecurso;
            this.monto = _monto;
            this.cantidad = _cantidad;
        }

        public DataTable consultarDetallesTarea(int _id_tarea)
        {
            DataTable dtDetalles = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string query = string.Format("select * from detalletarea where id_tarea = {0}", _id_tarea);
                dtDetalles = objConexion.consultar(query);
                objConexion.cerrarConexion();
            }
            return dtDetalles;
        }

        public bool nuevoDetalleTarea(int _id_proyecto, string _costo)
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                if (objConexion.beginTransaction())
                {
                    string statement = string.Format("insert into detalletarea values({0}, {1}, '{2}', {3});", this.objTarea.getIdTarea(), this.objRecurso.getIdRecurso(), this.monto, this.cantidad);
                    if (objConexion.gestionTransaction(statement) == 1)
                    {
                        DataTable dtCantidadRecursos = new DataTable();
                        string query = string.Format("select stock from recurso where id = {0};", this.objRecurso.getIdRecurso());
                        dtCantidadRecursos = objConexion.consultaTransaction(query);
                        if (dtCantidadRecursos.Rows.Count == 1)
                        {
                            int stock = (Convert.ToInt32(dtCantidadRecursos.Rows[0][0]) - this.cantidad);
                            if (stock < 0)
                            {
                                stock = 0;
                            }
                            statement = string.Format("update recurso set stock = {0} where id = {1};", stock, this.objRecurso.getIdRecurso());
                            if (objConexion.gestionTransaction(statement) == 1)
                            {
                                string costoActualizado = "" + (Convert.ToDouble(_costo) + Convert.ToDouble(this.monto));
                                statement = string.Format("update tarea set costo = '{0}' where id = {1};", costoActualizado, this.objTarea.getIdTarea());
                                if (objConexion.gestionTransaction(statement) == 1)
                                {
                                    DataTable dtCostoProyecto = new DataTable();
                                    query = string.Format("select costo_total from proyecto where id = {0};", _id_proyecto);
                                    dtCostoProyecto = objConexion.consultaTransaction(query);
                                    if (dtCostoProyecto.Rows.Count == 1)
                                    {
                                        costoActualizado = "" + (Convert.ToDouble(costoActualizado) + Convert.ToDouble(dtCostoProyecto.Rows[0][0]));
                                        statement = string.Format("update proyecto set costo_total = '{0}' where id = {1};", costoActualizado, _id_proyecto);
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

        public bool eliminarDetalle(int _id_proyecto, string _costo)
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                if (objConexion.beginTransaction())
                {
                    DataTable dtMontoDetalle = new DataTable();
                    string query = string.Format("select monto from detalletarea where id = {0};", this.id);
                    dtMontoDetalle = objConexion.consultaTransaction(query);
                    if (dtMontoDetalle.Rows.Count == 1)
                    {
                        string statement = string.Format("delete detalletarea where id = {0};", this.id);
                        if (objConexion.gestionTransaction(statement) == 1)
                        {
                            string costoActualizado = "" + (Convert.ToDouble(_costo) - Convert.ToDouble(dtMontoDetalle.Rows[0][0]));
                            if (Convert.ToDouble(costoActualizado) < 0)
                            {
                                costoActualizado = "0";
                            }
                            statement = string.Format("update tarea set costo = '{0}' where id = {1};", costoActualizado, this.objTarea.getIdTarea());
                            if (objConexion.gestionTransaction(statement) == 1)
                            {
                                DataTable dtCostoProyecto = new DataTable();
                                query = string.Format("select costo_total from proyecto where id = {0};", _id_proyecto);
                                dtCostoProyecto = objConexion.consultaTransaction(query);
                                if (dtCostoProyecto.Rows.Count == 1)
                                {
                                    costoActualizado = "" + (Convert.ToDouble(dtCostoProyecto.Rows[0][0]) - Convert.ToDouble(dtMontoDetalle.Rows[0][0]));
                                    if (Convert.ToDouble(costoActualizado) < 0)
                                    {
                                        costoActualizado = "0";
                                    }
                                    statement = string.Format("update proyecto set costo_total = '{0}' where id = {1};", costoActualizado, _id_proyecto);
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
    }
}
