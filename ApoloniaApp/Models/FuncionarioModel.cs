using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class FuncionarioModel : EntityModelBase
    {
        #region Atributos
        public string Run { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EstadoModel Estado { get; set; }
        public RolModel Rol { get; set; }
        public UnidadModel Unidad { get; set; }
        public SubUnidadModel Subunidad { get; set; }
        public string Username { get; set; }
        #endregion

        private OracleConnection conn = new OracleConnection();
        private OracleDataReader r = null;

        public FuncionarioModel()
        {
            Estado = new EstadoModel();
            Rol = new RolModel();
            Unidad = new UnidadModel();
            Subunidad = new SubUnidadModel();
        }
        #region CRUD
        public bool Create()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_funcionario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("i_nombres", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_apellidop", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("i_apellidom", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("i_email", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.Unidad.Rut;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                cmd.Parameters.Add("i_id_rol", OracleDbType.Int32).Value = this.Rol.Id;
                cmd.Parameters.Add("i_password", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("i_id_estado_usuario", OracleDbType.Int32).Value = this.Estado.Id;

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
        public List<FuncionarioModel> ReadAll()
        {

            List<FuncionarioModel> listaNegocio = new List<FuncionarioModel>();

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_funcionarios_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {

                    FuncionarioModel f = new FuncionarioModel()
                    {
                        Run = r.GetString(0),
                        Username = r.GetString(0),
                        Nombre = r.GetString(1),
                        ApellidoP = r.GetString(2),
                        ApellidoM = r.GetString(3),
                        Email = r.GetString(4)
                    };

                    f.Unidad = new UnidadModel() { RazonSocial = r.GetString(5), Rut = r.GetString(9) };
                    f.Subunidad = new SubUnidadModel() { Nombre = r.GetString(6), Id = r.GetInt32(10) };
                    f.Rol = new RolModel() { Nombre = r.GetString(7), Id = r.GetInt32(11) };
                    f.Estado = new EstadoModel() { Detalle = r.GetString(8), Id = r.GetInt32(12) };

                    listaNegocio.Add(f);
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

        public bool ReadByRun()
        {
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand("select * from funcionarios where RUN = :username", conn);
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

                OracleCommand cmd = new OracleCommand("u_funcionario", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_run", OracleDbType.NVarchar2).Value = this.Run;
                cmd.Parameters.Add("i_nombres", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_apellidop", OracleDbType.NVarchar2).Value = this.ApellidoP;
                cmd.Parameters.Add("i_apellidom", OracleDbType.NVarchar2).Value = this.ApellidoM;
                cmd.Parameters.Add("i_email", OracleDbType.NVarchar2).Value = this.Email;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.Unidad.Rut;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                cmd.Parameters.Add("i_id_rol", OracleDbType.Int32).Value = this.Rol.Id;
                cmd.Parameters.Add("i_password", OracleDbType.NVarchar2).Value = this.Password;
                cmd.Parameters.Add("i_id_estado_usuario", OracleDbType.Int32).Value = this.Estado.Id;

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

        #endregion
    }
}
