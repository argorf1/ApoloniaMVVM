using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUserCreateViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;

        private int _selectedIndex;
        public ObservableCollection<PerfilModel> _perfiles;
        public IEnumerable<PerfilModel> Perfiles => _perfiles;
        private PerfilModel _selectedPerfil;

        private UsuarioInternoModel _newUser = new UsuarioInternoModel()
        {
            IdEstado = 1,
            Password = "1234"
        };

        #region Property

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                if (value > 0)
                {
                    _newUser.IdPerfil = _selectedIndex;
                }
                OnPropertyChanged("SelectedIndex");
            }
        }

        public PerfilModel SelectedPerfil
        {
            get { return _selectedPerfil; }
            set 
            {
                _selectedPerfil = _perfiles.ElementAt(SelectedIndex);
                OnPropertyChanged("SelectedPerfil");
            }
        }

        public string Run
        {
            get { return _newUser.Run; }
            set
            {
                _newUser.Run = value;
                OnPropertyChanged("Run");
            }
        }

        public string Nombres
        {
            get { return _newUser.Nombre; }
            set
            {
                _newUser.Nombre = value;
                OnPropertyChanged("Nombres");
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


        public int RolId
        {
            get { return _newUser.IdPerfil; }
            set
            {
                _newUser.IdPerfil = value;
                OnPropertyChanged("RolId");
            }
        }


        public int Estado
        {
            get { return _newUser.IdEstado; }
            set
            {
                _newUser.IdEstado = value;
                OnPropertyChanged("Estado");
            }
        }

        #endregion
        public AdminUserCreateViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;

            _perfiles = _listStore.perfiles;
            SelectedPerfil = Perfiles.ElementAt(SelectedIndex);

            NavigationUsers = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore, CurrentAccount, _listStore));
            CreateUser = new CRUDCommand<AdminUserViewModel, UsuarioInternoModel>(() => _newUser.Create(), () => new AdminUserViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _newUser.ReadByRun(), _newUser);
        }

        public ICommand NavigationUsers { get; }
        public ICommand CreateUser { get; }
    }
}
