using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace ApoloniaApp.Models
{
    public class Conexion
    {
        static OracleConnection conn;
        public static OracleConnection AbrirConexion()
        {
            string connectionString = "USER ID=APOLONIADB; PASSWORD=chachesoflo;DATA SOURCE=oraclegcp.bellann.cl;PERSIST SECURITY INFO=True";
            conn = new OracleConnection(connectionString);

            try
            {
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw new Exception("Error de Conexion");
            }
        }

    }
}
