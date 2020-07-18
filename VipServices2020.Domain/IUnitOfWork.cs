using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.Domain {
    public interface IUnitOfWork : IDisposable {
        ICategoryRepository Categories { get; }
        ICustomerRepository Customers { get; }
        IAddressRepository Addresses { get; }
        ILimousineRepository Limousines { get; }
        int Complete();
    }
}
