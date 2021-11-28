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

        private PerfilModel rol;
        public string LogUser
        {
            get => (CurrentAccount.Nombre + " " + CurrentAccount.ApellidoP + " ( " + rol.Nombre + " )");
            set {
                OnPropertyChanged("LogUser"); 
            }

        }
        private bool _isCheck;
        public bool IsCheck
        {
            get => _isCheck;
            set
            {
                _isCheck = value;

                Collapse = _isCheck ? "Collapsed" : "Visible";
                OnPropertyChanged("IsCheck");
            }
        }
        private string _collapse;
        public string Collapse
        {
            get => _collapse;
            set
            {
                _collapse = value;

                OnPropertyChanged("Collapse");
            }
        }
        public AdminViewModel(NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;
            _frameStore = new FrameStore();
            CurrentAccount = accountStore.CurrentAccount;

            _listStore.Adminview();


            rol = new PerfilModel(CurrentAccount.Perfil.Id);

            _frameStore.CurrentViewModelChanged += OnCurrentPanelChanged;

            NavigationUser = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore,CurrentAccount,_listStore, this));
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore,CurrentAccount, _listStore, this));
            NavigationClient = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore,CurrentAccount,_listStore, this));
            NavigationRol = new NavigatePanelCommand<AdminRolViewModel>(_frameStore, () => new AdminRolViewModel(_frameStore,CurrentAccount,_listStore, this));
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
