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
    class AdminUnitViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;

        #region Unidad
        private readonly ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;
        private UnidadModel _editUnit;
        private UnidadModel _selectedUnidad;
        private bool _canEditUnit;


        public UnidadModel SelectedUnidad
        {
            get { return _selectedUnidad; }
            set
            {
                _selectedUnidad = value;

                _editUnit.Rut = value.Rut;
                _editUnit.RazonSocial = value.RazonSocial;
                _editUnit.Rubro = value.Rubro;
                _editUnit.PersonaContacto = value.PersonaContacto;
                _editUnit.TelefonoContacto = value.TelefonoContacto;
                _editUnit.EmailContacto = value.EmailContacto;
                _editUnit.Direccion = value.Direccion;
                _editUnit.Responsable = value.Responsable;
                _editUnit.Estado = value.Estado;

                Subunidades = _subunidades.Where(p => p.RutUnidad == _editUnit.Rut).ToList();
                if (_editUnit != null)
                {
                    CanEditUnit = true;
                    CanCreateSubunit = true;
                }
                else
                {
                    CanEditUnit = false;
                }
                OnPropertyChanged("SelectedUnidad");

                OnPropertyChanged("Rut");
                OnPropertyChanged("RazonSocial");
                OnPropertyChanged("Rubro");
                OnPropertyChanged("PersonaContacto");
                OnPropertyChanged("TelefonoContacto");
                OnPropertyChanged("EmailContacto");
                OnPropertyChanged("Calle");
                OnPropertyChanged("Numero");
                OnPropertyChanged("Complemento");
                OnPropertyChanged("Region");
                OnPropertyChanged("Provincia");
                OnPropertyChanged("Comuna");
                OnPropertyChanged("ResponsableRun");
                OnPropertyChanged("ResponsableNombre");
                OnPropertyChanged("Estado");
            }
        }
        public bool CanEditUnit
        {
            get { return _canEditUnit; }
            set
            {
                _canEditUnit = value;
                OnPropertyChanged("CanEditUnit");
            }
        }
        public string Rut
        {
            get
            {
                return _editUnit.Rut;
            }
            set
            {
                OnPropertyChanged("Rut");
            }
        }
        public string RazonSocial
        {
            get
            {

                return _editUnit.RazonSocial;

            }
            set
            {
                OnPropertyChanged("RazonSocial");
            }

        }
        public string Rubro
        {
            get
            {

                return _editUnit.Rubro.Nombre;

            }
            set
            {
                OnPropertyChanged("Rubro");
            }

        }
        public int Direccion
        {
            get
            {

                return _editUnit.Direccion.Id;

            }
            set
            {
                OnPropertyChanged("Direccion");
            }

        }
        public string PersonaContacto
        {
            get
            {

                return _editUnit.PersonaContacto;

            }
            set
            {
                OnPropertyChanged("PersonaContacto");
            }

        }
        public string TelefonoContacto
        {
            get
            {

                return _editUnit.TelefonoContacto;

            }
            set
            {
                OnPropertyChanged("TelefonoContacto");
            }

        }
        public string EmailContacto
        {
            get
            {

                return _editUnit.EmailContacto;

            }
            set
            {
                OnPropertyChanged("EmailContacto");
            }

        }
        public string ResponsableNombre
        {
            get
            {

                return _editUnit.Responsable.Nombre + " " + _editUnit.Responsable.ApellidoP;

            }
            set
            {
                OnPropertyChanged("ResponsableNombre");
            }

        }
        public string Estado
        {
            get
            {

                return _editUnit.Estado.Nombre;

            }
            set
            {
                OnPropertyChanged("Estado");
            }

        }
        public string Calle
        {
            get
            {

                return _editUnit.Direccion.Calle;

            }
            set
            {
                OnPropertyChanged("Calle");
            }

        }
        public string Numero
        {
            get
            {
                return _editUnit.Direccion.Numero;
            }
            set
            {
                OnPropertyChanged("Numero");
            }

        }
        public string Complemento
        {
            get
            {

                return _editUnit.Direccion.Complemento;

            }
            set
            {
                OnPropertyChanged("Complemento");
            }


        }
        public string Region
        {
            get
            {

                return _editUnit.Direccion.Region.Nombre;
            }
            set
            {
                OnPropertyChanged("Region");
            }

        }
        public string Provincia
        {
            get
            {

                return _editUnit.Direccion.Provincia.Nombre;

            }
            set
            {
                OnPropertyChanged("Provincia");
            }

        }
        public string Comuna
        {
            get
            {

                return _editUnit.Direccion.Comuna.Nombre;

            }
            set
            {
                OnPropertyChanged("Comuna");
            }

        }

        #endregion
        #region Subunidad
        private readonly ObservableCollection<SubUnidadModel> _subunidades;
        private IEnumerable<SubUnidadModel> subunidades;
        private SubUnidadModel _editSubunit;
        private SubUnidadModel _selectedSubunidad;
        private bool _canCreateSubunit;
        private bool _canEditSubunit;
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
            get => _selectedSubunidad;
            set
            {
                if (value != null)
                {

                    _selectedSubunidad = value;
                }
                else
                {
                    _selectedSubunidad = new SubUnidadModel();
                }
                if (_selectedSubunidad.Id != 0)
                {
                    CanEditSubunit = true;
                }
                else
                {
                    CanEditSubunit = false;
                }

                _editSubunit.Id = _selectedSubunidad.Id;
                _editSubunit.Nombre = _selectedSubunidad.Nombre;
                _editSubunit.Descripcion = _selectedSubunidad.Descripcion;
                _editSubunit.RutUnidad = _selectedSubunidad.RutUnidad;
                _editSubunit.SubunidadPadre = _selectedSubunidad.SubunidadPadre;

                Funcionarios = _funcionarios.Where(f => f.Subunidad.Id == _editSubunit.Id);
                OnPropertyChanged("SelectedSubunidad");
            }
        }

        private readonly ObservableCollection<FuncionarioModel> _funcionarios;
        private IEnumerable<FuncionarioModel> funcionarios;

        public IEnumerable<FuncionarioModel> Funcionarios
        {
            get { return funcionarios; }
            set
            {
                funcionarios = value;
                OnPropertyChanged("Funcionarios");
            }
        }

        public bool CanCreateSubunit
        {
            get { return _canCreateSubunit; }
            set
            {
                _canCreateSubunit = value;
                OnPropertyChanged("CanCreateSubunit");
            }
        }
        public bool CanEditSubunit
        {
            get { return _canEditSubunit; }
            set
            {
                _canEditSubunit = value;
                OnPropertyChanged("CanEditSubunit");
            }
        }
        #endregion


        public AdminUnitViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore, AdminViewModel mainView)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _editUnit = new UnidadModel();

            _unidades = new ObservableCollection<UnidadModel>();
            _subunidades = new ObservableCollection<SubUnidadModel>();
            _funcionarios = new ObservableCollection<FuncionarioModel>();

            mainView.IsCheck = false;

            _unidades = _listStore.unidades;
            _subunidades = _listStore.subunidades;
            _funcionarios = _listStore.funcionarios;

            _editSubunit = new SubUnidadModel();

            NavigationCreateUnit = new NavigatePanelCommand<AdminUnitCRUDViewModel>(_frameStore, () => new AdminUnitCRUDViewModel(_frameStore, CurrentAccount,new UnidadModel(), 1,_listStore, mainView));
            NavigationEditUnit = new NavigatePanelCommand<AdminUnitCRUDViewModel>(_frameStore, () => new AdminUnitCRUDViewModel(_frameStore, CurrentAccount, _editUnit, 2,_listStore, mainView));
            NavigationCreateSubunit = new NavigatePanelCommand<AdminSubunitCrudViewModel>(_frameStore, () => new AdminSubunitCrudViewModel(_frameStore, CurrentAccount, new SubUnidadModel() { RutUnidad = _editUnit.Rut }, 1, _listStore, mainView));
            NavigationEditSubunit = new NavigatePanelCommand<AdminSubunitCrudViewModel>(_frameStore, () => new AdminSubunitCrudViewModel(_frameStore, CurrentAccount, _editSubunit, 2, _listStore, mainView));
            DeleteSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _editSubunit.Delete(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, () => _editSubunit.ReadContent(), _editSubunit, () => _listStore.Subunidades(), 3);
        }

        public ICommand NavigationCreateUnit { get; }
        public ICommand NavigationEditUnit { get; }
        public ICommand NavigationCreateSubunit { get; }
        public ICommand NavigationEditSubunit { get; }
        public ICommand DeleteSubunit { get; }
    }
}
