﻿using ApoloniaApp.Models;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using ApoloniaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace ApoloniaApp.Commands
{
    class CRUDCommand<TViewModel, TModel> : CommandBase
        where TViewModel : ViewModelBase
        where TModel : EntityModelBase
    {
        private readonly Func<bool> _crudModel;
        private readonly Func<bool> _checkModel;
        private readonly Func<TViewModel> _viewModel;
        private readonly TModel _model;
        private readonly int _estado;
        private FrameStore _frameStore;
        private ObservableCollection<TModel> _lista;

        public CRUDCommand(Func<bool> crudModel, Func<TViewModel> viewModel, FrameStore frameStore, Func<bool> checkModel, TModel model, ObservableCollection<TModel> lista, int estado)
        {
            _crudModel = crudModel;
            _viewModel = viewModel;
            _frameStore = frameStore;
            _checkModel = checkModel;
            _model = model;
            _lista = lista;
            _estado = estado;
        }

        public CRUDCommand(Func<bool> crudModel, Func<TViewModel> viewModel, FrameStore frameStore, TModel model, ObservableCollection<TModel> lista, int estado)
        {
            _crudModel = crudModel;
            _viewModel = viewModel;
            _frameStore = frameStore;
            _checkModel = null;
            _model = model;
            _lista = lista;
            _estado = estado;
        }

        public override void Execute(object parameter)
        {
            if (MessageBox.Show("¿Esta seguro de continuar?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                switch (_estado)
                {
                    case 1:
                        if (!_checkModel())
                        {
                            if (_crudModel())
                            {
                                MessageBox.Show("Creación de " + _model.NombreEntidad + " realizada con exito");
                                _frameStore.CurrentViewModel = _viewModel();
                                _lista.Add(_model);
                            }
                            else
                            {
                                MessageBox.Show("Fallo de Conexión con la Base de Datos");
                            }
                        }
                        else
                        {
                            MessageBox.Show(_model.NombreEntidad + " ya existente");
                        }
                        break;

                    case 2:
                        if (_crudModel())
                        {
                            MessageBox.Show("Actualización de " + _model.NombreEntidad + " realizada con exito");
                            _frameStore.CurrentViewModel = _viewModel();

                        }
                        else
                        {
                            MessageBox.Show("Fallo de Conexión con la Base de Datos");
                        }
                        break;
                    case 3:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
