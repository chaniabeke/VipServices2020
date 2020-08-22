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
    public class StaffelDiscountTest
    {
        [TestMethod]
        public void AddStaffel_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            StaffelDiscount staffelDiscount = new StaffelDiscount(5, 5, CategoryType.concertpromotor);

            Action act = () =>
            {
                m.AddStaffel(staffelDiscount);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(1, contextTest.StaffelDiscounts.Local.Count);
            var staffelInDB = contextTest.StaffelDiscounts.First();
            Assert.AreEqual(staffelInDB.Category,CategoryType.concertpromotor);
            Assert.AreEqual(staffelInDB.NumberOfBookedReservations, 5);
            Assert.AreEqual(staffelInDB.DiscountPercentage, 5);
        }
        [TestMethod]
        public void CalculateStaffel_None_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Vip_0Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Vip_2Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Vip_15Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Vip_20Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Planner_0Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Planner_5Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Planner25Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void CalculateStaffel_Planner_30Res_ShouldBeCorrect()
        {
            Assert.Fail();
        }
    }
}
