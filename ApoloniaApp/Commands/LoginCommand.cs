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

        public LoginCommand(LoginViewModel viewModel, NavigationStore navigationStore, AccountStore accountStore)
        {
            _viewModel = viewModel;
            _navigationStore = navigationStore;
            _accountStore = accountStore;
        }

        public override void Execute(object parameter)
        {
            UsuarioInterno acc = new UsuarioInterno()
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
                        _navigationStore.CurrentViewModel=new AdminViewModel(_navigationStore,_accountStore);
                        break;
                    case 2:
                        _navigationStore.CurrentViewModel = new ProcessDesignerViewModel(_navigationStore);
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
