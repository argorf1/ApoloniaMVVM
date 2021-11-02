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
    class AdminUnitCreateViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;



        private UnidadModel _createUnit = new UnidadModel() { EstadoId = 1 };
        #region Definicion Listas
        private readonly ObservableCollection<RubroModel> _rubros;
        public IEnumerable<RubroModel> Rubros => _rubros;
        private RubroModel _selectedRubro;

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

        //private readonly ObservableCollection<SubUnidadTipo> _subUnidadTipo;
        //public IEnumerable<SubUnidadTipo> SubUnidadTipo => _subUnidadTipo;
        //private SubUnidadTipo _selectedSubUnidad;
        #endregion

        #region Propiedades
        public string Rut
        {
            get
            {
                return _createUnit.Rut;
            }
            set
            {
                _createUnit.Rut = value;
                OnPropertyChanged("Rut");
            }
        }
        public string RazonSocial
        {
            get
            {
                return _createUnit.RazonSocial;
            }
            set
            {
                _createUnit.RazonSocial = value;
                OnPropertyChanged("RazonSocial");
            }
        }
        public int SelectedRubroIndex
        {
            get
            {
                return _createUnit.RubroId;
            }
            set
            {
                _createUnit.RubroId = value;
                OnPropertyChanged("SelectedRubroIndex");
            }
        }
        public RubroModel SelectedRubro
        {
            get { return _selectedRubro; }
            set
            {
                _selectedRubro = Rubros.ElementAt(_createUnit.RubroId);
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
                _selectedRegion = Regiones.ElementAt(_selectedRegionIndex);
                if(_selectedRegionIndex > 0)
                    Provincias = _provincias.Where(p => p.IdRegion == _selectedRegion.Id ||p.Id == 0).ToList();
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
                _selectedProvincia = Provincias.ElementAt(_selectedProvinciaIndex);
                if (_selectedRegionIndex > 0)
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
                _selectedComuna = Comunas.ElementAt(_selectedComunaIndex);
                OnPropertyChanged("SelectedComuna");
            }
        }
        public string PersonaContacto
        {
            get
            {
                return _createUnit.PersonaContacto;
            }
            set
            {
                _createUnit.PersonaContacto = value;
                OnPropertyChanged("PersonaContacto");
            }
        }
        public string TelefonoContacto
        {
            set
            {
                _createUnit.TelefonoContacto = int.Parse(value);
                OnPropertyChanged("TelefonoContacto");
            }
        }
        public string EmailContacto
        {
            get
            {
                return _createUnit.EmailContacto;
            }
            set
            {
                _createUnit.EmailContacto = value;
                OnPropertyChanged("EmailContacto");
            }
        }
        public string Calle
        {
            get
            {
                return _createUnit.Calle;
            }
            set
            {
                _createUnit.Calle = value;
                OnPropertyChanged("Calle");
            }
        }
        public string Numero
        {
            get
            {
                return _createUnit.Numero;
            }
            set
            {
                _createUnit.Numero = value;
                OnPropertyChanged("Numero");
            }
        }
        public string Complemento
        {
            get
            {
                return _createUnit.Complemento;
            }
            set
            {
                _createUnit.Complemento = value;
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
                _selectedResponsable = _responsables.ElementAt(_selectedReponsableIndex);
                _createUnit.ResponsableNombre = _selectedResponsable.NombreCompleto;
                _createUnit.ResponsableRun = _selectedResponsable.Run;
                OnPropertyChanged("SelectedResponsable");
            }
        }
        #endregion

        public AdminUnitCreateViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;

            #region Listas
            _rubros = new ObservableCollection<RubroModel>();
            _rubros.Add(new RubroModel() { Id = 0, Detalle = "--Seleccionar--" });
            foreach (RubroModel r in new RubroModel().ReadAll())
            {
                _rubros.Add(r);
            }

            _regiones = new ObservableCollection<RegionModel>();
            _regiones.Add(new RegionModel() { Id = 0, Detalle = "--Seleccionar--" });
            foreach (RegionModel r in new RegionModel().ReadAll())
            {
                _regiones.Add(r);
            }

            _provincias = new ObservableCollection<ProvinciaModel>();
            _provincias.Add(new ProvinciaModel() { Id = 0, Detalle = "--Seleccionar--", IdRegion = 0 });
            foreach (ProvinciaModel p in new ProvinciaModel().ReadAll())
            {
                _provincias.Add(p);
            }

            _comunas = new ObservableCollection<ComunaModel>();
            _comunas.Add(new ComunaModel() { Id = 0, Detalle = "--Seleccionar--", IdProvincia = 0 });
            foreach (ComunaModel c in new ComunaModel().ReadAll())
            {
                _comunas.Add(c);
            }

            //_subUnidadTipo = new ObservableCollection<SubUnidadTipo>();
            //foreach (SubUnidadTipo s in new SubUnidadTipo().ReadAll())
            //{
            //    if (s.Id == 1)
            //    {
            //        s.Add = true;
            //        s.Editable = false;
            //    }
            //    _subUnidadTipo.Add(s);
            //}

            _responsables = new ObservableCollection<UsuarioInternoModel>();
            _responsables.Add(new UsuarioInternoModel() {NombreCompleto = "-- Seleccionar --" });
            foreach (UsuarioInternoModel u in new UsuarioInternoModel().ReadDesigner())
            {
                _responsables.Add(u);
            }

            Provincias = _provincias.Where(p => p.Id == 0).ToList();
            Comunas = _comunas.Where(p => p.Id == 0).ToList();

            SelectedRubro = _rubros.ElementAt(SelectedRubroIndex);
            SelectedRegion = _regiones.ElementAt(SelectedRegionIndex);
            SelectedProvincia = Provincias.ElementAt(SelectedProvinciaIndex);
            SelectedComuna = Comunas.ElementAt(SelectedComunaIndex);
            //SelectedSubUnidad = _subUnidadTipo.ElementAt(0);
            SelectedResponsable = _responsables.ElementAt(0);
            #endregion

            #region Buttons
            NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount));
            CreateUnit = new CRUDCommand<AdminUserViewModel, UnidadModel>(() => _createUnit.Create(), () => new AdminUserViewModel(_frameStore, CurrentAccount), _frameStore, () => _createUnit.ReadByRut(), _createUnit);
            #endregion
        }

        public ICommand NavigationUnit { get; }
        
        public ICommand CreateUnit { get; }
    }
}
