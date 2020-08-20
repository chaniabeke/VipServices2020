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
    [TestClass]
    public class StaffelTest
    {
        [TestMethod]
        public void AddStaffel_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Discount discount = new Discount(CategoryType.huwelijksplanner);
            Staffel staffel = new Staffel(5, 5, discount);

            Action act = () =>
            {
                m.AddStaffel(staffel);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(1, contextTest.Staffels.Local.Count);
            var staffelInDB = contextTest.Staffels.First();
            Assert.AreEqual(staffelInDB.Discount.Category, discount.Category);
            Assert.AreEqual(staffelInDB.NumberOfBookedReservations, 5);
            Assert.AreEqual(staffelInDB.DiscountPercentage, 5);
        }
        [TestMethod]
        public void CalculateStaffel_ShouldWork()
        {

        }
    }
}
