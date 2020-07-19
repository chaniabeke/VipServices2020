using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Town { get; set; }

        public Location(string town)
        {
            Town = town;
        }
    }
}
