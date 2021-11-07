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
        
        public  ViewModelBase CurrentViewModel => _frameStore.CurrentViewModel;
        public UsuarioInternoModel CurrentAccount;

        private string _logUser;
        public string LogUser
        {
            get { return string.IsNullOrEmpty(CurrentAccount.PerfilDet).ToString(); }
            set {
                _logUser = value;
                OnPropertyChanged("LogUser"); 
            }

        }
        public AdminViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _frameStore = new FrameStore();
            CurrentAccount = accountStore.CurrentAccount;

            PerfilModel rol = new PerfilModel(CurrentAccount.IdPerfil);
            LogUser += CurrentAccount.Nombres + " " + CurrentAccount.ApellidoP + "(" + rol.Detalle + ")";
            _frameStore.CurrentViewModelChanged += OnCurrentPanelChanged;

            NavigationUser = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore,CurrentAccount));
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore,CurrentAccount));
            NavigationClient = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore,CurrentAccount));
            NavigationRol = new NavigatePanelCommand<AdminRolViewModel>(_frameStore, () => new AdminRolViewModel(_frameStore,CurrentAccount));
            LogoutCommand = new LogoutCommand(_navigationStore, _accountStore);
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
