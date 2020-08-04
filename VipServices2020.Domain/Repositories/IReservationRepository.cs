using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain.Repositories
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation reservation);
        Reservation Find(int id);
        IEnumerable<Reservation> Find(Customer customer);
        IEnumerable<Reservation> Find(DateTime reservationDate);
        IEnumerable<Reservation> FindAll();
    }
}
