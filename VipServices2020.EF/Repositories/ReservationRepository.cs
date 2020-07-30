using System;
using System.Collections.Generic;
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
    }
}
