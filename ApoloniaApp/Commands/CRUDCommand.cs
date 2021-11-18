﻿using ApoloniaApp.Models;
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
        private ObservableCollection<TModel> _models;
        private FrameStore _frameStore;

        public CRUDCommand(Func<bool> crudModel, Func<TViewModel> viewModel, FrameStore frameStore, Func<bool> checkModel, TModel model/*, ObservableCollection<TModel> listStore*/)
        {
            _crudModel = crudModel;
            _viewModel = viewModel;
            _frameStore = frameStore;
            _checkModel = checkModel;
            _model = model;
        }

        public CRUDCommand(Func<bool> crudModel, Func<TViewModel> viewModel, FrameStore frameStore, TModel model)
        {
            _crudModel = crudModel;
            _viewModel = viewModel;
            _frameStore = frameStore;
            _checkModel = null;
            _model = model;
        }

        public override void Execute(object parameter)
        {
            if (MessageBox.Show("¿Esta seguro de continuar?", "Confirmación", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (_checkModel == null)
                {
                    if (_crudModel())
                    {
                        MessageBox.Show("Actualización de " + _model.GetType().Name + " realizada con exito");
                        _frameStore.CurrentViewModel = _viewModel();

                    }
                    else
                    {
                        MessageBox.Show("Fallo de Conexión con la Base de Datos");
                    }
                }
                else
                {

                    if (!_checkModel())
                    {
                        if (_crudModel())
                        {
                            MessageBox.Show("Creación de " + _model.GetType().Name + " realizada con exito");
                            _frameStore.CurrentViewModel = _viewModel();
                        }
                        else
                        {
                            MessageBox.Show("Fallo de Conexión con la Base de Datos");
                        }
                    }
                    else
                    {
                        MessageBox.Show(_model.GetType().Name + " ya existente");
                    }
                }
            }
        }
    }
}
