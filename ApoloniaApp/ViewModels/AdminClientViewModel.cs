using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminClientViewModel : ViewModelBase
    {

        
        private readonly FrameStore _frameStore;
        private FuncionarioModel _crudFuncionario;
        public UsuarioInternoModel CurrentAccount;

        private readonly ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;
        private UnidadModel _selectedUnidad;

        private readonly ObservableCollection<FuncionarioModel> _funcionarios;
        private IEnumerable<FuncionarioModel> funcionarios;

        #region Property

        public UnidadModel SelectedUnidad
        {
            get { return _selectedUnidad; }
            set
            {
                _selectedUnidad = value;
                if(_selectedUnidad.Rut != "-- Unidad --")
                {
                    Funcionarios = _funcionarios.Where(p => p.Unidad.Rut == _selectedUnidad.Rut);
                }
                OnPropertyChanged("SelectedUnidad");
            }
        }

        public IEnumerable<FuncionarioModel> Funcionarios
        {
            get { return funcionarios; } 
            set
            {
                funcionarios = value;
                OnPropertyChanged("Funcionarios");
            }
        }

        public FuncionarioModel SelectedFuncionario
        {
            get { return _crudFuncionario; }
            set
            {
                _crudFuncionario = value;
                OnPropertyChanged("SelectedFuncionario");

                OnPropertyChanged("Run");
                OnPropertyChanged("Nombre");
                OnPropertyChanged("ApellidoP");
                OnPropertyChanged("ApellidoM");
                OnPropertyChanged("Email");
                OnPropertyChanged("Rol");
                OnPropertyChanged("Estado");
                OnPropertyChanged("Username");
                OnPropertyChanged("Unidad");
                OnPropertyChanged("Subunidad");
            }
        }


        public string Run
        {
            get
            {
                return _crudFuncionario.Run;
            }
            set
            {
                _crudFuncionario.Run = value;
                OnPropertyChanged("Run");
            }
        }
        public string Nombre
        {
            get
            {
                return _crudFuncionario.Nombre;
            }
            set
            {
                _crudFuncionario.Nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string ApellidoP
        {
            get
            {
                return _crudFuncionario.ApellidoP;
            }
            set
            {
                _crudFuncionario.ApellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get
            {
                return _crudFuncionario.ApellidoM;
            }
            set
            {
                _crudFuncionario.ApellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get
            {
                return _crudFuncionario.Email;
            }
            set
            {
                _crudFuncionario.Email = value;
                OnPropertyChanged("Email");
            }
        }

        public string Rol
        {
            get
            {
                return _crudFuncionario.Rol.Nombre;
            }
            set
            {
                _crudFuncionario.Rol.Nombre = value;
                OnPropertyChanged("Rol");
            }
        }

        public string Estado
        {
            get
            {
                return _crudFuncionario.Estado.Detalle;
            }
            set
            {
                _crudFuncionario.Estado.Detalle = value;
                OnPropertyChanged("Estado");
            }
        }

        public string Username
        {
            get
            {
                return _crudFuncionario.Username;
            }
            set
            {
                _crudFuncionario.Username = value;
                OnPropertyChanged("Username");
            }
        }

        public string Unidad
        {
            get { return _crudFuncionario.Unidad.RazonSocial; }
            set
            {
                _crudFuncionario.Unidad.RazonSocial= value;
                OnPropertyChanged("Unidad");
            }
        }

        public string Subunidad
        {
            get { return _crudFuncionario.Subunidad.Nombre; }
            set
            {
                _crudFuncionario.Subunidad.Nombre = value;
                OnPropertyChanged("Subunidad");
            }
        }
        #endregion

        public AdminClientViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crudFuncionario = new FuncionarioModel();

            _unidades = new ObservableCollection<UnidadModel>();
            _unidades.Add(new UnidadModel() { RazonSocial = "-- Unidad --" });
            foreach (UnidadModel u in new UnidadModel().ReadDesigner())
            {
                _unidades.Add(u);
            }

            _funcionarios = new ObservableCollection<FuncionarioModel>();
            foreach (FuncionarioModel f in new FuncionarioModel().ReadAll())
            {
                _funcionarios.Add(f);
            }

            Funcionarios = _funcionarios;
            SelectedUnidad = _unidades.First(p => p.Rut == _crudFuncionario.Unidad.Rut);
            NavigationCreateUsers = new NavigatePanelCommand<AdminClientCRUDViewModel>(_frameStore, () => new AdminClientCRUDViewModel(1,_frameStore, CurrentAccount, new FuncionarioModel() { Password = "12345678"}));
            NavigationEditUsers = new NavigatePanelCommand<AdminClientCRUDViewModel>(_frameStore, () => new AdminClientCRUDViewModel(2, _frameStore, CurrentAccount, _crudFuncionario));

        }

        public ICommand NavigationCreateUsers { get; }
        public ICommand NavigationEditUsers { get; }
    }
}
