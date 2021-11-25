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
    class DPProcesosCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private ProcesoModel _crudProceso;

        #region Validation Properties
        private List<Func<bool>> _validations;
        private bool _canCrud = false;

        public bool CanCrud
        {
            get => _canCrud;
            set
            {
                _canCrud = value;
                OnPropertyChanged("CanCrud");
            }
        }
        #endregion
        public ICommand CrudCommand { get; }
        public ICommand Return { get; }
        public DPProcesosCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ProcesoModel crudProceso, int estado, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudProceso = crudProceso;
            _crudProceso.Creador.Run = CurrentAccount.Run;
            _estado = estado;

            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Proceso");
            _estadoDetalle.Add(2, "Modificar Proceso");
            #endregion


            switch (_estado)
            {
                case 1:
                    CrudCommand = new CRUDCommand<DPProcesosViewModel, ProcesoModel>(() => _crudProceso.Create(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudProceso.ReadByNombre(), _crudProceso, () => _listStore.ProcesosView(), 1);
                    break;
                case 2:
                    CrudCommand = new CRUDCommand<DPProcesosViewModel, ProcesoModel>(() => _crudProceso.Update(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudProceso, () => _listStore.ProcesosView(), 2);
                    break;
                default:
                    break;
            }
            Return = new NavigatePanelCommand<DPProcesosViewModel>(_frameStore, () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore));
            #endregion

           
            #region Carga Listas
            _subunidades = ChargeComboBoxService<SubUnidadModel>.ChargeComboBox(_listStore.subunidades.Where(p => p.RutUnidad == _crudProceso.Unidad.Rut), _subunidades, new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad --" });

            _roles = ChargeComboBoxService<RolModel>.ChargeComboBox(_listStore.roles.Where(p => p.Unidad.Rut == _crudProceso.Unidad.Rut), _roles, new RolModel() { Id = 0, Nombre = "-- Subunidad --" });
            SelectedSubunidad = _subunidades.Last(s => s.Id == _crudProceso.Subunidad.Id);
            #endregion

            #region CargaValidaciones
            _validations = new List<Func<bool>>();
            _validations.AddRange(new List<Func<bool>>()
            {
                () => ValidateText(Nombre),
                () => ValidateText(Descripcion),
                ValidateSubunidad
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

        public SubUnidadModel SelectedSubunidad
        {

            get { return _crudProceso.Subunidad; }
            set
            {
                _crudProceso.Subunidad = value;
                if (ValidateSubunidad())
                {
                    ValidateAll();
                    roles = _roles.Where(r => r.Subunidad.Id == _crudProceso.Subunidad.Id);
                    _crudProceso.Rol = roles.FirstOrDefault(r => r.Nivel == roles.Min(r => r.Nivel));
                }
                OnPropertyChanged("SelectedSubunidad");

                OnPropertyChanged("Rol");
            }
        }
        #endregion

        #region Properties
        private bool _validNombre = false;
        public string Nombre
        {
            get => _crudProceso.Nombre;
            set
            {
                _crudProceso.Nombre = value;

                ValidNombre = ValidateText(value);
                ValidateAll();
                OnPropertyChanged("Nombre");
            }
        }
        public bool ValidNombre
        {
            get => _validNombre;
            set
            {
                _validNombre = value;
                OnPropertyChanged("ValidNombre");
            }
        }
        private bool _validDescripcion = false;
        public string Descripcion
        {
            get => _crudProceso.Descripcion;
            set
            {
                _crudProceso.Descripcion = value;

                _validDescripcion = ValidateText(value);
                ValidateAll();
                OnPropertyChanged("Descripcion");
            }
        }

        private ObservableCollection<RolModel> _roles;
        private IEnumerable<RolModel> roles;

        public string Rol
        {
            get
            {
                if (_crudProceso.Rol != null)
                    return _crudProceso.Rol.Nombre;
                else
                    return "";
            }
            set
            {
                OnPropertyChanged("Rol");
            }
        }

        #endregion

        #region Validaciones

        private bool ValidateText(string text) => !string.IsNullOrEmpty(text);

        private bool ValidateSubunidad()
        {
            return (SelectedSubunidad.Id != 0 ? true : false);
        }
        public void ValidateAll()
        {

            foreach (Func<bool> f in _validations)
            {
                if (!f())
                {
                    CanCrud = false;
                    return;
                }
            }
            CanCrud = true;
        }
        #endregion    
    }
}

