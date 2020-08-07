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
using VipServices2020.EF;
using VipServices2020.Domain;
using VipServices2020.Domain.Model;
using System.Linq;

namespace VipServices2020.WPF
{
    /// <summary>
    /// Interaction logic for AddReservation.xaml
    /// </summary>
    public partial class AddReservation : Page
    {
        private int[] hours = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
        VipServicesManager vipServicesManager = new VipServicesManager(new UnitOfWork(new VipServicesContext("Production")));
        public AddReservation()
        {
            InitializeComponent();
            cmbCustomer.ItemsSource = vipServicesManager.GetAllCustomers();
            cmbStartLocation.ItemsSource = vipServicesManager.GetAllLocations();
            cmbArrivalLocation.ItemsSource = vipServicesManager.GetAllLocations();
            cmbArrangement.ItemsSource = typeof(ArrangementType).GetEnumValues();
            cmbStartTime.ItemsSource = hours;
            cmbEndTime.ItemsSource = hours;
            lblLimousine.Visibility = Visibility.Hidden;
            cmbLimousine.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(cmbArrangement.SelectedItem != null)
            {
                lblLimousine.Visibility = Visibility.Visible;
                cmbLimousine.Visibility = Visibility.Visible;
                cmbLimousine.ItemsSource =
                vipServicesManager.GetAllAvailableLimousines(DateTime.Now, DateTime.Now,
                    (ArrangementType)Enum.Parse(typeof(ArrangementType), cmbArrangement.SelectedItem.ToString()));
            }
            else
            {
                MessageBox.Show("Gelieve eerst een arrangement aanduiden.");
            }
        }
    }
}
