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
    public class ReservationTest
    {
        [TestMethod]
        public void GetReservation_ShouldWork()
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
            CategoryType category = CategoryType.geen;

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            double discountPercentage = m.CalculateStaffel(customer, startTime, category);
            Price price = PriceCalculator.WeddingPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation weddingReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price);

            m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
               startTime, endTime, limousine, category);

            Action act = () =>
            {
                m.GetReservation(1);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            var reservationInDb = m.GetReservation(1);
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
        public void GetAllReservations_ShouldWork()
        {
        }
        [TestMethod]
        public void GetAllReservations_ShouldWork(int customerId)
        {
           
        }
        [TestMethod]
        public void GetAllReservations_ShouldWork(DateTime date)
        {
           
        }
        [TestMethod]
        public void GetAllReservations_ShouldWork(int customerId, DateTime date)
        {
            
        }
    }
}
