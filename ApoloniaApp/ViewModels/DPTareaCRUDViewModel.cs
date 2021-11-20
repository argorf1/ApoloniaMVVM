﻿using ApoloniaApp.Commands;
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
    class DPTareaCRUDViewModel : ViewModelBase
    {
        private readonly ListStore _listStore;
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private TareaModel _crudTarea;
        private List<Func<bool>> _validations;

        #region Commands
        public ICommand Crud { get; }
        public ICommand Return { get; }
        #endregion
        public DPTareaCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, TareaModel crudTarea, int estado, UnidadModel unidad, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudTarea = crudTarea;
            _crudTarea.Creador = currentAccount;
            _estado = estado;

            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Tarea");
            _estadoDetalle.Add(2, "Modificar Tarea");
            #endregion


            switch (_estado)
            {
                case 1:
                    Crud = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Create(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _crudTarea.ReadByName(), _crudTarea, _listStore.tareas, 1);
                    break;
                case 2:
                    Crud = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Update(), () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, _crudTarea, _listStore.tareas, 2);
                    break;
                default:
                    break;
            }
            #endregion
            Return = new NavigatePanelCommand<DPProcesosViewModel>(_frameStore, () => new DPProcesosViewModel(_frameStore, CurrentAccount, _listStore));

            #region Carga Listas
            _funcionarios = new ObservableCollection<FuncionarioModel>();
            foreach (FuncionarioModel f in _listStore.funcionarios.Where(p=> p.Unidad.Rut == unidad.Rut))
            {
                _funcionarios.Add(f);
            }
            _tareas = new ObservableCollection<TareaModel>();
            foreach (TareaModel t in _listStore.tareas.Where(p=> p.Proceso.Id == _crudTarea.Proceso.Id))
            {
                _tareas.Add(t);
            }
            _responsables = new ObservableCollection<FuncionarioModel>();
            foreach (ResponsableModel r in _listStore.responsables.Where(p=> p.IdTarea == _crudTarea.Id))
            {
                FuncionarioModel f = _funcionarios.First(p => p.Run == r.Responsable.Run);
                _crudTarea.Responsables.Add(f);
                _funcionarios.Remove(f);
            }
            _dependencias = new ObservableCollection<TareaModel>();
            foreach (DependenciaModel d in _listStore.dependencias.Where(p=> p.IdTarea == _crudTarea.Id))
            {
                TareaModel t = _tareas.First(p => p.Id == d.Tarea.Id);
                _crudTarea.Dependencias.Add(t);
                _tareas.Remove(t);
            }
            _responsables = _crudTarea.Responsables;
            _dependencias = _crudTarea.Dependencias;
            #endregion


            _selectedDependencia = new TareaModel();
            _selectedTarea = new TareaModel();

            _selectedResponsable = new FuncionarioModel();
            _selectedFuncionario = new FuncionarioModel();


            #region CargaValidaciones
            _validations = new List<Func<bool>>();
            _validations.AddRange(new List<Func<bool>>()
            {
                ValidateNombre,
                ValidateDescripcion,
                ValidateDuracion,
                ValidateResponsable
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
        private ObservableCollection<FuncionarioModel> _funcionarios;
        public IEnumerable<FuncionarioModel> Funcionarios => _funcionarios;
        private FuncionarioModel _selectedFuncionario;

        public FuncionarioModel SelectedFuncionario
        {

            get { return _selectedFuncionario; }
            set
            {
                _selectedFuncionario = value;
                OnPropertyChanged("SelectedFuncionario");
            }
        }

        private ObservableCollection<FuncionarioModel> _responsables;
        public IEnumerable<FuncionarioModel> Responsables => _responsables;
        private FuncionarioModel _selectedResponsable;
        private bool _validResponsable;
        public FuncionarioModel SelectedResponsable
        {

            get { return _selectedResponsable; }
            set
            {
                _selectedResponsable = value;
                OnPropertyChanged("SelectedResponsable");
            }
        }

        public bool ValidResponsable
        {
            get => _validResponsable;
            set
            {
                _validResponsable = value;
                OnPropertyChanged("ValidResponsable");
            }
        }

        private ObservableCollection<TareaModel> _tareas;
        public IEnumerable<TareaModel> Tareas => _tareas;
        private TareaModel _selectedTarea;

        public TareaModel SelectedTarea
        {

            get { return _selectedTarea; }
            set
            {
                _selectedTarea = value;
                OnPropertyChanged("SelectedTarea");
            }
        }

        private ObservableCollection<TareaModel> _dependencias;
        public IEnumerable<TareaModel> Dependencias => _dependencias;
        private TareaModel _selectedDependencia;

        public TareaModel SelectedDependencia
        {

            get { return _selectedDependencia; }
            set
            {
                _selectedDependencia = value;
                OnPropertyChanged("SelectedDependencia");
            }
        }

        #endregion

        #region Propiedades

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

        private bool _validNombre;
        public string Nombre
        {
            get => _crudTarea.Nombre;
            set
            {
                _crudTarea.Nombre = value;
                _validNombre = ValidateNombre();
                // ImagenNobre = (_validNombre ? diccionarioImagenes[0]: diccionarioImagenes[1]);
                if (_validNombre)
                    ValidateAll();
                OnPropertyChanged("Nombre");
            }
        }

        private bool _validDescripcion;
        public string Descripcion
        {
            get => _crudTarea.Descripcion;
            set
            {
                _crudTarea.Descripcion = value;
                _validDescripcion = ValidateDescripcion();
                if (_validDescripcion)
                    ValidateAll();
                OnPropertyChanged("Descripcion");
            }
        }

        private bool _validDuracion;
        public int Duracion
        {
            get => _crudTarea.Duracion;
            set
            {
                _crudTarea.Duracion = value;
                _validDuracion = ValidateDuracion();
                if (_validDuracion)
                    ValidateAll();
                OnPropertyChanged("Duracion");
            }
        }
        #endregion
        #region Validaciones

        private bool ValidateNombre()
        {
            if (Nombre == "")
            {
                return false;
            }
            return true;
        }

        private bool ValidateDescripcion()
        {
            if (Descripcion == "")
            {
                return false;
            }
            return true;
        }

        private bool ValidateDuracion()
        {
            if (Duracion < 0)
            {
                return false;
            }
            return true;
        }

        private bool ValidateResponsable()
        {
            if (!Responsables.Any())
            {
                return false;
            }
            return true;
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

        #region Commands

        private void ExchangeList<TModel>(ObservableCollection<TModel> source, ObservableCollection<TModel> target, TModel model)
            where TModel : ModelBase
        {
            if (model != null)
            {
                source.Remove(model);
                target.Add(model);
                model = null;
                ValidateAll();
            }
        }
        private ICommand _addResponsable;
        public ICommand AddResponsable
        {
            get
            {
                return _addResponsable ?? (_addResponsable = new CommandHandler(() => ExchangeList<FuncionarioModel>(_funcionarios, _responsables, SelectedFuncionario), true));
            }
        }
        private ICommand _extractResponsable;
        public ICommand ExtractResponsable
        {
            get
            {
                return _extractResponsable ?? (_extractResponsable = new CommandHandler(() => ExchangeList<FuncionarioModel>(_responsables, _funcionarios, SelectedResponsable), true));
            }
        }
        private ICommand _addDependencia;
        public ICommand AddDependencia
        {
            get
            {
                return _addDependencia ?? (_addDependencia = new CommandHandler(() => ExchangeList<TareaModel>(_tareas, _dependencias, SelectedTarea), true));
            }
        }
        private ICommand _extractDependencia;
        public ICommand ExtractDependencia
        {
            get
            {
                return _extractDependencia ?? (_extractDependencia = new CommandHandler(() => ExchangeList<TareaModel>(_dependencias, _tareas, SelectedDependencia), true));
            }
        }

    }

    public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

    }
    #endregion
}
