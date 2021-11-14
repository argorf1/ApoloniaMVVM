using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class DPTareaCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private TareaModel _crudTarea;

        #region Commands
        public ICommand Crud { get; }
        #endregion
        public DPTareaCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, TareaModel crudTarea, int estado, ObservableCollection<ResponsableModel> responsables, ObservableCollection<DependenciaModel> dependencias)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crudTarea = crudTarea;
            _estado = estado;
            _responsables = responsables;
            _dependencias = dependencias;
            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Tarea");
            _estadoDetalle.Add(2, "Modificar Tarea");
            #endregion


            switch (_estado)
            {
                case 1:
                    Crud = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Create(), () => new DPProcesosViewModel(_frameStore, CurrentAccount), _frameStore, () => _crudTarea.ReadByName(), _crudTarea);
                    break;
                case 2:
                    Crud = new CRUDCommand<DPProcesosViewModel, TareaModel>(() => _crudTarea.Update(), () => new DPProcesosViewModel(_frameStore, CurrentAccount), _frameStore, _crudTarea);
                    break;
                default:
                    break;
            }
            #endregion

            #region Carga Listas
            _funcionarios = new ReadAllCommand<ResponsableModel>().ReadAll(()=> new ResponsableModel().ReadResponsableAll());
            _tareas = new ReadAllCommand<DependenciaModel>().ReadAll(() => new DependenciaModel().ReadTareasAll());
            #endregion


            _selectedDependencia = new DependenciaModel();
            _selectedTarea = new DependenciaModel();

            _selectedResponsable = new ResponsableModel();
            _selectedFuncionario = new ResponsableModel();


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
        private ObservableCollection<ResponsableModel> _funcionarios;
        public IEnumerable<ResponsableModel> Funcionarios => _funcionarios;
        private ResponsableModel _selectedFuncionario;

        public ResponsableModel SelectedFuncionario
        {

            get { return _selectedFuncionario; }
            set
            {
                _selectedFuncionario = value;
                OnPropertyChanged("SelectedFuncionario");
            }
        }

        private ObservableCollection<ResponsableModel> _responsables;
        public IEnumerable<ResponsableModel> Responsables => _responsables;
        private ResponsableModel _selectedResponsable;

        public ResponsableModel SelectedResponsable
        {

            get { return _selectedResponsable; }
            set
            {
                _selectedResponsable = value;
                OnPropertyChanged("SelectedResponsable");
            }
        }

        private ObservableCollection<DependenciaModel> _tareas;
        public IEnumerable<DependenciaModel> Tareas => _tareas;
        private DependenciaModel _selectedTarea;

        public DependenciaModel SelectedTarea
        {

            get { return _selectedTarea; }
            set
            {
                _selectedTarea = value;
                OnPropertyChanged("SelectedTarea");
            }
        }

        private ObservableCollection<DependenciaModel> _dependencias;
        public IEnumerable<DependenciaModel> Dependencias => _dependencias;
        private DependenciaModel _selectedDependencia;

        public DependenciaModel SelectedDependencia
        {

            get { return _selectedDependencia; }
            set
            {
                _selectedDependencia = value;
                OnPropertyChanged("SelectedDependencia");
            }
        }

        #endregion


        private void ExchangeList<TModel>(ObservableCollection<TModel> source, ObservableCollection<TModel> target, TModel model)
            where TModel : ModelBase
        {
            if (model != null)
            {
                source.Remove(model);
                target.Add(model);
                model = null;
            }
        }
        private ICommand _addResponsable;
        public ICommand AddResponsable
        {
            get
            {
                return _addResponsable ?? (_addResponsable = new CommandHandler(() => ExchangeList<ResponsableModel>(_funcionarios,_responsables,SelectedFuncionario), true));
            }
        }
        private ICommand _extractResponsable;
        public ICommand ExtractResponsable
        {
            get
            {
                return _extractResponsable ?? (_extractResponsable = new CommandHandler(() => ExchangeList<ResponsableModel>(_responsables,_funcionarios,SelectedResponsable), true));
            }
        }
        private ICommand _addDependencia;
        public ICommand AddDependencia
        {
            get
            {
                return _addDependencia ?? (_addDependencia = new CommandHandler(() => ExchangeList<DependenciaModel>(_tareas,_dependencias,SelectedTarea), true));
            }
        }
        private ICommand _extractDependencia;
        public ICommand ExtractDependencia
        {
            get
            {
                return _extractDependencia ?? (_extractDependencia = new CommandHandler(() => ExchangeList<DependenciaModel>(_dependencias,_tareas,SelectedDependencia), true));
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
}
