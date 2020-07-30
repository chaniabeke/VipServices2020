using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private VipServicesContext context;

        public CustomerRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }
        
        public IEnumerable<Customer> FindAllCustomers()
        {
            return context.Customers.OrderBy(c => c.Name).AsEnumerable<Customer>();
        }
    }
}