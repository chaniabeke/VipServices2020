using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;
using VipServices2020.EF;
using VipServices2020.Tests.EFLayer;

namespace VipServices2020.Tests.DomainLayer
{
    [TestClass]
    public class PriceCalculatorTest
    {
        [TestMethod]
        public void PerHourPriceCalculator_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 14, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 1, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.PerHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 3);
            Assert.AreEqual(price.NightHourPrice, 2520);
            Assert.AreEqual(price.SecondHourCount, 7);
            Assert.AreEqual(price.SecondHourPrice, 2730);
            Assert.AreEqual(price.SubTotal, 5850);
            Assert.AreEqual(price.ExclusiveBtw, 5557.5);
            Assert.AreEqual(price.BtwPrice, 333.45);
            Assert.AreEqual(price.Total, 5890.95);
        }
        [TestMethod]
        public void WeddingPriceCalculator_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 14, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 1, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WeddingPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 3);
            Assert.AreEqual(price.NightHourPrice, 2520);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 5620);
            Assert.AreEqual(price.ExclusiveBtw, 5339);
            Assert.AreEqual(price.BtwPrice, 320.34);
            Assert.AreEqual(price.Total, 5659.34);
        }
        [TestMethod]
        public void WelnessCalculator_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 8, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WelnessCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2700);
            Assert.AreEqual(price.SubTotal, 2700);
            Assert.AreEqual(price.ExclusiveBtw, 2565);
            Assert.AreEqual(price.BtwPrice, 153.9);
            Assert.AreEqual(price.Total, 2718.9);
        }
        [TestMethod]
        public void NightLifeCalculator_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 21, 22, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 9, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.NightLifeCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 1);
            Assert.AreEqual(price.NightHourPrice, 840);
            Assert.AreEqual(price.OvertimeCount, 2);
            Assert.AreEqual(price.OvertimePrice, 780);
            Assert.AreEqual(price.SubTotal, 3720);
            Assert.AreEqual(price.ExclusiveBtw, 3534);
            Assert.AreEqual(price.BtwPrice, 212.04);
            Assert.AreEqual(price.Total, 3746.04);
        }
    }
}
