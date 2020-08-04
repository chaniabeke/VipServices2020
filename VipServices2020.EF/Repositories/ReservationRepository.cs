using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Model;
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

        public void AddReservation(Reservation reservation)
        {
            context.Reservations.Add(reservation);
        }

        public Reservation Find(int id)
        {
            return context.Reservations.Find(id);
        }

        public IEnumerable<Reservation> Find(Customer customer)
        {
            return context.Reservations.Where(r => r.Customer.CustomerNumber == customer.CustomerNumber).AsEnumerable<Reservation>();
        }

        public IEnumerable<Reservation> Find(DateTime reservationDate)
        {
            return context.Reservations.Where(r => r.StartTime == reservationDate).AsEnumerable<Reservation>();
        }

        public IEnumerable<Reservation> FindAll()
        {
            return context.Reservations.OrderBy(r => r.StartTime).ThenBy(r => r.Customer).AsEnumerable<Reservation>();
        }
    }
}
