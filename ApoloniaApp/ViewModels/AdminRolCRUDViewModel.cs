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
        public ICommand CrudRol { get; }
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
                    CrudRol = new CRUDCommand<AdminRolViewModel, RolModel>(() => _crudRol.Create(), () => new AdminRolViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudRol.ReadByNombre(), _crudRol);
                    break;
                case 2:
                    CrudRol = new CRUDCommand<AdminRolViewModel, RolModel>(() => _crudRol.Update(), () => new AdminRolViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudRol);
                    break;
                default:
                    break;
            }
            #endregion

            #region Carga Listas
            _subunidades = new ChargeComboBoxService<SubUnidadModel>().ChargeComboBox(_listStore.subunidades.Where(p=> p.RutUnidad == _crudRol.Unidad.Rut), _subunidades, new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad --" });
            _roles = new ChargeComboBoxService<RolModel>().ChargeComboBox(_listStore.roles,_roles, new RolModel() { Id = 0, Nombre = "-- Rol --" });

            _roles.Remove(_roles.FirstOrDefault(r => r.Id == _crudRol.Id && r.Id != 0));

            SelectedSubunidad = _subunidades.LastOrDefault(s => s.Id == _crudRol.Subunidad.Id || s.Id == 0);
            SelectedRol = _roles.LastOrDefault(r => r.Id == _crudRol.RolSuperior || r.Id == 0);
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

        public SubUnidadModel SelectedSubunidad
        {

            get { return _crudRol.Subunidad; }
            set
            {
                _crudRol.Subunidad = value;
                if (_crudRol.Unidad != null)
                {
                    Roles = _roles.Where(s => s.Subunidad.Id == _crudRol.Subunidad.Id || s.Id == 0);
                }
                OnPropertyChanged("SelectedUnidad");
            }
        }

        //Lista que necesita filtrarse
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
                _crudRol.RolSuperior = _selectedRol.Id;
                if (_selectedRol.Id != 0)
                    _crudRol.Nivel = _selectedRol.Nivel + 1;
                else
                    _crudRol.Nivel = 0;

                OnPropertyChanged("SelectedRol");

            }
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return _crudRol.Nombre; }
            set
            {
                _crudRol.Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }
        public string Descripcion
        {
            get { return _crudRol.Descripcion; }
            set
            {
                _crudRol.Descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        public RolModel Superior
        {
            get
            {
                return _selectedRol;
            }
            set
            {
                _selectedRol = value;
                _crudRol.RolSuperior = _selectedRol.Id;
                OnPropertyChanged("Superior");
            }
        }

        public SubUnidadModel Subunidad
        {
            get { return _crudRol.Subunidad; }
            set 
            {
                _crudRol.Subunidad = value;
                OnPropertyChanged("Subunidad"); 
            }
        }
        #endregion
    }
}
