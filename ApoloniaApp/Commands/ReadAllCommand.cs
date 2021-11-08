using ApoloniaApp.Models;
using ApoloniaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ApoloniaApp.Commands
{
    class ReadAllCommand<TModel>
        where TModel : ModelBase
    {     
        public ObservableCollection<TModel> ReadAll(Func<List<TModel>> readModel, TModel firstModel)
        {
            ObservableCollection<TModel> listModel = new ObservableCollection<TModel>();
            listModel.Add(firstModel);
            foreach (TModel m in readModel())
            {
                listModel.Add(m);
            }

            return listModel;
        }

        public ObservableCollection<TModel> ReadAll(Func<List<TModel>> readModel)
        {
            ObservableCollection<TModel> listModel = new ObservableCollection<TModel>();
            foreach (TModel m in readModel())
            {
                listModel.Add(m);
            }

            return listModel;
        }
    }
}
