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
    public class LimousineTest
    {
        [TestMethod]
        public void AddLimousine_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Limousine limousine = new Limousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);

            Action act = () =>
            {
                m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Limousines.Local.Count);
            var limousinesInDb = contextTest.Limousines.First();
            Assert.AreEqual(limousinesInDb.Brand, limousine.Brand);
            Assert.AreEqual(limousinesInDb.Model, limousine.Model);
            Assert.AreEqual(limousinesInDb.Color, limousine.Color);
            Assert.AreEqual(limousinesInDb.FirstHourPrice, limousine.FirstHourPrice);
            Assert.AreEqual(limousinesInDb.NightLifePrice, limousine.NightLifePrice);
            Assert.AreEqual(limousinesInDb.WeddingPrice, limousine.WeddingPrice);
            Assert.AreEqual(limousinesInDb.WelnessPrice, limousine.WelnessPrice);
        }
        [TestMethod]
        public void GetAllLimousines_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Action act = () =>
            {
                m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
                m.AddLimousine("Lincoln", "Limousine", "Pink", 180, 900, 500, 1000);
                m.AddLimousine("Tesla", "Model S", "White", 500, 0, 2000, 2200);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(3, contextTest.Limousines.Local.Count);
        }
        [TestMethod]
        public void GetAllAvailableLimousines_ShouldWork()
        {
            //VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            //VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            //m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            //m.AddLimousine("Lincoln", "Limousine", "Pink", 180, 900, 0, 1000);
            //m.AddLimousine("Tesla", "Model S", "White", 500, 0, 2000, 2200);

            //Action act = () =>
            //{
            //    m.GetAllAvailableLimousines();
            //};
        }
    }
}
