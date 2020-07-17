using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Repositories;
using VipServices2020.EF.Repositories;

namespace VipServices2020.EF {
    public class UnitOfWork : IUnitOfWork {
        private VipServicesContext context;

        public UnitOfWork(VipServicesContext context) {
            this.context = context;
            Categories = new CategoryRepository(this.context);
            Customers = new CustomerRepository(this.context);
        }

        public ICategoryRepository Categories { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public int Complete() {
            try {
                return context.SaveChanges();
            } catch (Exception ex)
              //TODO : SqlExceptions
              {
                Console.WriteLine(ex.InnerException.Message);
                throw;
              
            }
        }

        public void Dispose() {
            context.Dispose(); ;
        }
    }
}


