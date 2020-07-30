using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace VipServices2020.Domain.Model
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public Customer Customer { get; set; }

        [Required]
        public DateTime ReservationCreated { get; set; }

        public Address LimousineExpectedAddress { get; set; }

        public Location StartLocation { get; set; }

        public Location ArrivalLocation { get; set; }

        public ArrangementType ArrangementType { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public Limousine Limousine { get; set; }

        public Price Price { get; set; }


        public Reservation(Customer customer, DateTime reservationCreated, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
            ArrangementType arrangementType, DateTime reservationDate, TimeSpan startTime, TimeSpan endTime, Limousine limousine, Price price)
        {
            Customer = customer;
            ReservationCreated = reservationCreated;
            LimousineExpectedAddress = limousineExpectedAddress;
            StartLocation = startLocation;
            ArrivalLocation = arrivalLocation;
            ArrangementType = arrangementType;
            ReservationDate = reservationDate;
            StartTime = startTime;
            EndTime = endTime;
            Limousine = limousine;
            Price = price;
        }
        public Reservation(DateTime reservationCreated, DateTime reservationDate, TimeSpan startTime, TimeSpan endTime)
        {
            ReservationCreated = reservationCreated;
            ReservationDate = reservationDate;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
//price vSTE PRIJS EN EEN TEBEREKEN PRIJS
// add method different weddingreservation, nightlifereservation,...
//istaffeldiscount, empty, vip, planner