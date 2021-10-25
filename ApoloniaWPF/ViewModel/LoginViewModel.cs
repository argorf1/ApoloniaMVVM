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
    class LoginViewModel : BaseViewModel, IPageViewModel
    {

        #region Atributos

        private string run;
        private string password;
        private string message;
        #endregion

        #region Propiedades

        public string Run
        {
            get
            {

                return run;

            }//Fin de get.
            set
            {

                run = value;

                OnPropertyChanged("Run");
            }//Fin de set.
        }//Fin de propiedad Id.

        public string Password
        {
            get
            {

                return password;

            }//Fin de get.
            set
            {

                password = value;

                OnPropertyChanged("Password");
            }//Fin de set.
        }

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;

                OnPropertyChanged("Message");
            }//Fin de set.
        }//Fin de propiedad Id.
        #endregion

        #region Constructores
        //public LoginViewModel()
        //{

        //}
        #endregion

    }
}
