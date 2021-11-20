using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class RegionModel : ModelBase
    {


        public RegionModel()
        {
            Id = 0;
        }
        public List<RegionModel> ReadAll()
        {

            List<RegionModel> listaNegocio = new List<RegionModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_REGIONES_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    RegionModel region = new RegionModel()
                    {
                        Nombre = r.GetString(0),
                        Id = r.GetInt32(1)
                    };
                    listaNegocio.Add(region);
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
