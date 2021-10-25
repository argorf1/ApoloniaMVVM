using ApoloniaWPF.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApoloniaWPF.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; }

        public MainWindowViewModel()
        {
            CurrentViewModel = new LoginViewModel();
        }
    }
}
