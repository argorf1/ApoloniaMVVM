using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApoloniaWPF.Model
{
    class Usuario: NotifyBase
    {
        #region Atributos
        private string run;
        private string nombre;
        private string apellidoP;
        private string apellidoM;
        private string email;
        private string password;
        private int subunidadId;
        private int rolId;
        private int unidadId;
        private int estado;
        #endregion

        #region Propiedades
        OracleConnection conn = new OracleConnection();
        OracleDataReader r = null;
        public string Run
        {
            get { return run; }
            set { 
                run = value;
                OnPropertyChanged("Run");
            }
        }
        public string Nombre
        {
            get { return nombre; }
            set
            {
                nombre = value;
                OnPropertyChanged("Nombre");
            }
        }
        public string ApellidoP
        {
            get { return apellidoP; }
            set
            {
                apellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }
        public string ApellidoM
        {
            get { return apellidoM; }
            set
            {
                apellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("password");
            }
        }
        public int SubunidadId
        {
            get { return subunidadId; }
            set
            {
                subunidadId = value;
                OnPropertyChanged("SubunidadId");
            }
        }
        public int RolId
        {
            get { return rolId; }
            set
            {
                rolId = value;
                OnPropertyChanged("RolId");
            }
        }
        public int UnidadId
        {
            get { return unidadId; }
            set
            {
                unidadId = value;
                OnPropertyChanged("UnidadId");
            }
        }
        public int Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                OnPropertyChanged("Estado");
            }
        }
        #endregion

        #region CRUD
        public List<Usuario> ReadAll()
        {

            List<Usuario> listaNegocio = new List<Usuario>();

            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_USUARIOS_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    Usuario u = new Usuario();
                    u.Run = r.GetString(0);
                    u.Nombre = r.GetString(1);
                    u.ApellidoP = r.GetString(2);
                    u.ApellidoM = r.GetString(3);
                    u.Email = r.GetString(4);
                    listaNegocio.Add(u);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                throw;
            }


            return listaNegocio;
        }

        public bool Read_Login()
        {
            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand("select * from USUARIOs where RUN = :username AND PASSWORD = :pass AND ESTADO = 1", conn);
                cmd.Parameters.Add(":username", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add(":pass", OracleDbType.NVarchar2).Value = this.Password;

                OracleDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    this.nombre = r.GetString(1);
                    this.apellidoP = r.GetString(2);
                    this.apellidoM = r.GetString(3);
                    this.email = r.GetString(4);
                    this.password = String.Empty;
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                throw;
            }

        }
        #endregion
    }
}
