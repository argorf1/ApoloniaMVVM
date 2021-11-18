using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class ResponsableModel : EntityModelBase
    {

        public int Id { get; set; }
        public int IdTarea { get; set; }
        public FuncionarioModel Responsable { get; set; }

        private OracleConnection conn;
        private OracleDataReader r;


        public ResponsableModel()
        {
            Responsable = new FuncionarioModel();
        }

        public ResponsableModel(FuncionarioModel responsable)
        {
            Responsable = responsable;
        }

        public bool Create()
        {

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_resp_tarea_tipo", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_id_tarea_tipo", OracleDbType.Int32).Value = this.IdTarea;
                cmd.Parameters.Add("i_run_funcionario", OracleDbType.NVarchar2).Value = this.Responsable.Run;

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

        public List<ResponsableModel> ReadAll()
        {
            List<ResponsableModel> listaNegocio = new List<ResponsableModel>();

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_respon_tt_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    ResponsableModel t = new ResponsableModel()
                    {
                        Id = r.GetInt32(0),
                        IdTarea = r.GetInt32(3)
                    };
                    t.Responsable = new FuncionarioModel() { Run = r.GetString(1), Nombre = r.GetString(2) };

                    listaNegocio.Add(t);
                }
                conn.Close();
                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<ResponsableModel>();
            }
        }

        public List<ResponsableModel> ReadResponsableAll()
        {
            List<ResponsableModel> listaNegocio = new List<ResponsableModel>();

            try
            {

                foreach (FuncionarioModel t in new FuncionarioModel().ReadAll())
                {
                    listaNegocio.Add(new ResponsableModel(t));
                }

                return listaNegocio;

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<ResponsableModel>();
            }
        }

        public bool Delete(int id)
        {
            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "d_responsable_tarea_tipo_by_id";
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
