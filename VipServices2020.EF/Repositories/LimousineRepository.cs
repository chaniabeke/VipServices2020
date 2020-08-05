using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Limousine> FindAll()
        {
            return context.Limousines.OrderBy(l => l.Brand).ThenBy(l => l.Model).ThenBy(l => l.Color).AsEnumerable<Limousine>();
        }
    }
}
