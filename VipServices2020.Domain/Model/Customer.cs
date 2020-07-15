using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Interfaces;

namespace VipServices2020.Domain.Model {
    public class Customer : ICustomer {

        IAddress _address;
        ICategory _customerCategory;

        public Customer(IAddress address, ICategory customerCategory) {
            _address = address;
            _customerCategory = customerCategory;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string BtwNumber { get; set; }

    }
}
