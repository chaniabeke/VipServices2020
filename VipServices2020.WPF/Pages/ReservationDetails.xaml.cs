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
using VipServices2020.Domain.Models;
using VipServices2020.EF;

namespace VipServices2020.WPF
{
    /// <summary>
    /// Interaction logic for ReservationDetails.xaml
    /// </summary>
    public partial class ReservationDetails : Page
    {
        VipServicesManager vipServicesManager = new VipServicesManager(new UnitOfWork(new VipServicesContext("Production")));
        public ReservationDetails(int reservationId)
        {
            InitializeComponent();
            try
            {
                Reservation reservation = vipServicesManager.GetReservation(reservationId);
                txtCustomerNumber.Text = reservation.Customer.CustomerNumber.ToString();
                txtCustomerName.Text = reservation.Customer.Name.ToString();
                txtCustomerCategory.Text = reservation.Customer.Category.ToString();
                if (reservation.Customer.BtwNumber == "")
                {
                    txtCustomerBtwNumber.Text = "Geen";
                }
                else
                {
                    txtCustomerBtwNumber.Text = reservation.Customer.BtwNumber.ToString();
                }
                txtCustomerAddress.Text = reservation.Customer.Address.ToString();

                txtReservationCreated.Text = reservation.ReservationCreated.Date.ToLongDateString();
                txtReservationId.Text = reservation.Id.ToString();
                txtLimousineExpectedAddress.Text = reservation.LimousineExpectedAddress.ToString();
                txtReservationStartLocation.Text = reservation.StartLocation.ToString();
                txtReservationArrivalLocation.Text = reservation.ArrivalLocation.ToString();
                txtReservationStartTime.Text = reservation.StartTime.ToLongDateString() + "  " + reservation.StartTime.ToShortTimeString();
                txtReservationEndTime.Text = reservation.EndTime.ToLongDateString() + "  " + reservation.EndTime.ToShortTimeString();
                txtLimousineArrangementType.Text = reservation.ArrangementType.ToString();

                txtLimousineBrand.Text = reservation.Limousine.Brand.ToString();
                txtLimousineModel.Text = reservation.Limousine.Model.ToString();
                txtLimousineNumber.Text = reservation.Limousine.Id.ToString();
                txtLimousineColor.Text = reservation.Limousine.Color.ToString();

                txtFirstHourPrice.Text = "\u20AC" + reservation.Price.FirstHourPrice.ToString();
                txtSecondHourCount.Text = reservation.Price.SecondHourCount.ToString() + " x ";
                txtSecondHourPrice.Text = "\u20AC" + reservation.Price.SecondHourPrice.ToString();
                txtOvertimeCount.Text = reservation.Price.OvertimeCount.ToString() + " x ";
                txtOvertimePrice.Text = "\u20AC" + reservation.Price.OvertimePrice.ToString();
                txtNightHourCount.Text = reservation.Price.NightHourCount.ToString() + " x ";
                txtNightHourPrice.Text = "\u20AC" + reservation.Price.NightHourPrice.ToString();
                txtFixedPrice.Text = "\u20AC" + reservation.Price.FixedPrice.ToString();
                txtSubTotal.Text = "\u20AC" + reservation.Price.SubTotal.ToString();
                txtExclusiveBtw.Text = "\u20AC" + reservation.Price.ExclusiveBtw.ToString();
                txtStaffelDiscount.Text = reservation.Price.StaffelDiscount.ToString() + "%";
                txtBtw.Text = reservation.Price.Btw.ToString() + "%";
                txtBtwPrice.Text = "\u20AC" + reservation.Price.BtwPrice.ToString();
                txtTotal.Text = "\u20AC" + reservation.Price.Total.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fout: " + ex.Message,
                                  "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

    }
}
