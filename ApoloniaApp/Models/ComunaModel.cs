using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class ComunaModel
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        public int IdProvincia { get; set; }

        public List<ComunaModel> ReadAll()
        {

            List<ComunaModel> listaNegocio = new List<ComunaModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_COMUNAS_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    ComunaModel c = new ComunaModel()
                    {
                        Detalle = r.GetString(0),
                        Id = r.GetInt32(1),
                        IdProvincia = r.GetInt32(2)
                    };
                    listaNegocio.Add(c);
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
