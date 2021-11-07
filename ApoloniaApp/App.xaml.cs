using ApoloniaApp.Models;
using ApoloniaApp.Stores;
using ApoloniaApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ApoloniaApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore();
            AccountStore accountStore = new AccountStore();
            accountStore.CurrentAccount = new UsuarioInternoModel() { Run = "1", IdPerfil=1};
            navigationStore.CurrentViewModel = new LoginViewModel(accountStore, navigationStore);
            //navigationStore.CurrentViewModel = new ProcessDesignerViewModel(navigationStore,accountStore);
            accountStore.CurrentAccount = new UsuarioInternoModel();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore,accountStore)
            };
            MainWindow.Show();

            base.OnStartup(e);


        }
    }
}
