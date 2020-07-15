using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Repositories;
using VipServices2020.EF.Repositories;

namespace VipServices2020.EF {
    public class UnitOfWork : IUnitOfWork {
        VipServicesContext _context;

        public UnitOfWork(VipServicesContext context) {
            this._context = context;
            Categories = new CategoryRepository(_context);
            Customers = new CustomerRepository(_context);
        }

        public ICategoryRepository Categories { get; private set; }

        public ICustomerRepository Customers { get; private set; }

        public int Complete() {
            try {
                return _context.SaveChanges();
            } catch (Exception ex)
              //TODO : SqlExceptions
              {
                throw;
            }
        }

        public void Dispose() {
            _context.Dispose(); ;
        }
    }
}


