using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class StaffelDiscounts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public IList<StaffelDiscount> StaffelDiscountList { get; set; }
    }
}
