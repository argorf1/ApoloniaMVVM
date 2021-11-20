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
    class AdminSubunitCrudViewModel : ViewModelBase
    {
        Dictionary<int, string> _estadoDetalle = new Dictionary<int, string>();
        private int _estado = 0;
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private SubUnidadModel _crudSubunidad;

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

        private ObservableCollection<SubUnidadModel> _subunidades;
        public IEnumerable<SubUnidadModel> Subunidades => _subunidades;
        private SubUnidadModel _selectedSubunidad;


        public AdminSubunitCrudViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, SubUnidadModel createSubunidad, int estado, ListStore listStore)
        {
            _estado = estado;
            _listStore = listStore;
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crudSubunidad = createSubunidad;

            _subunidades = ChargeComboBoxService<SubUnidadModel>.ChargeComboBox(_listStore.subunidades.Where(p=> p.RutUnidad == _crudSubunidad.RutUnidad),_subunidades,new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad Padre --"});

            SelectedSubunidad = _subunidades.FirstOrDefault(p => p.Id == _crudSubunidad.SubunidadPadre.Id);

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Subnidad");
            _estadoDetalle.Add(2, "Modificar Subnidad");
            #endregion
            SelectedSubunidad = _subunidades.FirstOrDefault(p => p.Id == _crudSubunidad.SubunidadPadre.Id);

            switch (_estado)
            {
                case 1:
                    CrudSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _crudSubunidad.Create(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudSubunidad.ReadByName(), _crudSubunidad, _listStore.subunidades, 1);
                    break;
                case 2:
                    CrudSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _crudSubunidad.Update(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudSubunidad, _listStore.subunidades, 2);
                    _subunidades.Remove(_subunidades.First(p => p.Id == _crudSubunidad.Id));
                    break;
                default:
                    break;
            }
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore));

            #region CargaValidaciones
            _validations = new List<Func<bool>>();
            _validations.AddRange(new List<Func<bool>>()
            {
                 ()=> ValidationService.Text(Nombre)
                ,()=> ValidationService.Text(Descripcion)
                ,()=> ValidationService.ComboBoxId(SelectedSubunidad.Id)
            });
            #endregion
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
            get
            {
                return _crudSubunidad.Nombre;
            }
            set
            {
                _crudSubunidad.Nombre = value;

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
            get
            {
                return _crudSubunidad.Descripcion;
            }
            set
            {
                _crudSubunidad.Descripcion = value;

                ValidDescripcion = ValidationService.Text(Descripcion);
                OnPropertyChanged("Descripcion");
            }
        }

        private bool _validPadre = false;
        public bool ValidPadre
        {
            get => _validPadre;
            set
            {
                _validPadre = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidPadre");
            }
        }

        public SubUnidadModel SelectedSubunidad
        {
            get { return _selectedSubunidad; }
            set
            {
                _selectedSubunidad = value;
                if (_selectedSubunidad != null)
                    _crudSubunidad.SubunidadPadre.Id = _selectedSubunidad.Id;
                else
                    _crudSubunidad.SubunidadPadre.Id = 0;

                ValidPadre = ValidationService.ComboBoxId(SelectedSubunidad.Id);
                OnPropertyChanged("SelectedSubunidad");
            }
        }

        public string Estado
        {
            get { return _estadoDetalle[_estado]; }
            set
            {
                OnPropertyChanged("Estado");
            }
        }
        public ICommand CrudSubunit { get; }
        public ICommand NavigationUnit { get; }
    }
}
