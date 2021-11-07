using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ApoloniaApp.Models
{
    public class UnidadModel : EntityModelBase
    {
        #region Atributos
        #region Unidad
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public int RubroId { get; set; }
        public int DireccionId { get; set; }
        public string PersonaContacto { get; set; }
        public long TelefonoContacto { get; set; }
        public string EmailContacto { get; set; }
        public string ResponsableRun { get; set; }
        public int EstadoId { get; set; }
        #endregion
        #region Direccion
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Region { get; set; }
        public string Provincia { get; set; }
        public string Comuna { get; set; }
        public int ComunaId { get; set; }
        #endregion
        #region Detalle
        public string Rubro { get; set; }
        public string ResponsableNombre { get; set; }
        public string Estado { get; set; }
        #endregion
        #endregion

        OracleConnection conn = new OracleConnection();
        OracleDataReader r = null;

        public UnidadModel()
        {
            Rut = "";
            RazonSocial = "";
            RubroId = 0;
            DireccionId = 0;
            PersonaContacto = "";
            TelefonoContacto = 0;
            EmailContacto = "";
            ResponsableRun = "";
            EstadoId = 0;
            Calle = "";
            Numero = "";
            Complemento = "";
            Region = "";
            Provincia = "";
            Comuna = "";
            ComunaId = 0;
            Rubro = "";
            ResponsableNombre = "";
            Estado = "";
        }
        #region CRUD



        public bool Create()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_unidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut", OracleDbType.NVarchar2).Value = this.Rut;
                cmd.Parameters.Add("i_razon_social", OracleDbType.NVarchar2).Value = this.RazonSocial;
                cmd.Parameters.Add("i_id_rubro", OracleDbType.Int32).Value = this.RubroId;
                cmd.Parameters.Add("i_calle", OracleDbType.NVarchar2).Value = this.Calle;
                cmd.Parameters.Add("i_numero", OracleDbType.NVarchar2).Value = this.Numero;
                cmd.Parameters.Add("i_complemento", OracleDbType.NVarchar2).Value = this.Complemento;
                cmd.Parameters.Add("i_id_comuna", OracleDbType.Int32).Value = this.ComunaId;
                cmd.Parameters.Add("i_persona_contacto", OracleDbType.NVarchar2).Value = this.PersonaContacto;
                cmd.Parameters.Add("i_telefono_contacto", OracleDbType.Int32).Value = this.TelefonoContacto;
                cmd.Parameters.Add("i_email_contacto", OracleDbType.NVarchar2).Value = this.EmailContacto;
                cmd.Parameters.Add("i_responsable_cuenta", OracleDbType.NVarchar2).Value = this.ResponsableRun;

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
        public List<UnidadModel> ReadAll()
        {

            List<UnidadModel> listaNegocio = new List<UnidadModel>();

            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_unidad_all";
                cmd.CommandType = CommandType.StoredProcedure;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {
                    UnidadModel u = new UnidadModel()
                    {
                        Rut = r.GetString(0),
                        RazonSocial = r.GetString(1),
                        Rubro = r.GetString(2),
                        Calle = r.GetString(3),
                        Numero = r.GetString(4),
                        Complemento = r.GetString(5),
                        Region = r.GetString(6),
                        Provincia = r.GetString(7),
                        Comuna = r.GetString(8),
                        PersonaContacto = r.GetString(9),
                        TelefonoContacto = r.GetInt64(10),
                        EmailContacto = r.GetString(11),
                        ResponsableRun = r.GetString(12),
                        ResponsableNombre = r.GetString(13),
                        Estado = r.GetString(14),
                        EstadoId = r.GetInt32(15),
                        DireccionId = r.GetInt32(16),
                        RubroId = r.GetInt32(17)
                    };

                    listaNegocio.Add(u);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                return listaNegocio;
            }


            return listaNegocio;
        }

        public List<SubUnidadModel> ReadByUnidad()
        {

            List<SubUnidadModel> listaNegocio = new List<SubUnidadModel>();

            conn = new OracleConnection();
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_subunidades";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.Rut;
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
                        SubUnidadPadreId = r.GetInt32(4),
                        RutUnidad = this.Rut
                    };
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


        public bool ReadByRut()
        {
            try
            {
                conn = new Conexion().AbrirConexion();
                OracleCommand cmd = new OracleCommand("select * from unidades where RUT = :rut", conn);
                cmd.Parameters.Add(":rut", OracleDbType.NVarchar2).Value = this.Rut;
                r = cmd.ExecuteReader();

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
            catch (Exception)
            {
                conn.Close();
                return false;
            }

        }
        #endregion

        public bool Update()
        {
            try
            {
                conn = new Conexion().AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_unidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut", OracleDbType.NVarchar2).Value = this.Rut;
                cmd.Parameters.Add("i_razon_social", OracleDbType.NVarchar2).Value = this.RazonSocial;
                cmd.Parameters.Add("i_id_rubro", OracleDbType.Int32).Value = this.RubroId;
                cmd.Parameters.Add("i_id_direccion", OracleDbType.Int32).Value = this.DireccionId;
                cmd.Parameters.Add("i_calle", OracleDbType.NVarchar2).Value = this.Calle;
                cmd.Parameters.Add("i_numero", OracleDbType.NVarchar2).Value = this.Numero;
                cmd.Parameters.Add("i_complemento", OracleDbType.NVarchar2).Value = this.Complemento;
                cmd.Parameters.Add("i_id_comuna", OracleDbType.Int32).Value = this.ComunaId;
                cmd.Parameters.Add("i_persona_contacto", OracleDbType.NVarchar2).Value = this.PersonaContacto;
                cmd.Parameters.Add("i_telefono_contacto", OracleDbType.Int32).Value = this.TelefonoContacto;
                cmd.Parameters.Add("i_email_contacto", OracleDbType.NVarchar2).Value = this.EmailContacto;
                cmd.Parameters.Add("i_run_resp_cuenta", OracleDbType.NVarchar2).Value = this.ResponsableRun;
                cmd.Parameters.Add("i_id_estado_unidad", OracleDbType.Int32).Value = this.EstadoId;


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

        #endregion
    }
}
