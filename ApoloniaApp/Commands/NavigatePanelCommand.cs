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
        private bool _isCheck;

        public NavigatePanelCommand(FrameStore frameStore, Func<TViewModel> createViewModel)
        {
            _frameStore = frameStore;
            _createViewModel = createViewModel;
        }

        public NavigatePanelCommand(FrameStore frameStore, Func<TViewModel> createViewModel, bool isCheck)
        {
            _frameStore = frameStore;
            _createViewModel = createViewModel;
            _isCheck = isCheck;
        }

        public override void Execute(object parameter)
        {
            _frameStore.CurrentViewModel = _createViewModel();
            _isCheck = false;
        }
    }
}
