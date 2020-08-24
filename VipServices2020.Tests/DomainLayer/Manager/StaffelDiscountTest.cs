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
    public class StaffelDiscountTest
    {
        [TestInitialize]
        public void StaffelDiscountTestInitialize()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            CategoryType vip = CategoryType.vip;
            StaffelDiscount staffel1 = new StaffelDiscount(2, 5, vip);
            StaffelDiscount staffel2 = new StaffelDiscount(7, 7.5, vip);
            StaffelDiscount staffel3 = new StaffelDiscount(15, 10, vip);
            m.AddStaffel(staffel1);
            m.AddStaffel(staffel2);
            m.AddStaffel(staffel3);

            CategoryType planner = CategoryType.huwelijksplanner;
            StaffelDiscount staffel4 = new StaffelDiscount(5, 7.5, planner);
            StaffelDiscount staffel5 = new StaffelDiscount(10, 10, planner);
            StaffelDiscount staffel6 = new StaffelDiscount(15, 12.5, planner);
            StaffelDiscount staffel7 = new StaffelDiscount(20, 15, planner);
            StaffelDiscount staffel8 = new StaffelDiscount(25, 25, planner);
            m.AddStaffel(staffel4);
            m.AddStaffel(staffel5);
            m.AddStaffel(staffel6);
            m.AddStaffel(staffel7);
            m.AddStaffel(staffel8);
        }
        [TestMethod]
        public void AddStaffel_ShouldWork()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            StaffelDiscount staffelDiscount = new StaffelDiscount(5, 5, CategoryType.concertpromotor);

            Action act = () =>
            {
                m.AddStaffel(staffelDiscount);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(1, contextTest.StaffelDiscounts.Local.Count);
            var staffelInDB = contextTest.StaffelDiscounts.First();
            Assert.AreEqual(staffelInDB.Category,CategoryType.concertpromotor);
            Assert.AreEqual(staffelInDB.NumberOfBookedReservations, 5);
            Assert.AreEqual(staffelInDB.DiscountPercentage, 5);
        }
        [TestMethod]
        public void CalculateStaffel_None_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.geen);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 0);
        }
        [TestMethod]
        public void CalculateStaffel_Vip_0Res_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            StaffelDiscountTestInitialize();

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.vip);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 0);
        }
        [TestMethod]
        public void CalculateStaffel_Vip_2Res_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            StaffelDiscountTestInitialize();

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.vip);
            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            Limousine limousine = limousineRepo.Find(1);

            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 22, 10, 0, 0), new DateTime(2020, 09, 22, 20, 0, 0), limousine);
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 23, 10, 0, 0), new DateTime(2020, 09, 23, 20, 0, 0), limousine);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 5);
        }
        [TestMethod]
        public void CalculateStaffel_Vip_15Res_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            StaffelDiscountTestInitialize();

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.vip);
            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            Limousine limousine = limousineRepo.Find(1);

            //1
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 22, 10, 0, 0), new DateTime(2020, 09, 22, 20, 0, 0), limousine);
            //2
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 23, 10, 0, 0), new DateTime(2020, 09, 23, 20, 0, 0), limousine);
            //3
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 24, 10, 0, 0), new DateTime(2020, 09, 24, 20, 0, 0), limousine);
            //4
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 25, 10, 0, 0), new DateTime(2020, 09, 25, 20, 0, 0), limousine);
            //5
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 26, 10, 0, 0), new DateTime(2020, 09, 26, 20, 0, 0), limousine);
            //6
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 27, 10, 0, 0), new DateTime(2020, 09, 27, 20, 0, 0), limousine);
            //7
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 28, 10, 0, 0), new DateTime(2020, 09, 28, 20, 0, 0), limousine);
            //8
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 29, 10, 0, 0), new DateTime(2020, 09, 29, 20, 0, 0), limousine);
            //9
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 30, 10, 0, 0), new DateTime(2020, 09, 30, 20, 0, 0), limousine);
            //10
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 01, 10, 0, 0), new DateTime(2020, 10, 01, 20, 0, 0), limousine);
            //11
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 02, 10, 0, 0), new DateTime(2020, 10, 02, 20, 0, 0), limousine);
            //12
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 03, 10, 0, 0), new DateTime(2020, 10, 03, 20, 0, 0), limousine);
            //13
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 04, 10, 0, 0), new DateTime(2020, 10, 04, 20, 0, 0), limousine);
            //14
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 05, 10, 0, 0), new DateTime(2020, 10, 05, 20, 0, 0), limousine);
            //15
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 06, 10, 0, 0), new DateTime(2020, 10, 06, 20, 0, 0), limousine);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 10);
        }
        [TestMethod]
        public void CalculateStaffel_Vip_20Res_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            StaffelDiscountTestInitialize();

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.vip);
            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            Limousine limousine = limousineRepo.Find(1);

            //1
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 22, 10, 0, 0), new DateTime(2020, 09, 22, 20, 0, 0), limousine);
            //2
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 23, 10, 0, 0), new DateTime(2020, 09, 23, 20, 0, 0), limousine);
            //3
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 24, 10, 0, 0), new DateTime(2020, 09, 24, 20, 0, 0), limousine);
            //4
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 25, 10, 0, 0), new DateTime(2020, 09, 25, 20, 0, 0), limousine);
            //5
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 26, 10, 0, 0), new DateTime(2020, 09, 26, 20, 0, 0), limousine);
            //6
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 27, 10, 0, 0), new DateTime(2020, 09, 27, 20, 0, 0), limousine);
            //7
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 28, 10, 0, 0), new DateTime(2020, 09, 28, 20, 0, 0), limousine);
            //8
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 29, 10, 0, 0), new DateTime(2020, 09, 29, 20, 0, 0), limousine);
            //9
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 30, 10, 0, 0), new DateTime(2020, 09, 30, 20, 0, 0), limousine);
            //10
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 01, 10, 0, 0), new DateTime(2020, 10, 01, 20, 0, 0), limousine);
            //11
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 02, 10, 0, 0), new DateTime(2020, 10, 02, 20, 0, 0), limousine);
            //12
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 03, 10, 0, 0), new DateTime(2020, 10, 03, 20, 0, 0), limousine);
            //13
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 04, 10, 0, 0), new DateTime(2020, 10, 04, 20, 0, 0), limousine);
            //14
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 05, 10, 0, 0), new DateTime(2020, 10, 05, 20, 0, 0), limousine);
            //15
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 06, 10, 0, 0), new DateTime(2020, 10, 06, 20, 0, 0), limousine);
            //16
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 07, 10, 0, 0), new DateTime(2020, 10, 07, 20, 0, 0), limousine);
            //17
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 08, 10, 0, 0), new DateTime(2020, 10, 08, 20, 0, 0), limousine);
            //18
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 09, 10, 0, 0), new DateTime(2020, 10, 09, 20, 0, 0), limousine);
            //19
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 10, 10, 0, 0), new DateTime(2020, 10, 10, 20, 0, 0), limousine);
            //20
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 10, 11, 10, 0, 0), new DateTime(2020, 10, 11, 20, 0, 0), limousine);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 10);
        }
        [TestMethod]
        public void CalculateStaffel_Planner_0Res_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));

            StaffelDiscountTestInitialize();

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.huwelijksplanner);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 0);
        }
        [TestMethod]
        public void CalculateStaffel_Planner_5Res_ShouldBeCorrect()
        {
            VipServicesContextTest contextTest = new VipServicesContextTest(keepExistingDB: false);
            VipServicesManager m = new VipServicesManager(new UnitOfWork(contextTest));
            LimousineRepository limousineRepo = new LimousineRepository(contextTest);

            StaffelDiscountTestInitialize();

            Address address = new Address("Groenlaan", "17", "Herzele");
            Customer customer = new Customer("Jan", "BE0502358347", address, CategoryType.vip);
            m.AddLimousine("Chrysler", "300C Limousine", "White", 175, 800, 500, 1000);
            Limousine limousine = limousineRepo.Find(1);

            //1
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 22, 10, 0, 0), new DateTime(2020, 09, 22, 20, 0, 0), limousine);
            //2
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
                new DateTime(2020, 09, 23, 10, 0, 0), new DateTime(2020, 09, 23, 20, 0, 0), limousine);
            //3
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 24, 10, 0, 0), new DateTime(2020, 09, 24, 20, 0, 0), limousine);
            //4
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 25, 10, 0, 0), new DateTime(2020, 09, 25, 20, 0, 0), limousine);
            //5
            m.AddWeddingReservation(customer, address, new Location("Gent"), new Location("Brussel"),
               new DateTime(2020, 09, 26, 10, 0, 0), new DateTime(2020, 09, 26, 20, 0, 0), limousine);

            Action act = () =>
            {
                m.CalculateStaffel(customer);
            };

            act.Should().NotThrow<Exception>();
            Assert.AreEqual(m.CalculateStaffel(customer), 7.5);
        }
    }
}
