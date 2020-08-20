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
    public class LocationTest
    {
        [TestMethod]
        public void AddLocation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Location location = new Location("Brussel");

            Action act = () =>
            {
                m.AddLocation("Brussel");
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(1, contextTest.Locations.Local.Count);
            var locationInDB = contextTest.Locations.First();
            Assert.AreEqual(locationInDB.Town, location.Town);
        }
        [TestMethod]
        public void GetAllLocations_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            m.AddLocation("Brussel");
            m.AddLocation("Gent");
            m.AddLocation("Antwerpen");
            m.AddLocation("Brugge");
            m.AddLocation("Luik");

            Action act = () =>
            {
                m.GetAllLocations();
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(5, contextTest.Locations.Local.Count);
            Assert.AreEqual(5, m.GetAllLocations().Count);
        }
    }
}
