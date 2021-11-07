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
    class AdminRolCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private RolModel _crudRol;
        public ICommand CrudRol { get; }
        public AdminRolCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, RolModel crudRol, int estado)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _estado = estado;
            _crudRol = crudRol;

            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Funcionario");
            _estadoDetalle.Add(2, "Modificar Funcionario");
            #endregion


            switch (_estado)
            {
                case 1:
                    CrudRol = new CRUDCommand<AdminClientViewModel, RolModel>(() => _crudRol.Create(), () => new AdminClientViewModel(_frameStore, CurrentAccount), _frameStore, () => _crudRol.ReadByNombre(), _crudRol);
                    break;
                case 2:
                    CrudRol = new CRUDCommand<AdminClientViewModel, RolModel>(() => _crudRol.Update(), () => new AdminClientViewModel(_frameStore, CurrentAccount), _frameStore, _crudRol);
                    break;
                default:
                    break;
            }
            #endregion

            #region Carga Listas
            _subunidades = new ObservableCollection<SubUnidadModel>();
            _roles = new ObservableCollection<RolModel>();

            _subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => _crudRol.Unidad.ReadByUnidad(), new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad --" });
            _roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());
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
