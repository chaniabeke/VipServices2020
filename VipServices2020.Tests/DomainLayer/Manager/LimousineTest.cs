using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;
using VipServices2020.EF;
using VipServices2020.EF.Repositories;
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

            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            m.AddLimousine("Lincoln", "Limousine", "Pink", 180, 900, 500, 1000);
            m.AddLimousine("Tesla", "Model S", "White", 500, 0, 2000, 2200);

            Action act = () =>
            {
                m.GetAllLimousines();
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(3, contextTest.Limousines.Local.Count);
            Assert.AreEqual(3, m.GetAllLimousines().Count);
        }
        [TestMethod]
        public void GetAllAvailableLimousines_AvailableLimousine_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");

            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            Limousine limousineChrysler = limousineRepo.Find(1);

            // 6 uur ervoor
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                 new DateTime(2020, 09, 22, 0, 0, 0), new DateTime(2020, 09, 22, 2, 0, 0), limousineChrysler);

            // 6 uur erna
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                 new DateTime(2020, 09, 22, 16, 0, 0), new DateTime(2020, 09, 22, 18, 0, 0), limousineChrysler);

            List<Limousine> limousines = m.GetAllAvailableLimousines(new DateTime(2020, 09, 22, 8, 0, 0), 
                new DateTime(2020, 09, 22, 10, 0, 0), ArrangementType.NightLife);

            Assert.AreEqual(1, contextTest.Limousines.Local.Count);
            Assert.AreEqual(2, contextTest.Reservations.Local.Count);
            Assert.AreEqual(limousines.Count, 1);

        }
        [TestMethod]
        public void GetAllAvailableLimousines_ArrangementNotAvailable_ShouldNotShow()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 3, 0, 0);

            m.AddLimousine("Lincoln", "Limousine", "Pink", 180, 0, 850, 1000);

            List<Limousine> limousines = m.GetAllAvailableLimousines(startTime, endTime, ArrangementType.NightLife);

            Assert.AreEqual(1, contextTest.Limousines.Local.Count);
            Assert.AreEqual(limousines.Count, 0);
        }
        [TestMethod]
        public void GetAllAvailableLimousines_LimousineNotAvailable_ShouldNotShow()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 3, 0, 0);

            //Niet beschikbaar - al gereserveerd
            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            Limousine limousineChrysler = limousineRepo.Find(1);
            m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival, startTime,
               endTime, limousineChrysler);

            List<Limousine> limousines = m.GetAllAvailableLimousines(startTime, endTime, ArrangementType.NightLife);

            Assert.AreEqual(1, contextTest.Limousines.Local.Count);
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            Assert.AreEqual(limousines.Count, 0);
        }
    }
}
