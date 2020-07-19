using System;
using System.Collections.Generic;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class StaffelDiscounts
    {
        public int Id { get; set; }
        public Category Category { get; set; }
        public IList<StaffelDiscount> StaffelDiscountList { get; set; }
    }
}
