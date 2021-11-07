using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class ProvinciaModel
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int IdRegion { get; set; }

        public List<ProvinciaModel> ReadAll()
        {

            List<ProvinciaModel> listaNegocio = new List<ProvinciaModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_PROVINCIAS_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    ProvinciaModel p = new ProvinciaModel()
                    {
                        Detalle = r.GetString(0),
                        Id = r.GetInt32(1),
                        IdRegion = r.GetInt32(2)
                    };
                    listaNegocio.Add(p);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                return null;
            }
            return listaNegocio;
        }
    }
}
