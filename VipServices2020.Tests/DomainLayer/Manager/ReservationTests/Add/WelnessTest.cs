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
    public class WelnessTest
    {
        [TestMethod]
        public void AddWelnessReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            double discountPercentage = m.CalculateStaffel(customer);
            Price price = PriceCalculator.WelnessPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation welnessReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wellness, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddWelnessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            var reservationInDb = contextTest.Reservations.First();
            Assert.AreEqual(reservationInDb.Customer, welnessReservation.Customer);
            Assert.AreEqual(reservationInDb.LimousineExpectedAddress, welnessReservation.LimousineExpectedAddress);
            Assert.AreEqual(reservationInDb.StartLocation, welnessReservation.StartLocation);
            Assert.AreEqual(reservationInDb.ArrivalLocation, welnessReservation.ArrivalLocation);
            Assert.AreEqual(reservationInDb.ArrangementType, welnessReservation.ArrangementType);
            Assert.AreEqual(reservationInDb.StartTime, welnessReservation.StartTime);
            Assert.AreEqual(reservationInDb.EndTime, welnessReservation.EndTime);
            Assert.AreEqual(reservationInDb.TotalHours, welnessReservation.TotalHours);
            Assert.AreEqual(reservationInDb.Limousine, welnessReservation.Limousine);
            Assert.AreEqual(reservationInDb.Price.Total, welnessReservation.Price.Total);
        }
        [TestMethod]
        public void AddWelnessReservation_WithWrongStartHour_ShouldThrowException()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 13, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 23, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddWelnessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Welness reservatie moet starten tussen 07u00 en 12u00.");
        }
        [TestMethod]
        public void AddWelnessReservation_WithLessThen10Hours_ShouldThrowException()
        {
            Assert.Fail();
        }
            [TestMethod]
        public void AddWelnessReservation_WithMoreThen10Hours_ShouldThrowException()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 20, 0, 0);
            TimeSpan totalHours = endTime - startTime;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddWelnessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Welness reservatie moet altijd 10 uur zijn.");
        }
        [TestMethod]
        public void AddWelnessReservation_EndDateBeforeStartDate_ShouldThrowException()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void AddWelnessReservation_WithNotAvailableLimousine_ShouldThrowException()
        {
            Assert.Fail();
        }
    }
}
