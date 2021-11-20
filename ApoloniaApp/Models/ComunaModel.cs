using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class ComunaModel : ModelBase
    {
        public int IdProvincia { get; set; }

        public ComunaModel()
        {
            Id = 0;
        }

        public List<ComunaModel> ReadAll()
        {

            List<ComunaModel> listaNegocio = new List<ComunaModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();

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
                        Nombre = r.GetString(0),
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
