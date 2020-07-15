using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories {
    public class CustomerRepository : ICustomerRepository {

        private VipServicesContext _context;

        public CustomerRepository(VipServicesContext context) {
            this._context = context;
        }

        public void AddCustomer(Customer customer) {
            _context.Customers.Add(customer);
        }
    }
}