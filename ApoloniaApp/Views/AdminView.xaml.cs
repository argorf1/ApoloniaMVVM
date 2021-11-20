using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApoloniaApp.Views
{
    /// <summary>
    /// Lógica de interacción para AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public AdminView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set Tooltip visibility

            if (Tg_btn.IsChecked == true)
            {
                tt_internalusr.Visibility = Visibility.Collapsed;
                tt_units.Visibility = Visibility.Collapsed;
                tt_roles.Visibility = Visibility.Collapsed;
                tt_externalusr.Visibility = Visibility.Collapsed;
                tt_logout.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_internalusr.Visibility = Visibility.Visible;
                tt_units.Visibility = Visibility.Visible;
                tt_roles.Visibility = Visibility.Visible;
                tt_externalusr.Visibility = Visibility.Visible;
                tt_logout.Visibility = Visibility.Visible;

            }
        }

    }
}
