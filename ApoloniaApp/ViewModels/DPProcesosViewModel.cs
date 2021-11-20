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
        #endregion

        public DPProcesosViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;

            #region Carga Listas
            _unidades = ChargeComboBoxService<UnidadModel>.ChargeComboBox(_listStore.unidades.Where(p => p.Responsable.Run == currentAccount.Run), _unidades, new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _procesos = _listStore.procesos;
            _tareas = _listStore.tareas;
            _responsables = _listStore.responsables;

            SelectedUnidad = _unidades.ElementAt(0);
            #endregion

            #region Configuracion Botones
            NavigationCreateProceso = new NavigatePanelCommand<DPProcesosCRUDViewModel>(frameStore, () => new DPProcesosCRUDViewModel(frameStore, currentAccount, new ProcesoModel() { Unidad = _selectedUnidad }, 1, _listStore));
            NavigationUpdateProceso = new NavigatePanelCommand<DPProcesosCRUDViewModel>(frameStore, () => new DPProcesosCRUDViewModel(frameStore, currentAccount, _crudProceso, 2, _listStore));
            NavigationCreateTarea = new NavigatePanelCommand<DPTareaCRUDViewModel>(frameStore, () => new DPTareaCRUDViewModel(frameStore, currentAccount, new TareaModel() { Proceso = _crudProceso }, 1, _selectedUnidad, _listStore));
            NavigationUpdateTarea = new NavigatePanelCommand<DPTareaCRUDViewModel>(frameStore, () => new DPTareaCRUDViewModel(frameStore, currentAccount, _crudTarea, 2, _selectedUnidad, _listStore));
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
            get { return _crudProceso; }
            set
            {
                _crudProceso = value;

                CanEditProc = (_crudProceso != null ? true : false);
                CanCreateTarea = (_crudProceso != null ? true : false);
                Tareas = (_crudProceso != null ? _tareas.Where(t => t.Proceso.Id == _crudProceso.Id) : null);
                OnPropertyChanged("SelectedProceso");
            }
        }

        private readonly ObservableCollection<TareaModel> _tareas;
        private IEnumerable<TareaModel> tareas;
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
            get => _crudTarea;
            set
            {
                _crudTarea = value;

                CanEditTarea = (_crudTarea != null ? true: false);
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
