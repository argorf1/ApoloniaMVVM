using ApoloniaApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ApoloniaApp.Services
{
    class ChargeComboBoxService<TModel>
        where TModel : ModelBase
    {

        public ObservableCollection<TModel> ChargeComboBox(ObservableCollection<TModel> listStore, ObservableCollection<TModel> comboList, TModel firstModel)
        {
            comboList = new ObservableCollection<TModel>();
            comboList.Add(firstModel);
            foreach (TModel m in listStore)
            {
                comboList.Add(m);
            }

            return comboList;
        }

        public ObservableCollection<TModel> ChargeComboBox(IEnumerable<TModel> listStore, ObservableCollection<TModel> comboList, TModel firstModel)
        {
            comboList = new ObservableCollection<TModel>();
            comboList.Add(firstModel);
            foreach (TModel m in listStore)
            {
                comboList.Add(m);
            }

            return comboList;
        }
    }
}
