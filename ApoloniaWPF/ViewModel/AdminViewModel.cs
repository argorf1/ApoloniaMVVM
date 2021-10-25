using ApoloniaWPF.EventsHandlers;
using ApoloniaWPF.Interface;
using ApoloniaWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ApoloniaWPF.ViewModel
{
    class AdminViewModel : BaseViewModel, IPageViewModel
    {
        private ICommand _goTo1;

        public ICommand GoTo1
        {
            get
            {
                return _goTo1 ?? (_goTo1 = new RelayCommand(x =>
                {
                    Mediator.Notify("GoTo1Screen", "");
                }));
            }
        }
    }
}
