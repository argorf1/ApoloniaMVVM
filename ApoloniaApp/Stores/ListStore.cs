using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ApoloniaApp.Stores
{
    public class ListStore
    {
        public ObservableCollection<UsuarioInternoModel> usuarios;
        public ObservableCollection<UsuarioInternoModel> designers;
        public ObservableCollection<UnidadModel> unidades;
        public ObservableCollection<TareaModel> tareas;
        public ObservableCollection<SubUnidadModel> subunidades;
        public ObservableCollection<ProcesoModel> procesos;
        public ObservableCollection<FuncionarioModel> funcionarios;
        public ObservableCollection<DependenciaModel> dependencias;
        public ObservableCollection<ResponsableModel> responsables;
        public ObservableCollection<RolModel> roles;
        public ObservableCollection<RegionModel> regiones;
        public ObservableCollection<ProvinciaModel> provincias;
        public ObservableCollection<ComunaModel> comunas;
        public ObservableCollection<RubroModel> rubros;
        public ObservableCollection<PerfilModel> perfiles;
        public ObservableCollection<EstadoModel> estados;

        public ListStore()
        {
            usuarios = new ObservableCollection<UsuarioInternoModel>();
            designers = new ObservableCollection<UsuarioInternoModel>();
            unidades = new ObservableCollection<UnidadModel>();
            tareas = new ObservableCollection<TareaModel>();
            subunidades = new ObservableCollection<SubUnidadModel>();
            procesos = new ObservableCollection<ProcesoModel>();
            funcionarios = new ObservableCollection<FuncionarioModel>();
            dependencias = new ObservableCollection<DependenciaModel>();
            responsables = new ObservableCollection<ResponsableModel>();
            roles = new ObservableCollection<RolModel>();
            regiones = new ObservableCollection<RegionModel>();
            provincias = new ObservableCollection<ProvinciaModel>();
            comunas = new ObservableCollection<ComunaModel>();
            rubros = new ObservableCollection<RubroModel>();
            perfiles = new ObservableCollection<PerfilModel>();
            estados = new ObservableCollection<EstadoModel>();
        }

        #region Admin
        public void Adminview()
        {


            usuarios = new ReadAllCommand<UsuarioInternoModel>().ReadAll(() => new UsuarioInternoModel().ReadAll());
            designers = new ReadAllCommand<UsuarioInternoModel>().ReadAll(() => new UsuarioInternoModel().ReadByDesignerPerfil(), new UsuarioInternoModel() { Run = "0", Nombre = "-- Seleccionar --" });
            unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => new UnidadModel().ReadAll());
            subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => new SubUnidadModel().ReadAll());
            roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
            funcionarios = new ReadAllCommand<FuncionarioModel>().ReadAll(() => new FuncionarioModel().ReadAll());
            regiones = new ReadAllCommand<RegionModel>().ReadAll(() => new RegionModel().ReadAll(), new RegionModel() { Id = 0, Nombre = "--Seleccionar--" });
            provincias = new ReadAllCommand<ProvinciaModel>().ReadAll(() => new ProvinciaModel().ReadAll(), new ProvinciaModel() { Id = 0, Nombre = "--Seleccionar--", IdRegion = 0 });
            comunas = new ReadAllCommand<ComunaModel>().ReadAll(() => new ComunaModel().ReadAll(), new ComunaModel() { Id = 0, Nombre = "--Seleccionar--", IdProvincia = 0 });
            rubros = new ReadAllCommand<RubroModel>().ReadAll(() => new RubroModel().ReadAll(), new RubroModel() { Id = 0, Nombre = "--Seleccionar--" });
            perfiles = new ReadAllCommand<PerfilModel>().ReadAll(() => new PerfilModel().ReadAll(), new PerfilModel() { Id = 0, Nombre = "--Seleccionar--" });
            estados = new ReadAllCommand<EstadoModel>().ReadAll(() => new EstadoModel().ReadAll(), new EstadoModel() { Id = 0, Nombre = "--Seleccionar--" });

        }

        public void Usuarios()
        {
            usuarios = new ReadAllCommand<UsuarioInternoModel>().ReadAll(() => new UsuarioInternoModel().ReadAll());
            designers = new ReadAllCommand<UsuarioInternoModel>().ReadAll(() => new UsuarioInternoModel().ReadByDesignerPerfil(), new UsuarioInternoModel() { Run = "0", Nombre = "-- Seleccionar --" });
        }

        public void Unidades()
        {
            unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => new UnidadModel().ReadAll());
            subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => new SubUnidadModel().ReadAll());
            roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
        }

        public void Subunidades()
        {
            subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => new SubUnidadModel().ReadAll());
            roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
        }

        public void Roles()
        {
            roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
        }

        public void Funcionarios()
        {
            funcionarios = new ReadAllCommand<FuncionarioModel>().ReadAll(() => new FuncionarioModel().ReadAll());
        }
        #endregion
        #region Process Designer
        public void PDView()
        {
            procesos = new ReadAllCommand<ProcesoModel>().ReadAll(() => new ProcesoModel().ReadAll());
            tareas = new ReadAllCommand<TareaModel>().ReadAll(() => new TareaModel().ReadAll());
            dependencias = new ReadAllCommand<DependenciaModel>().ReadAll(() => new DependenciaModel().ReadAll());
            responsables = new ReadAllCommand<ResponsableModel>().ReadAll(() => new ResponsableModel().ReadAll());
            unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => new UnidadModel().ReadAll());
            subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => new SubUnidadModel().ReadAll());
            funcionarios = new ReadAllCommand<FuncionarioModel>().ReadAll(() => new FuncionarioModel().ReadAll());
            roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
        }

        public void ProcesosView()
        {
            procesos = new ReadAllCommand<ProcesoModel>().ReadAll(() => new ProcesoModel().ReadAll());
        }

        public void TareasView()
        {
            tareas = new ReadAllCommand<TareaModel>().ReadAll(() => new TareaModel().ReadAll());
            dependencias = new ReadAllCommand<DependenciaModel>().ReadAll(() => new DependenciaModel().ReadAll());
            responsables = new ReadAllCommand<ResponsableModel>().ReadAll(() => new ResponsableModel().ReadAll());
        }

        #endregion    
    }
}
