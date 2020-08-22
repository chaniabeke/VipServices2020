using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.Domain.Models
{
    public class StaffelDiscount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int NumberOfBookedReservations { get; set; }
        [Required]
        public double DiscountPercentage { get; set; }
        [Required]
        public CategoryType Category { get; set; }

        public StaffelDiscount(int numberOfBookedReservations, double discountPercentage)
        {
            NumberOfBookedReservations = numberOfBookedReservations;
            DiscountPercentage = discountPercentage;
        }
        public StaffelDiscount(int numberOfBookedReservations, double discountPercentage, CategoryType category)
        {
            NumberOfBookedReservations = numberOfBookedReservations;
            DiscountPercentage = discountPercentage;
            Category = category;
        }
    }
}
