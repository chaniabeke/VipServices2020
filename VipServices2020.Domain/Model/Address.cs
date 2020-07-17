using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Town { get; set; }

        public Address(string streetName, string streetNumber, string town)
        {
            StreetName = streetName;
            StreetNumber = streetNumber;
            Town = town;
        }
    }
}
