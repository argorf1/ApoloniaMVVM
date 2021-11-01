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

        public string Rut
        {
            get 
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Rut = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Rut;
                    return _editUnit.Rut;
                }
                else 
                {
                    return _editUnit.Rut;
                }
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
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.RazonSocial = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).RazonSocial;
                    return _editUnit.RazonSocial;
                }
                else
                {
                    return _editUnit.RazonSocial;
                }
            }
            set
            {
                _editUnit.Rut = value;
                OnPropertyChanged("RazonSocial");
            }
        }
        public string Rubro
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Rubro = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Rubro;
                    _editUnit.RubroId = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).RubroId;
                    return _editUnit.Rubro;
                }
                else
                {
                    return _editUnit.Rubro;
                }
            }
            set
            {
                _editUnit.Rubro = value;
                OnPropertyChanged("Rubro");
            }
        }
        public int Direccion
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.DireccionId = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).DireccionId;
                    return _editUnit.DireccionId;
                }
                else
                {
                    return _editUnit.DireccionId;
                }
            }
            set
            {
                _editUnit.DireccionId = value;
                OnPropertyChanged("Rut");
            }
        }
        public string PersonaContacto
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.PersonaContacto = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).PersonaContacto;
                    return _editUnit.PersonaContacto;
                }
                else
                {
                    return _editUnit.PersonaContacto;
                }
            }
            set
            {
                _editUnit.PersonaContacto = value;
                OnPropertyChanged("PersonaContacto");
            }
        }
        public int TelefonoContacto
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.TelefonoContacto = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).TelefonoContacto;
                    return _editUnit.TelefonoContacto;
                }
                else
                {
                    return _editUnit.TelefonoContacto;
                }
            }
            set
            {
                _editUnit.TelefonoContacto = value;
                OnPropertyChanged("TelefonoContacto");
            }
        }
        public string EmailContacto
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.EmailContacto = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).EmailContacto;
                    return _editUnit.EmailContacto;
                }
                else
                {
                    return _editUnit.EmailContacto;
                }
            }
            set
            {
                _editUnit.EmailContacto = value;
                OnPropertyChanged("EmailContacto");
            }
        }
        public string ResponsableNombre
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.ResponsableNombre = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).ResponsableNombre;
                    _editUnit.ResponsableRun = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).ResponsableRun;
                    return _editUnit.ResponsableNombre;
                }
                else
                {
                    return _editUnit.ResponsableNombre;
                }
            }
            set
            {
                _editUnit.ResponsableNombre = value;
                OnPropertyChanged("ResponsableNombre");
            }
        }
        public string Estado
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Estado = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Estado;
                    _editUnit.EstadoId = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).EstadoId;
                    return _editUnit.Estado;
                }
                else
                {
                    return _editUnit.Estado;
                }
            }
            set
            {
                _editUnit.Estado = value;
                OnPropertyChanged("Estado");
            }
        }
        public string Calle
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Calle = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Calle;
                    return _editUnit.Calle;
                }
                else
                {
                    return _editUnit.Calle;
                }
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
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Numero = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Numero;
                    return _editUnit.Numero;
                }
                else
                {
                    return _editUnit.Numero;
                }
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
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Complemento = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Complemento;
                    return _editUnit.Complemento;
                }
                else
                {
                    return _editUnit.Complemento;
                }
            }
            set
            {
                _editUnit.Complemento = value;
                OnPropertyChanged("Complemento");
            }
        }
        public string Region
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Region = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Region;
                    return _editUnit.Region;
                }
                else
                {
                    return _editUnit.Region;
                }
            }
            set
            {
                _editUnit.Region = value;
                OnPropertyChanged("Region");
            }
        }
        public string Provincia
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Provincia = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Provincia;
                    return _editUnit.Provincia;
                }
                else
                {
                    return _editUnit.Provincia;
                }
            }
            set
            {
                _editUnit.Provincia = value;
                OnPropertyChanged("Provincia");
            }
        }
        public string Comuna
        {
            get
            {
                if (SelectedUnitIndex > -1)
                {
                    _editUnit.Comuna = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).Comuna;
                    _editUnit.ComunaId = Unidades.ElementAt<UnidadModel>(SelectedUnitIndex).ComunaId;
                    return _editUnit.Comuna;
                }
                else
                {
                    return _editUnit.Comuna;
                }
            }
            set
            {
                _editUnit.Comuna = value;
                OnPropertyChanged("Comuna");
            }
        }

        #endregion



        public AdminUnitViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _editUnit = new UnidadModel();
            _unidades = new ObservableCollection<UnidadModel>();
            _selectedUnitIndex = -1;

            foreach (UnidadModel u in new UnidadModel().ReadAll())
            {
                _unidades.Add(u);
            }

            NavigationCreateUnit = new NavigatePanelCommand<AdminUnitCreateViewModel>(_frameStore, () => new AdminUnitCreateViewModel(_frameStore, CurrentAccount));
            NavigationEditUnit = new NavigatePanelCommand<AdminUnitEditViewModel>(_frameStore, () => new AdminUnitEditViewModel(_frameStore, /*Cambiar esto por el usuario seleccionado*/CurrentAccount, new UnidadModel()));

        }

        public ICommand NavigationCreateUnit { get; }
        public ICommand NavigationEditUnit { get; }
    }
}
