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
    public class TareaModel : EntityModelBase
    {

        
        public string Descripcion { get; set; }
        public int Duracion { get; set; }
        public ProcesoModel Proceso { get; set; }
        public UsuarioInternoModel Creador { get; set; }
        public FuncionarioModel Responsable { get; set; }
        public TareaModel Dependencia { get; set; }

        OracleConnection conn = null;
        OracleDataReader r = null;


        public TareaModel()
        {
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Duracion = 0;
            Proceso = new ProcesoModel();
            Creador = new UsuarioInternoModel();
            Responsable = new FuncionarioModel() { Run = "0"};
            Dependencia = new TareaModel("");

            NombreEntidad = "Tarea";
            Mensaje = "";
        }

        public TareaModel(string vacio)
        {
        }
    
        #region CRUD

        public bool Create()
        {
            bool create = true;
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_tarea_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_duracion", OracleDbType.Int32).Value = this.Duracion;
                cmd.Parameters.Add("i_id_proceso_tipo", OracleDbType.Int32).Value = this.Proceso.Id;
                cmd.Parameters.Add("i_run_disenador", OracleDbType.NVarchar2).Value = this.Creador.Run;
                OracleParameter id = cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32);
                id.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                this.Id = int.Parse(id.Value.ToString());
                conn.Close();
                if(Responsable.Run != "0")
                    create = new ResponsableModel(this.Responsable, this.Id).Create();

                if (create && Dependencia.Id != 0)
                {
                    create = new DependenciaModel(this.Dependencia, this.Id).Create();
                }


                if (!create)
                {
                    this.Delete();
                    return false;
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
                conn = Conexion.AbrirConexion();
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
                    };
                    t.Creador = new UsuarioInternoModel() { Nombre = r.GetString(4), Run = r.GetString(5) };
                    t.Proceso = new ProcesoModel() { Nombre = r.GetString(6), Id = r.GetInt32(7) };

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
                conn = Conexion.AbrirConexion();

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
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_tarea_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.Id;
                cmd.Parameters.Add("i_nombre", OracleDbType.NVarchar2).Value = this.Nombre;
                cmd.Parameters.Add("i_descripcion", OracleDbType.NVarchar2).Value = this.Descripcion;
                cmd.Parameters.Add("i_duracion", OracleDbType.Int32).Value = this.Duracion;


                cmd.ExecuteNonQuery();

                new ResponsableModel(this.Responsable, this.Id).Update();
                _ = Dependencia.Id != 0 ? new DependenciaModel(this.Dependencia, this.Id).Update() : new DependenciaModel().Delete(this.Id);

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
                cmd.CommandText = "d_tarea_tipo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.Id;

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
