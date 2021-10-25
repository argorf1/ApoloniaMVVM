using ApoloniaApp.Stores;
using ApoloniaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.Commands
{
    class NavigatePanelCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        private readonly Func<TViewModel> _createViewModel;

        public NavigatePanelCommand(FrameStore frameStore, Func<TViewModel> createViewModel)
        {
            _frameStore = frameStore;
            _createViewModel = createViewModel;
        }

        public override void Execute(object parameter)
        {
            _frameStore.CurrentViewModel = _createViewModel();
        }
    }
}
