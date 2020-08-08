using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.Domain.Models
{
    public class Staffel
    {
        [Key]
        public int NumberOfBookedReservations { get; set; }
        [Required]
        public int Discount { get; set; }

        public Staffel(int numberOfBookedReservations, int discount)
        {
            NumberOfBookedReservations = numberOfBookedReservations;
            Discount = discount;
        }
    }
}
