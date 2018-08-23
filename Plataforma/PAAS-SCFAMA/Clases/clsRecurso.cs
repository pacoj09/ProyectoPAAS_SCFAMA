using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsRecurso
    {
        private int id { get; set; }
        private clsEmpresa objEmpresa { get; set; }
        private string tipo { get; set; }
        private string descripcion { get; set; }
        private int stock { get; set; }
        private bool inhabilitado { get; set; }

        public int getIdRecurso()
        {
            return this.id;
        }

        public clsRecurso() { }

        public clsRecurso(int _id)
        {
            this.id = _id;
        }

        public clsRecurso(int _id, bool _inhabilitado)
        {
            this.id = _id;
            this.inhabilitado = _inhabilitado;
        }

        public clsRecurso(int _id, clsEmpresa _objEmpresa, string _tipo, string _descripcion, int _stock, bool _inhabilitado)
        {
            this.id = _id;
            this.objEmpresa = _objEmpresa;
            this.tipo = _tipo;
            this.descripcion = _descripcion;
            this.stock = _stock;
            this.inhabilitado = _inhabilitado;
        }

        public DataTable consultarRecurso(int _stock, int _id_empresa)
        {
            DataTable dtRecursos = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string query = "";
                if (_stock == 0)
                {
                    query = string.Format("select * from recurso where inhabilitado = 0 and id_empresa = {0};", _id_empresa);
                }
                else
                {
                    query = string.Format("select * from recurso where inhabilitado = 0 and stock > 0 and id_empresa = {0};", _id_empresa);
                }
                dtRecursos = objConexion.consultar(query);
                objConexion.cerrarConexion();
            }
            return dtRecursos;
        }

        public DataTable obtenerCantidad()
        {
            DataTable dtCantidadRecursos = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string query = string.Format("select stock from recurso where id = {0};", this.id);
                dtCantidadRecursos = objConexion.consultar(query);
                objConexion.cerrarConexion();
            }
            return dtCantidadRecursos;
        }

        public bool crearRecurso()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("insert into recurso values({0}, '{1}', '{2}', {3}, {4});", this.objEmpresa.getIdEmpresa(), this.tipo, this.descripcion, this.stock, Convert.ToInt32(this.inhabilitado));
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool actualizarRecurso()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("update recurso set tipo = '{0}', descripcion = '{1}', stock = {2} where id = {3};", this.tipo, this.descripcion, this.stock, this.id);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool inhabilitarRecurso()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("update recurso set inhabilitado = {0} where id = {1};", Convert.ToInt32(this.inhabilitado), this.id);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }
    }
}
