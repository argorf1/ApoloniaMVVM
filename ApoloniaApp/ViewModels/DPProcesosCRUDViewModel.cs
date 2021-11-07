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
    class DPProcesosCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private ProcesoModel _crudProceso;

        public ICommand CrudCommand { get; }
        public DPProcesosCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ProcesoModel crudProceso, int estado)
        {
            _frameStore = frameStore;
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
                    CrudCommand = new CRUDCommand<DPProcesosViewModel, ProcesoModel>(() => _crudProceso.Create(), () => new DPProcesosViewModel(_frameStore, CurrentAccount), _frameStore, () => _crudProceso.ReadByNombre(), _crudProceso);
                    break;
                case 2:
                    CrudCommand = new CRUDCommand<DPProcesosViewModel, ProcesoModel>(() => _crudProceso.Update(), () => new DPProcesosViewModel(_frameStore, CurrentAccount), _frameStore, _crudProceso);
                    break;
                default:
                    break;
            }
            #endregion

            #region Carga Listas
            _subunidades = new ReadAllCommand<SubUnidadModel>().ReadAll(() => _crudProceso.Unidad.ReadSubunidadByUnidad(), new SubUnidadModel() { Id = 0, Nombre = "-- Subunidad --" });
            _roles = new ReadAllCommand<RolModel>().ReadAll(() => _crudProceso.Unidad.ReadRolByUnidad());
            SelectedSubunidad = _subunidades.Last(s => s.Id == _crudProceso.Subunidad.Id);
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
                if (_crudProceso.Subunidad.Id != 0)
                {
                    roles = _roles.Where(r => r.Subunidad.Id == _crudProceso.Subunidad.Id);
                    _crudProceso.Rol = roles.FirstOrDefault(r => r.Nivel== roles.Min(r => r.Nivel));
                }
                OnPropertyChanged("SelectedSubunidad");

                OnPropertyChanged("Rol");
            }
        }
        #endregion

        public string Nombre
        {
            get => _crudProceso.Nombre;
            set
            {
                _crudProceso.Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string Descripcion
        {
            get => _crudProceso.Descripcion;
            set
            {
                _crudProceso.Descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        private ObservableCollection<RolModel> _roles;
        private IEnumerable<RolModel> roles;

        public string Rol
        {
            get 
            {   if (_crudProceso.Rol != null)
                    return _crudProceso.Rol.Nombre;
                else
                    return "";
            }
            set
            {
                OnPropertyChanged("Rol");
            }
        }

    }
}
