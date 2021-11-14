using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    class DependenciaModel : EntityModelBase
    {
        public int Id { get; set; }
        public int IdTarea { get; set; }
        public TareaModel Tarea{ get; set; }

        private OracleConnection conn;
        private OracleDataReader r;

        public DependenciaModel()
        {
            Tarea = new TareaModel();
        }

        public DependenciaModel(TareaModel tareaPrevia)
        {
            Tarea = tareaPrevia;
        }

        public bool Create()
        {

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.IdTarea;
                cmd.Parameters.Add("i_id_tarea_previa", OracleDbType.Int32).Value = this.Tarea.Id;

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
                conn = new Conexion().AbrirConexion();
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
                    t.Tarea = new TareaModel() { Id = r.GetInt32(2), Nombre = r.GetString(3) };
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

        public List<DependenciaModel> ReadTareasAll()
        {
            List<DependenciaModel> listaNegocio = new List<DependenciaModel>();

            try
            {
                foreach (TareaModel  t in new TareaModel().ReadAll())
                {
                    listaNegocio.Add(new DependenciaModel(t));
                }
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<DependenciaModel>();
            }
        }


    }
}
