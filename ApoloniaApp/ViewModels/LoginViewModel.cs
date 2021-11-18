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
    public class LoginViewModel : ViewModelBase
    {
        private readonly AccountStore _accountStore;
        private readonly ListStore _listStore;
        public UsuarioInternoModel CurrentAccount => _accountStore.CurrentAccount;

        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(AccountStore accountStore, NavigationStore navigationStore, ListStore listStore)
        {
            LoginCommand = new LoginCommand(this, navigationStore, accountStore, listStore);
            _listStore = listStore;
        }
    }
}
