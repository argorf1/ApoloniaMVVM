using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminUnitCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private UnidadModel _crud;
        private int _estado = 0;
        private bool _editing = false;
        private ListStore _listStore;

        public ICommand CrudCommand { get; }
        public ICommand ReturnCommand { get; }
        public AdminUnitCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, UnidadModel crudUnidad, int estado, ListStore listStore, AdminViewModel mainView)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crud = crudUnidad;
            _estado = estado;
            _listStore = listStore;

            mainView.IsCheck = false;

            #region Configuracion Estado

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Unidad");
            _estadoDetalle.Add(2, "Modificar Unidad");
            #endregion

            #region Carga Listas
            _rubros = _listStore.rubros;
            _regiones = _listStore.regiones;
            _provincias = _listStore.provincias;
            _comunas = _listStore.comunas;
            _responsables = _listStore.designers;
            _estados = _listStore.estados;

            SelectedRubro = _rubros.First(p => p.Id == _crud.Rubro.Id);
            SelectedResponsable = _responsables.First(p => p.Run == _crud.Responsable.Run);
            SelectedEstado = _estados.First(p => p.Id == _crud.Estado.Id);

            string region = _crud.Direccion.Region.Nombre;
            string provincia = _crud.Direccion.Provincia.Nombre;
            string comuna = _crud.Direccion.Comuna.Nombre;
            #endregion

            _validations = new List<Func<bool>>();
            switch (_estado)
            {
                case 1:
                    CrudCommand = new CRUDCommand<AdminUnitViewModel, UnidadModel>(() => _crud.Create(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, () => _crud.ReadByRut(), _crud, () => _listStore.Unidades(), 1);
                    SelectedRegion = _regiones.Last(p => p.Id == 0);
                    SelectedProvincia = provincias.Last(p => p.Id == 0);
                    SelectedComuna = comunas.Last(p => p.Id == 0);
                    #region CargaValidaciones
                    _validations.AddRange(new List<Func<bool>>()
                    {
                        ()=> ValidationService.Run(Rut),
                        ()=> ValidationService.Text(RazonSocial),
                        ()=> ValidationService.ComboBoxId(SelectedRubro.Id),
                        ()=> ValidationService.Text(Calle),
                        ()=> ValidationService.Text(Numero),
                        ()=> ValidationService.Text(Complemento),
                        ()=> ValidationService.ComboBoxId(SelectedComuna.Id),
                        ()=> ValidationService.Text(PersonaContacto),
                        ()=> ValidationService.Text(TelefonoContacto),
                        ()=> ValidationService.Email(EmailContacto),
                        ()=> ValidationService.Run(SelectedResponsable.Run)
                    });
                    #endregion
                    break;
                case 2:
                    _editing = true;
                    CrudCommand = new CRUDCommand<AdminUnitViewModel, UnidadModel>(() => _crud.Update(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore, mainView), _frameStore, _crud, () => _listStore.Unidades(), 2);
                    SelectedRegion = _regiones.Last(p => p.Nombre == region);
                    SelectedProvincia = provincias.Last(p => p.Nombre == provincia);
                    SelectedComuna = comunas.Last(p => p.Nombre == comuna);
                    #region CargaValidaciones
                    _validations.AddRange(new List<Func<bool>>()
                    {
                        ()=> ValidationService.Run(Rut),
                        ()=> ValidationService.Text(RazonSocial),
                        ()=> ValidationService.ComboBoxId(SelectedRubro.Id),
                        ()=> ValidationService.Text(Calle),
                        ()=> ValidationService.Text(Numero),
                        ()=> ValidationService.Text(Complemento),
                        ()=> ValidationService.ComboBoxId(SelectedComuna.Id),
                        ()=> ValidationService.Text(PersonaContacto),
                        ()=> ValidationService.Text(TelefonoContacto),
                        ()=> ValidationService.Email(EmailContacto),
                        ()=> ValidationService.Run(SelectedResponsable.Run),
                        ()=> ValidationService.ComboBoxId(SelectedEstado.Id)
                    });
                    #endregion

                    break;
                default:
                    break;
            }

            ReturnCommand = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore, mainView));
            #endregion

        }

        #region Configuracion Vista Estado

        Dictionary<int, string> _estadoDetalle = new Dictionary<int, string>();

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

        public bool EstadoEdit
        {
            get => _estado == 2;
        }
        #endregion

        #region Validation Properties
        private List<Func<bool>> _validations;


        public bool CanCrud
        {
            get => ValidationService.All(_validations);
            set
            {
                OnPropertyChanged("CanCrud");
            }
        }
        #endregion

        #region Properties
        private bool _validRut = false;
        public bool ValidRut
        {
            get => _validRut;
            set
            {
                _validRut = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidRut");
            }
        }
        public string Rut
        {
            get => _crud.Rut;
            set
            {
                _crud.Rut = value;
                ValidRut = ValidationService.Run(Rut);
                OnPropertyChanged("Rut");
            }
        }

        private bool _validRazonSocial = false;
        public bool ValidRazonSocial
        {
            get => _validRazonSocial;
            set
            {
                _validRazonSocial = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidRazonSocial");
            }
        }
        public string RazonSocial
        {
            get => _crud.RazonSocial;
            set
            {
                _crud.RazonSocial = value;
                ValidRazonSocial = ValidationService.Text(RazonSocial);
                OnPropertyChanged("RazonSocial");
            }
        }

        private readonly ObservableCollection<RubroModel> _rubros;
        public IEnumerable Rubros => _rubros;
        private bool _validRubro = false;
        public bool ValidRubro
        {
            get => _validRubro;
            set
            {
                _validRubro = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidRubro");
            }
        }
        public RubroModel SelectedRubro
        {
            get => _crud.Rubro;
            set
            {
                _crud.Rubro = value;
                ValidRubro = ValidationService.ComboBoxId(SelectedRubro.Id);
                OnPropertyChanged("SelectedRubro");
            }
        }

        private bool _validCalle = false;
        public bool ValidCalle
        {
            get => _validCalle;
            set
            {
                _validCalle = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidCalle");
            }
        }
        public string Calle
        {
            get => _crud.Direccion.Calle;
            set
            {
                _crud.Direccion.Calle = value;

                ValidCalle = ValidationService.Text(Calle);
                OnPropertyChanged("Calle");
            }
        }
        private bool _validNumero = false;
        public bool ValidNumero
        {
            get => _validNumero;
            set
            {
                _validNumero = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidNumero");
            }
        }
        public string Numero
        {
            get => _crud.Direccion.Numero;
            set
            {
                _crud.Direccion.Numero = value;

                ValidNumero = ValidationService.Text(Numero);
                OnPropertyChanged("Numero");
            }
        }

        private bool _validComplemento = false;
        public bool ValidComplemento
        {
            get => _validComplemento;
            set
            {
                _validComplemento = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidComplemento");
            }
        }
        public string Complemento
        {
            get => _crud.Direccion.Complemento;
            set
            {
                _crud.Direccion.Complemento = value;

                ValidComplemento = ValidationService.Text(Complemento);
                OnPropertyChanged("Complemento");
            }

        }

        private readonly ObservableCollection<RegionModel> _regiones;
        public IEnumerable<RegionModel> Regiones => _regiones;

        public RegionModel SelectedRegion
        {
            get => _crud.Direccion.Region;
            set
            {
                _crud.Direccion.Region = value;
                Provincias = value.Id != 0 ? _provincias.Where(p => p.IdRegion == _crud.Direccion.Region.Id || p.Id == 0): _provincias.Where(p=> p.Id == 0);
                OnPropertyChanged("SelectedRegion");
            }
        }

        private readonly ObservableCollection<ProvinciaModel> _provincias;
        private IEnumerable<ProvinciaModel> provincias;

        public IEnumerable<ProvinciaModel> Provincias
        {
            get => provincias;
            set
            {
                provincias = value;
                SelectedProvincia = provincias.First(p => p.Id == 0);
                OnPropertyChanged("Provincias");
            }
        }

        public ProvinciaModel SelectedProvincia
        {
            get => _crud.Direccion.Provincia;
            set
            {
                if(!_editing)
                    _crud.Direccion.Provincia = value;

                Comunas = value != null ? _comunas.Where(p => p.IdProvincia == value.Id || p.Id == 0): _comunas.Where(p => p.Id == 0);
                OnPropertyChanged("SelectedProvincia");
            }
        }

        private readonly ObservableCollection<ComunaModel> _comunas;
        private IEnumerable<ComunaModel> comunas;

        private bool _validComuna = false;
        public bool ValidComuna
        {
            get => _validComuna;
            set
            {
                _validComuna = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidComuna");
            }
        }
        public IEnumerable<ComunaModel> Comunas
        {
            get => comunas;
            set
            {
                comunas = value;
                if (!_editing)
                    SelectedComuna = comunas.First(p => p.Id == 0);
                else
                    _editing = false;
                OnPropertyChanged("Comunas");
            }
        }
        public ComunaModel SelectedComuna
        {
            get => _crud.Direccion.Comuna;
            set
            {
                
                _crud.Direccion.Comuna = value;
                
                ValidComuna = ValidationService.ComboBoxId(SelectedComuna.Id);
                OnPropertyChanged("SelectedComuna");

            }
        }


        private bool _validPContacto = false;
        public bool ValidPContacto
        {
            get => _validPContacto;
            set
            {
                _validPContacto = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidPContacto");
            }
        }
        public string PersonaContacto
        {
            get
            {
                return _crud.PersonaContacto;
            }
            set
            {
                _crud.PersonaContacto = value;
                ValidationService.Text(PersonaContacto);
                OnPropertyChanged("PersonaContacto");
            }
        }

        private bool _validTContacto = false;
        public bool ValidTContacto
        {
            get => _validTContacto;
            set
            {
                _validTContacto = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidTContacto");
            }
        }
        public string TelefonoContacto
        {
            get=> _crud.TelefonoContacto;

            set
            {
                _crud.TelefonoContacto = value;

                ValidTContacto = ValidationService.PhoneNumber(TelefonoContacto);
                OnPropertyChanged("TelefonoContacto");
            }
        }

        private bool _validEmail = false;
        public bool ValidEmail
        {
            get => _validEmail;
            set
            {
                _validEmail = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidEmail");
            }
        }

        public string EmailContacto
        {
            get
            {
                return _crud.EmailContacto;
            }
            set
            {
                _crud.EmailContacto = value;

                ValidEmail = ValidationService.Email(EmailContacto);
                OnPropertyChanged("EmailContacto");
            }
        }

        private ObservableCollection<UsuarioInternoModel> _responsables;
        public IEnumerable<UsuarioInternoModel> Responsables => _responsables;
        private bool _validResponsable = false;
        public bool ValidResponsable
        {
            get => _validResponsable;
            set
            {
                _validResponsable = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidResponsable");
            }
        }

        public UsuarioInternoModel SelectedResponsable
        {
            get => _crud.Responsable;
            set
            {
                _crud.Responsable = value;
                ValidResponsable = ValidationService.Run(SelectedResponsable.Run);
                OnPropertyChanged("SelectedResponsable");
            }
        }

        private ObservableCollection<EstadoModel> _estados;
        public IEnumerable<EstadoModel> Estados => _estados;

        private bool _validEstado = false;
        public bool ValidEstado
        {
            get => _validEstado;
            set
            {
                _validEstado = value;

                OnPropertyChanged("CanCrud");

                OnPropertyChanged("ValidEstado");
            }
        }


        public EstadoModel SelectedEstado
        {
            get => _crud.Estado;
            set
            {
                _crud.Estado = value;
                ValidEstado = ValidationService.ComboBoxId(SelectedEstado.Id);
                OnPropertyChanged("SelectedEstado");
            }
        }
        #endregion
    }
}
