using ApoloniaApp.Commands;
using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ApoloniaApp.ViewModels
{
    class AdminClientViewModel : ViewModelBase
    {
        private int _selectedIndex;

        private string _run;
        private string _nombre;
        private string _apellidoP;
        private string _apellidoM;
        private string _email;
        private int _subunidadId;
        private int _rolId;
        private string _unidadId;
        private int _estado;
        private string _username;

        private readonly FrameStore _frameStore;
        private readonly ObservableCollection<UsuarioInternoModel> _usuarios;
        public IEnumerable<UsuarioInternoModel> Usuarios => _usuarios;
        public UsuarioInternoModel CurrentAccount;

        #region Property

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");

                OnPropertyChanged("Run");
                OnPropertyChanged("Nombre");
                OnPropertyChanged("ApellidoP");
                OnPropertyChanged("ApellidoM");
                OnPropertyChanged("Email");
                OnPropertyChanged("SubunidadId");
                OnPropertyChanged("RolId");
                OnPropertyChanged("UnidadId");
                OnPropertyChanged("Estado");
                OnPropertyChanged("Username");

            }
        }
        public string Run
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).Run;
                }
                else
                {
                    return _run;
                }
            }
            set
            {
                _run = value;
                OnPropertyChanged("Run");
            }
        }
        public string Nombre
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).Nombres;
                }
                else
                {
                    return _nombre;
                }
            }
            set
            {
                _nombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        public string ApellidoP
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).ApellidoP;
                }
                else
                {
                    return _apellidoP;
                }
            }
            set
            {
                _apellidoP = value;
                OnPropertyChanged("ApellidoP");
            }
        }

        public string ApellidoM
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).ApellidoM;
                }
                else
                {
                    return _apellidoM;
                }
            }
            set
            {
                _apellidoM = value;
                OnPropertyChanged("ApellidoM");
            }
        }

        public string Email
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).Email;
                }
                else
                {
                    return _email;
                }
            }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        //public int SubunidadId
        //{
        //    get
        //    {
        //        if (SelectedIndex > -1)
        //        {
        //            return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).SubunidadId;
        //        }
        //        else
        //        {
        //            return _subunidadId;
        //        }
        //    }
        //    set
        //    {
        //        _subunidadId = value;
        //        OnPropertyChanged("SubunidadId");
        //    }
        //}

        public int RolId
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).IdPerfil;
                }
                else
                {
                    return _rolId;
                }
            }
            set
            {
                _rolId = value;
                OnPropertyChanged("RolId");
            }
        }

        //public string UnidadId
        //{
        //    get
        //    {
        //        if (SelectedIndex > -1)
        //        {
        //            return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).UnidadId;
        //        }
        //        else
        //        {
        //            return _unidadId;
        //        }
        //    }
        //    set
        //    {
        //        _unidadId = value;
        //        OnPropertyChanged("UnidadId");
        //    }
        //}

        public int Estado
        {
            get
            {
                if (SelectedIndex > -1)
                {
                    return Usuarios.ElementAt<UsuarioInternoModel>(SelectedIndex).IdEstado;
                }
                else
                {
                    return _estado;
                }
            }
            set
            {
                _estado = value;
                OnPropertyChanged("Estado");
            }
        }

        //public string Username
        //{
        //    get
        //    {
        //        if (SelectedIndex > -1)
        //        {
        //            return Usuarios.ElementAt<UsuarioInterno>(SelectedIndex).Username;
        //        }
        //        else
        //        {
        //            return _username;
        //        }
        //    }
        //    set
        //    {
        //        _username = value;
        //        OnPropertyChanged("Username");
        //    }
        //}
        #endregion

        public AdminClientViewModel(FrameStore frameStore, UsuarioInternoModel currentAccount)
        {
            _frameStore = frameStore;
            CurrentAccount = currentAccount;
            _usuarios = new ObservableCollection<UsuarioInternoModel>();
            _selectedIndex = -1;
            foreach (UsuarioInternoModel user in new UsuarioInternoModel().ReadAll())
            {
                _usuarios.Add(user);
            }
            NavigationCreateUsers = new NavigatePanelCommand<AdminClientCreateViewModel>(_frameStore, () => new AdminClientCreateViewModel(_frameStore, CurrentAccount));
            NavigationEditUsers = new NavigatePanelCommand<AdminClientEditViewModel>(_frameStore, () => new AdminClientEditViewModel(_frameStore, new UsuarioInternoModel(), CurrentAccount));

        }

        public ICommand NavigationCreateUsers { get; }
        public ICommand NavigationEditUsers { get; }
    }
}
