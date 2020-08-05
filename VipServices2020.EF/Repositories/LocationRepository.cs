using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Model;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.EF.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private VipServicesContext context;

        public LocationRepository(VipServicesContext context)
        {
            this.context = context;
        }

        public void AddLocation(Location location)
        {
            context.Locations.Add(location);
        }

        public IEnumerable<Location> FindAll()
        {
            return context.Locations.OrderBy(l => l.Town).AsEnumerable<Location>();
        }
    }
}
