﻿using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain.Repositories
{
    public interface IDiscountRepository
    {
        void AddDiscount(Discount discounts);
        Discount Find(CategoryType category);
    }
}
