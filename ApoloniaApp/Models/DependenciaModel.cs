using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class DependenciaModel : EntityModelBase
    {
        public int IdTarea { get; set; }
        public TareaModel TareaPrevia{ get; set; }

        private OracleConnection conn;
        private OracleDataReader r;

        public DependenciaModel()
        {
            TareaPrevia = new TareaModel();
        }

        public DependenciaModel(TareaModel tareaPrevia, int id)
        {
            TareaPrevia = tareaPrevia;
            IdTarea = id;
        }

        public bool Create()
        {

            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_dependen_tarea_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.IdTarea;
                cmd.Parameters.Add("i_id_tarea_previa", OracleDbType.Int32).Value = this.TareaPrevia.Id;

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

        public List<DependenciaModel> ReadAll()
        {
            List<DependenciaModel> listaNegocio = new List<DependenciaModel>();

            conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_dependencias_tt_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    DependenciaModel t = new DependenciaModel()
                    {
                        Id = r.GetInt32(0),
                        IdTarea = r.GetInt32(1)
                    };
                    t.TareaPrevia = new TareaModel() { Id = r.GetInt32(2), Nombre = r.GetString(3) };
                    listaNegocio.Add(t);
                }
                conn.Close();
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<DependenciaModel>();
            }
        }

        public bool Update()
        {

            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_dependen_tarea_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.IdTarea;
                cmd.Parameters.Add("i_id_tarea_previa", OracleDbType.Int32).Value = this.TareaPrevia.Id;


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
        public bool Delete(int id)
        {
            conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "d_dependen_tarea_tipo_by_id";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = id;

                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return true;
            }
        }


    }
}
