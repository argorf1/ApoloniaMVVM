using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUserViewModel : ViewModelBase
    {
        private int _selectedIndex;

        private string _run;
        private string _nombre;
        private string _apellidoP;
        private string _apellidoM;
        private string _email;
        private int _idPerfil;
        private int _idEstado;
        private UsuarioInterno user;
        private readonly FrameStore _frameStore;
        private readonly ObservableCollection<UsuarioInterno> _usuarios;
        public IEnumerable<UsuarioInterno> Usuarios => _usuarios;
        public UsuarioInterno CurrentAccount;

        #region Property

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");

                OnPropertyChanged("Run");
                OnPropertyChanged("Nombre");
                OnPropertyChanged("ApellidoP");
                OnPropertyChanged("ApellidoM");
                OnPropertyChanged("Email");
                OnPropertyChanged("SubunidadId");
                OnPropertyChanged("RolId");
                OnPropertyChanged("UnidadId");
                OnPropertyChanged("Estado");
                OnPropertyChanged("Username");

            }
        }
        public string Run
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    user.Run = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Run;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Run;
                }
                else
                {   
                    return _run;
                }
            }
            set
            {
                _run = value;
                OnPropertyChanged("Run");
            }
        }
        public string Nombre
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    user.Nombres = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Nombres;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Nombres;
                }
                else
                {
                    return _nombre;
                }
            }
            set
            {
                _nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string ApellidoP
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    user.ApellidoP = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).ApellidoP;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).ApellidoP;
                }
                else
                {
                    return _apellidoP;
                }
            }
            set
            {
                _apellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    user.ApellidoM = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).ApellidoM;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).ApellidoM;
                }
                else
                {
                    return _apellidoM;
                }
            }
            set
            {
                _apellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    user.Email = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Email;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Email;
                }
                else
                {
                    return _email;
                }
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        

        public int IdPerfil
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    user.IdPerfil = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).IdPerfil;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).IdPerfil;
                }
                else
                {
                    return _idPerfil;
                }
            }
            set
            {
                _idPerfil = value;
                OnPropertyChanged("RolId");
            }
        }

        

        public int IdEstado
        {
            get
            {
                if (SelectedIndex > -1)
                {

                    user.IdEstado = Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).IdEstado;
                    return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).IdEstado;
                }
                else
                {
                    return _idEstado;
                }
            }
            set
            {
                _idEstado = value;
                OnPropertyChanged("Estado");
            }
        }

        #endregion

        public AdminUserViewModel(FrameStore frameStore, UsuarioInterno currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            user = new UsuarioInterno();
            _usuarios = new ObservableCollection<UsuarioInterno>();
            _selectedIndex = -1;
            foreach (UsuarioInterno user in new UsuarioInterno().ReadAll())
            {
                _usuarios.Add(user);
            }
            NavigationCreateUsers = new NavigatePanelCommand<AdminUserCreateViewModel>(_frameStore, () => new AdminUserCreateViewModel(_frameStore, CurrentAccount));
            NavigationEditUsers = new NavigatePanelCommand<AdminUserEditViewModel>(_frameStore, () => new AdminUserEditViewModel(_frameStore, new UsuarioInterno(), CurrentAccount));

        }

        public ICommand NavigationCreateUsers { get; }
        public ICommand NavigationEditUsers { get; }
    }
}
