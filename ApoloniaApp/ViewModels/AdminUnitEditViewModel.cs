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
    class AdminUnitEditViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private readonly ListStore _listStore;
        private UnidadModel _editUnit;
        public UsuarioInternoModel CurrentAccount;


        #region Definicion Listas
        private readonly ObservableCollection<RubroModel> _rubros;
        public IEnumerable<RubroModel> Rubros => _rubros;
        private RubroModel _selectedRubro;
        private int _selectedRubroIndex;

        private readonly ObservableCollection<RegionModel> _regiones;
        public IEnumerable<RegionModel> Regiones => _regiones;
        private RegionModel _selectedRegion;
        private int _selectedRegionIndex;

        private readonly ObservableCollection<ProvinciaModel> _provincias;
        private IEnumerable<ProvinciaModel> provincias;
        private ProvinciaModel _selectedProvincia;
        private int _selectedProvinciaIndex;

        private readonly ObservableCollection<ComunaModel> _comunas;
        private IEnumerable<ComunaModel> comunas;
        private ComunaModel _selectedComuna;
        private int _selectedComunaIndex;

        private readonly ObservableCollection<UsuarioInternoModel> _responsables;
        public IEnumerable<UsuarioInternoModel> Responsables => _responsables;
        private UsuarioInternoModel _selectedResponsable;
        private int _selectedReponsableIndex;

        private readonly ObservableCollection<EstadoModel> _estados;
        public IEnumerable<EstadoModel> Estados => _estados;
        private EstadoModel _selectedEstado;

        //private readonly ObservableCollection<SubUnidadTipo> _subUnidadTipo;
        //public IEnumerable<SubUnidadTipo> SubUnidadTipo => _subUnidadTipo;
        //private SubUnidadTipo _selectedSubUnidad;
        #endregion

        #region Propiedades
        public string Rut
        {
            get
            {
                return _editUnit.Rut;
            }
            set
            {
                _editUnit.Rut = value;
                OnPropertyChanged("Rut");
            }
        }
        public string RazonSocial
        {
            get
            {
                return _editUnit.RazonSocial;
            }
            set
            {
                _editUnit.RazonSocial = value;
                OnPropertyChanged("RazonSocial");
            }
        }
        public int SelectedRubroIndex
        {
            get
            {
                return _selectedRubroIndex;
            }
            set
            {
                _selectedRubroIndex = value;
                OnPropertyChanged("SelectedRubroIndex");
            }
        }
        public RubroModel SelectedRubro
        {
            get { return _selectedRubro; }
            set
            {
                _selectedRubro = value;
                _editUnit.RubroId = _selectedRubro.Id;
                OnPropertyChanged("SelectedRubro");
            }
        }
        public int SelectedRegionIndex
        {
            get
            {
                return _selectedRegionIndex;
            }
            set
            {
                _selectedRegionIndex = value;
                OnPropertyChanged("SelectedRegionIndex");
            }
        }
        public RegionModel SelectedRegion
        {
            get { return _selectedRegion; }
            set
            {
                _selectedRegion = value;
                if (_selectedRegion.Id > 0)
                    Provincias = _provincias.Where(p => p.IdRegion == _selectedRegion.Id || p.Id == 0).ToList();
                else
                    Provincias = _provincias.Where(p => p.Id == 0).ToList();
                OnPropertyChanged("SelectedRegion");
                OnPropertyChanged("Provincias");
            }
        }

        public IEnumerable<ProvinciaModel> Provincias
        {
            get
            {
                return provincias;
            }
            set
            {
                provincias = value;
                OnPropertyChanged("Provincias");
            }
        }

        public int SelectedProvinciaIndex
        {
            get
            {
                return _selectedProvinciaIndex;
            }
            set
            {
                _selectedProvinciaIndex = value;
                OnPropertyChanged("SelectedProvinciaIndex");
            }
        }
        public ProvinciaModel SelectedProvincia
        {
            get { return _selectedProvincia; }
            set
            {
                _selectedProvincia = value;
                if (_selectedProvincia.Id > 0)
                    Comunas = _comunas.Where(p => p.IdProvincia == _selectedProvincia.Id || p.Id == 0).ToList();
                else
                    Comunas = _comunas.Where(p => p.Id == 0).ToList();
                OnPropertyChanged("SelectedProvincia");
                OnPropertyChanged("Comunas");
            }
        }
        public IEnumerable<ComunaModel> Comunas
        {
            get
            {
                return comunas;
            }
            set
            {
                comunas = value;
                OnPropertyChanged("Comunas");
            }
        }

        public int SelectedComunaIndex
        {
            get
            {
                return _selectedComunaIndex;
            }
            set
            {
                _selectedComunaIndex = value;
                OnPropertyChanged("SelectedComunaIndex");
            }
        }
        public ComunaModel SelectedComuna
        {
            get { return _selectedComuna; }
            set
            {
                _selectedComuna = value;
                if(_selectedComuna.Id > 0)
                    _editUnit.ComunaId = _selectedComuna.Id;
                OnPropertyChanged("SelectedComuna");
            }
        }
        public string PersonaContacto
        {
            get
            {
                return _editUnit.PersonaContacto;
            }
            set
            {
                _editUnit.PersonaContacto = value;
                OnPropertyChanged("PersonaContacto");
            }
        }
        public string TelefonoContacto
        {
            get
            {
                if (_editUnit.TelefonoContacto > 0)
                    return _editUnit.TelefonoContacto.ToString();
                else
                    return "";
            }
            set
            {
                _editUnit.TelefonoContacto = int.Parse(value);
                OnPropertyChanged("TelefonoContacto");
            }
        }
        public string EmailContacto
        {
            get
            {
                return _editUnit.EmailContacto;
            }
            set
            {
                _editUnit.EmailContacto = value;
                OnPropertyChanged("EmailContacto");
            }
        }
        public string Calle
        {
            get
            {
                return _editUnit.Calle;
            }
            set
            {
                _editUnit.Calle = value;
                OnPropertyChanged("Calle");
            }
        }
        public string Numero
        {
            get
            {
                return _editUnit.Numero;
            }
            set
            {
                _editUnit.Numero = value;
                OnPropertyChanged("Numero");
            }
        }
        public string Complemento
        {
            get
            {
                return _editUnit.Complemento;
            }
            set
            {
                _editUnit.Complemento = value;
                OnPropertyChanged("Complemento");
            }
        }

        public int SelectedResponsableIndex
        {
            get { return _selectedReponsableIndex; }
            set
            {
                _selectedReponsableIndex = value;
                OnPropertyChanged("SelectedResponsableIndex");
            }
        }
        public UsuarioInternoModel SelectedResponsable
        {
            get
            {
                return _selectedResponsable;
            }
            set
            {
                _selectedResponsable = value;
                _editUnit.ResponsableNombre = _selectedResponsable.NombreCompleto;
                _editUnit.ResponsableRun = _selectedResponsable.Run;
                OnPropertyChanged("SelectedResponsable");
            }
        }

        public EstadoModel SelectedEstado
        {
            get { return _selectedEstado; }
            set
            {
                _selectedEstado = value;
                OnPropertyChanged("SelectedEstado");
            }
        }
        #endregion

        public AdminUnitEditViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, UnidadModel editUnit, ListStore listStore)
        {
            _frameStore = frameStore;
            _listStore = listStore;
            CurrentAccount = currentAccount;
            _editUnit = editUnit;
            #region Listas
            _rubros = _listStore.rubros;
            _regiones = _listStore.regiones;
            _provincias = _listStore.provincias;
            _comunas = _listStore.comunas;
            _responsables = _listStore.designers;
            _estados = _listStore.estados;

            Provincias = _provincias.Where(p => p.Id == 0).ToList();
            Comunas = _comunas.Where(p => p.Id == 0).ToList();

            SelectedRubro = _rubros.First(p => p.Id == _editUnit.RubroId);
            SelectedRegion =_regiones.First(p => p.Detalle == _editUnit.Region);
            SelectedProvincia = Provincias.First(p => p.Detalle == _editUnit.Provincia);
            SelectedComuna = Comunas.First(p => p.Detalle == _editUnit.Comuna);
            SelectedResponsable = _responsables.FirstOrDefault(p => p.Run == _editUnit.ResponsableRun);
            SelectedEstado = _estados.ElementAt(_editUnit.EstadoId);
            #endregion

            #region Buttons
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore));
            EditUnit = new CRUDCommand<AdminUnitViewModel, UnidadModel>(() => _editUnit.Update(), () => new AdminUnitViewModel(_frameStore, CurrentAccount,_listStore), _frameStore, _editUnit);
            #endregion
        }

        public ICommand NavigationUnit { get; }

        public ICommand EditUnit { get; }
    }
}
