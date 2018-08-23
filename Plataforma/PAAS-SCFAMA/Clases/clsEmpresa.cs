using ConexionBD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases
{
    public class clsEmpresa
    {
        private int id;
        private string cedula;
        private string nombre;
        private string telefono;
        private string correo;
        private string ubicacion;

        public string getNombre()
        {
            return this.nombre;
        }

        public int getIdEmpresa()
        {
            return this.id;
        }

        public clsEmpresa(int _id, string _cedula, string _nombre, string _telefono, string _correo, string _ubicacion)
        {
            this.id = _id;
            this.cedula = _cedula;
            this.nombre = _nombre;
            this.telefono = _telefono;
            this.correo = _correo;
            this.ubicacion = _ubicacion;
        }

        ~clsEmpresa()
        {
            this.id = 0;
            this.cedula = string.Empty;
            this.nombre = string.Empty;
            this.telefono = string.Empty;
            this.correo = string.Empty;
            this.ubicacion = string.Empty;
        }
    }
}
