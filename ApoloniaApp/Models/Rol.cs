using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class Rol
    {

        public int Id { get; set; }
        public string Detalle { get; set; }

        public Rol()
        {
        }

        public Rol(int id)
        {
            Id = id;
            ReadById(); 
        }

        public List<Rol> ReadAll()
        {

            List<Rol> listaNegocio = new List<Rol>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_ROL_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    Rol rol = new Rol()
                    {
                        Id = r.GetInt32(0),
                        Detalle = r.GetString(1)
                    };
                    listaNegocio.Add(rol);
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

        public void ReadById()
        {
            OracleConnection conn = new OracleConnection();
            try
            {
                conn = new Conexion().abrirConexion();
                OracleCommand cmd = new OracleCommand("select nombre from Rol where ID = :id", conn);
                cmd.Parameters.Add(":id", OracleDbType.NVarchar2).Value = this.Id;
                OracleDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    this.Detalle = r.GetString(0);
                    conn.Close();
                    return;
                }
                else
                {
                    conn.Close();
                    return;
                }
            }
            catch (Exception)
            {
                conn.Close();
                return;
            }

        }
    }
}
