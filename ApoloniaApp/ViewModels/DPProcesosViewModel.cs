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
    class DPProcesosViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        public UsuarioInternoModel CurrentAccount;
        private ProcesoModel _crudProceso = new ProcesoModel();
        private TareaModel _crudTarea = new TareaModel();
        //private TareaModel _crudTarea;

        #region CommandButtons
        public ICommand NavigationCreateProceso { get; }
        public ICommand NavigationUpdateProceso { get; }
        public ICommand NavigationCreateTarea { get; }
        public ICommand NavigationUpdateTarea { get; }
        public ICommand DeleteProceso { get; }
        public ICommand DeleteTarea { get; }
        #endregion

        public DPProcesosViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore, ProcessDesignerViewModel mainView)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;

            mainView.IsCheck = false;

            _crudProceso = new ProcesoModel();
            _crudTarea = new TareaModel();

            #region Carga Listas
            _unidades = ChargeComboBoxService<UnidadModel>.ChargeComboBox(_listStore.unidades.Where(p => p.Responsable.Run == currentAccount.Run), _unidades, new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _procesos = _listStore.procesos;
            _tareas = _listStore.tareas;
            _responsables = _listStore.responsables;

            SelectedUnidad = _unidades.ElementAt(0);
            #endregion

            #region Configuracion Botones
            NavigationCreateProceso = new NavigatePanelCommand<DPProcesosCRUDViewModel>(frameStore, () => new DPProcesosCRUDViewModel(frameStore, currentAccount, new ProcesoModel() { Unidad = _selectedUnidad }, 1, _listStore, mainView));
            NavigationUpdateProceso = new NavigatePanelCommand<DPProcesosCRUDViewModel>(frameStore, () => new DPProcesosCRUDViewModel(frameStore, currentAccount, _crudProceso, 2, _listStore, mainView));
            NavigationCreateTarea = new NavigatePanelCommand<DPTareaCRUDViewModel>(frameStore, () => new DPTareaCRUDViewModel(frameStore, currentAccount, new TareaModel() { Proceso = _selectedProceso }, 1, _selectedUnidad, _listStore, mainView));
            NavigationUpdateTarea = new NavigatePanelCommand<DPTareaCRUDViewModel>(frameStore, () => new DPTareaCRUDViewModel(frameStore, currentAccount, _crudTarea, 2, _selectedUnidad, _listStore, mainView));

            DeleteProceso = new CRUDCommand<DPProcesosViewModel, ProcesoModel>(() => _crudProceso.Delete(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, _crudProceso, () => _listStore.ProcesosView(), 4);
            DeleteTarea = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Delete(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore,  _crudTarea, () => _listStore.TareasView(), 4);

            #endregion
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

                CanCreateProc = (_selectedUnidad.Rut != "0" ? true : false);
                Procesos = _procesos.Where(p => p.Unidad.Rut == _selectedUnidad.Rut);
                OnPropertyChanged("SelectedUnidad");
            }
        }

        //Lista que necesita filtrarse
        private readonly ObservableCollection<ProcesoModel> _procesos;
        private IEnumerable<ProcesoModel> procesos;
        private ProcesoModel _selectedProceso;
        private bool _canCreateProc = false;
        private bool _canEditProc = false;

        public bool CanCreateProc
        {
            get => _canCreateProc;
            set
            {
                _canCreateProc = value;
                OnPropertyChanged("CanCreateProc");
            }
        }
        public bool CanEditProc
        {
            get => _canEditProc;
            set
            {
                _canEditProc = value;
                OnPropertyChanged("CanEditProc");
            }
        }
        public IEnumerable<ProcesoModel> Procesos
        {
            get { return procesos; }
            set
            {
                procesos = value;
                OnPropertyChanged("Procesos");
            }
        }
        public ProcesoModel SelectedProceso
        {
            get { return _selectedProceso; }
            set
            {
                _selectedProceso = value != null ? value:new ProcesoModel();

                _crudProceso.Id             = _selectedProceso.Id;
                _crudProceso.Nombre         = _selectedProceso.Nombre;
                _crudProceso.Descripcion    = _selectedProceso.Descripcion;
                _crudProceso.Rol            = _selectedProceso.Rol;
                _crudProceso.Unidad         = _selectedProceso.Unidad;
                _crudProceso.Creador        = _selectedProceso.Creador;

                CanEditProc = _selectedProceso != null;
                CanCreateTarea = _selectedProceso != null;
                Tareas = _crudProceso != null ? _tareas.Where(t => t.Proceso.Id == _crudProceso.Id) : null;
                OnPropertyChanged("SelectedProceso");
            }
        }

        private readonly ObservableCollection<TareaModel> _tareas;
        private IEnumerable<TareaModel> tareas;
        private TareaModel _selectedTarea;
        private bool _canCreateTarea = false;
        private bool _canEditTarea = false;

        public bool CanCreateTarea
        {
            get => _canCreateTarea;
            set
            {
                _canCreateTarea = value;
                OnPropertyChanged("CanCreateTarea");
            }
        }
        public bool CanEditTarea
        {
            get => _canEditTarea;
            set
            {
                _canEditTarea = value;
                OnPropertyChanged("CanEditTarea");
            }
        }
        public IEnumerable<TareaModel> Tareas
        {
            get => tareas;
            set
            {
                tareas = value;
                OnPropertyChanged("Tareas");
            }
        }

        public TareaModel SelectedTarea
        {
            get => _selectedTarea;
            set
            {
                _selectedTarea = value != null ? value : new TareaModel();

                _crudTarea.Id = _selectedTarea.Id;
                _crudTarea.Nombre = _selectedTarea.Nombre;
                _crudTarea.Descripcion = _selectedTarea.Descripcion;
                _crudTarea.Duracion = _selectedTarea.Duracion;
                _crudTarea.Responsable = _selectedTarea.Responsable;
                _crudTarea.Dependencia = _selectedTarea.Dependencia;
                _crudTarea.Proceso = _selectedProceso;
                CanEditTarea = (_selectedTarea.Id != 0 ? true: false);
                Responsables = (_crudTarea != null ? _responsables.Where(p => p.IdTarea == _crudTarea.Id) : null);
                OnPropertyChanged("SelectedTarea");

            }
        }

        private ObservableCollection<ResponsableModel> _responsables;
        private IEnumerable<ResponsableModel> responsables;

        public IEnumerable<ResponsableModel> Responsables
        {
            get => responsables;
            set
            {
                responsables = value;
                OnPropertyChanged("Responsables");
            }
        }
        #endregion
    }
}
