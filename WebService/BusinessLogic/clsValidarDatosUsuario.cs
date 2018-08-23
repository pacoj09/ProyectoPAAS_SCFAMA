using AccessDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class clsValidarDatosUsuario
    {
        public string[] validar(string[] _Campos)
        {
            string[] Datos = new string[12];
            clsConexion objConexion = new clsConexion();
            if (objConexion.abrirConexion())
            {
                DataTable dtEmpresa = new DataTable();
                string query = string.Format("select * from empresa where nombre = '{0}';", _Campos[0]);
                dtEmpresa = objConexion.consultar(query);
                if (dtEmpresa != null && dtEmpresa.Rows.Count == 1)
                {
                    DataTable dtUsuario = new DataTable();
                    query = string.Format("select * from usuario where nombre = '{0}' and id_empresa = {1};", _Campos[1], Convert.ToInt32(dtEmpresa.Rows[0][0]));
                    dtUsuario = objConexion.consultar(query);
                    if (dtUsuario != null && dtUsuario.Rows.Count == 1)
                    {
                        if (_Campos[2].Equals(dtUsuario.Rows[0][3].ToString()))
                        {
                            Datos[0] = (dtEmpresa.Rows[0][0].ToString());
                            Datos[1] = (dtEmpresa.Rows[0][1].ToString());
                            Datos[2] = (dtEmpresa.Rows[0][2].ToString());
                            Datos[3] = (dtEmpresa.Rows[0][3].ToString());
                            Datos[4] = (dtEmpresa.Rows[0][4].ToString());
                            Datos[5] = (dtEmpresa.Rows[0][5].ToString());
                            Datos[6] = (dtUsuario.Rows[0][0].ToString());
                            Datos[7] = (dtUsuario.Rows[0][1].ToString());
                            Datos[8] = (dtUsuario.Rows[0][2].ToString());
                            Datos[9] = (dtUsuario.Rows[0][3].ToString());
                            Datos[10] = (dtUsuario.Rows[0][4].ToString());
                            Datos[11] = (Convert.ToInt32(dtUsuario.Rows[0][5])).ToString();
                        }
                    }
                }
                objConexion.cerrarConexion();
            }
            return Datos;
        }
    }
}
