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
        public int RolSuperior { get; set; }
        public int Nivel { get; set; }

        public RolModel()
        {
            Subunidad = new SubUnidadModel();
        }

        public List<RolModel> ReadAll()
        {
            List<RolModel> listaNegocio = new List<RolModel>();

            OracleConnection conn = new OracleConnection();
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


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    

                    RolModel ro = new RolModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2),
                        Nivel = r.GetInt32(3)
                    };
                    ro.Subunidad.Nombre = r.GetString(4);
                    ro.Subunidad.Id = r.GetInt32(6);
                    listaNegocio.Add(ro);
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
