using ApoloniaApp.Commands;
using ApoloniaApp.Services;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class ProcessDesignerViewModel : ViewModelBase
    {

        public ICommand NavigationLoginCommand { get; }
        public ProcessDesignerViewModel(NavigationStore navigationStore)
        {
        }
    }
}
