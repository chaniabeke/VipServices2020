using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain.Model
{
    public class Customer
    {
        [Key]
        public int CustomerNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(16, MinimumLength = 16)]
        public string BtwNumber { get; set; }

        [Required]
        public Address Address { get; set; }
        [Required]
        public CategoryType Category { get; set; }

        public Customer()
        {

        }
        public Customer(string name, string btwNumber)
        {
            Name = name;
            BtwNumber = btwNumber;
        }

        public Customer(string name, string btwNumber, Address address, CategoryType category)
        {
            Name = name;
            BtwNumber = btwNumber;
            Address = address;
            Category = category;
        }

        public override string ToString()
        {
            return $"{CustomerNumber} {Name} {BtwNumber} {Address} {Category}";
        }
    }
}
