using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class SubUnidadModel : EntityModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int SubUnidadPadreId { get; set; }
        public string SubUnidadPadre { get; set; }
        public string RutUnidad { get; set; }
        public int NumFuncionarios { get; set; }
        public int NumProcesos { get; set; }


        OracleConnection conn = null;
        OracleDataReader r = null;

        public SubUnidadModel()
        {
            conn = null;
            r = null;
        }



        #region CRUD
        public bool Create()
        {
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand("c_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.RutUnidad;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad_padre", OracleDbType.Int32).Value = this.SubUnidadPadreId;
                

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
        public List<SubUnidadModel> ReadAll()
        {

            List<SubUnidadModel> listaNegocio = new List<SubUnidadModel>();

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_subunidades_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    SubUnidadModel s = new SubUnidadModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2),
                        SubUnidadPadreId = r.GetInt32(4),
                        RutUnidad = r.GetString(8)
                    };
                    listaNegocio.Add(s);
                }
                    conn.Close();
                    return listaNegocio;
                
            }
            catch (Exception e)
            {
                conn.Close();
                return listaNegocio;
            }
        }

        
        public bool ReadByName()
        {
            try
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_subunidad_by_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre_subunidad", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.RutUnidad;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;



                cmd.ExecuteNonQuery();

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
            catch (Exception e)
            {
                conn.Close();
                return true;
            }
        }
        #endregion

        public bool Update()
        {
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand("u_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Id;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad_padre", OracleDbType.Int32).Value = this.SubUnidadPadreId;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.RutUnidad;


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
