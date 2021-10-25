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

            navigationStore.CurrentViewModel = new LoginViewModel(accountStore, navigationStore);
            accountStore.CurrentAccount = new Usuario();
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore,accountStore)
            };
            MainWindow.Show();

            base.OnStartup(e);


        }
    }
}
