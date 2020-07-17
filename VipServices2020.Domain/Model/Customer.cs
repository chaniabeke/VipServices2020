using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Customer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string BtwNumber { get; set; }

        public Address Address { get; set; }
        public Category Category { get; set; }
    }
}
