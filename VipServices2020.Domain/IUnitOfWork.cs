using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.Domain {
    public interface IUnitOfWork : IDisposable {
      
        ICustomerRepository Customers { get; }
        IAddressRepository Addresses { get; }
        ILimousineRepository Limousines { get; }
        ILocationRepository Locations { get; }
        IReservationRepository Reservations { get; }
        IPriceRepository Prices { get; }
        IStaffelDiscountRepository StaffelDiscounts { get; }
        int Complete();
    }
}
