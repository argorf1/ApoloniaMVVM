using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class UsuarioInternoModel : EntityModelBase
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
        public string NombreCompleto { get; set; }
        #endregion
        private OracleConnection conn = new OracleConnection();
        private OracleDataReader r = null;
        #region CRUD

        public bool Create()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_usuario_interno", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("i_nombres", OracleDbType.NVarchar2).Value = this.Nombres;
                cmd.Parameters.Add("i_apellidop", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("i_apellidom", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("i_email", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("i_password", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("i_id_perfil", OracleDbType.Int32).Value = this.IdPerfil;
                cmd.Parameters.Add("i_estado", OracleDbType.Int32).Value = this.IdEstado;

                cmd.ExecuteNonQuery();

                conn.Close();
                
                 return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }
        #region Read
        public List<UsuarioInternoModel> ReadAll()
        {

            List<UsuarioInternoModel> listaNegocio = new List<UsuarioInternoModel>();

            try
            {
                conn = new Conexion().AbrirConexion();

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
                    UsuarioInternoModel u = new UsuarioInternoModel()
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
        public List<UsuarioInternoModel> ReadByDesignerPerfil()
        {

            List<UsuarioInternoModel> listaNegocio = new List<UsuarioInternoModel>();

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_desginers_act";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    UsuarioInternoModel u = new UsuarioInternoModel()
                    {
                        Run = r.GetString(0),
                        NombreCompleto = r.GetString(1)          
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

        public List<UnidadModel> ReadByDesigner()
        {

            List<UnidadModel> listaNegocio = new List<UnidadModel>();

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_unidades_by_designer";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run_designer", OracleDbType.NVarchar2).Value = this.Run;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    UnidadModel u = new UnidadModel()
                    {
                        Rut = r.GetString(0),
                        RazonSocial = r.GetString(1),
                        Rubro = r.GetString(2),
                        Calle = r.GetString(3),
                        Numero = r.GetString(4),
                        Complemento = r.GetString(5),
                        Region = r.GetString(6),
                        Provincia = r.GetString(7),
                        Comuna = r.GetString(8),
                        PersonaContacto = r.GetString(9),
                        TelefonoContacto = r.GetInt64(10),
                        EmailContacto = r.GetString(11),
                        ResponsableRun = r.GetString(12),
                        ResponsableNombre = r.GetString(13),
                        Estado = r.GetString(14),
                        EstadoId = r.GetInt32(15),
                        DireccionId = r.GetInt32(16)
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
                conn = new Conexion().AbrirConexion();
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
                conn = new Conexion().AbrirConexion();
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
        public bool Update()
        {
            OracleConnection conn = new OracleConnection();

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_usuario_interno", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("i_nombres", OracleDbType.NVarchar2).Value = this.Nombres;
                cmd.Parameters.Add("i_apellidop", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("i_apellidom", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("i_email", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("i_password", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("i_id_perfil", OracleDbType.Int32).Value = this.IdPerfil;
                cmd.Parameters.Add("i_estado", OracleDbType.Int32).Value = this.IdEstado;

                cmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                conn.Close();
                return false;
            }
        }
        #endregion
    }
}
