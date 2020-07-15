using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Model {
    public class StaffelDiscount {
        public int NumberOfBookedReservations { get; set; }
        public decimal Discount { get; set; }
    }
}
