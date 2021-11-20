using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUserEditViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;

        #region Validation Properties
        private List<Func<bool>> _validations;
        private bool _canCrud = false;

        public bool CanCrud
        {
            get => _canCrud;
            set
            {
                _canCrud = value;
                OnPropertyChanged("CanCrud");
            }
        }
        #endregion

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

        private bool _validRun = false;
        public bool ValidRun
        {
            get => _validRun;
            set
            {
                _validRun = value;
                OnPropertyChanged("ValidRun");
            }
        }

        public string Run
        {
            get { return _editUser.Run; }
            set
            {
                _editUser.Run = value;

                ValidRun = ValidationService.Run(value);
                OnPropertyChanged("Run");
            }
        }

        private bool _validNombre = false;
        public bool ValidNombre
        {
            get => _validNombre;
            set
            {
                _validNombre = value;
                OnPropertyChanged("ValidNombre");
            }
        }

        public string Nombres
        {
            get { return _editUser.Nombre; }
            set
            {
                _editUser.Nombre = value;
                OnPropertyChanged("Nombres");
            }
        }

        private bool _validApellidoP = false;
        public bool ValidApellidoP
        {
            get => _validApellidoP;
            set
            {
                _validApellidoP = value;
                OnPropertyChanged("ValidApellidoP");
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

        private bool _validApellidoM = false;
        public bool ValidApellidoM
        {
            get => _validApellidoM;
            set
            {
                _validApellidoM = value;
                OnPropertyChanged("ValidApellidoM");
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

        private bool _validEmail = false;
        public bool ValidEmail
        {
            get => _validEmail;
            set
            {
                _validEmail = value;
                OnPropertyChanged("ValidEmail");
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
        private bool _validEstado = false;
        public bool ValidEstado
        {
            get => _validEstado;
            set
            {
                _validEstado = value;
                OnPropertyChanged("ValidEstado");
            }
        }
        public int Estado
        {
            get { return _editUser.Estado.Id; }
            set
            {
                _editUser.Estado.Id = value;
                OnPropertyChanged("Estado");
            }
        }


        public PerfilModel SelectedPerfil
        {
            get { return _selectedPerfil; }
            set
            {
                _selectedPerfil = value;
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
                    _editUser.Estado.Id = _selectedEstadoIndex;
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
        public AdminUserEditViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, UsuarioInternoModel editUser, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _editUser = editUser;

            _perfiles = _listStore.perfiles;
            _estados = _listStore.estados;

            #region Combobox Constructor

            SelectedPerfil = Perfiles.First(p => p.Id == _editUser.Perfil.Id);
            SelectedEstado = Estados.First(p => p.Id == _editUser.Estado.Id);
            #endregion

            #region CargaValidaciones
            _validations = new List<Func<bool>>();
            _validations.AddRange(new List<Func<bool>>()
            {
                ()=>ValidationService.Run(Run),
                ()=>ValidationService.Text(Nombres),
                ()=>ValidationService.Text(ApellidoP),
                ()=>ValidationService.Text(ApellidoM),
                ()=>ValidationService.Email(Email),
                () =>ValidationService.ComboBoxId(_selectedPerfil.Id)
            });
            #endregion

            NavigationUsers = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore, _editUser, _listStore));
            //EditUser = new CRUDCommand<AdminUserViewModel, UsuarioInternoModel>(() => _editUser.Update(), () => new AdminUserViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _editUser);
        }

        public ICommand NavigationUsers { get; }
        public ICommand EditUser { get; }

    }
}
