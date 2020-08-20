using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;
using VipServices2020.EF;
using VipServices2020.Tests.EFLayer;

namespace VipServices2020.Tests.DomainLayer.Manager
{
    public class DiscountTest
    {
        public void AddDiscount_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Discount discount = new Discount(CategoryType.huwelijksplanner);

            Action act = () =>
            {
                m.AddDiscount(discount);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(1, contextTest.Discounts.Local.Count);
            var discountInDB = contextTest.Discounts.First();
            Assert.AreEqual(discountInDB.Category, discount.Category);
        }
    }
}
