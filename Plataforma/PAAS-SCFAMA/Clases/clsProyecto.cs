using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsProyecto
    {
        private int id { get; set; }
        private string codigo { get; set; }
        private clsUsuario objUsuarioCreador { get; set; }
        private clsUsuario objUsuarioModificador { get; set; }
        private string descripcion { get; set; }
        private string costo_total { get; set; }
        private int tiempo_total { get; set; }
        private string estado { get; set; }
        private bool inhabilitado { get; set; }

        public int getId_Proyecto()
        {
            return this.id;
        }

        public clsProyecto() { }

        public clsProyecto(int _id)
        {
            this.id = _id;
        }

        public clsProyecto(int _id, bool _inhabilitado)
        {
            this.id = _id;
            this.inhabilitado = _inhabilitado;
        }

        public clsProyecto(int _id, string _codigo, clsUsuario _objUsuarioCreador, clsUsuario _objUsuarioModificador, string _descripcion, string _costo_total, int _tiempo_total, string _estado, bool _inhabilitado)
        {
            this.id = _id;
            this.codigo = _codigo;
            this.objUsuarioCreador = _objUsuarioCreador;
            this.objUsuarioModificador = _objUsuarioModificador;
            this.descripcion = _descripcion;
            this.costo_total = _costo_total;
            this.tiempo_total = _tiempo_total;
            this.estado = _estado;
            this.inhabilitado = _inhabilitado;
        }

        public bool crearProyecto()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("insert into proyecto values(null, {0}, {1}, '{2}', '{3}', {4}, '{5}', {6});", this.objUsuarioCreador.getId(), this.objUsuarioModificador.getId(), this.descripcion, this.costo_total, this.tiempo_total, this.estado, Convert.ToInt32(this.inhabilitado));
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool actualizarProyecto()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("update proyecto set id_ultimo_usuario_modificar = {0}, descripcion = '{1}', costo_total = '{2}', tiempo_total = {3}, estado = '{4}' where id = {5};", this.objUsuarioModificador.getId(), this.descripcion, this.costo_total, this.tiempo_total, this.estado, this.id);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public bool inhabilitarProyecto()
        {
            bool exito = false;
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string statement = string.Format("update proyecto set inhabilitado = {0} where id = {1};", Convert.ToInt32(this.inhabilitado), this.id);
                if (objConexion.gestion(statement) == 1)
                {
                    exito = true;
                }
                objConexion.cerrarConexion();
            }
            return exito;
        }

        public DataTable consultaProyectos(int _id_empresa)
        {
            DataTable dtProyectos = new DataTable();
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                string query = string.Format("select p.* from proyecto p inner join usuario u on p.id_usuario = u.id where p.inhabilitado = 0 and u.id_empresa = {0};", _id_empresa);
                dtProyectos = objConexion.consultar(query);
                objConexion.cerrarConexion();
            }
            return dtProyectos;
        }

        ~clsProyecto()
        {
            this.id = 0;
            this.codigo = string.Empty;
            this.objUsuarioCreador = null;
            this.objUsuarioModificador = null;
            this.descripcion = string.Empty;
            this.costo_total = string.Empty;
            this.tiempo_total = 0;
            this.estado = string.Empty;
        }
    }
}
