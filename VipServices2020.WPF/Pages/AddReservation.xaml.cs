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
        VipServicesManager vipServicesManager = new VipServicesManager(new UnitOfWork(new VipServicesContext("Production")));
        public AddReservation()
        {
            InitializeComponent();
            cmbCustomer.ItemsSource = vipServicesManager.GetAllCustomers();
            cmbStartLocation.ItemsSource = vipServicesManager.GetAllLocations();
            cmbArrivalLocation.ItemsSource = vipServicesManager.GetAllLocations();
            cmbLimousine.ItemsSource = vipServicesManager.GetAllLimousines();
            cmbArrangement.ItemsSource = typeof(ArrangementType).GetEnumValues();
        }

    }
}
