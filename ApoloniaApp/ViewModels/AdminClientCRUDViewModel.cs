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
    class AdminClientCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private FuncionarioModel _crudUsuario;

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
                    return "hidden";
                else
                    return "visible";
            }
            set { OnPropertyChanged("EstadoVisibility"); }
        }

        public bool EstadoEdit
        {
            get => _estado == 2;
        }

        public bool _editing;
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

        public AdminClientCRUDViewModel(int estado, FrameStore frameStore, UsuarioInternoModel currentAccount, FuncionarioModel crudFuncionario, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudUsuario = crudFuncionario;


            #region Configuracion Estado

            _estado = estado;
            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Funcionario");
            _estadoDetalle.Add(2, "Modificar Funcionario");
            #endregion

            _validations = new List<Func<bool>>();
            switch (_estado)
            {
                case 1:
                    _crudUsuario.Password = PasswordGeneratorService.CreateRandomPassword(15);
                    CrudUser = new CRUDCommand<AdminClientViewModel, FuncionarioModel>(() => _crudUsuario.Create(), () => new AdminClientViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudUsuario.ReadByRun(), _crudUsuario, () => _listStore.Funcionarios(), 1);
                    #region Carga Validaciones
                    _validations.AddRange(new List<Func<bool>>()
                    {
                         ()=> ValidationService.Run(Run)
                        ,()=> ValidationService.Text(Nombre)
                        ,()=> ValidationService.Text(ApellidoP)
                        ,()=> ValidationService.Text(ApellidoM)
                        ,()=> ValidationService.Email(Email)
                        ,()=> ValidationService.Run(SelectedUnidad.Rut)
                        ,()=> ValidationService.ComboBoxId(SelectedSubunidad.Id)
                        ,()=> ValidationService.ComboBoxId(SelectedRol.Id)
                    });
                    #endregion
                    _editing = false;
                    break;
                case 2:
                    CrudUser = new CRUDCommand<AdminClientViewModel, FuncionarioModel>(() => _crudUsuario.Update(), () => new AdminClientViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudUsuario, () =>_listStore.Funcionarios(), 2);
                    #region Carga Validaciones
                    _validations.AddRange(new List<Func<bool>>()
                    {
                         ()=> ValidationService.Run(Run)
                        ,()=> ValidationService.Text(Nombre)
                        ,()=> ValidationService.Text(ApellidoP)
                        ,()=> ValidationService.Text(ApellidoM)
                        ,()=> ValidationService.Email(Email)
                        ,()=> ValidationService.Run(SelectedUnidad.Rut)
                        ,()=> ValidationService.ComboBoxId(SelectedSubunidad.Id)
                        ,()=> ValidationService.ComboBoxId(SelectedRol.Id)
                        ,()=> ValidationService.ComboBoxId(SelectedEstado.Id)
                        ,()=> ValidationService.Password(Password)
                        ,()=> ValidationService.Match(Password,PasswordConfirm)
                    });
                    #endregion
                    _editing = true;
                    break;
                default:
                    break;
            }
            #endregion

            #region Carga Listas

            _unidades = ChargeComboBoxService<UnidadModel>.ChargeComboBox(_listStore.unidades, _unidades, new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _subunidades = ChargeComboBoxService<SubUnidadModel>.ChargeComboBox(_listStore.subunidades, _subunidades, new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad" });
            _roles = ChargeComboBoxService<RolModel>.ChargeComboBox(_listStore.roles, _roles, new RolModel() { Id = 0, Nombre = "-- Rol --" });
            _estados = _listStore.estados;


            SelectedUnidad = _unidades.LastOrDefault(u => u.Rut == _crudUsuario.Unidad.Rut || u.Rut == "0");
            SelectedSubunidad = _subunidades.LastOrDefault(s => s.Id == _crudUsuario.Subunidad.Id || s.Id == 0);
            SelectedRol = _roles.LastOrDefault(r => r.Id == _crudUsuario.Rol.Id || r.Id == 0);
            SelectedEstado = _estados.First(e => e.Id == _crudUsuario.Estado.Id);
            #endregion

            NavigationUsers = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore, CurrentAccount, _listStore));

        }


        #region Funcionalidad Listas

        private ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;

        private bool _validUnidad = false;
        public bool ValidUnidad
        {
            get => _validUnidad;
            set
            {
                _validUnidad = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidUnidad");
            }
        }

        public UnidadModel SelectedUnidad
        {

            get { return _crudUsuario.Unidad; }
            set
            {
                _crudUsuario.Unidad = value;
                Subunidades = _subunidades.Where(s => s.RutUnidad == value.Rut || s.Id == 0);
                ValidUnidad = ValidationService.Run(Run);
                OnPropertyChanged("SelectedUnidad");
            }
        }

        private ObservableCollection<SubUnidadModel> _subunidades;
        private IEnumerable<SubUnidadModel> subunidades;

        private bool _validSubunidad = false;
        public bool ValidSubunidad
        {
            get => _validSubunidad;
            set
            {
                _validSubunidad = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidSubunidad");
            }
        }

        public IEnumerable<SubUnidadModel> Subunidades
        {
            get { return subunidades; }
            set
            {
                subunidades = value;
                if (!_editing) SelectedSubunidad = subunidades.First(p => p.Id == 0);
                OnPropertyChanged("Subunidades");
            }
        }
        public SubUnidadModel SelectedSubunidad
        {
            get { return _crudUsuario.Subunidad; }
            set
            {
                _crudUsuario.Subunidad = value != null ? value : new SubUnidadModel();
                Roles = _roles.Where(r => r.Subunidad.Id == _crudUsuario.Subunidad.Id || r.Id == 0);
                ValidSubunidad = ValidationService.ComboBoxId(SelectedSubunidad.Id);
                OnPropertyChanged("SelectedSubunidad");
            }
        }

        private ObservableCollection<RolModel> _roles;
        private IEnumerable<RolModel> roles;

        public IEnumerable<RolModel> Roles
        {
            get { return roles; }
            set
            {
                roles = value;
                if (!_editing) SelectedRol = roles.First(p => p.Id == 0);
                else _editing = false;
                OnPropertyChanged("Roles");
            }
        }
        private bool _validRol = false;
        public bool ValidRol
        {
            get => _validRol;
            set
            {
                _validRol = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidRol");
            }
        }

        public RolModel SelectedRol
        {
            get { return _crudUsuario.Rol; }
            set
            {
                _crudUsuario.Rol = value != null ? value : new RolModel();
                ValidRol = ValidationService.ComboBoxId(SelectedRol.Id);
                OnPropertyChanged("SelectedRol");
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
               ValidEstado = ValidationService.ComboBoxId(SelectedEstado.Id);
                OnPropertyChanged("SelectedUnidad");
            }
        }

        #endregion

        #region Property Funcionarios
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
                Username = value.Replace(".", "").Replace("-", "");
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

        public string Nombre
        {
            get { return _crudUsuario.Nombre; }
            set
            {
                _crudUsuario.Nombre = value;
                ValidNombre = ValidationService.Text(Nombre);
                OnPropertyChanged("Nombre");
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

                ValidPassword = ValidationService.Password(Password);
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

        public string Username
        {
            get { return _crudUsuario.Username; }
            set
            {
                _crudUsuario.Username = value;
                OnPropertyChanged("Username");
            }
        }
        #endregion


        public ICommand NavigationUsers { get; }
        public ICommand CrudUser { get; }
    }
}
