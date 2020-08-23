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
    public class NightLifeTest
    {
        [TestMethod]
        public void AddNightLifeReservation_ShouldWork()
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
            DateTime endTime = new DateTime(2020, 09, 23, 4, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            double discountPercentage = m.CalculateStaffel(customer);
            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime,
                discountPercentage);
            Reservation nightLifeReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.NightLife, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            var reservationInDb = contextTest.Reservations.First();
            Assert.AreEqual(reservationInDb.Customer, nightLifeReservation.Customer);
            Assert.AreEqual(reservationInDb.LimousineExpectedAddress, nightLifeReservation.LimousineExpectedAddress);
            Assert.AreEqual(reservationInDb.StartLocation, nightLifeReservation.StartLocation);
            Assert.AreEqual(reservationInDb.ArrivalLocation, nightLifeReservation.ArrivalLocation);
            Assert.AreEqual(reservationInDb.ArrangementType, nightLifeReservation.ArrangementType);
            Assert.AreEqual(reservationInDb.StartTime, nightLifeReservation.StartTime);
            Assert.AreEqual(reservationInDb.EndTime, nightLifeReservation.EndTime);
            Assert.AreEqual(reservationInDb.TotalHours, nightLifeReservation.TotalHours);
            Assert.AreEqual(reservationInDb.Limousine, nightLifeReservation.Limousine);
            Assert.AreEqual(reservationInDb.Price.Total, nightLifeReservation.Price.Total);
        }
        [TestMethod]
        public void AddNightLifeReservation_WithMoreThen11Hours_ShouldThrowException()
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
            DateTime endTime = new DateTime(2020, 09, 23, 8, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een reservatie mag niet langer zijn dan 11uur.");
        }
        [TestMethod]
        public void AddNightLifeReservation_WithWrongStartHour_ShouldThrowException()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 19, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 4, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een NightLife reservatie moet starten tussen 20u00 en 24u00.");
        }
        [TestMethod]
        public void AddNightLifeReservation_WithLessThan7TotalHours_ShouldThrowException()
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
            DateTime endTime = new DateTime(2020, 09, 23, 22, 0, 0);
            TimeSpan totalHours = endTime - startTime;
 
            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een NightLife reservatie moet minstens 7uur zijn.");
        }
        [TestMethod]
        public void AddNightLifeReservation_EndDateBeforeStartDate_ShouldThrowException()
        {
            Assert.Fail();
        }
        public void AddNightLifeReservation_WithNotAvailableLimousine_ShouldThrowException()
        {
            Assert.Fail();
        }
    }
}
