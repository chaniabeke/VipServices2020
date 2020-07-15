using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Interfaces;

namespace VipServices2020.Domain.Model {
    public class Address : IAddress {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Town { get; set; }
    }
}
