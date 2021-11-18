using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly AccountStore _accountStore;
        private readonly ListStore _listStore;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;
        public UsuarioInternoModel CurrentAccount => _accountStore.CurrentAccount;


        public MainViewModel(NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _accountStore.CurrentAccountChanged += OnCurrentAccountChanged;

        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        private void OnCurrentAccountChanged()
        {
            OnPropertyChanged(nameof(CurrentAccount));
        }
    }
}
