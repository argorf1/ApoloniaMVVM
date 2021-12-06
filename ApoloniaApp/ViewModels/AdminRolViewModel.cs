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
    class AdminRolViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private RolModel _crudRol;
        public AdminRolViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore, AdminViewModel mainView)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudRol = new RolModel();

            mainView.IsCheck = false;

            #region Carga Listas
            _unidades = ChargeComboBoxService<UnidadModel>.ChargeComboBox(_listStore.unidades, _unidades, new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _roles = _listStore.roles;


            SelectedUnidad = _unidades.ElementAt(0);
            SelectedRol = new RolModel();
            #endregion

            NavigationCreateRol = new NavigatePanelCommand<AdminRolCRUDViewModel>(_frameStore, () => new AdminRolCRUDViewModel(_frameStore, CurrentAccount, new RolModel(_selectedUnidad), 1, _listStore, mainView));
            NavigationUpdateRol = new NavigatePanelCommand<AdminRolCRUDViewModel>(_frameStore, () => new AdminRolCRUDViewModel(_frameStore, CurrentAccount, _crudRol, 2, _listStore, mainView));

        }

        #region Funcionalidad Listas

        // Lista sin filtrar
        private ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;
        private UnidadModel _selectedUnidad;

        public UnidadModel SelectedUnidad
        {

            get { return _selectedUnidad; }
            set
            {
                _selectedUnidad = value;
                _crudRol.Unidad = _selectedUnidad.Rut != "0" ? _selectedUnidad : new UnidadModel();
                CanCreate = _selectedUnidad.Rut != "0";
                Roles = _roles.Where(s => s.Unidad.Rut == _selectedUnidad.Rut || s.Id == 0);
                OnPropertyChanged("SelectedUnidad");
            }
        }
        private bool _canCreate = false;
        public bool CanCreate
        {
            get => _canCreate;
            set
            {
                _canCreate = value;
                OnPropertyChanged("CanCreate");
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
                _selectedRol = value != null ? value : new RolModel();

                _crudRol.Id = _selectedRol.Id;
                _crudRol.Nombre = _selectedRol.Nombre;
                _crudRol.Descripcion = _selectedRol.Descripcion;
                _crudRol.RolSuperior = _selectedRol.RolSuperior;
                _crudRol.Subunidad = _selectedRol.Subunidad;
                
                CanEdit = _crudRol.Id != 0;
                OnPropertyChanged("SelectedRol");

                OnPropertyChanged("Nombre");
                OnPropertyChanged("Descripcion");
                OnPropertyChanged("Superior");
                OnPropertyChanged("Subunidad");
            }
        }

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
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return _crudRol.Nombre; }
            set
            {
                OnPropertyChanged("Nombre");
            }
        }
        public string Descripcion
        {
            get { return _crudRol.Descripcion; }
            set
            {
                OnPropertyChanged("Descripcion");
            }
        }

        public string Superior
        {
            get
            {
                if (_crudRol.Id != 0)
                    return roles.FirstOrDefault(r => r.RolSuperior == _crudRol.RolSuperior).Nombre;
                else
                    return "";
            }
            set
            {
                OnPropertyChanged("Superior");
            }
        }

        public string Subunidad
        {
            get { return _crudRol.Subunidad.Nombre; }
            set { OnPropertyChanged("Subunidad"); }
        }
        #endregion

        #region CommandButtons
        public ICommand NavigationCreateRol { get; }
        public ICommand NavigationUpdateRol { get; }
        #endregion
    }
}
