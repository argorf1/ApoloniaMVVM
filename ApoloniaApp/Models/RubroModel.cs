using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class RubroModel
    {
        public int Id { get; set; }
        public string Detalle { get; set; }

        public List<RubroModel> ReadAll()
        {

            List<RubroModel> listaNegocio = new List<RubroModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_RUBROS_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    RubroModel rubro = new RubroModel()
                    {
                        Detalle = r.GetString(0),
                        Id = r.GetInt32(1)
                    };
                    listaNegocio.Add(rubro);
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
