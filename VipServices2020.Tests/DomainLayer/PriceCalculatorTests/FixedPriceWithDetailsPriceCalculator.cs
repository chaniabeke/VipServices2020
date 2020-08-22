using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;

namespace VipServices2020.Tests.DomainLayer.PriceCalculatorTests
{
    [TestClass]
    public class FixedPriceWithDetailsPriceCalculator
    {
        [TestMethod]
        public void Wedding_7TotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.FixedPriceWithDetailsPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage, ArrangementType.Wedding);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 2500);
            Assert.AreEqual(price.ExclusiveBtw, 2375);
            Assert.AreEqual(price.BtwPrice, 142.5);
            Assert.AreEqual(price.Total, 2517.5);
        }
        [TestMethod]
        public void NightLife_7TotalHours_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void Wedding_8TotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.FixedPriceWithDetailsPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage, ArrangementType.Wedding);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 3100);
            Assert.AreEqual(price.ExclusiveBtw, 2945);
            Assert.AreEqual(price.BtwPrice, 176.7);
            Assert.AreEqual(price.Total, 3121.7);
        }
        [TestMethod]
        public void NightLife_8TotalHours_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void Wedding_11TotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 23, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.FixedPriceWithDetailsPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage, ArrangementType.Wedding);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 1);
            Assert.AreEqual(price.NightHourPrice, 840);
            Assert.AreEqual(price.OvertimeCount, 2);
            Assert.AreEqual(price.OvertimePrice, 780);
            Assert.AreEqual(price.SubTotal, 4720);
            Assert.AreEqual(price.ExclusiveBtw, 4484);
            Assert.AreEqual(price.BtwPrice, 269.03999999999996);
            Assert.AreEqual(price.Total, 4753.04);
        }
        [TestMethod]
        public void NightLife_11TotalHours_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void Wedding_Start7hEnd18h_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void Wedding_Start15hEnd2h_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void NightLife_Start20hEnd6h_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void NightLife_Start24hEnd11h_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void With5ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Assert.Fail();
        }
        [TestMethod]
        public void With15ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Assert.Fail();
        }
    }
}
