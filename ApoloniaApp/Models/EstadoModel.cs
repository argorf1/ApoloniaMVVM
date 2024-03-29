﻿using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class EstadoModel: ModelBase
    {
        public EstadoModel()
        {
            Id = 1;
        }

        public List<EstadoModel> ReadAll()
        {

            List<EstadoModel> listaNegocio = new List<EstadoModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_ESTADO_USUARIOS_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    EstadoModel e = new EstadoModel()
                    {
                        Nombre = r.GetString(0),
                        Id = r.GetInt32(1)
                    };
                    listaNegocio.Add(e);
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
