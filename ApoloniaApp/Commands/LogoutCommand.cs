using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using ApoloniaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Commands
{
    class LogoutCommand : CommandBase
    {
        private NavigationStore _navigationStore;
        private AccountStore _accountStore;
        private ListStore _listStore;

        public LogoutCommand(NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;
        }

        public override void Execute(object parameter)
        {
            _accountStore.CurrentAccount = null;
            _navigationStore.CurrentViewModel = new LoginViewModel(_accountStore, _navigationStore, _listStore);
        }
    }
}
