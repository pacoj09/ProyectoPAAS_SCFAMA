using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConexionBD
{
    public class clsConexion
    {
        #region Atributos

        private SqlConnection CNX = null;
        private SqlTransaction Transaction = null;

        #endregion

        #region Constructor

        public clsConexion()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["CNX"].ToString();
            CNX = new SqlConnection(ConnectionString);

        }

        #endregion

        #region Funciones

        public bool abrirConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Open();
            }
            catch (Exception)
            {
                Exito = false;
            }
            return Exito;
        }

        public bool cerrarConexion()
        {
            bool Exito = true;
            try
            {
                CNX.Close();
            }
            catch (Exception)
            {
                Exito = false;
            }
            return Exito;
        }

        public DataTable consultar(string query)
        {
            DataTable dtResultado = new DataTable();
            try
            {
                SqlCommand Comando = new SqlCommand(query, CNX);
                Comando.CommandType = CommandType.Text;

                SqlDataAdapter Adapter = new SqlDataAdapter(Comando);
                Adapter.Fill(dtResultado);
            }
            catch
            {
                dtResultado = null;
            }
            return dtResultado;
        }

        public int gestion(string statement)
        {
            int RegistroAfectado = 0;
            SqlCommand Comando = new SqlCommand(statement, CNX);
            try
            {
                RegistroAfectado = Comando.ExecuteNonQuery();
            }
            catch (Exception)
            {
                RegistroAfectado = 0;
            }
            return RegistroAfectado;
        }

        public bool beginTransaction()
        {
            bool bandera = false;

            try
            {
                this.Transaction = this.CNX.BeginTransaction();
                bandera = true;
            }
            catch (Exception)
            {
                bandera = false;
            }

            return bandera;
        }

        public int gestionTransaction(string statement)
        {
            int RegistrosAfectados = 0;
            SqlCommand Gestion = new SqlCommand(statement, CNX, Transaction);
            try
            {
                RegistrosAfectados = Gestion.ExecuteNonQuery();
            }
            catch (Exception)
            {

                RegistrosAfectados = 0;
            }
            return RegistrosAfectados;
        }

        public DataTable consultaTransaction(string query)
        {
            DataTable RegistrosAfectados = new DataTable();
            SqlCommand Gestion = new SqlCommand(query, CNX, Transaction);
            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(Gestion);
                adapter.Fill(RegistrosAfectados);
            }
            catch (Exception)
            {

                RegistrosAfectados = null;
            }
            return RegistrosAfectados;
        }

        public bool rollback()
        {
            bool bandera = false;
            try
            {
                this.Transaction.Rollback();
                bandera = true;
            }
            catch (Exception)
            {
                bandera = false;
            }

            return bandera;
        }

        public bool commit()
        {
            bool bandera = false;
            try
            {
                this.Transaction.Commit();
                bandera = true;
            }
            catch (Exception)
            {
                bandera = false;
            }

            return bandera;
        }

        #endregion

        #region Destructor

        ~clsConexion()
        {
            CNX = null;
            Transaction = null;
        }

        #endregion
    }
}
