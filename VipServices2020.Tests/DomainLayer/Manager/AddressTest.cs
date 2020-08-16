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
    public class AddressTest
    {
        [TestMethod]
        public void AddAddress_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address address = new Address("Groenlaan", "17", "Herzele");

            Action act = () =>
            {
                m.AddAddress("Groenlaan", "17", "Herzele");
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Addresses.Local.Count);
            var addressInDb = contextTest.Addresses.First();
            Assert.AreEqual(addressInDb.StreetName, address.StreetName);
            Assert.AreEqual(addressInDb.StreetNumber, address.StreetNumber);
            Assert.AreEqual(addressInDb.Town, address.Town);
        }
    }
}
