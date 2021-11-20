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

        private UsuarioInternoModel _crudUsuario;
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


            }
        }

        public UsuarioInternoModel SelectedUsuario
        {
            get => _crudUsuario;
            set
            {
                _crudUsuario = value;
                OnPropertyChanged("SelectedUsuario");

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
            get => _crudUsuario.Run;
            set
            {
                _crudUsuario.Run = value;
                OnPropertyChanged("Run");
            }
        }
        public string Nombre
        {
            get => _crudUsuario.Nombre;
            set
            {
                _crudUsuario.Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string NombreCompleto
        {
            get => _crudUsuario.NombreCompleto;
            set
            {
                OnPropertyChanged("NombreCompleto");
            }
        }

        public string ApellidoP
        {
            get => _crudUsuario.ApellidoP;
            set
            {
                _crudUsuario.ApellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get => _crudUsuario.ApellidoM; 
            set
            {
                _crudUsuario.ApellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get => _crudUsuario.Email;
            set
            {
                _crudUsuario.Email = value;
                OnPropertyChanged("Email");
            }
        }

        

        public string Perfil
        {
            get => _crudUsuario.Perfil.Nombre;
            set
            {
                OnPropertyChanged("RolId");
            }
        }

        

        public string Estado
        {
            get => _crudUsuario.Estado.Nombre;
            set
            {
                _crudUsuario.Estado.Nombre = value;
                OnPropertyChanged("Estado");
            }
        }

        #endregion

        public AdminUserViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudUsuario = new UsuarioInternoModel();
            _usuarios = _listStore.usuarios;
            _selectedIndex = -1;
            NavigationCreateUsers = new NavigatePanelCommand<AdminUserCRUDViewModel>(_frameStore, () => new AdminUserCRUDViewModel(_frameStore, CurrentAccount, _listStore,new UsuarioInternoModel(),1));
            NavigationEditUsers = new NavigatePanelCommand<AdminUserCRUDViewModel>(_frameStore, () => new AdminUserCRUDViewModel(_frameStore, CurrentAccount, _listStore, _crudUsuario, 2));

        }

        public ICommand NavigationCreateUsers { get; }
        public ICommand NavigationEditUsers { get; }
    }
}
