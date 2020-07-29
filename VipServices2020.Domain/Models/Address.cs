using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string StreetName { get; set; }
        [Required]
        [StringLength(5, MinimumLength = 1)]
        public string StreetNumber { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Town { get; set; }

        public Address(string streetName, string streetNumber, string town)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
            Town = town;
        }
    }
}
