using ApoloniaApp.Services;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class UsuarioInternoModel : UsuarioModelBase
    {
        #region Atributos
        public PerfilModel Perfil { get; set; }
        #endregion
        private OracleConnection conn = new OracleConnection();
        private OracleDataReader r = null;

        public UsuarioInternoModel()
        {
            Run = "";
            Nombre = "";
            ApellidoP = "";
            ApellidoM = "";
            Nombre = "";

            NombreEntidad = "Usuario Interno";
            Mensaje = "";

            Perfil = new PerfilModel();
            Estado = new EstadoModel();
        }
        
        #region CRUD
        public bool Create()
        {
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_usuario_interno", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("i_nombres", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_apellidop", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("i_apellidom", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("i_email", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("i_password", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("i_id_perfil", OracleDbType.Int32).Value = this.Perfil.Id;
                cmd.ExecuteNonQuery();

                conn.Close();

                MailingService.NewUser(this.Email, this.NombreCompleto, this.Run, this.Password);

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
                conn = Conexion.AbrirConexion();

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
                        Nombre = r.GetString(1),
                        ApellidoP = r.GetString(2),
                        ApellidoM = r.GetString(3),
                        Email = r.GetString(4)
                    };
                    u.Perfil = new PerfilModel() { Id = r.GetInt32(7), Nombre = r.GetString(6) };
                    u.Estado = new EstadoModel() { Id = r.GetInt32(8), Nombre = r.GetString(5) };
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
                conn = Conexion.AbrirConexion();

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
                         Run = r.GetString(0)
                        ,Nombre = r.GetString(1)          
                        ,ApellidoP = r.GetString(2)          
                        ,ApellidoM = r.GetString(3)          
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
                conn = Conexion.AbrirConexion();

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
                        PersonaContacto = r.GetString(9),
                        TelefonoContacto = r.GetString(10),
                        EmailContacto = r.GetString(11),
                    };
                    u.Direccion = new DireccionModel() { Id = r.GetInt32(16), Calle = r.GetString(3), Numero = r.GetString(4), Complemento = r.GetString(5) };
                    u.Direccion.Region = new RegionModel() { Nombre = r.GetString(6) };
                    u.Direccion.Provincia = new ProvinciaModel() { Nombre = r.GetString(7) };
                    u.Direccion.Comuna = new ComunaModel() { Nombre = r.GetString(8) };
                    u.Rubro = new RubroModel() { Id = r.GetInt32(17), Nombre = r.GetString(2) };
                    u.Estado = new EstadoModel() { Id = r.GetInt32(15), Nombre = r.GetString(14) };
                    u.Responsable = new UsuarioInternoModel() { Run = r.GetString(12), Nombre = r.GetString(13) };
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
                conn = Conexion.AbrirConexion();
                OracleCommand cmd = new OracleCommand("select nombres, apellidoP, apellidoM, Email, ID_PERFIL from usuario_internos where RUN = :username AND PASSWORD = :pass AND ID_ESTADO = 1", conn);
                cmd.Parameters.Add(":username", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add(":pass", OracleDbType.NVarchar2).Value = this.Password;

                r = cmd.ExecuteReader();

                if (r.Read())
                {
                    this.Nombre = r.GetString(0);
                    this.ApellidoP = r.GetString(1);
                    this.ApellidoM = r.GetString(2);
                    this.Email = r.GetString(3);
                    this.Perfil.Id = r.GetInt32(4);
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
                conn = Conexion.AbrirConexion();
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
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_usuario_interno", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("i_nombres", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_apellidop", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("i_apellidom", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("i_email", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("i_password", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("i_id_perfil", OracleDbType.Int32).Value = this.Perfil.Id;
                cmd.Parameters.Add("i_estado", OracleDbType.Int32).Value = this.Estado.Id;

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
