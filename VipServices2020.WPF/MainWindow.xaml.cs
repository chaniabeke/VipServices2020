using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }

        private void SearchReservationScreen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                changingWindow.Source = new Uri("Pages/SearchReservation.xaml", UriKind.Relative);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddReservationScreen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                changingWindow.Source = new Uri("Pages/AddReservation.xaml", UriKind.Relative);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show((e.ExceptionObject as Exception).Message, "Unhandled UI Exception");
        }
    }
}