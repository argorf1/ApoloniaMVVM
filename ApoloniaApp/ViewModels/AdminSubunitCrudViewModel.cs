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
    class AdminSubunitCrudViewModel : ViewModelBase
    {
        Dictionary<int, string> _estadoDetalle = new Dictionary<int, string>();
        private int _estado = 0;
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private SubUnidadModel _crudSubunidad;


        private ObservableCollection<SubUnidadModel> _subunidades;
        public IEnumerable<SubUnidadModel> Subunidades => _subunidades;
        private SubUnidadModel _selectedSubunidad;


        public AdminSubunitCrudViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, SubUnidadModel createSubunidad, ObservableCollection<SubUnidadModel> subunidades,int estado)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crudSubunidad = createSubunidad;
            _subunidades = subunidades;
            _estado = estado;

            #region CargaDiccionario
            _estadoDetalle.Add(1, "Crear Subnidad");
            _estadoDetalle.Add(2, "Modificar Subnidad");
            #endregion



            SelectedSubunidad = _subunidades.FirstOrDefault(p => p.Id == _crudSubunidad.SubUnidadPadreId);

            switch (_estado)
            {
                case 1:
                    CrudSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _crudSubunidad.Create(), () => new AdminUnitViewModel(_frameStore, CurrentAccount), _frameStore,() =>_crudSubunidad.ReadByName() ,_crudSubunidad);
                    break;
                case 2:
                    CrudSubunit = new CRUDCommand<AdminUnitViewModel, SubUnidadModel>(() => _crudSubunidad.Update(), () => new AdminUnitViewModel(_frameStore, CurrentAccount), _frameStore, _crudSubunidad);
                    break;
                default:
                    break;
            }
        }

        public string Nombre
        {
            get
            {
                return _crudSubunidad.Nombre;
            }
            set
            {
                _crudSubunidad.Nombre = value;

                OnPropertyChanged("Nombre");
            }
        }

        public string Descripcion
        {
            get
            {
                return _crudSubunidad.Descripcion;
            }
            set
            {
                _crudSubunidad.Descripcion = value;
                OnPropertyChanged("Descripcion");
            }
        }

        public SubUnidadModel SelectedSubunidad
        {
            get { return _selectedSubunidad; }
            set
            {
                _selectedSubunidad = value;
                if(_selectedSubunidad != null)
                    _crudSubunidad.SubUnidadPadreId = _selectedSubunidad.SubUnidadPadreId;
                OnPropertyChanged("SelectedSubunidad");
            }
        }

        public string Estado
        {
            get { return _estadoDetalle[_estado]; }
            set
            {
                OnPropertyChanged("Estado");
            }
        }
        public ICommand CrudSubunit { get; }


    }
}