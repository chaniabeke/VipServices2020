using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private VipServicesContext context;

        public PriceRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddPrice(Price price)
        {
            context.Prices.Add(price);
        }
    }
}
