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
        private FuncionarioModel _crudFuncionario;

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
        #endregion



        #region Funcionalidad Listas

        private ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;
        private UnidadModel _selectedUnidad;

        public UnidadModel SelectedUnidad
        {

            get { return _selectedUnidad; }
            set
            {
                _selectedUnidad = value;
                if (_selectedUnidad != null)
                {
                    _crudFuncionario.Unidad.Rut = _selectedUnidad.Rut;
                    Subunidades = _subunidades.Where(s => s.RutUnidad == _selectedUnidad.Rut || s.Id == 0);
                }
                OnPropertyChanged("SelectedUnidad");
            }
        }

        private ObservableCollection<SubUnidadModel> _subunidades;
        private IEnumerable<SubUnidadModel> subunidades;
        private SubUnidadModel _selectedSubnidad;

        public IEnumerable<SubUnidadModel> Subunidades
        {
            get { return subunidades; }
            set
            {
                subunidades = value;
                OnPropertyChanged("Subunidades");
            }
        }
        public SubUnidadModel SelectedSubunidad
        {
            get { return _selectedSubnidad; }
            set
            {
                _selectedSubnidad = value;
                if (_selectedSubnidad != null)
                {
                    _crudFuncionario.Subunidad.Id = _selectedSubnidad.Id;
                    Roles = _roles.Where(r => r.Subunidad.Id == _selectedSubnidad.Id || r.Id == 0);
                }

                OnPropertyChanged("SelectedSubunidad");
            }
        }

        private ObservableCollection<RolModel> _roles;
        private IEnumerable<RolModel> roles;
        private RolModel _selectedRol;

        public IEnumerable<RolModel> Roles
        {
            get { return roles; }
            set
            {
                roles = value;
                OnPropertyChanged("Roles");
            }
        }
        public RolModel SelectedRol
        {
            get { return _selectedRol; }
            set
            {
                _selectedRol = value;
                if (_selectedRol != null)
                    _crudFuncionario.Rol.Id = _selectedRol.Id;

                OnPropertyChanged("SelectedRol");
            }
        }

        private ObservableCollection<EstadoModel> _estados;
        public IEnumerable<EstadoModel> Estados => _estados;
        private EstadoModel _selectedEstado;

        public EstadoModel SelectedEstado
        {

            get { return _selectedEstado; }
            set
            {
                _selectedEstado = value;
                if (_selectedEstado.Id != 0)
                {
                    _crudFuncionario.Estado.Id = _selectedEstado.Id;
                }
                OnPropertyChanged("SelectedUnidad");
            }
        }

        #endregion
        public AdminClientCRUDViewModel(int estado, FrameStore frameStore, UsuarioInternoModel currentAccount, FuncionarioModel crudFuncionario, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudFuncionario = crudFuncionario;


            #region Configuracion Estado

            _estado = estado;
            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Funcionario");
            _estadoDetalle.Add(2, "Modificar Funcionario");
            #endregion


            switch (_estado)
            {
                case 1:
                    CrudUser = new CRUDCommand<AdminClientViewModel, FuncionarioModel>(() => _crudFuncionario.Create(), () => new AdminClientViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudFuncionario.ReadByRun(), _crudFuncionario);
                    break;
                case 2:
                    CrudUser = new CRUDCommand<AdminClientViewModel, FuncionarioModel>(() => _crudFuncionario.Update(), () => new AdminClientViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudFuncionario);
                    break;
                default:
                    break;
            }
            #endregion

            #region Carga Listas

            //_unidades = _listStore.unidades;
            //_subunidades = _listStore.subunidades;
            //_roles = new ObservableCollection<RolModel>();
            //_estados = new ObservableCollection<EstadoModel>();

            _unidades = new ChargeComboBoxService<UnidadModel>().ChargeComboBox(_listStore.unidades,_unidades,new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --"});
            _subunidades = new ChargeComboBoxService<SubUnidadModel>().ChargeComboBox(_listStore.subunidades,_subunidades, new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad" });
            _roles = new ChargeComboBoxService<RolModel>().ChargeComboBox(_listStore.roles,_roles, new RolModel() { Id = 0, Nombre = "-- Rol --" });
            _estados = _listStore.estados;

            SelectedUnidad = _unidades.LastOrDefault(u => u.Rut == _crudFuncionario.Unidad.Rut || u.Rut == "0");
            SelectedSubunidad = _subunidades.LastOrDefault(s => s.Id == _crudFuncionario.Subunidad.Id || s.Id == 0);
            SelectedRol = _roles.LastOrDefault(r => r.Id == _crudFuncionario.Rol.Id || r.Id == 0);
            SelectedEstado = _estados.LastOrDefault(e => e.Id == _crudFuncionario.Estado.Id);
            #endregion

            NavigationUsers = new NavigatePanelCommand<AdminClientViewModel>(_frameStore, () => new AdminClientViewModel(_frameStore, CurrentAccount, _listStore));

        }

        #region Property Funcionarios
        public string Run
        {
            get { return _crudFuncionario.Run; }
            set
            {
                _crudFuncionario.Run = value;
                Username = value.Replace(".", "").Replace("-", "");
                OnPropertyChanged("Run");
            }
        }
        public string Nombre
        {
            get { return _crudFuncionario.Nombre; }
            set
            {
                _crudFuncionario.Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string ApellidoP
        {
            get { return _crudFuncionario.ApellidoP; }
            set
            {
                _crudFuncionario.ApellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get { return _crudFuncionario.ApellidoM; }
            set
            {
                _crudFuncionario.ApellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get { return _crudFuncionario.Email; }
            set
            {
                _crudFuncionario.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return _crudFuncionario.Password; }
            set
            {
                _crudFuncionario.Password = value;
                OnPropertyChanged("Password");
            }
        }


        public int Rol
        {
            get { return _crudFuncionario.Rol.Id; }
            set
            {
                _crudFuncionario.Rol.Id = value;
                OnPropertyChanged("RolId");
            }
        }


        public int Estado
        {
            get { return _crudFuncionario.Estado.Id; }
            set
            {
                _crudFuncionario.Estado.Id = value;
                OnPropertyChanged("Estado");
            }
        }

        public string Username
        {
            get { return _crudFuncionario.Username; }
            set
            {
                _crudFuncionario.Username = value;
                OnPropertyChanged("Username");
            }
        }
        #endregion


        public ICommand NavigationUsers { get; }
        public ICommand CrudUser { get; }
    }
}
