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
    class ProcessDesignerViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        public ViewModelBase CurrentViewModel => _frameStore.CurrentViewModel;
        public UsuarioInternoModel CurrentAccount;

        private string _logUser;
        public string LogUser
        {
            get { return _logUser; }
            set
            {
                _logUser = value;
                OnPropertyChanged("LogUser");
            }

        }
        public ProcessDesignerViewModel(NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;
            _frameStore = new FrameStore();
            CurrentAccount = accountStore.CurrentAccount;

            _listStore.procesos = new ReadAllCommand<ProcesoModel>().ReadAll(() => new ProcesoModel().ReadAll());
            _listStore.tareas = new ReadAllCommand<TareaModel>().ReadAll(() => new TareaModel().ReadAll());
            _listStore.dependencias = new ReadAllCommand<DependenciaModel>().ReadAll(() => new DependenciaModel().ReadAll());
            _listStore.responsables = new ReadAllCommand<ResponsableModel>().ReadAll(() => new ResponsableModel().ReadAll());
            _listStore.unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => new UnidadModel().ReadAll());
            _listStore.subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => new SubUnidadModel().ReadAll());
            _listStore.funcionarios = new ReadAllCommand<FuncionarioModel>().ReadAll(()=> new FuncionarioModel().ReadAll());
            _listStore.roles = new ReadAllCommand<RolModel>().ReadAll(()=> new RolModel().ReadAll());

            PerfilModel rol = new PerfilModel(CurrentAccount.Perfil.Id);
            LogUser += CurrentAccount.Nombre + " " + CurrentAccount.ApellidoP + "(" + rol.Nombre + ")";
            _frameStore.CurrentViewModelChanged += OnCurrentPanelChanged;

            NavigationProcesos = new NavigatePanelCommand<DPProcesosViewModel>(_frameStore, () => new DPProcesosViewModel(_frameStore,CurrentAccount,_listStore));
            NavigationMonitoreo = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount,_listStore));
            LogoutCommand = new LogoutCommand(_navigationStore, _accountStore, _listStore);
        }

        public ICommand NavigationMonitoreo { get; }
        public ICommand NavigationProcesos { get; }
        public ICommand LogoutCommand { get; }

        private void OnCurrentPanelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
