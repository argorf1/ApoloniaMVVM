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
    class AdminUnitViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;

        #region Unidad
        private readonly ObservableCollection<UnidadModel> _unidades;
        public IEnumerable<UnidadModel> Unidades => _unidades;
        private UnidadModel _editUnit;
        private int _selectedUnitIndex;

        

        public int SelectedUnitIndex
        {
            get { return _selectedUnitIndex; }
            set
            {
                _selectedUnitIndex = value;
                OnPropertyChanged("SelectedUnitIndex");

                OnPropertyChanged("Rut");
                OnPropertyChanged("RazonSocial");
                OnPropertyChanged("PersonaContacto");
                OnPropertyChanged("TelefonoContacto");
                OnPropertyChanged("EmailContacto");
                OnPropertyChanged("ResponsableRun");
                OnPropertyChanged("Calle");
                OnPropertyChanged("Numero");
                OnPropertyChanged("Complemento");
                OnPropertyChanged("Region");
                OnPropertyChanged("Provincia");
                OnPropertyChanged("Comuna");
                OnPropertyChanged("Rubro");
                OnPropertyChanged("ResponsableNombre");
                OnPropertyChanged("Estado");
            }
        }
        public UnidadModel SelectedUnidad
        {
            get { return _editUnit; }
            set
            {
                _editUnit = value;
                Subunidades = _subunidades.Where(p => p.RutUnidad == _editUnit.Rut).ToList();
                OnPropertyChanged("SelectedUnidad");

                OnPropertyChanged("Rut");
                OnPropertyChanged("RazonSocial");
                OnPropertyChanged("PersonaContacto");
                OnPropertyChanged("TelefonoContacto");
                OnPropertyChanged("EmailContacto");
                OnPropertyChanged("ResponsableRun");
                OnPropertyChanged("Calle");
                OnPropertyChanged("Numero");
                OnPropertyChanged("Complemento");
                OnPropertyChanged("Region");
                OnPropertyChanged("Provincia");
                OnPropertyChanged("Comuna");
                OnPropertyChanged("Rubro");
                OnPropertyChanged("ResponsableNombre");
                OnPropertyChanged("Estado");
            }
        }
        public string Rut
        {
            get
            {
                return _editUnit.Rut;
            }
            set
            {
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
                OnPropertyChanged("RazonSocial");
            }

        }
        public string Rubro
        {
            get
            {

                return _editUnit.Rubro;

            }
            set
            {
                OnPropertyChanged("Rubro");
            }

        }
        public int Direccion
        {
            get
            {

                return _editUnit.DireccionId;

            }
            set
            {
                OnPropertyChanged("Direccion");
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
                OnPropertyChanged("PersonaContacto");
            }

        }
        public int TelefonoContacto
        {
            get
            {

                return _editUnit.TelefonoContacto;

            }
            set
            {
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
                OnPropertyChanged("EmailContacto");
            }

        }
        public string ResponsableNombre
        {
            get
            {

                return _editUnit.ResponsableNombre;

            }
            set
            {
                OnPropertyChanged("ResponsableNombre");
            }

        }
        public string Estado
        {
            get
            {

                return _editUnit.Estado;

            }
            set
            {
                OnPropertyChanged("Estado");
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
                OnPropertyChanged("Complemento");
            }


        }
        public string Region
        {
            get
            {

                return _editUnit.Region;
            }
            set
            {
                OnPropertyChanged("Region");
            }

        }
        public string Provincia
        {
            get
            {

                return _editUnit.Provincia;

            }
            set
            {
                OnPropertyChanged("Provincia");
            }

        }
        public string Comuna
        {
            get
            {

                return _editUnit.Comuna;

            }
            set
            {
                OnPropertyChanged("Comuna");
            }

        }

        #endregion
        #region Subunidad
        private readonly ObservableCollection<SubUnidadModel> _subunidades;
        private IEnumerable<SubUnidadModel> subunidades;
        private SubUnidadModel _editSubunit;

        public IEnumerable<SubUnidadModel> Subunidades
        {
            get { return subunidades; }
            set
            {
                subunidades = value;
                OnPropertyChanged("Subunidades");
            }
        }

        public SubUnidadModel SelectedSubunidad
        {
            get { return _editSubunit; }
            set
            {
                _editSubunit = value;
                OnPropertyChanged("SelectedSubunidad");
            }
        }
        #endregion


        public AdminUnitViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _editUnit = new UnidadModel();
            _selectedUnitIndex = -1;

            _unidades = new ObservableCollection<UnidadModel>();
            foreach (UnidadModel u in new UnidadModel().ReadAll())
            {
                _unidades.Add(u);
            }

            _subunidades = new ObservableCollection<SubUnidadModel>();
            foreach (SubUnidadModel s in new SubUnidadModel().ReadAll())
            {
                _subunidades.Add(s);
            }

            NavigationCreateUnit = new NavigatePanelCommand<AdminUnitCreateViewModel>(_frameStore, () => new AdminUnitCreateViewModel(_frameStore, CurrentAccount));
            NavigationEditUnit = new NavigatePanelCommand<AdminUnitEditViewModel>(_frameStore, () => new AdminUnitEditViewModel(_frameStore, CurrentAccount, _editUnit));

        }

        public ICommand NavigationCreateUnit { get; }
        public ICommand NavigationEditUnit { get; }
    }
}
