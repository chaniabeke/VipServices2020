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
    public class CustomerTest
    {
        [TestMethod]
        public void AddCustomer_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.geen);

            Action act = () =>
            {
                m.AddCustomer("Jan", "BE0502358347", address, CategoryType.geen);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Customers.Local.Count);
            var customerInDb = contextTest.Customers.First();
            Assert.AreEqual(customerInDb.Name, customer.Name);
            Assert.AreEqual(customerInDb.BtwNumber, customer.BtwNumber);
            Assert.AreEqual(customerInDb.Address, customer.Address);
            Assert.AreEqual(customerInDb.Category, customer.Category);
        }
        [TestMethod]
        public void GetAllCustomers_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Action act = () =>
            {
                m.AddCustomer("Jan", "BE0502358347", new Address("Groenlaan", "17", "Herzele"), CategoryType.geen);
                m.AddCustomer("Bob", "", new Address("Dorpstraat", "101", "Antwerpen"), CategoryType.concertpromotor);
                m.AddCustomer("Piet", "", new Address("Schoolstraat", "80", "Blankenberge"), CategoryType.huwelijksplanner);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(3, contextTest.Customers.Local.Count);
        }
    }
}
