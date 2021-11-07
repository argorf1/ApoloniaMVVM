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
    class DPProcesosViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private ProcesoModel _crudProceso = new ProcesoModel();
        //private TareaModel _crudTarea;

        #region CommandButtons
        public ICommand NavigationCreateProceso { get; }
        public ICommand NavigationUpdateProceso { get; }
        #endregion

        public DPProcesosViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;

            #region Carga Listas

            _unidades = new ReadAllCommand<UnidadModel>().ReadAll(() => CurrentAccount.ReadByDesigner(), new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _procesos = new ReadAllCommand<ProcesoModel>().ReadAll(() => new ProcesoModel().ReadAll());

            SelectedUnidad = _unidades.ElementAt(0);
            #endregion

            #region Configuracion Botones
            NavigationCreateProceso = new NavigatePanelCommand<DPProcesosCRUDViewModel>(frameStore, () => new DPProcesosCRUDViewModel(frameStore,currentAccount,new ProcesoModel(_selectedUnidad),1));
            NavigationUpdateProceso = new NavigatePanelCommand<DPProcesosCRUDViewModel>(frameStore, () => new DPProcesosCRUDViewModel(frameStore,currentAccount,_crudProceso,2));
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
                if (_selectedUnidad != null)
                {
                    Procesos = _procesos.Where(p => p.Unidad.Rut == _selectedUnidad.Rut);
                }
                OnPropertyChanged("SelectedUnidad");
            }
        }

        //Lista que necesita filtrarse
        private ObservableCollection<ProcesoModel> _procesos;
        private IEnumerable<ProcesoModel> procesos;


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

                OnPropertyChanged("SelectedProceso");
            }
        }

        #endregion
    }
}
