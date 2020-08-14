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
            CategoryType category = CategoryType.geen;

            double discountPercentage = m.CalculateStaffel(customer, startTime, category);
            Price price = PriceCalculator.WeddingPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation weddingReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wedding, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
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
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 8, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 20, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een reservatie mag niet langer zijn dan 11uur.");
        }
        [TestMethod]
        public void AddWeddingReservation_WithWrongStartHour_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 17, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 0, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Wedding reservatie moet starten tussen 07u00 en 15u00.");
        }
        [TestMethod]
        public void AddWeddingReservation_WithLessThan7TotalHours_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 12, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddWeddingReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Wedding reservatie moet minstens 7uur zijn.");
        }
        [TestMethod]
        public void AddNightLifeReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 4, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            double discountPercentage = m.CalculateStaffel(customer, startTime, category);
            Price price = PriceCalculator.NightLifeCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation nightLifeReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.NightLife, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
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
        public void AddNightLifeReservation_WithMoreThen11Hours_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 8, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een reservatie mag niet langer zijn dan 11uur.");
        }
        [TestMethod]
        public void AddNightLifeReservation_WithWrongStartHour_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 19, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 4, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een NightLife reservatie moet starten tussen 20u00 en 24u00.");
        }
        [TestMethod]
        public void AddNightLifeReservation_WithLessThan7TotalHours_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 22, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddNightLifeReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een NightLife reservatie moet minstens 7uur zijn.");
        }
        [TestMethod]
        public void AddWelnessReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            double discountPercentage = m.CalculateStaffel(customer, startTime, category);
            Price price = PriceCalculator.WelnessCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation welnessReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Wellness, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddWelnessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
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
        public void AddwelnessReservation_WithWrongStartHour_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 13, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 23, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddWelnessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Welness reservatie moet starten tussen 07u00 en 12u00.");
        }
        [TestMethod]
        public void AddWelnessReservation_WithMoreThen10Hours_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 20, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddWelnessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Welness reservatie moet altijd 10 uur zijn.");
        }
        [TestMethod]
        public void AddAirportReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            double discountPercentage = m.CalculateStaffel(customer, startTime, category);
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

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 19, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddAirportReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Airport reservatie mag niet langer zijn dan 11uur.");
        }
        [TestMethod]
        public void AddBusinessReservation_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            double discountPercentage = m.CalculateStaffel(customer, startTime, category);
            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);
            Reservation businessReservation = new Reservation(customer, DateTime.Now, limousineExceptedAddress, locationStart, locationArrival,
                ArrangementType.Business, startTime, endTime, totalHours, limousine, price);

            Action act = () =>
            {
                m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().NotThrow<DomainException>();
            Assert.AreEqual(1, contextTest.Reservations.Local.Count);
            var reservationInDb = contextTest.Reservations.First();
            Assert.AreEqual(reservationInDb.Customer, businessReservation.Customer);
            Assert.AreEqual(reservationInDb.LimousineExpectedAddress, businessReservation.LimousineExpectedAddress);
            Assert.AreEqual(reservationInDb.StartLocation, businessReservation.StartLocation);
            Assert.AreEqual(reservationInDb.ArrivalLocation, businessReservation.ArrivalLocation);
            Assert.AreEqual(reservationInDb.ArrangementType, businessReservation.ArrangementType);
            Assert.AreEqual(reservationInDb.StartTime, businessReservation.StartTime);
            Assert.AreEqual(reservationInDb.EndTime, businessReservation.EndTime);
            Assert.AreEqual(reservationInDb.TotalHours, businessReservation.TotalHours);
            Assert.AreEqual(reservationInDb.Limousine, businessReservation.Limousine);
            Assert.AreEqual(reservationInDb.Price.Total, businessReservation.Price.Total);
        }
        [TestMethod]
        public void AddBusinessReservation_WithMoreThen11Hours_ShouldFail()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address addressCustomer = new Address("Groenlaan", "17", "Herzele");
            Address limousineExceptedAddress = new Address("Nieuwstraat", "5B", "Brussel");
            Customer customer = new Customer("Jan", "", addressCustomer, CategoryType.particulier);
            Location locationStart = new Location("Gent");
            Location locationArrival = new Location("Brussel");
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 19, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            CategoryType category = CategoryType.geen;

            Action act = () =>
            {
                m.AddBusinessReservation(customer, limousineExceptedAddress, locationStart, locationArrival,
                startTime, endTime, limousine, category);
            };

            act.Should().Throw<DomainException>().WithMessage("Een Business reservatie mag niet langer zijn dan 11uur.");
        }
    }
}
