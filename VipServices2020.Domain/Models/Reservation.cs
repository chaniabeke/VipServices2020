using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace VipServices2020.Domain.Models
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
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public TimeSpan TotalHours { get; set; }

        public Limousine Limousine { get; set; }

        public Price Price { get; set; }


        public Reservation(Customer customer, DateTime reservationCreated, Address limousineExpectedAddress, Location startLocation, Location arrivalLocation,
            ArrangementType arrangementType, DateTime startTime, DateTime endTime, TimeSpan totalHours, Limousine limousine, Price price)
        {
            Customer = customer;
            ReservationCreated = reservationCreated;
            LimousineExpectedAddress = limousineExpectedAddress;
            StartLocation = startLocation;
            ArrivalLocation = arrivalLocation;
            ArrangementType = arrangementType;
            StartTime = startTime;
            EndTime = endTime;
            TotalHours = totalHours;
            Limousine = limousine;
            Price = price;
        }
        public Reservation(DateTime reservationCreated, DateTime startTime, DateTime endTime, TimeSpan totalHours)
        {
            ReservationCreated = reservationCreated;
            StartTime = startTime;
            EndTime = endTime;
            TotalHours = totalHours;
        }
    }
}