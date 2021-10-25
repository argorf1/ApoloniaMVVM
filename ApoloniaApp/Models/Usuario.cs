using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class Usuario : EntityModelBase
    {
        #region Atributos
        public string Run { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int SubunidadId { get; set; }
        public int RolId { get; set; }
        public string UnidadId { get; set; }
        public int Estado { get; set; }
        public string Username { get; set; }

        #endregion
        private OracleConnection conn = new OracleConnection();
        private OracleDataReader r = null;
        #region CRUD

        public bool Create()
        {
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand("C_USUARIOS", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("RUN1", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("NOMBRES1", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("APELLIDO1", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("APELLIDO2", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("EMAIL1", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("PASSWORD1", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("SUBUNIDAD1", OracleDbType.Int32).Value = this.SubunidadId;
                cmd.Parameters.Add("ROL1", OracleDbType.Int32).Value = this.RolId;
                cmd.Parameters.Add("UNIDAD1", OracleDbType.NVarchar2).Value = this.UnidadId;
                cmd.Parameters.Add("ESTADO1", OracleDbType.Int32).Value = this.Estado;
                cmd.Parameters.Add("USERNAME1", OracleDbType.NVarchar2).Value = this.Username;


                int a = cmd.ExecuteNonQuery();
                conn.Close();
                if (a > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }
        #region Read
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
            conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand("select nombres, apellidoP, apellidoM, Email, Rol_Id from USUARIOs where RUN = :username AND PASSWORD = :pass AND ESTADO = 1", conn);
                cmd.Parameters.Add(":username", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add(":pass", OracleDbType.NVarchar2).Value = this.Password;

                r = cmd.ExecuteReader();

                if (r.Read())
                {
                    this.Nombre = r.GetString(0);
                    this.ApellidoP = r.GetString(1);
                    this.ApellidoM = r.GetString(2);
                    this.Email = r.GetString(3);
                    this.RolId = r.GetInt32(4);
                    this.Password = String.Empty;
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
        public bool ReadByRun()
        {
            conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand("select * from USUARIOs where RUN = :username", conn);
                cmd.Parameters.Add(":username", OracleDbType.NVarchar2).Value = this.Run;
                r = cmd.ExecuteReader();

                if (r.Read())
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }

        }
        #endregion
        #endregion
    }
}
