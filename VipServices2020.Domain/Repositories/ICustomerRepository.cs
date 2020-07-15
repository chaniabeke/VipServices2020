using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Interfaces;

namespace VipServices2020.Domain.Repositories {
    public interface ICustomerRepository {
        void AddCustomer(ICustomer customer);
    }
}
