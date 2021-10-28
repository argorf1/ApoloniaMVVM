using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUnitViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;

        public UsuarioInterno CurrentAccount;

        public AdminUnitViewModel(FrameStore frameStore, UsuarioInterno currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;

            NavigationCreateUnit = new NavigatePanelCommand<AdminUnitCreateViewModel>(_frameStore, () => new AdminUnitCreateViewModel(_frameStore, CurrentAccount));
            NavigationEditUnit = new NavigatePanelCommand<AdminUnitEditViewModel>(_frameStore, () => new AdminUnitEditViewModel(_frameStore, /*Cambiar esto por el usuario seleccionado*/CurrentAccount, new Unidad()));

        }

        public ICommand NavigationCreateUnit { get; }
        public ICommand NavigationEditUnit { get; }
    }
}
