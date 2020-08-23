using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;

namespace VipServices2020.Tests.DomainLayer.PriceCalculatorTests
{
    [TestClass]
    public class PerHourPriceCalculator
    {
        [TestMethod]
        public void Start7hEnd17h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.PerHourPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 0);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 9);
            Assert.AreEqual(price.SecondHourPrice, 3510);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 4110);
            Assert.AreEqual(price.ExclusiveBtw, 3904.5);
            Assert.AreEqual(price.BtwPrice, 234.26999999999998);
            Assert.AreEqual(price.Total, 4138.77);
        }
        [TestMethod]
        public void Start20hEnd4h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 4, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.PerHourPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 0);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 6);
            Assert.AreEqual(price.NightHourPrice, 5040);
            Assert.AreEqual(price.SecondHourCount, 1);
            Assert.AreEqual(price.SecondHourPrice, 390);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 6030);
            Assert.AreEqual(price.ExclusiveBtw, 5728.5);
            Assert.AreEqual(price.BtwPrice, 343.71);
            Assert.AreEqual(price.Total, 6072.21);
        }
        [TestMethod]
        public void Start5hEnd12h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 5, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 12, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.PerHourPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 0);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 1);
            Assert.AreEqual(price.NightHourPrice, 840);
            Assert.AreEqual(price.SecondHourCount, 5);
            Assert.AreEqual(price.SecondHourPrice, 1950);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 3390);
            Assert.AreEqual(price.ExclusiveBtw, 3220.5);
            Assert.AreEqual(price.BtwPrice, 193.23);
            Assert.AreEqual(price.Total, 3413.73);
        }
        [TestMethod]
        public void Start12hEnd13h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 13, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.PerHourPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 0);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 600);
            Assert.AreEqual(price.ExclusiveBtw, 570);
            Assert.AreEqual(price.BtwPrice, 34.199999999999996);
            Assert.AreEqual(price.Total, 604.2);
        }
        [TestMethod]
        public void With0ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 0;

            Price price = PriceCalculator.PerHourPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 0);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 9);
            Assert.AreEqual(price.SecondHourPrice, 3510);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 4110);
            Assert.AreEqual(price.ExclusiveBtw, 4110);
            Assert.AreEqual(price.BtwPrice, 246.6);
            Assert.AreEqual(price.Total, 4356.6);
        }
        [TestMethod]
        public void With15ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 15;

            Price price = PriceCalculator.PerHourPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 0);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 9);
            Assert.AreEqual(price.SecondHourPrice, 3510);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 4110);
            Assert.AreEqual(price.ExclusiveBtw, 3493.5);
            Assert.AreEqual(price.BtwPrice, 209.60999999999999);
            Assert.AreEqual(price.Total, 3703.11);
        }
    }
}
