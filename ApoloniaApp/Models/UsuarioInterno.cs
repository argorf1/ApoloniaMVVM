using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class UsuarioInterno : EntityModelBase
    {
        #region Atributos
        public string Run { get; set; }
        public string Nombres { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PerfilDet { get; set; }
        public string EstadoDet { get; set; }
        public int IdPerfil { get; set; }
        public int IdEstado { get; set; }

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
                cmd.Parameters.Add("NOMBRES1", OracleDbType.NVarchar2).Value = this.Nombres;
                cmd.Parameters.Add("APELLIDO1", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("APELLIDO2", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("EMAIL1", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("PASSWORD1", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("ROL1", OracleDbType.Int32).Value = this.IdPerfil;
                cmd.Parameters.Add("ESTADO1", OracleDbType.Int32).Value = this.IdEstado;

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
        public List<UsuarioInterno> ReadAll()
        {

            List<UsuarioInterno> listaNegocio = new List<UsuarioInterno>();

            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_usuarios_inter_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    UsuarioInterno u = new UsuarioInterno()
                    {
                        Run = r.GetString(0),
                        Nombres = r.GetString(1),
                        ApellidoP = r.GetString(2),
                        ApellidoM = r.GetString(3),
                        Email = r.GetString(4),
                        EstadoDet = r.GetString(5),
                        PerfilDet = r.GetString(6),
                        IdPerfil = r.GetInt32(7),
                        IdEstado = r.GetInt32(8)
                    };
                    
                    listaNegocio.Add(u);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                return listaNegocio;
            }


            return listaNegocio;
        }

        public bool Read_Login()
        {
            conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand("select nombres, apellidoP, apellidoM, Email, ID_PERFIL from usuario_internos where RUN = :username AND PASSWORD = :pass AND ID_ESTADO = 1", conn);
                cmd.Parameters.Add(":username", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add(":pass", OracleDbType.NVarchar2).Value = this.Password;

                r = cmd.ExecuteReader();

                if (r.Read())
                {
                    this.Nombres = r.GetString(0);
                    this.ApellidoP = r.GetString(1);
                    this.ApellidoM = r.GetString(2);
                    this.Email = r.GetString(3);
                    this.IdPerfil = r.GetInt32(4);
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
                return false;
            }

        }
        public bool ReadByRun()
        {
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand("select * from usuario_internos where RUN = :username", conn);
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
