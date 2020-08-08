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
using VipServices2020.Domain.Models;
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

        private void btnPrice_Click(object sender, RoutedEventArgs e)
        {
            CalculatePrice();
        }

        private void btnLimousine_Click(object sender, RoutedEventArgs e)
        {
            if (cmbArrangement.SelectedItem != null)
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
        private void btnReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CalculatePrice()
        {
            DateTime startDate = new DateTime(dtpStartDate.SelectedDate.Value.Year,
                dtpStartDate.SelectedDate.Value.Month, dtpStartDate.SelectedDate.Value.Day,
                (int)cmbStartTime.SelectedItem, 0, 0);
            DateTime endDate = new DateTime(dtpEndDate.SelectedDate.Value.Year,
                dtpEndDate.SelectedDate.Value.Month, dtpEndDate.SelectedDate.Value.Day,
                (int)cmbEndTime.SelectedItem, 0, 0);

            TimeSpan totalHours = endDate - startDate;

            Price price = new Price();

            if (cmbArrangement.SelectedItem.Equals(ArrangementType.NightLife))
            {
                price = PriceCalculator.NightLifeCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                     startDate, endDate, new Staffel(0, 0));
            }
            if (cmbArrangement.SelectedItem.Equals(ArrangementType.Wedding))
            {
                price = PriceCalculator.WeddingPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                  startDate, endDate, new Staffel(0, 0));
            }
            if (cmbArrangement.SelectedItem.Equals(ArrangementType.Wellness))
            {
                price = PriceCalculator.WelnessCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                  startDate, endDate, new Staffel(0, 0));
            }
            if (cmbArrangement.SelectedItem.Equals(ArrangementType.Business))
            {
                price = PriceCalculator.PerHourPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                 startDate, endDate, new Staffel(0, 0));
            }
            if (cmbArrangement.SelectedItem.Equals(ArrangementType.Airport))
            {
                price = PriceCalculator.PerHourPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                  startDate, endDate, new Staffel(0, 0));
            }

            txtFirstHourPrice.Text = "\u20AC" + price.FirstHourPrice.ToString();
            txtSecondHourCount.Text = price.SecondHourCount.ToString() + " x ";
            txtSecondHourPrice.Text = "\u20AC" + price.SecondHourPrice.ToString();
            txtOvertimeCount.Text = price.OvertimeCount.ToString() + " x ";
            txtOvertimePrice.Text = "\u20AC" + price.OvertimePrice.ToString();
            txtNightHourCount.Text = price.NightHourCount.ToString() + " x ";
            txtNightHourPrice.Text = "\u20AC" + price.NightHourPrice.ToString();
            txtFixedPrice.Text = "\u20AC" + price.FixedPrice.ToString();
            txtSubTotal.Text = "\u20AC" + price.SubTotal.ToString();
            txtExclusiveBtw.Text = "\u20AC" + price.ExclusiveBtw.ToString();
            //txtStaffelDiscount.Text = price.Staffel.Discount.ToString() + "%";
            txtBtw.Text = price.Btw.ToString() + "%";
            txtBtwPrice.Text = "\u20AC" + price.BtwPrice.ToString();
            txtTotal.Text = "\u20AC" + price.Total.ToString();
        }
    }
}
