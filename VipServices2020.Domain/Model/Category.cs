using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Interfaces;

namespace VipServices2020.Domain.Model {
    public class Category : ICategory {
        public string CategoryName { get; set; }
    }
}
