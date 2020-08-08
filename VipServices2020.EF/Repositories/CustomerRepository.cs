using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Models;
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
        
        public IEnumerable<Customer> FindAll()
        {
            return context.Customers.OrderBy(c => c.Name).Include(c => c.Address).AsEnumerable<Customer>();
        }

        public int FindReservationCount(Customer customer, DateTime dateTime)
        {
            int reservationtCount = context.Reservations.Where(r => r.Customer == customer)
                .Where(r => r.StartTime.Year == dateTime.Year).Count();
            return reservationtCount;
        }
    }
}