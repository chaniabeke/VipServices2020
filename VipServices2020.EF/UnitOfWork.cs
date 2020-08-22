using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Repositories;
using VipServices2020.EF.Repositories;

namespace VipServices2020.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private VipServicesContext context;

        public UnitOfWork(VipServicesContext context)
        {
            this.context = context;
            
            Customers = new CustomerRepository(this.context);
            Addresses = new AddressRepository(this.context);
            Limousines = new LimousineRepository(this.context);
            Locations = new LocationRepository(this.context);
            Reservations = new ReservationRepository(this.context);
            Prices = new PriceRepository(this.context);
            StaffelDiscounts = new StaffelDiscountRepository(this.context);
        }
        public ICustomerRepository Customers { get; private set; }
        public IAddressRepository Addresses { get; private set; }
        public ILimousineRepository Limousines { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IReservationRepository Reservations { get; private set; }
        public IPriceRepository Prices { get; private set; }
        public IStaffelDiscountRepository StaffelDiscounts { get; private set; }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                throw;
            }
        }

        public void Dispose()
        {
            context.Dispose(); ;
        }
    }
}


