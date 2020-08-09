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
    public class ReservationTest
    {
       
        //if (startTime<DateTime.Now) throw new DomainException("Een reservatie mag niet in het verleden zijn.");
        // if(endTime<startTime) throw new DomainException("Een reservatie mag niet eindigen voor het begint.");
        //    if (startTime.Hour< 7) if (startTime.Hour > 12) throw new DomainException("Een Welness reservatie moet starten tussen 07u00 en 12u00.");
        //TimeSpan totalHours = endTime - startTime;
        //    if (totalHours.Hours != 10) throw new DomainException("Een Welness reservatie moet altijd 10 uur zijn.");

        [TestMethod]
        public void AddWeddingReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 8, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Staffel staffel = new Staffel(5, 5);

            Price price = PriceCalculator.WeddingPriceCalculator(limousine, totalHours, startTime, endTime, staffel);
            Reservation weddingReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, staffel);
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
        public void AddWeddingReservation_WithMoreThen11Hours_ShouldFail()
        {

        }
        [TestMethod]
        public void AddWeddingReservation_WithWrongStartHour_ShouldFail()
        {

        }
        [TestMethod]
        public void AddNightLifeReservation_ShouldWork()
        {

        }
        [TestMethod]
        public void AddNightLifeReservation_WithMoreThen11Hours_ShouldFail()
        {

        }
        [TestMethod]
        public void AddNightLifeReservation_WithWrongStartHour_ShouldFail()
        {

        }
        [TestMethod]
        public void AddWelnessReservation_ShouldWork()
        {

        }
        [TestMethod]
        public void AddwelnessReservation_WithWrongStartHour_ShouldFail()
        {

        }
        [TestMethod]
        public void AddWelnessReservation_WithMoreThen10Hours_ShouldFail()
        {

        }
        [TestMethod]
        public void AddAirportReservation_ShouldWork()
        {

        }
        [TestMethod]
        public void AddAirportReservation_WithMoreThen11Hours_ShouldFail()
        {

        }
        [TestMethod]
        public void AddBusinessReservation_ShouldWork()
        {

        }
        [TestMethod]
        public void AddBusinessReservation_WithMoreThen11Hours_ShouldFail()
        {

        }
    }
}
