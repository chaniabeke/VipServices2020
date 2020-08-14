using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain.Repositories
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation reservation);
        IEnumerable<Reservation> Find(int id);
        IEnumerable<Reservation> FindAll();
        IEnumerable<Reservation> FindAll(Customer customer);
        IEnumerable<Reservation> FindAll(DateTime reservationDate);
        IEnumerable<Reservation> FindAll(Customer customer, DateTime reservationDate);
        IEnumerable<Reservation> FindAllNotAvailable(DateTime startTime, DateTime endTime);
    }
}
