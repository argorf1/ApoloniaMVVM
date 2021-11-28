using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUserCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private UsuarioInternoModel _crudUsuario;

        #region Configuracion Vista Estado

        Dictionary<int, string> _estadoDetalle = new Dictionary<int, string>();
        private int _estado = 0;


        public string EstadoView
        {
            get { return _estadoDetalle[_estado]; }
            set
            {
                OnPropertyChanged("EstadoView");
            }
        }
        public string EstadoVisibility
        {
            get
            {
                if (_estado == 1)
                    return "collapsed";
                else
                    return "visible";
            }
            set { OnPropertyChanged("EstadoVisibility"); }
        }

        public bool EstadoEdit
        {
            get => _estado == 2;
        }
        #endregion
        #region Validation Properties
        private List<Func<bool>> _validations;

        public bool CanCrud
        {
            get => ValidationService.All(_validations);
            set
            {
                OnPropertyChanged("CanCrud");
            }
        }
        #endregion
        public ICommand ReturnCommand { get; }
        public ICommand CrudUser { get; }

        public AdminUserCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore, UsuarioInternoModel crudUsuario, int estado, AdminViewModel mainView)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudUsuario = crudUsuario;

            mainView.IsCheck = false;

            #region Configuracion Estado
            _estado = estado;

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Usuario");
            _estadoDetalle.Add(2, "Modificar Usuario");
            #endregion
            

            _validations = new List<Func<bool>>();
            switch (_estado)
            {
                case 1:
                    _crudUsuario.Password = PasswordGeneratorService.CreateRandomPassword(15);
                    CrudUser = new CRUDCommand<AdminUserViewModel, UsuarioInternoModel>(() => _crudUsuario.Create(), () => new AdminUserViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, () => _crudUsuario.ReadByRun(), _crudUsuario, () => _listStore.Usuarios(), 1);
                    #region Carga Validaciones
                    _validations.AddRange(new List<Func<bool>>()
                    {
                        ()=> ValidationService.Run(Run),
                        ()=> ValidationService.Text(Nombres),
                        ()=> ValidationService.Text(ApellidoP),
                        ()=> ValidationService.Text(ApellidoM),
                        ()=> ValidationService.Email(Email),
                        ()=> ValidationService.ComboBoxId(_crudUsuario.Perfil.Id)
                    });
                    #endregion
                    break;
                case 2:
                    CrudUser = new CRUDCommand<AdminUserViewModel, UsuarioInternoModel>(() => _crudUsuario.Update(), () => new AdminUserViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, _crudUsuario, () => _listStore.Usuarios(), 2);
                    #region Carga Validaciones
                    _validations.AddRange(new List<Func<bool>>()
                    {
                        ()=> ValidationService.Run(Run),
                        ()=> ValidationService.Text(Nombres),
                        ()=> ValidationService.Text(ApellidoP),
                        ()=> ValidationService.Text(ApellidoM),
                        ()=> ValidationService.Email(Email),
                        ()=> ValidationService.ComboBoxId(_crudUsuario.Perfil.Id),
                        ()=> ValidationService.ComboBoxId(_crudUsuario.Estado.Id),
                        ()=> ValidationService.Password(Password),
                        ()=> ValidationService.Match(Password,PasswordConfirm)
                    });
                    #endregion
                    break;
                default:
                    break;
            }

            ReturnCommand = new NavigatePanelCommand<AdminUserViewModel>(_frameStore, () => new AdminUserViewModel(_frameStore, CurrentAccount, _listStore, mainView)); 
            #endregion

            #region Carga Listas
            _perfiles = _listStore.perfiles;
            _estados = _listStore.estados;

            SelectedPerfil = _perfiles.First(p => p.Id == _crudUsuario.Perfil.Id);
            SelectedEstado = _estados.First(p => p.Id == _crudUsuario.Estado.Id);
            #endregion


        }


        #region Properties

        private ObservableCollection<PerfilModel> _perfiles;
        public IEnumerable<PerfilModel> Perfiles => _perfiles;

        private bool _validPerfil = false;
        public bool ValidPerfil
        {
            get => _validPerfil;
            set
            {
                _validPerfil = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidPerfil");
            }
        }

        public PerfilModel SelectedPerfil
        {

            get { return _crudUsuario.Perfil; }
            set
            {
                _crudUsuario.Perfil = value;
                ValidPerfil = ValidationService.ComboBoxId(_crudUsuario.Perfil.Id);
                OnPropertyChanged("SelectedPerfil");
            }
        }

        private ObservableCollection<EstadoModel> _estados;
        public IEnumerable<EstadoModel> Estados => _estados;

        private bool _validEstado = false;
        public bool ValidEstado
        {
            get => _validEstado;
            set
            {
                _validEstado = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidEstado");
            }
        }


        public EstadoModel SelectedEstado
        {

            get { return _crudUsuario.Estado; }
            set
            {
                _crudUsuario.Estado = value;
                ValidEstado = ValidationService.ComboBoxId(_crudUsuario.Estado.Id);
                OnPropertyChanged("SelectedEstado");
            }
        }

        private bool _validRun = false;
        public bool ValidRun
        {
            get => _validRun;
            set
            {
                _validRun = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidRun");
            }
        }

        public string Run
        {
            get { return _crudUsuario.Run; }
            set
            {
                _crudUsuario.Run = value;

                ValidRun = ValidationService.Run(Run);
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

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidNombre");
            }
        }

        public string Nombres
        {
            get { return _crudUsuario.Nombre; }
            set
            {
                _crudUsuario.Nombre = value;

                ValidationService.Text(Nombres);
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

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidApellidoP");
            }
        }

        public string ApellidoP
        {
            get { return _crudUsuario.ApellidoP; }
            set
            {
                _crudUsuario.ApellidoP = value;

                _validApellidoP = ValidationService.Text(ApellidoP);
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

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidApellidoM");
            }
        }

        public string ApellidoM
        {
            get { return _crudUsuario.ApellidoM; }
            set
            {
                _crudUsuario.ApellidoM = value;

                _validApellidoM = ValidationService.Text(ApellidoM);
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

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidEmail");
            }
        }

        public string Email
        {
            get { return _crudUsuario.Email; }
            set
            {
                _crudUsuario.Email = value;

                _validEmail = ValidationService.Email(Email);
                OnPropertyChanged("Email");
            }
        }

        private bool _validPassword = false;
        public bool ValidPassword
        {
            get => _validPassword;
            set
            {
                _validPassword = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidPassword");
            }
        }

        public string Password
        {
            get { return _crudUsuario.Password; }
            set
            {
                _crudUsuario.Password = value;

                _validPassword = ValidationService.Password(Password);
                OnPropertyChanged("Password");
            }
        }

        private bool _validPasswordCon = false;
        public bool ValidPasswordCon
        {
            get => _validPasswordCon;
            set
            {
                _validPasswordCon = value;

                OnPropertyChanged("CanCrud");
                OnPropertyChanged("ValidPasswordCon");
            }
        }

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get => _passwordConfirm;
            set
            {
                _passwordConfirm = value;

                ValidPasswordCon = ValidationService.Match(Password, PasswordConfirm);
                OnPropertyChanged("PasswordConfirm");
            }
        }
        #endregion
    }
}
