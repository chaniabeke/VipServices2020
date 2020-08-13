using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Models;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class StaffelRepository : IStaffelRepository
    {
        private VipServicesContext context;

        public StaffelRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddStaffel(Staffel staffel)
        {
            context.Staffels.Add(staffel);
        }
        public Staffel FindSmallestReservationCount(Discount discount)
        {
            return context.Staffels.Where(s => s.Discount == discount).OrderBy(s => s.NumberOfBookedReservations).FirstOrDefault();
        }
        public IEnumerable<Staffel> FindAll(Discount discount)
        {
            return context.Staffels.Where(s => s.Discount.Category == discount.Category).AsEnumerable<Staffel>();
        }
    }
}
