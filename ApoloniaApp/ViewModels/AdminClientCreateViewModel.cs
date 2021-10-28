﻿using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminClientCreateViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;

        public UsuarioInterno CurrentAccount;

        private UsuarioInterno _newUser = new UsuarioInterno()
        {

            IdPerfil = 1,
            IdEstado = 1,
            Password = "1234"
        };

        #region Property
        //public string Run
        //{
        //    get { return _newUser.Run; }
        //    set
        //    {
        //        _newUser.Run = value;
        //        Username = value.Replace(".", "").Replace("-", "");
        //        OnPropertyChanged("Run");
        //    }
        //}
        public string Nombre
        {
            get { return _newUser.Nombres; }
            set
            {
                _newUser.Nombres = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string ApellidoP
        {
            get { return _newUser.ApellidoP; }
            set
            {
                _newUser.ApellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get { return _newUser.ApellidoM; }
            set
            {
                _newUser.ApellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get { return _newUser.Email; }
            set
            {
                _newUser.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return _newUser.Password; }
            set
            {
                _newUser.Password = value;
                OnPropertyChanged("Password");
            }
        }

        //public int SubunidadId
        //{
        //    get { return _newUser.SubunidadId; }
        //    set
        //    {
        //        _newUser.SubunidadId = value;
        //        OnPropertyChanged("SubunidadId");
        //    }
        //}

        public int RolId
        {
            get { return _newUser.IdPerfil; }
            set
            {
                _newUser.IdPerfil = value;
                OnPropertyChanged("RolId");
            }
        }

        //public String UnidadId
        //{
        //    get { return _newUser.UnidadId; }
        //    set
        //    {
        //        _newUser.UnidadId = value;
        //        OnPropertyChanged("UnidadId");
        //    }
        //}

        public int Estado
        {
            get { return _newUser.IdEstado; }
            set
            {
                _newUser.IdEstado = value;
                OnPropertyChanged("Estado");
            }
        }

        //public string Username
        //{
        //    get { return _newUser.Username; }
        //    set
        //    {
        //        _newUser.Username = value;
        //        OnPropertyChanged("Username");
        //    }
        //}
        #endregion
        public AdminClientCreateViewModel(FrameStore frameStore, UsuarioInterno currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;

            NavigationUsers = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore, CurrentAccount));
            CreateUser = new CreateCommand<AdminClientViewModel,UsuarioInterno>(() => _newUser.Create(), () => new AdminClientViewModel(_frameStore, CurrentAccount), _frameStore,()=>_newUser.ReadByRun(),_newUser);
        }

        public ICommand NavigationUsers { get; }
        public ICommand CreateUser { get; }
    }
}
