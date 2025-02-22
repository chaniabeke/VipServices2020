﻿using FluentAssertions;
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

namespace VipServices2020.Tests.DomainLayer.Manager.ReservationTests
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
           
            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            Limousine limousine = limousineRepo.Find(1);

            double discountPercentage = m.CalculateStaffel(customer);
            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation weddingReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price);

            m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
               startTime, endTime, limousine);

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

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);

            Limousine limousine1 = limousineRepo.Find(1);
            m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
               startTime, endTime, limousine1);
            Limousine limousine2 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
             new DateTime(2020, 09, 09, 1, 0, 0), new DateTime(2020, 09, 09, 9, 0, 0), limousine2);
            Limousine limousine3 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
           new DateTime(2020, 09, 30, 1, 0, 0), new DateTime(2020, 09, 30, 9, 0, 0), limousine3);

            Action act = () =>
            {
                m.GetAllReservations();
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(3, contextTest.Reservations.Local.Count);
            Assert.AreEqual(m.GetAllReservations().Count, 3);
        }
        [TestMethod]
        public void GetAllReservations_CustomerId_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Customer customer2 = new Customer("Piet", "", addressCustomer, CategoryType.concertpromotor);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 8, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);

            //2 reservaties door dezelfde klant
            Limousine limousine1 = limousineRepo.Find(1);
            m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
               startTime, endTime, limousine1);
            Limousine limousine2 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
             new DateTime(2020, 09, 09, 1, 0, 0), new DateTime(2020, 09, 09, 9, 0, 0), limousine2);
            Limousine limousine3 = limousineRepo.Find(1);
            //1 reservatie door een andere klant
            m.AddBusinessReservation(customer2, limousineExceptedAddress, locationStart, locationArrival,
           new DateTime(2020, 09, 30, 1, 0, 0), new DateTime(2020, 09, 30, 9, 0, 0), limousine3);

            Action act = () =>
            {
                m.GetAllReservations(customer.CustomerNumber);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(3, contextTest.Reservations.Local.Count);
            Assert.AreEqual(m.GetAllReservations(customer.CustomerNumber).Count, 2);
        }
        [TestMethod]
        public void GetAllReservations_Date_ShouldWork()
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

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);

            Limousine limousine1 = limousineRepo.Find(1);
            m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
               startTime, endTime, limousine1);
            Limousine limousine2 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
             new DateTime(2020, 09, 09, 1, 0, 0), new DateTime(2020, 09, 09, 9, 0, 0), limousine2);
            Limousine limousine3 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
           new DateTime(2020, 09, 30, 1, 0, 0), new DateTime(2020, 09, 30, 9, 0, 0), limousine3);

            Action act = () =>
            {
                m.GetAllReservations(startTime);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(3, contextTest.Reservations.Local.Count);
            Assert.AreEqual(m.GetAllReservations(startTime).Count, 1);
        }
        [TestMethod]
        public void GetAllReservations_CustomerIdAndDate_ShouldWork()
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

            m.AddLimousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);

            Limousine limousine1 = limousineRepo.Find(1);
            m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
               startTime, endTime, limousine1);
            Limousine limousine2 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
             new DateTime(2020, 09, 09, 1, 0, 0), new DateTime(2020, 09, 09, 9, 0, 0), limousine2);
            Limousine limousine3 = limousineRepo.Find(1);
            m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
           new DateTime(2020, 09, 30, 1, 0, 0), new DateTime(2020, 09, 30, 9, 0, 0), limousine3);

            Action act = () =>
            {
                m.GetAllReservations(customer.CustomerNumber, startTime);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(3, contextTest.Reservations.Local.Count);
            Assert.AreEqual(m.GetAllReservations(customer.CustomerNumber, startTime).Count, 1);
        }
    }
}
