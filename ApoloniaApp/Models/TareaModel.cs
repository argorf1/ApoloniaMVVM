using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace ApoloniaApp.Models
{
    class TareaModel : EntityModelBase
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public ProcesoModel Proceso { get; set; }
        public bool Depencia { get; set; }
        public UsuarioInternoModel Creador { get; set; }
        public ObservableCollection<ResponsableModel> Responsables { get; set; }
        public ObservableCollection<DependenciaModel> Dependencias { get; set; }

        OracleConnection conn = null;
        OracleDataReader r = null;


        public TareaModel()
        {
            Proceso = new ProcesoModel();
            Creador = new UsuarioInternoModel();
            Responsables = new ObservableCollection<ResponsableModel>();
            Dependencias = new ObservableCollection<DependenciaModel>();
        }
        #region CRUD

        public bool Create()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_duracion", OracleDbType.Int32).Value = this.Duracion;
                cmd.Parameters.Add("i_id_proceso_tipo", OracleDbType.Int32).Value = this.Proceso.Id;
                cmd.Parameters.Add("i_tiene_dependencias", OracleDbType.Int32).Value = this.Depencia;
                cmd.Parameters.Add("i_run_disenador", OracleDbType.Int32).Value = this.Creador.Run;
                OracleParameter id = cmd.Parameters.Add("cl", OracleDbType.Int32);
                id.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                this.Id = (int)id.Value;

                conn.Close();
                foreach (DependenciaModel t in Dependencias)
                {
                    t.Create();
                }
                foreach (ResponsableModel f in Responsables)
                {
                    f.Create();
                }
                return true;
            }
            catch (Exception e)
            {
                conn.Close();
                return false;
            }
        }


        public List<TareaModel> ReadAll()
        {
            List<TareaModel> listaNegocio = new List<TareaModel>();



            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_tareas_tipo_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    TareaModel t = new TareaModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2),
                        Duracion = r.GetInt32(3),
                        Depencia = r.GetBoolean(4)
                    };
                    t.Creador = new UsuarioInternoModel() { Nombre = r.GetString(5), Run = r.GetString(6)};
                    t.Proceso = new ProcesoModel() { Nombre = r.GetString(7), Id = r.GetInt32(8) };

                    foreach (ResponsableModel f in new ResponsableModel().ReadAll())
                    {
                        t.Responsables.Add(f);
                    }
                    foreach (DependenciaModel d in new DependenciaModel().ReadAll())
                    {
                        t.Dependencias.Add(d);
                    }
                    listaNegocio.Add(t);
                }
                conn.Close();
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<TareaModel>();
            }
        }

        public bool ReadByName()
        {

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_tareas_tipo_by_name";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_proceso_tipo", OracleDbType.Int32).Value = this.Proceso.Id;
                cmd.Parameters.Add("i_nombre_tarea", OracleDbType.NVarchar2).Value = this.Nombre;
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

        public bool Update()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_subunidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.Id;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_duracion", OracleDbType.Int32).Value = this.Duracion;
                cmd.Parameters.Add("i_id_proceso_tipo", OracleDbType.Int32).Value = this.Proceso.Id;
                cmd.Parameters.Add("i_tiene_dependencias", OracleDbType.Int32).Value = this.Depencia;
                cmd.Parameters.Add("i_run_disenador", OracleDbType.Int32).Value = this.Creador.Run;
                OracleParameter id = cmd.Parameters.Add("cl", OracleDbType.Int32);
                id.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                this.Id = (int)id.Value;

                conn.Close();
                foreach (DependenciaModel t in Dependencias)
                {
                    t.Create();
                }
                foreach (ResponsableModel f in Responsables)
                {
                    f.Create();
                }
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
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "d_tarea_tipo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_proceso_tipo", OracleDbType.Int32).Value = this.Proceso.Id;
                cmd.Parameters.Add("i_nombre_tarea", OracleDbType.NVarchar2).Value = this.Nombre;
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
    }
}
