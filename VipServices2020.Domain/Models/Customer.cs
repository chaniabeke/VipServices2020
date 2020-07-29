using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerNumber { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(16, MinimumLength = 16)]
        public string BtwNumber { get; set; }

        [Required]
        public Address Address { get; set; }
        [Required]
        public Category Category { get; set; }

        public Customer(int customerNumber, string name, string btwNumber)
        {
            CustomerNumber = customerNumber;
            Name = name;
            BtwNumber = btwNumber;
        }

        public Customer(int customerNumber, string name, string btwNumber, Address address, Category category)
        {
            CustomerNumber = customerNumber;
            Name = name;
            BtwNumber = btwNumber;
            Address = address;
            Category = category;
        }
    }
}
