﻿using ApoloniaApp.Commands;
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
        public ProcessDesignerViewModel(NavigationStore navigationStore, AccountStore accountStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _frameStore = new FrameStore();
            CurrentAccount = accountStore.CurrentAccount;

            PerfilModel rol = new PerfilModel(CurrentAccount.IdPerfil);
            LogUser += CurrentAccount.Nombre + " " + CurrentAccount.ApellidoP + "(" + rol.Detalle + ")";
            _frameStore.CurrentViewModelChanged += OnCurrentPanelChanged;

            NavigationProcesos = new NavigatePanelCommand<DPProcesosViewModel>(_frameStore, () => new DPProcesosViewModel(_frameStore,CurrentAccount));
            NavigationMonitoreo = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount));
            LogoutCommand = new LogoutCommand(_navigationStore, _accountStore);
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
