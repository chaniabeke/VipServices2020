using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain.Models;
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

        /// <summary>
        ///voeg locatie object toe
        /// </summary>
        public void AddLocation(Location location)
        {
            context.Locations.Add(location);
        }

        /// <summary>
        /// Geef alle locaties, gesorteerd op gemeentenaam
        /// </summary>
        public IEnumerable<Location> FindAll()
        {
            return context.Locations.OrderBy(l => l.Town).AsEnumerable<Location>();
        }
    }
}
