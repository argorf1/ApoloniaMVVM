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


        OracleConnection conn = new OracleConnection();
        OracleDataReader r = null;

        #region CRUD
        public bool Create()
        {
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand("c_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
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

        #region Read
        public List<SubUnidadModel> ReadAll()
        {

            List<SubUnidadModel> listaNegocio = new List<SubUnidadModel>();

            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_unidad_all";
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
                        SubUnidadPadre = r.GetString(3),
                        SubUnidadPadreId = r.GetInt32(4),
                        NumFuncionarios = r.GetInt32(5),
                        NumProcesos = r.GetInt32(6)
                    };

                    listaNegocio.Add(s);
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
        #endregion
        #endregion
    }
}
