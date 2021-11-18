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
        private string _perfil;
        private string _estado;
        private UsuarioInternoModel _editUser;
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        
        private bool _canEdit = false;
        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                _canEdit = value;
                OnPropertyChanged("CanEdit");
            }
        }
        private readonly ObservableCollection<UsuarioInternoModel> _usuarios;
        public IEnumerable<UsuarioInternoModel> Usuarios => _usuarios;
        public UsuarioInternoModel CurrentAccount;

        #region Property

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;

                CanEdit = _selectedIndex >= 0;
                OnPropertyChanged("SelectedIndex");

                OnPropertyChanged("Run");
                OnPropertyChanged("Nombre");
                OnPropertyChanged("ApellidoP");
                OnPropertyChanged("ApellidoM");
                OnPropertyChanged("Email");
                OnPropertyChanged("Perfil");
                OnPropertyChanged("Estado");

            }
        }
        public string Run
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    _editUser.Run = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).Run;
                    return _editUser.Run;
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
                    _editUser.Nombre = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).Nombre;
                    return _editUser.Nombre;
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
                    _editUser.ApellidoP = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).ApellidoP;
                    return _editUser.ApellidoP;
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
                    _editUser.ApellidoM = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).ApellidoM;
                    return _editUser.ApellidoM;
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
                    _editUser.Email = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).Email;
                    return _editUser.Email;
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

        

        public string Perfil
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    _editUser.PerfilDet = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).PerfilDet;
                    _editUser.IdPerfil = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).IdPerfil;
                    return _editUser.PerfilDet;
                }
                else
                {
                    return _perfil;
                }
            }
            set
            {
                _perfil = value;
                OnPropertyChanged("RolId");
            }
        }

        

        public string Estado
        {
            get
            {
                if (SelectedIndex > -1)
                {

                    _editUser.EstadoDet = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).EstadoDet;
                    _editUser.IdEstado = Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).IdEstado;
                    return _editUser.EstadoDet;
                }
                else
                {
                    return _estado;
                }
            }
            set
            {
                _estado = value;
                OnPropertyChanged("Estado");
            }
        }

        #endregion

        public AdminUserViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _editUser = new UsuarioInternoModel();
            _usuarios = _listStore.usuarios;
            _selectedIndex = -1;
            NavigationCreateUsers = new NavigatePanelCommand<AdminUserCreateViewModel>(_frameStore, () => new AdminUserCreateViewModel(_frameStore, CurrentAccount, _listStore));
            NavigationEditUsers = new NavigatePanelCommand<AdminUserEditViewModel>(_frameStore, () => new AdminUserEditViewModel(_frameStore, CurrentAccount, _editUser, _listStore));

        }

        public ICommand NavigationCreateUsers { get; }
        public ICommand NavigationEditUsers { get; }
    }
}
