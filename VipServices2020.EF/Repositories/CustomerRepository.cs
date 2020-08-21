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

        /// <summary>
        ///voeg klant object toe
        /// </summary>
        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        /// <summary>
        /// Geef alle klanten met hun adress en sorteer op naam
        /// </summary>
        public IEnumerable<Customer> FindAll()
        {
            return context.Customers.OrderBy(c => c.Name).Include(c => c.Address).AsEnumerable<Customer>();
        }

        /// <summary>
        /// Zoek het aantal reservaties van de gekozen klant van dit jaar
        /// </summary>
        public int FindReservationCount(Customer customer)
        {
            int reservationtCount = context.Reservations.Where(r => r.Customer == customer)
                .Where(r => r.StartTime.Year == DateTime.Now.Year).Count();
            return reservationtCount;
        }
    }
}