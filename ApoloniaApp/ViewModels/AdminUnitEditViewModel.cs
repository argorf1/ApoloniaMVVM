using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUnitEditViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private UnidadModel _unidad;
        public UsuarioInternoModel CurrentAccount;

        public AdminUnitEditViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, UnidadModel unidad)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _unidad = unidad;

            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount));

        }

        public ICommand NavigationUnit { get; }
    }
}
