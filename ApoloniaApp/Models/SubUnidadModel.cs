using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class SubUnidadModel : EntityModelBase
    {
        
        public string Descripcion { get; set; }
        public SubUnidadModel SubunidadPadre { get; set; }
        public string RutUnidad { get; set; }
        public int NumFuncionarios { get; set; }
        public int NumProcesos { get; set; }


        OracleConnection conn = null;
        OracleDataReader r = null;

        public SubUnidadModel()
        {
            SubunidadPadre = new SubUnidadModel("");

            NombreEntidad = "Subunidad";
            Mensaje = "";
        }

        public SubUnidadModel(string vacio)
        {
            Id = 0;
            Nombre = "";
            Descripcion = "";
        }

        #region CRUD
        public bool Create()
        {
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.RutUnidad;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad_padre", OracleDbType.Int32).Value = this.SubunidadPadre.Id;
                

                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        #region Read
        public List<SubUnidadModel> ReadAll()
        {

            List<SubUnidadModel> listaNegocio = new List<SubUnidadModel>();

            conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_subunidades_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    SubUnidadModel s = new SubUnidadModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2),
                        RutUnidad = r.GetString(5)
                    };
                    s.SubunidadPadre = new SubUnidadModel() { Id = r.GetInt32(3) };
                    listaNegocio.Add(s);
                }
                    conn.Close();
                    return listaNegocio;
                
            }
            catch (Exception e)
            {
                conn.Close();
                return listaNegocio;
            }
        }

        
        public bool ReadByName()
        {
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_subunidad_by_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre_subunidad", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.RutUnidad;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;



                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                if (r.Read())
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return true;
            }
        }

        public bool ReadContent()
        {
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_subunidades_depen";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id", OracleDbType.NVarchar2).Value = this.Id;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;



                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                if (r.Read())
                {
                    conn.Close();
                    return true;
                }
                else
                {
                    conn.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                conn.Close();
                return true;
            }
        }
        #endregion

        public bool Update()
        {
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Id;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_id_subunidad_padre", OracleDbType.Int32).Value = this.SubunidadPadre.Id;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.RutUnidad;


                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }

        public bool Delete()
        {
            conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "d_subunidad";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_subunidad", OracleDbType.Int32).Value = this.Id;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return true;
            }
        }
        #endregion
    }
}
