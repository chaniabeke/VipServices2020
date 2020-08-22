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

namespace VipServices2020.Tests.DomainLayer.Manager.ReservationTests.Add
{
    [TestClass]
    public class WeddingTest
    {
        [TestMethod]
        public void AddWeddingReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 8, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            double discountPercentage = m.CalculateStaffel(customer);
            Price price = PriceCalculator.FixedPriceWithDetailsPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage, ArrangementType.Wedding);
            Reservation weddingReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            var reservationInDb = contextTest.Reservations.First();
            Assert.AreEqual(reservationInDb.Customer, weddingReservation.Customer);
            Assert.AreEqual(reservationInDb.LimousineExpectedAddress, weddingReservation.LimousineExpectedAddress);
            Assert.AreEqual(reservationInDb.StartLocation, weddingReservation.StartLocation);
            Assert.AreEqual(reservationInDb.ArrivalLocation, weddingReservation.ArrivalLocation);
            Assert.AreEqual(reservationInDb.ArrangementType, weddingReservation.ArrangementType);
            Assert.AreEqual(reservationInDb.StartTime, weddingReservation.StartTime);
            Assert.AreEqual(reservationInDb.EndTime, weddingReservation.EndTime);
            Assert.AreEqual(reservationInDb.TotalHours, weddingReservation.TotalHours);
            Assert.AreEqual(reservationInDb.Limousine, weddingReservation.Limousine);
            Assert.AreEqual(reservationInDb.Price.Total, weddingReservation.Price.Total);
        }
        [TestMethod]
        public void AddWeddingReservation_WithMoreThen11Hours_ShouldThrowException()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 8, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 20, 0, 0);
            TimeSpan totalHours = endTime - startTime;
  
            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een reservatie mag niet langer zijn dan 11uur.");
        }
        [TestMethod]
        public void AddWeddingReservation_WithWrongStartHour_ShouldThrowException()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 17, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 0, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Wedding reservatie moet starten tussen 07u00 en 15u00.");
        }
        [TestMethod]
        public void AddWeddingReservation_WithLessThan7TotalHours_ShouldThrowException()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 12, 0, 0);
            TimeSpan totalHours = endTime - startTime;
 
            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Wedding reservatie moet minstens 7uur zijn.");
        }
        [TestMethod]
        public void AddWeddingReservation_EndDateBeforeStartDate_ShouldThrowException()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void AddWeddingReservation_WithNotAvailableLimousine_ShouldThrowException()
        {
            Assert.Fail();
        }
    }
}
