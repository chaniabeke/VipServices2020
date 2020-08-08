using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Models;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class DiscountsRepository : IDiscountRepository
    {
        private VipServicesContext context;

        public DiscountsRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddDiscount(Discount discounts)
        {
            context.Discounts.Add(discounts);
        }
    }
}
