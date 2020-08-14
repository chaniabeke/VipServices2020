using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Models;
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

        public List<Limousine> FindAllAvailable(ArrangementType arrangement)
        {
            List<Limousine> limousines = new List<Limousine>();
            if(arrangement == ArrangementType.Wedding)
            {
                limousines = context.Limousines.Where(l => l.WeddingPrice != 0).ToList();
            }
            else if (arrangement == ArrangementType.Wellness)
            {
                limousines = context.Limousines.Where(l => l.WelnessPrice != 0).ToList();
            }
            else if (arrangement == ArrangementType.NightLife)
            {
                limousines = context.Limousines.Where(l => l.NightLifePrice != 0).ToList();
            }
            else
            {
                limousines = context.Limousines.ToList();
            }

            return limousines;
        }
    }
}
