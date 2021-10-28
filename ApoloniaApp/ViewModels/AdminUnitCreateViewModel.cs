using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUnitCreateViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;

        public UsuarioInterno CurrentAccount;

        public AdminUnitCreateViewModel(FrameStore frameStore, UsuarioInterno currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;

            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount));

        }

        public ICommand NavigationUnit { get; }
    }
}
