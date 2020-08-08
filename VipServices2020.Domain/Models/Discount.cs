using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public CategoryType Category { get; set; }
        [Required]
        public IList<Staffel> StaffelDiscountList { get; set; }
    }
}
