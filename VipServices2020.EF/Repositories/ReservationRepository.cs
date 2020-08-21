using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VipServices2020.Domain.Models;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private VipServicesContext context;

        public ReservationRepository(VipServicesContext context)
        {
            this.context = context;
        }

        /// <summary>
        ///voeg reservatie object toe
        /// </summary>
        public void AddReservation(Reservation reservation)
        {
            context.Reservations.Add(reservation);
        }

        /// <summary>
        /// geef een reservatie op basis van id
        /// </summary>
        public IEnumerable<Reservation> Find(int id)
        {
            return context.Reservations.Where(r => r.Id == id).Include(r => r.Customer).ThenInclude(c => c.Address)
                .Include(r => r.LimousineExpectedAddress)
                .Include(r => r.StartLocation).Include(r => r.ArrivalLocation)
                .Include(r => r.Limousine).Include(r => r.Price).AsEnumerable<Reservation>();
        }
        /// <summary>
        /// geef alle reservaties
        /// </summary>
        public IEnumerable<Reservation> FindAll()
        {
            return context.Reservations.OrderBy(r => r.StartTime).ThenBy(r => r.Customer)
                .Include(r => r.Customer).Include(r => r.LimousineExpectedAddress)
                .Include(r => r.StartLocation).Include(r => r.ArrivalLocation)
                .Include(r => r.Limousine).Include(r => r.Price).AsEnumerable<Reservation>();
        }

        /// <summary>
        /// geef alle reservaties van een gekozen klant
        /// </summary>
        public IEnumerable<Reservation> FindAll(Customer customer)
        {
            return context.Reservations.Where(r => r.Customer.CustomerNumber == customer.CustomerNumber).AsEnumerable<Reservation>();
        }

        /// <summary>
        /// geef alle reservaties van een gekozen datum
        /// </summary>
        public IEnumerable<Reservation> FindAll(DateTime startTime)
        {
            return context.Reservations.Where(r => r.StartTime.Date == startTime.Date).AsEnumerable<Reservation>();
        }

        /// <summary>
        /// geef alle reservaties van een gekozen klant en datum
        /// </summary>
        public IEnumerable<Reservation> FindAll(Customer customer, DateTime startTime)
        {
            return context.Reservations.Where(r => r.StartTime.Date == startTime.Date).Where(r => r.Customer == customer).AsEnumerable<Reservation>();
        }
    }
}
