using Microsoft.VisualBasic;
using System;
using System.Data.Common;

namespace VipServices2020.Domain.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public DateTime ReservationCreated { get; set; }
        public Address LimousineExpectedPlace { get; set; }


        public Location StartLocation { get; set; }
        public Location ArrivalLocation { get; set; }
        public ArrangementType ArrangementType { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public Limousine Limousine { get; set; }

        public Price Price { get; set; }
    }
}
//price vSTE PRIJS EN EEN TEBEREKEN PRIJS
// add method different weddingreservation, nightlifereservation,...
//istaffeldiscount, empty, vip, planner