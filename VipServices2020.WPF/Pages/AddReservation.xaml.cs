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
            try
            {
                cmbCustomer.ItemsSource = vipServicesManager.GetAllCustomers();
                cmbStartLocation.ItemsSource = vipServicesManager.GetAllLocations();
                cmbArrivalLocation.ItemsSource = vipServicesManager.GetAllLocations();
                cmbArrangement.ItemsSource = typeof(ArrangementType).GetEnumValues();
                cmbStartTime.ItemsSource = hours;
                cmbEndTime.ItemsSource = hours;
                lblLimousine.Visibility = Visibility.Hidden;
                cmbLimousine.Visibility = Visibility.Hidden;
                btnPrice.Visibility = Visibility.Hidden;
                stpPrice.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout: " + ex.Message,
                                    "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void btnPrice_Click(object sender, RoutedEventArgs e)
        {
            if (cmbArrangement.SelectedItem != null && dtpStartDate != null && dtpEndDate != null
                && cmbStartTime != null && cmbEndTime != null && cmbLimousine != null)
            {
                try
                {
                    stpPrice.Visibility = Visibility.Visible;
                    CalculatePrice();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fout: " + ex.Message,
                                   "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void btnLimousine_Click(object sender, RoutedEventArgs e)
        {
            if (cmbArrangement.SelectedItem != null)
            {
                try
                {
                    lblLimousine.Visibility = Visibility.Visible;
                    cmbLimousine.Visibility = Visibility.Visible;
                    DateTime startDate = GetStartDate();
                    DateTime endDate = GetEndDate();
                    GetAvailableLimousines(startDate, endDate);

                    btnPrice.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fout: " + ex.Message,
                                   "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Gelieve eerst een arrangement aanduiden.");
            }

        }

        private void btnReservation_Click(object sender, RoutedEventArgs e)
        {
            if (cmbCustomer.SelectedItem != null && txtStreet.Text != String.Empty && txtNumber.Text != String.Empty
                && txtTown.Text != String.Empty && cmbArrangement.SelectedItem != null && dtpStartDate != null
                && dtpEndDate != null && cmbStartLocation.SelectedItem != null && cmbEndTime.SelectedItem != null
                && cmbStartTime != null && cmbEndTime != null && cmbLimousine != null)
            {
                try
                {
                    Address address = new Address(txtStreet.Text, txtNumber.Text, txtTown.Text);

                    DateTime startDate = GetStartDate();
                    DateTime endDate = GetEndDate();

                    if (cmbArrangement.SelectedItem.Equals(ArrangementType.NightLife))
                    {
                        vipServicesManager.AddNightLifeReservation((Customer)cmbCustomer.SelectedItem, address, (Location)cmbStartLocation.SelectedItem,
                        (Location)cmbArrivalLocation.SelectedItem, startDate, endDate, (Limousine)cmbLimousine.SelectedItem);
                    }
                    if (cmbArrangement.SelectedItem.Equals(ArrangementType.Wedding))
                    {
                        vipServicesManager.AddWeddingReservation((Customer)cmbCustomer.SelectedItem, address, (Location)cmbStartLocation.SelectedItem,
                      (Location)cmbArrivalLocation.SelectedItem, startDate, endDate, (Limousine)cmbLimousine.SelectedItem);
                    }
                    if (cmbArrangement.SelectedItem.Equals(ArrangementType.Wellness))
                    {
                        vipServicesManager.AddWelnessReservation((Customer)cmbCustomer.SelectedItem, address, (Location)cmbStartLocation.SelectedItem,
                      (Location)cmbArrivalLocation.SelectedItem, startDate, endDate, (Limousine)cmbLimousine.SelectedItem);
                    }
                    if (cmbArrangement.SelectedItem.Equals(ArrangementType.Business))
                    {
                        vipServicesManager.AddBusinessReservation((Customer)cmbCustomer.SelectedItem, address, (Location)cmbStartLocation.SelectedItem,
                      (Location)cmbArrivalLocation.SelectedItem, startDate, endDate, (Limousine)cmbLimousine.SelectedItem);
                    }
                    if (cmbArrangement.SelectedItem.Equals(ArrangementType.Airport))
                    {
                        vipServicesManager.AddAirportReservation((Customer)cmbCustomer.SelectedItem, address, (Location)cmbStartLocation.SelectedItem,
                      (Location)cmbArrivalLocation.SelectedItem, startDate, endDate, (Limousine)cmbLimousine.SelectedItem);
                    }
                    MessageBox.Show("Reservatie is succesvol toegevoegd!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fout: " + ex.Message,
                     "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Gelieve alle velden in te vullen.");
            }
        }
        private void GetAvailableLimousines(DateTime startDate, DateTime endDate)
        {
            cmbLimousine.ItemsSource =
                                vipServicesManager.GetAllAvailableLimousines(startDate, endDate,
                                    (ArrangementType)Enum.Parse(typeof(ArrangementType), cmbArrangement.SelectedItem.ToString()));
        }
        private void CalculatePrice()
        {
            try
            {
                DateTime startDate = new DateTime(dtpStartDate.SelectedDate.Value.Year,
               dtpStartDate.SelectedDate.Value.Month, dtpStartDate.SelectedDate.Value.Day,
               (int)cmbStartTime.SelectedItem, 0, 0);
                DateTime endDate = new DateTime(dtpEndDate.SelectedDate.Value.Year,
                    dtpEndDate.SelectedDate.Value.Month, dtpEndDate.SelectedDate.Value.Day,
                    (int)cmbEndTime.SelectedItem, 0, 0);

                TimeSpan totalHours = endDate - startDate;

                Price price = new Price();

                double discountPercentage
                    = vipServicesManager.CalculateStaffel((Customer)cmbCustomer.SelectedItem);

                if (cmbArrangement.SelectedItem.Equals(ArrangementType.NightLife))
                {
                    price = PriceCalculator.NightlifePriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                         startDate, endDate, discountPercentage);
                }
                if (cmbArrangement.SelectedItem.Equals(ArrangementType.Wedding))
                {
                    price = PriceCalculator.WeddingPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                      startDate, endDate, discountPercentage);
                }
                if (cmbArrangement.SelectedItem.Equals(ArrangementType.Wellness))
                {
                    price = PriceCalculator.WelnessPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                      startDate, endDate, discountPercentage);
                }
                if (cmbArrangement.SelectedItem.Equals(ArrangementType.Business))
                {
                    price = PriceCalculator.PerHourPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                     startDate, endDate, discountPercentage);
                }
                if (cmbArrangement.SelectedItem.Equals(ArrangementType.Airport))
                {
                    price = PriceCalculator.PerHourPriceCalculator((Limousine)cmbLimousine.SelectedItem, totalHours,
                      startDate, endDate, discountPercentage);
                }

                txtFirstHourPrice.Text = "\u20AC" + price.FirstHourPrice.ToString();
                txtSecondHourCount.Text = price.SecondHourCount.ToString() + "x  = ";
                txtSecondHourPrice.Text = "\u20AC" + Math.Round(price.SecondHourPrice, 2).ToString();
                txtOvertimeCount.Text = price.OvertimeCount.ToString() + "x  = ";
                txtOvertimePrice.Text = "\u20AC" + Math.Round(price.OvertimePrice, 2).ToString();
                txtNightHourCount.Text = price.NightHourCount.ToString() + "x  = ";
                txtNightHourPrice.Text = "\u20AC" + Math.Round(price.NightHourPrice, 2).ToString();
                txtFixedPrice.Text = "\u20AC" + price.FixedPrice.ToString();
                txtSubTotal.Text = "\u20AC" + Math.Round(price.SubTotal, 2).ToString();
                txtExclusiveBtw.Text = "\u20AC" + Math.Round(price.ExclusiveBtw, 2).ToString();
                txtStaffelDiscount.Text = price.StaffelDiscount.ToString() + "%";
                txtBtw.Text = price.Btw.ToString() + "%";
                txtBtwPrice.Text = "\u20AC" + Math.Round(price.BtwPrice, 2).ToString();
                txtTotal.Text = "\u20AC" + Math.Round(price.Total, 2).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout: " + ex.Message,
                                   "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        private DateTime GetStartDate()
        {
            DateTime startDate = new DateTime(dtpStartDate.SelectedDate.Value.Year,
                       dtpStartDate.SelectedDate.Value.Month, dtpStartDate.SelectedDate.Value.Day,
                       (int)cmbStartTime.SelectedItem, 0, 0);
            return startDate;
        }
        private DateTime GetEndDate()
        {
            DateTime endDate = new DateTime(dtpEndDate.SelectedDate.Value.Year,
                dtpEndDate.SelectedDate.Value.Month, dtpEndDate.SelectedDate.Value.Day,
                (int)cmbEndTime.SelectedItem, 0, 0);
            return endDate;
        }
    }
}
