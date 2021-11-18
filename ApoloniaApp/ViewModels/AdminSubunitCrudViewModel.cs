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
    class AdminSubunitCrudViewModel : ViewModelBase
    {
        Dictionary<int, string> _estadoDetalle = new Dictionary<int, string>();
        private int _estado = 0;
        private readonly FrameStore _frameStore;
        private ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private SubUnidadModel _crudSubunidad;


        private ObservableCollection<SubUnidadModel> _subunidades;
        public IEnumerable<SubUnidadModel> Subunidades => _subunidades;
        private SubUnidadModel _selectedSubunidad;


        public AdminSubunitCrudViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, SubUnidadModel createSubunidad, IEnumerable<SubUnidadModel> subunidades,int estado, ListStore listStore)
        {
            _estado = estado;
            _listStore = listStore;
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crudSubunidad = createSubunidad;

            _subunidades = new ObservableCollection<SubUnidadModel>();
            foreach (SubUnidadModel s in subunidades)
            {
                _subunidades.Add(s);
            }
            SelectedSubunidad = _subunidades.FirstOrDefault(p => p.Id == _crudSubunidad.SubUnidadPadreId);
            if(estado ==2)
                _subunidades.Remove(_subunidades.First(p => p.Id == _crudSubunidad.Id));

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Subnidad");
            _estadoDetalle.Add(2, "Modificar Subnidad");
            #endregion
            SelectedSubunidad = _subunidades.FirstOrDefault(p => p.Id == _crudSubunidad.SubUnidadPadreId);

            switch (_estado)
            {
                case 1:
                    CrudSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _crudSubunidad.Create(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore), _frameStore,() =>_crudSubunidad.ReadByName() ,_crudSubunidad);
                    break;
                case 2:
                    CrudSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _crudSubunidad.Update(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudSubunidad);
                    break;
                default:
                    break;
            }
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore));

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

                OnPropertyChanged("Nombre");
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
                OnPropertyChanged("Descripcion");
            }
        }

        public SubUnidadModel SelectedSubunidad
        {
            get { return _selectedSubunidad; }
            set
            {
                _selectedSubunidad = value;
                if (_selectedSubunidad != null)
                    _crudSubunidad.SubUnidadPadreId = _selectedSubunidad.Id;
                else
                    _crudSubunidad.SubUnidadPadreId = 0;
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
