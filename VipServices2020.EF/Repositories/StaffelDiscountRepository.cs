using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Models;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class StaffelDiscountRepository : IStaffelDiscountRepository
    {
        private VipServicesContext context;

        public StaffelDiscountRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddStaffel(StaffelDiscount staffel)
        {
            context.StaffelDiscounts.Add(staffel);
        }
        public StaffelDiscount FindSmallestReservationCount(CategoryType category)
        {
            return context.StaffelDiscounts.Where(s => s.Category == category).OrderBy(s => s.NumberOfBookedReservations).FirstOrDefault();
        }
        public StaffelDiscount FindBiggestReservationCount(CategoryType category)
        {
            return context.StaffelDiscounts.Where(s => s.Category == category).OrderByDescending(s => s.NumberOfBookedReservations).FirstOrDefault();
        }
        public IEnumerable<StaffelDiscount> FindAll(CategoryType category)
        {
            return context.StaffelDiscounts.Where(s => s.Category == category).AsEnumerable<StaffelDiscount>();
        }
    }
}
