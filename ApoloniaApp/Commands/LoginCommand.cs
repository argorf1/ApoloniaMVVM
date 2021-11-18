using ApoloniaApp.Models;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using ApoloniaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Commands
{
    public class LoginCommand : CommandBase
    {
        private readonly LoginViewModel _viewModel;
        private NavigationStore _navigationStore;
        private AccountStore _accountStore;
        private ListStore _listStore;

        public LoginCommand(LoginViewModel viewModel, NavigationStore navigationStore, AccountStore accountStore, ListStore listStore)
        {
            _viewModel = viewModel;
            _navigationStore = navigationStore;
            _accountStore = accountStore;
            _listStore = listStore;
        }

        public override void Execute(object parameter)
        {
            UsuarioInternoModel acc = new UsuarioInternoModel()
            {
                Run = _viewModel.Username,
                Password = _viewModel.Password
            };
            if(acc.Read_Login())
            {
                _accountStore.CurrentAccount = acc;
                switch (acc.IdPerfil)
                {
                    case 1:
                        _navigationStore.CurrentViewModel=new AdminViewModel(_navigationStore,_accountStore,_listStore);
                        break;
                    case 2:
                        _navigationStore.CurrentViewModel = new ProcessDesignerViewModel(_navigationStore, _accountStore, _listStore);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
