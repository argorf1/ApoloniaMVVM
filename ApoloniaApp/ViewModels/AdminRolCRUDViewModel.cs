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
    class AdminRolCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private RolModel _crudRol;

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
        public ICommand CrudRol { get; }
        public ICommand ReturnCommand { get; }
        public AdminRolCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, RolModel crudRol, int estado, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _estado = estado;
            _crudRol = crudRol;

            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Rol");
            _estadoDetalle.Add(2, "Modificar Rol");
            #endregion


            switch (_estado)
            {
                case 1:
                    CrudRol = new CRUDCommand<AdminRolViewModel, RolModel>(() => _crudRol.Create(), () => new AdminRolViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudRol.ReadByNombre(), _crudRol, _listStore.roles, 1);
                    break;
                case 2:
                    CrudRol = new CRUDCommand<AdminRolViewModel, RolModel>(() => _crudRol.Update(), () => new AdminRolViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudRol, _listStore.roles, 2);
                    break;
                default:
                    break;
            }
            ReturnCommand = new NavigatePanelCommand<AdminRolViewModel>(_frameStore, () => new AdminRolViewModel(_frameStore, CurrentAccount, _listStore));

            #endregion

            #region Carga Listas
            _subunidades = ChargeComboBoxService<SubUnidadModel>.ChargeComboBox(_listStore.subunidades.Where(p=> p.RutUnidad == _crudRol.Unidad.Rut), _subunidades, new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad --" });
            _roles = ChargeComboBoxService<RolModel>.ChargeComboBox(_listStore.roles,_roles, new RolModel() { Id = 0, Nombre = "-- Rol --" });

            _roles.Remove(_roles.FirstOrDefault(r => r.Id == _crudRol.Id && r.Id != 0));

            SelectedSubunidad = _subunidades.LastOrDefault(s => s.Id == _crudRol.Subunidad.Id || s.Id == 0);
            SelectedRol = _roles.LastOrDefault(r => r.Id == _crudRol.RolSuperior || r.Id == 0);
            #endregion

            #region CargaValidaciones
            _validations = new List<Func<bool>>();
            _validations.AddRange(new List<Func<bool>>()
            {
                 ()=> ValidationService.Text(Nombre)
                ,()=> ValidationService.Text(Descripcion)
                ,()=> ValidationService.ComboBoxId(SelectedSubunidad.Id)
                ,()=> ValidationService.ComboBoxId(SelectedRol.Id)
            });
            #endregion
        }

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

        // Lista sin filtrar
        private ObservableCollection<SubUnidadModel> _subunidades;
        public IEnumerable<SubUnidadModel> Subunidades => _subunidades;

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

        public SubUnidadModel SelectedSubunidad
        {

            get { return _crudRol.Subunidad; }
            set
            {
                _crudRol.Subunidad = value;
                Roles = _roles.Where(s => s.Subunidad.Id == _crudRol.Subunidad.Id || s.Id == 0 || (s.Nivel == 0 && s.Unidad.Rut == _crudRol.Unidad.Rut && SelectedSubunidad.Id != 0));
                ValidSubunidad = ValidationService.ComboBoxId(SelectedSubunidad.Id);
                OnPropertyChanged("SelectedUnidad");
            }
        }

        //Lista que necesita filtrarse
        private ObservableCollection<RolModel> _roles;
        private IEnumerable<RolModel> roles;
        private RolModel _selectedRol;

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
                _crudRol.RolSuperior = _selectedRol.Id;
                _crudRol.Nivel = _selectedRol.Nivel + 1;
                ValidRol = ValidationService.ComboBoxId(SelectedRol.Id);

                OnPropertyChanged("SelectedRol");

            }
        }
        #endregion

        #region Propiedades
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
            get { return _crudRol.Nombre; }
            set
            {
                _crudRol.Nombre = value;
                ValidNombre = ValidationService.Text(Nombre);
                OnPropertyChanged("Nombre");
            }
        }

        private bool _validDescripcion = false;
        public bool ValidDescripcion
        {
            get => _validDescripcion;
            set
            {
                _validDescripcion = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidDescripcion");
            }
        }

        public string Descripcion
        {
            get { return _crudRol.Descripcion; }
            set
            {
                _crudRol.Descripcion = value;
                ValidDescripcion = ValidationService.Text(Descripcion);
                OnPropertyChanged("Descripcion");
            }
        }
        #endregion
    }
}
