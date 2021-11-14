using ApoloniaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ApoloniaApp.Commands
{
    class ExchangeListCommand<TModel> : CommandBase
        where TModel : ModelBase
    {
        private ObservableCollection<TModel> _source;
        private ObservableCollection<TModel> _target;
        private TModel _model;

        public ExchangeListCommand(ObservableCollection<TModel> source, ObservableCollection<TModel> target, TModel model)
        {
            _source = source;
            _target = target;
            _model = model;
        }

        public override void Execute(object parameter)
        {
            if (_model != null)
            {
                _source.Remove(_model);
                _target.Add(_model);
                _model = null;
            }
        }
    }
}
