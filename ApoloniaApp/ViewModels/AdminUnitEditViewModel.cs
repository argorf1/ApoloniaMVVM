﻿using ApoloniaApp.Commands;
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
        private Unidad _unidad;
        public Usuario CurrentAccount;

        public AdminUnitEditViewModel(FrameStore frameStore, Usuario currentAccount, Unidad unidad)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _unidad = unidad;

            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount));

        }

        public ICommand NavigationUnit { get; }
    }
}
