using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApoloniaApp.ViewModels
{
    class DPTareaCRUDViewModel : ViewModelBase
    {
        private readonly FrameStore _frameStore;
        public UsuarioInternoModel CurrentAccount;
        private TareaModel _crudTarea;

        public DPTareaCRUDViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount, TareaModel crudTarea, int estado)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _crudTarea = crudTarea;
            _estado = estado;
        }

        #region Configuracion Vista Estado

        Dictionary<int, string> _estadoDetalle = new Dictionary<int, string>();
        private int _estado = 0;

        public string EstadoView
        {
            get { return _estadoDetalle[_estado]; }
            set
            {
                OnPropertyChanged("EstadoView");
            }
        }
        public string EstadoVisibility
        {
            get
            {
                if (_estado == 1)
                    return "hidden";
                else
                    return "visible";
            }
            set { OnPropertyChanged("EstadoVisibility"); }
        }
        #endregion
    }
}
