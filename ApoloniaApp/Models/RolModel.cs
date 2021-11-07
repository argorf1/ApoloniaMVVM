using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class RolModel : EntityModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public SubUnidadModel Subunidad { get; set; }
        public UnidadModel Unidad { get; set; }
        public int RolSuperior { get; set; }
        public int Nivel { get; set; }

        OracleConnection conn = null;
        OracleDataReader r = null;

        public RolModel()
        {
            Subunidad = new SubUnidadModel();
            Unidad = new UnidadModel();
        }

        public RolModel(UnidadModel unidad)
        {
            Subunidad = new SubUnidadModel();
            Unidad = unidad;
        }

        #region CRUD
        public bool Create()
        {
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand("c_rol", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre_rol", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                cmd.Parameters.Add("i_id_rol_superior", OracleDbType.Int32).Value = this.RolSuperior;
                cmd.Parameters.Add("i_nivel", OracleDbType.Int32).Value = this.Nivel;
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
        public List<RolModel> ReadAll()
        {
            List<RolModel> listaNegocio = new List<RolModel>();

            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_roles_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {


                    RolModel ro = new RolModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2),
                        Nivel = r.GetInt32(3),
                        RolSuperior = r.GetInt32(4)
                    };
                    ro.Subunidad.Nombre = r.GetString(5);
                    ro.Subunidad.Id = r.GetInt32(7);
                    ro.Unidad.RazonSocial = r.GetString(6);
                    ro.Unidad.Rut = r.GetString(8);
                    listaNegocio.Add(ro);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<RolModel>();
            }

            return listaNegocio;
        }

        public bool ReadByNombre()
        {
            bool read = true;
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_rol_by_nombre";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre_rol", OracleDbType.NVarchar2).Value = this.Nombre;
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
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand("u_rol", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_rol", OracleDbType.Int32).Value = this.Id;
                cmd.Parameters.Add("i_nombre_rol", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Subunidad.Id;
                cmd.Parameters.Add("i_id_rol_superior", OracleDbType.Int32).Value = this.RolSuperior;
                cmd.Parameters.Add("i_nivel", OracleDbType.Int32).Value = this.Nivel;

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
    }

    #endregion
}
