using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUserEditViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public Usuario CurrentAccount;
        private Usuario _usuario;

        public AdminUserEditViewModel(FrameStore frameStore, Usuario usuario, Usuario currentAccount)
        {
            _frameStore = frameStore;
            _usuario = usuario;
            CurrentAccount = currentAccount;

            NavigationUsers = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore, CurrentAccount));

        }

        public ICommand NavigationUsers { get; }
    }
}
