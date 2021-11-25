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
        public string Rut { get; set; }
        public string RazonSocial { get; set; }
        public string PersonaContacto { get; set; }
        public string TelefonoContacto { get; set; }
        public string EmailContacto { get; set; }
        public UsuarioInternoModel Responsable { get; set; }
        public RubroModel Rubro { get; set; }
        public EstadoModel Estado { get; set; }
        public DireccionModel Direccion { get; set; }
        #endregion

        public UnidadModel()
        {
            Rut = "";

            Responsable = new UsuarioInternoModel() { Run = "0"};
            Rubro = new RubroModel();
            Estado = new EstadoModel();
            Direccion = new DireccionModel();

            NombreEntidad = "Unidad";
            Mensaje = "";
        }

        OracleConnection conn = new OracleConnection();
        OracleDataReader r = null;

        #region CRUD

        public bool Create()
        {
            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("c_unidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut", OracleDbType.NVarchar2).Value = this.Rut;
                cmd.Parameters.Add("i_razon_social", OracleDbType.NVarchar2).Value = this.RazonSocial;
                cmd.Parameters.Add("i_id_rubro", OracleDbType.Int32).Value = this.Rubro.Id;
                cmd.Parameters.Add("i_calle", OracleDbType.NVarchar2).Value = this.Direccion.Calle;
                cmd.Parameters.Add("i_numero", OracleDbType.NVarchar2).Value = this.Direccion.Numero;
                cmd.Parameters.Add("i_complemento", OracleDbType.NVarchar2).Value = this.Direccion.Complemento;
                cmd.Parameters.Add("i_id_comuna", OracleDbType.Int32).Value = this.Direccion.Comuna.Id;
                cmd.Parameters.Add("i_persona_contacto", OracleDbType.NVarchar2).Value = this.PersonaContacto;
                cmd.Parameters.Add("i_telefono_contacto", OracleDbType.NVarchar2).Value = this.TelefonoContacto;
                cmd.Parameters.Add("i_email_contacto", OracleDbType.NVarchar2).Value = this.EmailContacto;
                cmd.Parameters.Add("i_responsable_cuenta", OracleDbType.NVarchar2).Value = this.Responsable.Run;

                cmd.ExecuteNonQuery();

                conn.Close();

                if (!new SubUnidadModel() { Nombre = "Gerencia General", Descripcion = "Área que está relacionada con el proceso de la operación general de la empresa. En ella se definen los objetivos, se toman las decisiones más importantes y desde ahí se dirigen todas las operaciones de la organización. Dado que es la responsable de que todo funcione bien, se relaciona directamente con todas las otras áreas y las controla.", RutUnidad = this.Rut }.Create())
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

        #region Read

        public List<UnidadModel> ReadAll()
        {
            List<UnidadModel> listaNegocio = new List<UnidadModel>();
            try
            {
                conn = Conexion.AbrirConexion();

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
                        PersonaContacto = r.GetString(9),
                        TelefonoContacto = r.GetString(10),
                        EmailContacto = r.GetString(11),
                    };
                    u.Direccion = new DireccionModel() { Id = r.GetInt32(18), Calle = r.GetString(3), Numero = r.GetString(4), Complemento = r.GetString(5) };
                    u.Direccion.Region = new RegionModel() { Nombre = r.GetString(6) };
                    u.Direccion.Provincia = new ProvinciaModel() { Nombre = r.GetString(7) };
                    u.Direccion.Comuna = new ComunaModel() { Nombre = r.GetString(8) };
                    u.Rubro = new RubroModel() { Id = r.GetInt32(19), Nombre = r.GetString(2) };
                    u.Estado = new EstadoModel() { Id = r.GetInt32(17), Nombre = r.GetString(16) };
                    u.Responsable = new UsuarioInternoModel() { Run = r.GetString(12), Nombre = r.GetString(13), ApellidoP = r.GetString(14), ApellidoM = r.GetString(15) };
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
        public List<UnidadModel> ReadDesigner()
        {

            List<UnidadModel> listaNegocio = new List<UnidadModel>();

            try
            {
                conn = Conexion.AbrirConexion();

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
                        PersonaContacto = r.GetString(9),
                        TelefonoContacto = r.GetString(10),
                        EmailContacto = r.GetString(11),
                    };
                    u.Direccion = new DireccionModel() { Id = r.GetInt32(16), Calle = r.GetString(3), Numero = r.GetString(4), Complemento = r.GetString(5) };
                    u.Direccion.Region = new RegionModel() { Nombre = r.GetString(6) };
                    u.Direccion.Provincia = new ProvinciaModel() { Nombre = r.GetString(7) };
                    u.Direccion.Comuna = new ComunaModel() { Nombre = r.GetString(8) };
                    u.Rubro = new RubroModel() { Id = r.GetInt32(17), Nombre = r.GetString(2) };
                    u.Estado = new EstadoModel() { Id = r.GetInt32(15), Nombre = r.GetString(14) };
                    u.Responsable = new UsuarioInternoModel() { Run = r.GetString(12), Nombre = r.GetString(13) };

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

        public List<SubUnidadModel> ReadSubunidadByUnidad()
        {

            List<SubUnidadModel> listaNegocio = new List<SubUnidadModel>();

            conn = new OracleConnection();
            try
            {
                conn = Conexion.AbrirConexion();
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
                        RutUnidad = this.Rut
                    };
                    s.SubunidadPadre.Id = r.GetInt32(4);
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

        public List<RolModel> ReadRolByUnidad()
        {
            List<RolModel> listaNegocio = new List<RolModel>();

            try
            {
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "r_roles_by_unidad";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut_unidad", OracleDbType.NVarchar2).Value = this.Rut;
                OracleParameter o = cmd.Parameters.Add("cl", OracleDbType.RefCursor);
                o.Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();


                r = ((OracleRefCursor)o.Value).GetDataReader();
                while (r.Read())
                {


                    RolModel ro = new RolModel()
                    {
                        Id = r.GetInt32(0),
                        Nombre = r.GetString(1),
                        Descripcion = r.GetString(2),
                        Nivel = r.GetInt32(3)
                    };
                    ro.Subunidad = new SubUnidadModel() { Nombre = r.GetString(4), Id = r.GetInt32(6) };
                    ro.Unidad = new UnidadModel() { RazonSocial = r.GetString(5), Rut = r.GetString(7) };
                    listaNegocio.Add(ro);
                }

                conn.Close();

            }
            catch (Exception e)
            {
                conn.Close();
                return new List<RolModel>();
            }

            return listaNegocio;
        }
        public bool ReadByRut()
        {
            try
            {
                conn = Conexion.AbrirConexion();
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
                conn = Conexion.AbrirConexion();

                OracleCommand cmd = new OracleCommand("u_unidad", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut", OracleDbType.NVarchar2).Value = this.Rut;
                cmd.Parameters.Add("i_razon_social", OracleDbType.NVarchar2).Value = this.RazonSocial;
                cmd.Parameters.Add("i_id_rubro", OracleDbType.Int32).Value = this.Rubro.Id;
                cmd.Parameters.Add("i_id_direccion", OracleDbType.Int32).Value = this.Direccion.Id;
                cmd.Parameters.Add("i_calle", OracleDbType.NVarchar2).Value = this.Direccion.Calle;
                cmd.Parameters.Add("i_numero", OracleDbType.NVarchar2).Value = this.Direccion.Numero;
                cmd.Parameters.Add("i_complemento", OracleDbType.NVarchar2).Value = this.Direccion.Complemento;
                cmd.Parameters.Add("i_id_comuna", OracleDbType.Int32).Value = this.Direccion.Comuna.Id;
                cmd.Parameters.Add("i_persona_contacto", OracleDbType.NVarchar2).Value = this.PersonaContacto;
                cmd.Parameters.Add("i_telefono_contacto", OracleDbType.Int32).Value = this.TelefonoContacto;
                cmd.Parameters.Add("i_email_contacto", OracleDbType.NVarchar2).Value = this.EmailContacto;
                cmd.Parameters.Add("i_run_resp_cuenta", OracleDbType.NVarchar2).Value = this.Responsable.Run;
                cmd.Parameters.Add("i_id_estado_unidad", OracleDbType.Int32).Value = this.Estado.Id;


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
                cmd.CommandText = "d_unidad";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("i_rut", OracleDbType.Varchar2).Value = this.Rut;

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
