using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private VipServicesContext context;

        public AddressRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddAddress(Address address)
        {
            context.Addresses.Add(address);
        }
    }
}
