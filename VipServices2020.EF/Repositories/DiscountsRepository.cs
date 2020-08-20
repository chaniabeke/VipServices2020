using System;
using System.Collections.Generic;
using System.Linq;
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
        public Discount Find(CategoryType category)
        {
            return context.Discounts.Where(d => d.Category == category).FirstOrDefault();
        }

        public IEnumerable<Discount> FindAll()
        {
            return context.Discounts.OrderBy(d => d.Category).AsEnumerable<Discount>();
        }
    }
}
