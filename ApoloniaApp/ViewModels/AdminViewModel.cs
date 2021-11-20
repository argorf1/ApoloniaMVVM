using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        
        public  ViewModelBase CurrentViewModel => _frameStore.CurrentViewModel;
        public UsuarioInternoModel CurrentAccount;

        private string _logUser;
        public string LogUser
        {
            get { return _logUser; }
            set {
                _logUser = value;
                OnPropertyChanged("LogUser"); 
            }

        }
        public AdminViewModel(NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;
            _frameStore = new FrameStore();
            CurrentAccount = accountStore.CurrentAccount;

            #region Carga listas

            _listStore.usuarios = new ReadAllCommand<UsuarioInternoModel>().ReadAll(() => new UsuarioInternoModel().ReadAll());
            _listStore.designers = new ReadAllCommand<UsuarioInternoModel>().ReadAll(() => new UsuarioInternoModel().ReadByDesignerPerfil(), new UsuarioInternoModel() { Run="0", Nombre = "-- Seleccionar --"});
            _listStore.unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => new UnidadModel().ReadAll());
            _listStore.subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => new SubUnidadModel().ReadAll());
            _listStore.roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
            _listStore.funcionarios= new ReadAllCommand<FuncionarioModel>().ReadAll(() => new FuncionarioModel().ReadAll());
            _listStore.regiones = new ReadAllCommand<RegionModel>().ReadAll(() => new RegionModel().ReadAll(), new RegionModel() { Id = 0, Nombre = "--Seleccionar--" });
            _listStore.provincias = new ReadAllCommand<ProvinciaModel>().ReadAll(() => new ProvinciaModel().ReadAll(), new ProvinciaModel() { Id = 0, Nombre = "--Seleccionar--", IdRegion = 0 });
            _listStore.comunas = new ReadAllCommand<ComunaModel>().ReadAll(() => new ComunaModel().ReadAll(), new ComunaModel() { Id = 0, Nombre = "--Seleccionar--", IdProvincia = 0 });
            _listStore.rubros = new ReadAllCommand<RubroModel>().ReadAll(() => new RubroModel().ReadAll(), new RubroModel() { Id = 0, Nombre = "--Seleccionar--"});
            _listStore.perfiles = new ReadAllCommand<PerfilModel>().ReadAll(() => new PerfilModel().ReadAll(), new PerfilModel() { Id = 0, Nombre = "--Seleccionar--"});
            _listStore.estados = new ReadAllCommand<EstadoModel>().ReadAll(() => new EstadoModel().ReadAll(), new EstadoModel() { Id = 0, Nombre = "--Seleccionar--"});

            #endregion


            PerfilModel rol = new PerfilModel(CurrentAccount.Perfil.Id);
            LogUser += CurrentAccount.Nombre + " " + CurrentAccount.ApellidoP + "(" + rol.Nombre + ")";
            _frameStore.CurrentViewModelChanged += OnCurrentPanelChanged;

            NavigationUser = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore,CurrentAccount,_listStore));
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore,CurrentAccount, _listStore));
            NavigationClient = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore,CurrentAccount,_listStore));
            NavigationRol = new NavigatePanelCommand<AdminRolViewModel>(_frameStore, () => new AdminRolViewModel(_frameStore,CurrentAccount,_listStore));
            LogoutCommand = new LogoutCommand(_navigationStore, _accountStore, new ListStore());
        }

        public ICommand NavigationUnit { get; }
        public ICommand NavigationUser { get; }
        public ICommand NavigationClient { get; }
        public ICommand NavigationRol { get; }
        public ICommand LogoutCommand { get; }

        private void OnCurrentPanelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

    }
}
