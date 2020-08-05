using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VipServices2020.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SearchReservationScreen_Click(object sender, RoutedEventArgs e)
        {
            changingWindow.Source = new Uri("Pages/SearchReservation.xaml", UriKind.Relative);
        }

        private void AddReservationScreen_Click(object sender, RoutedEventArgs e)
        {
            changingWindow.Source = new Uri("Pages/AddReservation.xaml", UriKind.Relative);
        }
    }
}
