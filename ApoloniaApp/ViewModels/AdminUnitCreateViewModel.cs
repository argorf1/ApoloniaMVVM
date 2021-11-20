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
    class AdminUnitCreateViewModel : ViewModelBase
    {
        //private readonly FrameStore _frameStore;
        //private readonly ListStore _listStore;
        //public UsuarioInternoModel CurrentAccount;



        //private UnidadModel _createUnit = new UnidadModel() { EstadoId = 1 };
        //#region Definicion Listas
        //private readonly ObservableCollection<RubroModel> _rubros;
        //public IEnumerable<RubroModel> Rubros => _rubros;
        //private RubroModel _selectedRubro;
        //private int _selectedRubroIndex;

        //private readonly ObservableCollection<RegionModel> _regiones;
        //public IEnumerable<RegionModel> Regiones => _regiones;
        //private RegionModel _selectedRegion;
        //private int _selectedRegionIndex;

        //private readonly ObservableCollection<ProvinciaModel> _provincias;
        //private IEnumerable<ProvinciaModel> provincias;
        //private ProvinciaModel _selectedProvincia;
        //private int _selectedProvinciaIndex;

        //private readonly ObservableCollection<ComunaModel> _comunas;
        //private IEnumerable<ComunaModel> comunas;
        //private ComunaModel _selectedComuna;
        //private int _selectedComunaIndex;

        //private readonly ObservableCollection<UsuarioInternoModel> _responsables;
        //public IEnumerable<UsuarioInternoModel> Responsables => _responsables;
        //private UsuarioInternoModel _selectedResponsable;
        //private int _selectedReponsableIndex;

        ////private readonly ObservableCollection<SubUnidadTipo> _subUnidadTipo;
        ////public IEnumerable<SubUnidadTipo> SubUnidadTipo => _subUnidadTipo;
        ////private SubUnidadTipo _selectedSubUnidad;
        //#endregion

        //#region Propiedades
        //public string Rut
        //{
        //    get
        //    {
        //        return _createUnit.Rut;
        //    }
        //    set
        //    {
        //        _createUnit.Rut = value;
        //        OnPropertyChanged("Rut");
        //    }
        //}
        //public string RazonSocial
        //{
        //    get
        //    {
        //        return _createUnit.RazonSocial;
        //    }
        //    set
        //    {
        //        _createUnit.RazonSocial = value;
        //        OnPropertyChanged("RazonSocial");
        //    }
        //}
        //public int SelectedRubroIndex
        //{
        //    get
        //    {
        //        return _selectedRubroIndex;
        //    }
        //    set
        //    {
        //        _selectedRubroIndex = value;
        //        OnPropertyChanged("SelectedRubroIndex");
        //    }
        //}
        //public RubroModel SelectedRubro
        //{
        //    get { return _selectedRubro; }
        //    set
        //    {
        //        _selectedRubro = Rubros.ElementAt(_selectedRubroIndex);
        //        _createUnit.RubroId = _selectedRubro.Id;
        //        OnPropertyChanged("SelectedRubro");
        //    }
        //}
        //public int SelectedRegionIndex
        //{
        //    get
        //    {
        //        return _selectedRegionIndex;
        //    }
        //    set
        //    {
        //        _selectedRegionIndex = value;
        //        OnPropertyChanged("SelectedRegionIndex");
        //    }
        //}
        //public RegionModel SelectedRegion
        //{
        //    get { return _selectedRegion; }
        //    set
        //    {
        //        _selectedRegion = Regiones.ElementAt(_selectedRegionIndex);
        //        if(_selectedRegionIndex > 0)
        //            Provincias = _provincias.Where(p => p.IdRegion == _selectedRegion.Id ||p.Id == 0).ToList();
        //        else
        //            Provincias = _provincias.Where(p => p.Id == 0).ToList();
        //        OnPropertyChanged("SelectedRegion");
        //        OnPropertyChanged("Provincias");
        //    }
        //}

        //public IEnumerable<ProvinciaModel> Provincias
        //{
        //    get 
        //    {
        //        return provincias;
        //    }
        //    set
        //    {
        //        provincias = value;
        //        OnPropertyChanged("Provincias");
        //    }
        //}

        //public int SelectedProvinciaIndex
        //{
        //    get
        //    {
        //        return _selectedProvinciaIndex;
        //    }
        //    set
        //    {
        //        _selectedProvinciaIndex = value;
        //        OnPropertyChanged("SelectedProvinciaIndex");
        //    }
        //}
        //public ProvinciaModel SelectedProvincia
        //{
        //    get { return _selectedProvincia; }
        //    set
        //    {
        //        _selectedProvincia = Provincias.ElementAt(_selectedProvinciaIndex);
        //        if (_selectedRegionIndex > 0)
        //            Comunas = _comunas.Where(p => p.IdProvincia == _selectedProvincia.Id || p.Id == 0).ToList();
        //        else
        //            Comunas = _comunas.Where(p => p.Id == 0).ToList();
        //        OnPropertyChanged("SelectedProvincia");
        //        OnPropertyChanged("Comunas");
        //    }
        //}
        //public IEnumerable<ComunaModel> Comunas
        //{
        //    get
        //    {
        //        return comunas;
        //    }
        //    set
        //    {
        //        comunas = value;
        //        OnPropertyChanged("Comunas");
        //    }
        //}

        //public int SelectedComunaIndex
        //{
        //    get
        //    {
        //        return _selectedComunaIndex;
        //    }
        //    set
        //    {
        //        _selectedComunaIndex = value;
        //        OnPropertyChanged("SelectedComunaIndex");
        //    }
        //}
        //public ComunaModel SelectedComuna
        //{
        //    get { return _selectedComuna; }
        //    set
        //    {
        //        _selectedComuna = Comunas.ElementAt(_selectedComunaIndex);
        //        _createUnit.ComunaId = _selectedComuna.Id;
        //        OnPropertyChanged("SelectedComuna");
        //    }
        //}
        //public string PersonaContacto
        //{
        //    get
        //    {
        //        return _createUnit.PersonaContacto;
        //    }
        //    set
        //    {
        //        _createUnit.PersonaContacto = value;
        //        OnPropertyChanged("PersonaContacto");
        //    }
        //}
        //public string TelefonoContacto
        //{
        //    set
        //    {
        //        _createUnit.TelefonoContacto = int.Parse(value);
        //        OnPropertyChanged("TelefonoContacto");
        //    }
        //}
        //public string EmailContacto
        //{
        //    get
        //    {
        //        return _createUnit.EmailContacto;
        //    }
        //    set
        //    {
        //        _createUnit.EmailContacto = value;
        //        OnPropertyChanged("EmailContacto");
        //    }
        //}
        //public string Calle
        //{
        //    get
        //    {
        //        return _createUnit.Calle;
        //    }
        //    set
        //    {
        //        _createUnit.Calle = value;
        //        OnPropertyChanged("Calle");
        //    }
        //}
        //public string Numero
        //{
        //    get
        //    {
        //        return _createUnit.Numero;
        //    }
        //    set
        //    {
        //        _createUnit.Numero = value;
        //        OnPropertyChanged("Numero");
        //    }
        //}
        //public string Complemento
        //{
        //    get
        //    {
        //        return _createUnit.Complemento;
        //    }
        //    set
        //    {
        //        _createUnit.Complemento = value;
        //        OnPropertyChanged("Complemento");
        //    }
        //}

        //public int SelectedResponsableIndex
        //{
        //    get { return _selectedReponsableIndex; }
        //    set
        //    {
        //        _selectedReponsableIndex = value;
        //        OnPropertyChanged("SelectedResponsableIndex");
        //    }
        //}
        //public UsuarioInternoModel SelectedResponsable
        //{
        //    get
        //    {
        //        return _selectedResponsable;
        //    }
        //    set
        //    {
        //        _selectedResponsable = _responsables.ElementAt(_selectedReponsableIndex);
        //        _createUnit.ResponsableNombre = _selectedResponsable.NombreCompleto;
        //        _createUnit.ResponsableRun = _selectedResponsable.Run;
        //        OnPropertyChanged("SelectedResponsable");
        //    }
        //}
        //#endregion

        //public AdminUnitCreateViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, ListStore listStore)
        //{
        //    _frameStore = frameStore;
        //    _listStore = listStore;
        //    CurrentAccount = currentAccount;

        //    #region Listas
        //    _rubros = _listStore.rubros;
        //    _regiones = _listStore.regiones;
        //    _provincias = _listStore.provincias;
        //    _comunas = _listStore.comunas;
        //    _responsables = _listStore.designers;

        //    Provincias = _provincias.Where(p => p.Id == 0);
        //    Comunas = _comunas.Where(p => p.Id == 0);

        //    SelectedRubro = _rubros.ElementAt(SelectedRubroIndex);
        //    SelectedRegion = _regiones.ElementAt(SelectedRegionIndex);
        //    SelectedProvincia = Provincias.ElementAt(SelectedProvinciaIndex);
        //    SelectedComuna = Comunas.ElementAt(SelectedComunaIndex);
        //    SelectedResponsable = _responsables.ElementAt(0);
        //    #endregion

        //    #region Buttons
        //    NavigationUnit = new NavigatePanelCommand<AdminUnitViewModel>(_frameStore, () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore));
        //    //CreateUnit = new CRUDCommand<AdminUnitViewModel, UnidadModel>(() => _createUnit.Create(), () => new AdminUnitViewModel(_frameStore, CurrentAccount, _listStore), _frameStore, () => _createUnit.ReadByRut(), _createUnit);
        //    #endregion
        //}

        //public ICommand NavigationUnit { get; }
        
        //public ICommand CreateUnit { get; }
    }
}
