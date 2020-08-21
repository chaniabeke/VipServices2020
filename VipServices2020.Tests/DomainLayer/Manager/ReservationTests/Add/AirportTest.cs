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
    public class AirportTest
    {
        [TestMethod]
        public void AddAirportReservation_ShouldWork()
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
            CategoryType category = CategoryType.geen;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            double discountPercentage = m.CalculateStaffel(customer, category);
            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation airportReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Airport, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddAirportReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            var reservationInDb = contextTest.Reservations.First();
            Assert.AreEqual(reservationInDb.Customer, airportReservation.Customer);
            Assert.AreEqual(reservationInDb.LimousineExpectedAddress, airportReservation.LimousineExpectedAddress);
            Assert.AreEqual(reservationInDb.StartLocation, airportReservation.StartLocation);
            Assert.AreEqual(reservationInDb.ArrivalLocation, airportReservation.ArrivalLocation);
            Assert.AreEqual(reservationInDb.ArrangementType, airportReservation.ArrangementType);
            Assert.AreEqual(reservationInDb.StartTime, airportReservation.StartTime);
            Assert.AreEqual(reservationInDb.EndTime, airportReservation.EndTime);
            Assert.AreEqual(reservationInDb.TotalHours, airportReservation.TotalHours);
            Assert.AreEqual(reservationInDb.Limousine, airportReservation.Limousine);
            Assert.AreEqual(reservationInDb.Price.Total, airportReservation.Price.Total);
        }
        [TestMethod]
        public void AddAirportReservation_WithMoreThen11Hours_ShouldFail()
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
            DateTime endTime = new DateTime(2020, 09, 22, 19, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            CategoryType category = CategoryType.geen;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            Action act = () =>
            {
                m.AddAirportReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Airport reservatie mag niet langer zijn dan 11uur.");
        }
        [TestMethod]
        public void AddAirportReservation_WithNotAvailableLimousine_ShouldFail()
        {

        }
    }
}
