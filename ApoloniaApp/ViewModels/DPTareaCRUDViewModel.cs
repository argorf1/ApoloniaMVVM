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
    class DPTareaCRUDViewModel : ViewModelBase
    {
        private readonly ListStore _listStore;
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private TareaModel _crudTarea;
        private List<Func<bool>> _validations;
        private bool _initialized = false;

        #region Commands
        public ICommand Crud { get; }
        public ICommand Return { get; }
        #endregion
        public DPTareaCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, TareaModel crudTarea, int estado, UnidadModel unidad, ListStore listStore, ProcessDesignerViewModel mainView)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudTarea = crudTarea;
            _crudTarea.Creador = currentAccount;
            _estado = estado;

            mainView.IsCheck = false;

            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Tarea");
            _estadoDetalle.Add(2, "Modificar Tarea");

            _validField.Add(0, "/Assets/Icons/ico_required.png");
            _validField.Add(1, "/Assets/Icons/ico_valid.png");
            _validField.Add(2, "/Assets/Icons/icon_invalid.png");
            #endregion


            switch (_estado)
            {
                case 1:
                    Crud = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Create(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, () => _crudTarea.ReadByName(), _crudTarea, () => _listStore.TareasView(), 1);
                    break;
                case 2:
                    Crud = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Update(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, _crudTarea, () => _listStore.TareasView(), 2);
                    break;
                default:
                    break;
            }
            #endregion
            Return = new NavigatePanelCommand<DPProcesosViewModel>(_frameStore, () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore, mainView));

            #region Carga Listas
            _responsables = ChargeComboBoxService<FuncionarioModel>.ChargeComboBox(_listStore.funcionarios.Where(p => p.Unidad.Rut == unidad.Rut), _responsables, new FuncionarioModel() {Run="0", Nombre="-- Responsable --" });
            _dependencias = ChargeComboBoxService<TareaModel>.ChargeComboBox(_listStore.tareas.Where(p => p.Proceso.Id == _crudTarea.Proceso.Id),_dependencias, new TareaModel() { Id = 0, Nombre = "-- Dependencia"});
            #endregion


            SelectedDependencia = _dependencias.First(p=> p.Id == _crudTarea.Dependencia.Id);

            SelectedResponsable = _responsables.First(p=> p.Run == _crudTarea.Responsable.Run);


            #region CargaValidaciones
            _validations = new List<Func<bool>>();
            _validations.AddRange(new List<Func<bool>>()
            {
                 () => ValidationService.Text(Nombre)
                ,() => ValidationService.Text(Descripcion)
                ,() => ValidationService.Number(Duracion)
                ,() => ValidationService.Run(SelectedResponsable.Run)
            });
            #endregion

            _initialized = true;
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
        private ObservableCollection<FuncionarioModel> _responsables;
        public IEnumerable<FuncionarioModel> Responsables => _responsables;
        private FuncionarioModel _selectedResponsable;
        private bool _validResponsable;
        public FuncionarioModel SelectedResponsable
        {

            get { return _crudTarea.Responsable; }
            set
            {
                _crudTarea.Responsable = value;

                ValidResponsable = ValidationService.Run(value.Run);

                OnPropertyChanged("SelectedResponsable");
            }
        }

        public bool ValidResponsable
        {
            get => ValidationService.Run(SelectedResponsable.Run);
            set
            {
                _validResponsable = value;
                OnPropertyChanged("ValidResponsable");
                OnPropertyChanged("CanCrud");
            }
        }

        private ObservableCollection<TareaModel> _dependencias;
        public IEnumerable<TareaModel> Dependencias => _dependencias;
        private TareaModel _selectedDependencia;

        public TareaModel SelectedDependencia
        {

            get { return _crudTarea.Dependencia; }
            set
            {
                _crudTarea.Dependencia = value;

                OnPropertyChanged("SelectedDependencia");
            }
        }

        #endregion

        #region Propiedades

        private bool _canCrud = false;
        Dictionary<int, string> _validField = new Dictionary<int, string>();

        public bool CanCrud
        {
            get => ValidationService.All(_validations);
            set
            {
                _canCrud = value;
                OnPropertyChanged("CanCrud");
            }
        }
        private bool _initializedNombre = false;
        private bool _validNombre;
        public bool ValidNombre
        {
            get => ValidationService.Text(Nombre);
            set
            {
                _validNombre = value;
                OnPropertyChanged("ValidNombre");
                OnPropertyChanged("CanCrud");
            }
        }
        public string ImageNombre
        {
            get => _initializedNombre ? (ValidNombre ? _validField[1] : _validField[2]):_validField[0];
        }
        public string Nombre
        {
            get => _crudTarea.Nombre;
            set
            {
                _crudTarea.Nombre = value;
                ValidNombre = ValidationService.Text(Nombre);
                _initializedNombre = true;
                OnPropertyChanged("Nombre");
                OnPropertyChanged("ValidNombre");
                OnPropertyChanged("ImageNombre");
            }
        }

        private bool _validDescripcion;
        public bool ValidDescripcion
        {
            get => _validDescripcion;
            set
            {
                _validDescripcion = value;
                OnPropertyChanged("ValidDescripcion");
                OnPropertyChanged("CanCrud");
            }
        }

        public string Descripcion
        {
            get => _crudTarea.Descripcion;
            set
            {
                _crudTarea.Descripcion = value;
                ValidDescripcion = ValidationService.Text(Descripcion);

                OnPropertyChanged("Descripcion");
            }
        }

        private bool _validDuracion;
        public bool ValidDuracion
        {
            get => _validDuracion;
            set
            {
                _validDuracion = value;
                OnPropertyChanged("ValidDuracion");
                OnPropertyChanged("CanCrud");
            }
        }
        public int Duracion
        {
            get => _crudTarea.Duracion;
            set
            {
                _crudTarea.Duracion = value;
                _validDuracion = ValidationService.Number(Duracion);
                OnPropertyChanged("Duracion");
            }
        }
        #endregion
        



       

    }

    

}
