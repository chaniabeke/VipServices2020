using System;
using System.Collections.Generic;
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
    }
}
