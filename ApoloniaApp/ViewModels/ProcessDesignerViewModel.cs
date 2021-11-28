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

        private PerfilModel rol;
        public string LogUser
        {
            get => (CurrentAccount.Nombre + " " + CurrentAccount.ApellidoP + " ( " + rol.Nombre + " )");
            set
            {
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


        public ProcessDesignerViewModel(NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;
            _frameStore = new FrameStore();
            CurrentAccount = accountStore.CurrentAccount;

            _listStore.PDView();

            rol = new PerfilModel(CurrentAccount.Perfil.Id);
            LogUser += CurrentAccount.Nombre + " " + CurrentAccount.ApellidoP + "(" + rol.Nombre + ")";
            _frameStore.CurrentViewModelChanged += OnCurrentPanelChanged;

            NavigationProcesos = new NavigatePanelCommand<DPProcesosViewModel>(_frameStore, () => new DPProcesosViewModel(_frameStore,CurrentAccount,_listStore,this));
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
