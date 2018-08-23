using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsUsuario
    {
        private int id;
        private string nombre, clave, tipoUsuario;
        private clsEmpresa objEmpresa;
        private bool inhabilitado;

        public clsUsuario() { }

        public clsUsuario(int _id)
        {
            this.id = _id;
        }

        public clsUsuario(int _id, bool _inhabilitado)
        {
            this.id = _id;
            this.inhabilitado = _inhabilitado;
        }

        public clsUsuario(int _id, string _nombre, string _clave, string _tipoUsuario, clsEmpresa _objEmpresa, bool _inhabilitado)
        {
            this.id = _id;
            this.nombre = _nombre;
            this.clave = _clave;
            this.tipoUsuario = _tipoUsuario;
            this.objEmpresa = _objEmpresa;
            this.inhabilitado = _inhabilitado;
        }

        public int getId()
        {
            return this.id;
        }

        public string getNombre()
        {
            return this.nombre;
        }

        public string getTipoUsuario()
        {
            return this.tipoUsuario;
        }

        public clsEmpresa getObjEmpresa()
        {
            return this.objEmpresa;
        }

        public DataTable consutarUsuarios(int _id_empresa)
        {
            DataTable dtUsuarios = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string query = string.Format("select * from usuario where id_empresa = {0};", _id_empresa);
                dtUsuarios = objConexion.consultar(query);
                objConexion.cerrarConexion();
            }
            return dtUsuarios;
        }

        public bool nuevoUsuario()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("insert into usuario values({0}, '{1}', '{2}', '{3}', {4});", this.objEmpresa.getIdEmpresa(), this.nombre, this.clave, this.tipoUsuario, Convert.ToInt32(this.inhabilitado));
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool actualizarUsuario()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("update usuario set nombre = '{0}', clave = '{1}', tipo = '{2}' where id = {3};", this.nombre, this.clave, this.tipoUsuario, this.id);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool inhabilitarUsuario()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("update usuario set inhabilitado = {0} where id = {1};", Convert.ToInt32(this.inhabilitado), this.id);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        ~clsUsuario()
        {
            this.id = 0;
            this.nombre = string.Empty;
            this.clave = string.Empty;
            this.tipoUsuario = string.Empty;
            this.objEmpresa = null;
        }
    }
}
