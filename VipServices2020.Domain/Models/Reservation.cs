using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace VipServices2020.Domain.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public DateTime ReservationCreated { get; set; }
        [Required]
        public Address LimousineExpectedPlace { get; set; }

        [Required]
        public Location StartLocation { get; set; }
        [Required]
        public Location ArrivalLocation { get; set; }
        [Required]
        public ArrangementType ArrangementType { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public Limousine Limousine { get; set; }

        [Required]
        public Price Price { get; set; }
    }
}
//price vSTE PRIJS EN EEN TEBEREKEN PRIJS
// add method different weddingreservation, nightlifereservation,...
//istaffeldiscount, empty, vip, planner