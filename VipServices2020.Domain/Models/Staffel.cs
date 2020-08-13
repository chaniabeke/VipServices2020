using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.Domain.Models
{
    public class Staffel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumberOfBookedReservations { get; set; }
        [Required]
        public double DiscountPercentage { get; set; }
        [Required]
        public Discount Discount { get; set; }

        public Staffel(int numberOfBookedReservations, double discountPercentage)
        {
            NumberOfBookedReservations = numberOfBookedReservations;
            DiscountPercentage = discountPercentage;
        }
        public Staffel(int numberOfBookedReservations, double discountPercentage, Discount discount)
        {
            NumberOfBookedReservations = numberOfBookedReservations;
            DiscountPercentage = discountPercentage;
            Discount = discount;
        }
    }
}
