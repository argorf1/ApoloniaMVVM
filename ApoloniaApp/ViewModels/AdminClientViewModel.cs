using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Services;
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
        private ListStore _listStore;
        private FuncionarioModel _crudFuncionario;
        public UsuarioInternoModel CurrentAccount;

        private readonly ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;
        private UnidadModel _selectedUnidad;

        private readonly ObservableCollection<FuncionarioModel> _funcionarios;
        private IEnumerable<FuncionarioModel> funcionarios;
        private FuncionarioModel _selectedFuncionario;
        public AdminClientViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _crudFuncionario = new FuncionarioModel();



            _unidades = ChargeComboBoxService<UnidadModel>.ChargeComboBox(_listStore.unidades, _unidades, new UnidadModel() { Rut = "0", RazonSocial = "-- Unidad --" });
            _funcionarios = _listStore.funcionarios;

            Funcionarios = _funcionarios;
            SelectedUnidad = _unidades.Last(p => p.Rut == _crudFuncionario.Unidad.Rut || p.Rut == "0");
            NavigationCreateUsers = new NavigatePanelCommand<AdminClientCRUDViewModel>(_frameStore, () => new AdminClientCRUDViewModel(1, _frameStore, CurrentAccount, new FuncionarioModel() { Password = "12345678" }, _listStore));
            NavigationEditUsers = new NavigatePanelCommand<AdminClientCRUDViewModel>(_frameStore, () => new AdminClientCRUDViewModel(2, _frameStore, CurrentAccount, _crudFuncionario, _listStore));

        }

        #region Property

        public UnidadModel SelectedUnidad
        {
            get { return _selectedUnidad; }
            set
            {
                _selectedUnidad = value;
                if (_selectedUnidad.Rut != "-- Unidad --")
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
            get { return _selectedFuncionario; }
            set
            {
                _selectedFuncionario = value != null ? value: new FuncionarioModel();

                _crudFuncionario.Run         = value.Run;
                _crudFuncionario.Nombre      = value.Nombre;
                _crudFuncionario.ApellidoP   = value.ApellidoP;
                _crudFuncionario.ApellidoM   = value.ApellidoM;
                _crudFuncionario.Email       = value.ApellidoP;
                _crudFuncionario.Rol         = value.Rol;
                _crudFuncionario.Estado      = value.Estado;
                _crudFuncionario.Unidad      = value.Unidad;
                _crudFuncionario.Subunidad   = value.Subunidad;
                OnPropertyChanged("SelectedFuncionario");

                CanEdit = SelectedFuncionario.Run != ""? true : false;

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

        private bool _canEdit = false;
        public bool CanEdit
        {
            get => _canEdit;
            set
            {
                _canEdit = value;
                OnPropertyChanged("CanEdit");
            }
        }

        public string Run
        {
            get
            {
                return SelectedFuncionario != null ? _crudFuncionario.Run:"";
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
                return SelectedFuncionario != null ? _crudFuncionario.Nombre : "";
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
                return SelectedFuncionario != null ? _crudFuncionario.ApellidoP : "";
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
                return SelectedFuncionario != null ? _crudFuncionario.ApellidoM : "";
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
                return SelectedFuncionario != null ? _crudFuncionario.Email : "";
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
                return SelectedFuncionario != null ? _crudFuncionario.Rol.Nombre : "";
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
                return SelectedFuncionario != null ? _crudFuncionario.Estado.Nombre : "";
            }
            set
            {
                _crudFuncionario.Estado.Nombre = value;
                OnPropertyChanged("Estado");
            }
        }

        public string Unidad
        {
            get { return SelectedFuncionario != null ? _crudFuncionario.Unidad.RazonSocial : ""; }
            set
            {
                _crudFuncionario.Unidad.RazonSocial = value;
                OnPropertyChanged("Unidad");
            }
        }

        public string Subunidad
        {
            get { return SelectedFuncionario != null ? _crudFuncionario.Subunidad.Nombre : ""; }
            set
            {
                _crudFuncionario.Subunidad.Nombre = value;
                OnPropertyChanged("Subunidad");
            }
        }
        #endregion


        public ICommand NavigationCreateUsers { get; }
        public ICommand NavigationEditUsers { get; }
    }
}
