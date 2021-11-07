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
                cmd.CommandText = "r_procesos_tipo_all";
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
                    t.Creador = new UsuarioInternoModel() { }
                    listaNegocio.Add(t);
                }
                conn.Close();
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<ProcesoModel>();
            }
        }

        #endregion
    }
}
