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
            Categories = new CategoryRepository(this.context);
            Customers = new CustomerRepository(this.context);
            Addresses = new AddressRepository(this.context);
            Limousines = new LimousineRepository(this.context);
            Locations = new LocationRepository(this.context);
            Reservations = new ReservationRepository(this.context);
            Prices = new PriceRepository(this.context);
        }

        public ICategoryRepository Categories { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public IAddressRepository Addresses { get; private set; }
        public ILimousineRepository Limousines { get; private set; }
        public ILocationRepository Locations { get; private set; }
        public IReservationRepository Reservations { get; private set; }
        public IPriceRepository Prices { get; private set; }

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


