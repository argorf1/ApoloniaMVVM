﻿using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class PerfilModel : ModelBase
    {


        

        public PerfilModel()
        {
        }

        public PerfilModel(int id)
        {
            Id = id;
            ReadById(); 
        }

        public List<PerfilModel> ReadAll()
        {

            List<PerfilModel> listaNegocio = new List<PerfilModel>();

            OracleConnection conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "R_PERFIL_USUARIOS_ALL";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                OracleDataReader r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    PerfilModel rol = new PerfilModel()
                    {
                        Nombre = r.GetString(0),
                        Id = r.GetInt32(1)
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
                conn = Conexion.AbrirConexion();
                OracleCommand cmd = new OracleCommand("select nombre from perfil_usuarios where ID = :id", conn);
                cmd.Parameters.Add(":id", OracleDbType.NVarchar2).Value = this.Id;
                OracleDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {
                    this.Nombre = r.GetString(0);
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
