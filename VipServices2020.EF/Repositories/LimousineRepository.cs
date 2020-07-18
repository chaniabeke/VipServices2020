using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class LimousineRepository : ILimousineRepository
    {
        private VipServicesContext context;

        public LimousineRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddLimousine(Limousine limousine)
        {
            context.Limousines.Add(limousine);
        }
    }
}
