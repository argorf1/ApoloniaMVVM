using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
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
        public IEnumerable<FuncionarioModel> Responsables { get; set; }
        public IEnumerable<TareaModel> Dependencias { get; set; }

        OracleConnection conn = null;
        OracleDataReader r = null;

        #region CRUD

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
                    t.Creador = new UsuarioInternoModel() { NombreCompleto = r.GetString(5), Run = r.GetString(6)};
                    t.Proceso = new ProcesoModel() { Nombre = r.GetString(7), Id = r.GetInt32(8) };
                    t.Responsables = t.ReadByResponsable();
                    if (t.Depencia)
                        t.Dependencias = t.ReadByDependencia();
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

        public List<FuncionarioModel> ReadByResponsable()
        {
            List<FuncionarioModel> listaNegocio = new List<FuncionarioModel>();

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_respon_tarea_tipo";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.Id;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    FuncionarioModel f = new FuncionarioModel()
                    {
                        Run = r.GetString(0),
                        Nombre = r.GetString(1)
                    };
                    f.Rol = new RolModel() { Nombre = r.GetString(2), Id = r.GetInt32(3) };

                    listaNegocio.Add(f);
                }
                conn.Close();
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<FuncionarioModel>();
            }
        }

        public List<TareaModel> ReadByDependencia()
        {
            List<TareaModel> listaNegocio = new List<TareaModel>();

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_tareas_t_depend_de";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.Id;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    TareaModel t = new TareaModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1)
                    };

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
        #endregion
    }
}
