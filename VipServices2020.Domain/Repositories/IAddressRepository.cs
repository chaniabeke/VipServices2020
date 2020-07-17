using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain.Repositories
{
    public interface IAddressRepository
    {
        void AddAddress(Address address);
    }
}
