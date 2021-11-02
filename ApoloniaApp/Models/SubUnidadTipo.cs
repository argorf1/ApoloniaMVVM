using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class SubUnidadTipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Add { get; set; }
        public bool Editable { get; set; }
        public List<SubUnidadTipo> ReadAll()
        {

            List<SubUnidadTipo> listaNegocio = new List<SubUnidadTipo>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_SUBUNI_TIPO_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    SubUnidadTipo s = new SubUnidadTipo()
                    {
                        Nombre = r.GetString(0),
                        Descripcion = r.GetString(1),
                        Id = r.GetInt32(2)
                    };
                    listaNegocio.Add(s);
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
