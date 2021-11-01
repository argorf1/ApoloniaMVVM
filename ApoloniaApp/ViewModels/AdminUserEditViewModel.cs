using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUserEditViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;

        private int _selectedPerfilIndex;
        private int _selectedEstadoIndex;
        public ObservableCollection<PerfilModel> _perfiles;
        public IEnumerable<PerfilModel> Perfiles => _perfiles;
        public ObservableCollection<EstadoModel> _estados;
        public IEnumerable<EstadoModel> Estados => _estados;

        private PerfilModel _selectedPerfil;
        private EstadoModel _selectedEstado;

        private UsuarioInternoModel _editUser;

        #region Property

        public string Run
        {
            get { return _editUser.Run; }
            set
            {
                _editUser.Nombres = value;
                OnPropertyChanged("Run");
            }
        }

        public string Nombres
        {
            get { return _editUser.Nombres; }
            set
            {
                _editUser.Nombres = value;
                OnPropertyChanged("Nombres");
            }
        }

        public string ApellidoP
        {
            get { return _editUser.ApellidoP; }
            set
            {
                _editUser.ApellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get { return _editUser.ApellidoM; }
            set
            {
                _editUser.ApellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get { return _editUser.Email; }
            set
            {
                _editUser.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return _editUser.Password; }
            set
            {
                _editUser.Password = value;
                OnPropertyChanged("Password");
            }
        }


        public int RolId
        {
            get { return _editUser.IdPerfil; }
            set
            {
                _editUser.IdPerfil = value;
                OnPropertyChanged("RolId");
            }
        }


        public int Estado
        {
            get { return _editUser.IdEstado; }
            set
            {
                _editUser.IdEstado = value;
                OnPropertyChanged("Estado");
            }
        }

        public int SelectedPerfilIndex
        {
            get { return _selectedPerfilIndex; }
            set
            {
                _selectedPerfilIndex = value;
                if (value > 0)
                {
                    _editUser.IdPerfil = _selectedPerfilIndex;
                }
                OnPropertyChanged("SelectedPerfilIndex");
            }
        }

        public PerfilModel SelectedPerfil
        {
            get { return _selectedPerfil; }
            set
            {
                _selectedPerfil = _perfiles.ElementAt<PerfilModel>(SelectedPerfilIndex);
                OnPropertyChanged("SelectedPerfil");
            }
        }

        public int SelectedEstadoIndex
        {
            get { return _selectedEstadoIndex; }
            set
            {
                _selectedEstadoIndex = value;
                if (value > 0)
                {
                    _editUser.IdEstado = _selectedEstadoIndex;
                }
                OnPropertyChanged("SelectedEstadoIndex");
            }
        }

        public EstadoModel SelectedEstado
        {
            get { return _selectedEstado; }
            set
            {
                _selectedEstado = _estados.ElementAt<EstadoModel>(SelectedEstadoIndex);
                OnPropertyChanged("SelectedEstado");
            }
        }

        #endregion
        public AdminUserEditViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, UsuarioInternoModel editUser)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _editUser = editUser;

            _perfiles = new ObservableCollection<PerfilModel>();
            _perfiles.Add(new PerfilModel() { Id = 0, Detalle = "--Seleccionar--" });
            foreach (PerfilModel p in new PerfilModel().ReadAll())
            {
                _perfiles.Add(p);
            }

            _estados = new ObservableCollection<EstadoModel>();
            _estados.Add(new EstadoModel() { Id = 0, Detalle = "--Seleccionar--" });
            foreach (EstadoModel e in new EstadoModel().ReadAll())
            {
                _estados.Add(e);
            }

            #region Combobox Constructor
            SelectedPerfilIndex = _editUser.IdPerfil;
            SelectedEstadoIndex = _editUser.IdEstado;
            SelectedPerfil = Perfiles.ElementAt<PerfilModel>(SelectedPerfilIndex);
            SelectedEstado = Estados.ElementAt<EstadoModel>(SelectedEstadoIndex);
            #endregion

            NavigationUsers = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore, _editUser));
            EditUser = new CRUDCommand<AdminUserViewModel, UsuarioInternoModel>(() => _editUser.Update(), () => new AdminUserViewModel(_frameStore, CurrentAccount), _frameStore, _editUser);
        }

        public ICommand NavigationUsers { get; }
        public ICommand EditUser { get; }
    }
}
