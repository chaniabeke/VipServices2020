using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain.Repositories
{
    public interface IStaffelDiscountRepository
    {
        void AddStaffel(StaffelDiscount staffelDiscount);
        StaffelDiscount FindSmallestReservationCount(CategoryType category);
        IEnumerable<StaffelDiscount> FindAll(CategoryType category);
    }
}
