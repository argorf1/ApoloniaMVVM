using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class ProcesoModel : EntityModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public SubUnidadModel Subunidad { get; set; }
        public RolModel Rol { get; set; }
        public UsuarioInternoModel Creador { get; set; }
        public UnidadModel Unidad { get; set; }

        OracleConnection conn = null;
        OracleDataReader r = null;

        #region CRUD

        public ProcesoModel()
        {
            Subunidad = new SubUnidadModel();
            Rol = new RolModel();
            Creador = new UsuarioInternoModel();
        }

        public ProcesoModel(UnidadModel unidad)
        {
            Subunidad = new SubUnidadModel();
            Rol = new RolModel() { Unidad = unidad};
            Creador = new UsuarioInternoModel();
        }

        public bool Create()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_proceso_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                cmd.Parameters.Add("i_rol_ejec_min", OracleDbType.Int32).Value = this.Rol.Id;
                cmd.Parameters.Add("i_run_disenador", OracleDbType.NVarchar2).Value = this.Creador.Run;
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
        public List<ProcesoModel> ReadAll()
        {

            List<ProcesoModel> listaNegocio = new List<ProcesoModel>();

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_procesos_tipo_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    ProcesoModel p = new ProcesoModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2)
                    };
                    p.Rol = new RolModel() { Nombre=r.GetString(3), Id = r.GetInt32(4)};
                    p.Unidad = new UnidadModel() { Rut = r.GetString(10) };
                    p.Creador = new UsuarioInternoModel() { NombreCompleto = r.GetString(5), Run = r.GetString(6) };
                    p.Subunidad = new SubUnidadModel() {Nombre=r.GetString(7), Id = r.GetInt32(8)};
                    listaNegocio.Add(p);
                }
                conn.Close();
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<ProcesoModel>();
            }
        }

        public bool ReadByNombre()
        {
            bool read = true;
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_proc_tipo_by_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                if (!r.Read())
                {
                    read = false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return read;
            }
            return read;
        }
        #endregion
        public bool Update()
        {

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_proceso_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id", OracleDbType.Int32).Value = this.Id;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                cmd.Parameters.Add("i_id_rol_ejec_min", OracleDbType.Int32).Value = this.Rol.Id;
                cmd.Parameters.Add("i_run_disenador", OracleDbType.NVarchar2).Value = this.Creador.Run;

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
