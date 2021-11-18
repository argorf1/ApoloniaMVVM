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
    }
}
