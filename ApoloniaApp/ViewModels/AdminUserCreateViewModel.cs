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
        private int _selectedIndex;
        public UsuarioInterno CurrentAccount;
        public ObservableCollection<Perfil> _perfiles;
        public IEnumerable<Perfil> Perfiles => _perfiles;
        private Perfil _selectedPerfil;
        private UsuarioInterno _newUser = new UsuarioInterno()
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

        public Perfil SelectedPerfil
        {
            get { return _selectedPerfil; }
            set 
            {
                _selectedPerfil = _perfiles.ElementAt<Perfil>(SelectedIndex);
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
        public AdminUserCreateViewModel(FrameStore frameStore, UsuarioInterno currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _perfiles = new ObservableCollection<Perfil>();
            _perfiles.Add(new Perfil() { Id = 0, Detalle = "--Seleccionar--" });
            foreach (Perfil p in new Perfil().ReadAll())
            {
                _perfiles.Add(p);
            }
            SelectedIndex = 0;
            SelectedPerfil = Perfiles.ElementAt<Perfil>(SelectedIndex);
            NavigationUsers = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore, CurrentAccount));
            CreateUser = new CreateCommand<AdminUserViewModel,UsuarioInterno>(() => _newUser.Create(), () => new AdminUserViewModel(_frameStore, CurrentAccount), _frameStore,()=>_newUser.ReadByRun(),_newUser);
        }

        public ICommand NavigationUsers { get; }
        public ICommand CreateUser { get; }
    }
}
