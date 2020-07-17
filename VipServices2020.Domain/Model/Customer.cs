using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Customer
    {

        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public string BtwNumber { get; set; }

        public Address Address { get; set; }
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
