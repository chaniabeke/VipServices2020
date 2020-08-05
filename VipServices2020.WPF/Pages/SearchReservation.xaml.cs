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
using VipServices2020.Domain;
using VipServices2020.EF;

namespace VipServices2020.WPF
{
    /// <summary>
    /// Interaction logic for SearchReservation.xaml
    /// </summary>
    public partial class SearchReservation : Page
    {
        VipServicesManager vipServicesManager = new VipServicesManager(new UnitOfWork(new VipServicesContext("Production")));
        public SearchReservation()
        {
            InitializeComponent();
            cmbCustomer.ItemsSource = vipServicesManager.GetAllCustomers();
            ltbReservations.ItemsSource = vipServicesManager.GetAllReservations();
        }
    }
}
