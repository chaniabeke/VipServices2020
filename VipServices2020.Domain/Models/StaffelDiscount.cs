using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class StaffelDiscount
    {
        [Key]
        public int NumberOfBookedReservations { get; set; }
        [Required]
        public decimal Discount { get; set; }
    }
}
