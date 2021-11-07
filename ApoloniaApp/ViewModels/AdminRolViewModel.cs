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
    class AdminRolViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private RolModel _crudRol;
        public AdminRolViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;

            _unidades = new ObservableCollection<UnidadModel>();
            _roles = new ObservableCollection<RolModel>();

            _unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => new UnidadModel().ReadAll(), new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _roles = new ReadAllCommand<RolModel>().ReadAll(() => new RolModel().ReadAll());

            SelectedUnidad = _unidades.ElementAt(0);
            SelectedRol = new RolModel();

            NavigationCreateRol = new NavigatePanelCommand<AdminRolCRUDViewModel>(_frameStore, () => new AdminRolCRUDViewModel(_frameStore, CurrentAccount, new RolModel(_selectedUnidad),1));
            NavigationUpdateRol = new NavigatePanelCommand<AdminRolCRUDViewModel>(_frameStore, () => new AdminRolCRUDViewModel(_frameStore, CurrentAccount,_selectedRol,2));

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
                if (_selectedUnidad.Rut != "0")
                {
                    _selectedRol.Unidad = _selectedUnidad;
                }
                    Roles = _roles.Where(s => s.Unidad.Rut == _selectedUnidad.Rut || s.Id == 0);
                OnPropertyChanged("SelectedUnidad");
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

                OnPropertyChanged("SelectedRol");

                OnPropertyChanged("Nombre");
                OnPropertyChanged("Descripcion");
                OnPropertyChanged("Superior");
                OnPropertyChanged("Subunidad");
            }
        }
        #endregion

        #region Propiedades
        public string Nombre
        {
            get { return _selectedRol.Nombre; }
            set
            {
                OnPropertyChanged("Nombre");
            }
        }
        public string Descripcion
        {
            get { return _selectedRol.Descripcion; }
            set
            {
                OnPropertyChanged("Descripcion");
            }
        }

        public string Superior
        {
            get 
            {
                if (_selectedRol.Id != 0)
                    return roles.FirstOrDefault(r => r.RolSuperior == _selectedRol.RolSuperior).Nombre;
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
            get { return _selectedRol.Subunidad.Nombre; }
            set { OnPropertyChanged("Subunidad"); }
        }
        #endregion

        #region CommandButtons
        public ICommand NavigationCreateRol { get; }
        public ICommand NavigationUpdateRol { get; }
        #endregion
    }
}
