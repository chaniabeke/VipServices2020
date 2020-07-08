using Microsoft.VisualBasic;
using System;
using System.Data.Common;

namespace VipServices2020.Domain.Domain {
    public class Reservation {
        public Customer Customer { get; set; }
        public DateTime ReservationCreated { get; set; }
        public int Id { get; set; }

        public LocationType StartLocation { get; set; }
        public LocationType ArrivalLocation { get; set; }
        public Limousine Limousine { get; set; }
        public ArrangementType ArrangementType { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public Price Price { get; set; }
    }
}
//price vSTE PRIJS EN EEN TEBEREKEN PRIJS
//weddingreservation, nightlifereservation,...