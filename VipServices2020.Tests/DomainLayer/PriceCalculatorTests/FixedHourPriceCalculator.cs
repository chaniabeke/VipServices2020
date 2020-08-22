using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;

namespace VipServices2020.Tests.DomainLayer.PriceCalculatorTests
{
    [TestClass]
    public class FixedHourPriceCalculator
    {
        [TestMethod]
        public void Start7hEnd17h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.FixedHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.FixedPrice, 2700);
            Assert.AreEqual(price.SubTotal, 2700);
            Assert.AreEqual(price.ExclusiveBtw, 2565);
            Assert.AreEqual(price.BtwPrice, 153.9);
            Assert.AreEqual(price.Total, 2718.9);
        }
        [TestMethod]
        public void Start10hEnd20h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 20, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.FixedHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.FixedPrice, 2700);
            Assert.AreEqual(price.SubTotal, 2700);
            Assert.AreEqual(price.ExclusiveBtw, 2565);
            Assert.AreEqual(price.BtwPrice, 153.9);
            Assert.AreEqual(price.Total, 2718.9);
        }
        [TestMethod]
        public void Start12hEnd22h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 22, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.FixedHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.FixedPrice, 2700);
            Assert.AreEqual(price.SubTotal, 2700);
            Assert.AreEqual(price.ExclusiveBtw, 2565);
            Assert.AreEqual(price.BtwPrice, 153.9);
            Assert.AreEqual(price.Total, 2718.9);
        }
        [TestMethod]
        public void With0ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 22, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 0;

            Price price = PriceCalculator.FixedHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.FixedPrice, 2700);
            Assert.AreEqual(price.SubTotal, 2700);
            Assert.AreEqual(price.ExclusiveBtw, 2700);
            Assert.AreEqual(price.BtwPrice, 162);
            Assert.AreEqual(price.Total, 2862);
        }
        [TestMethod]
        public void With15ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 22, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 15;

            Price price = PriceCalculator.FixedHourPriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.FixedPrice, 2700);
            Assert.AreEqual(price.SubTotal, 2700);
            Assert.AreEqual(price.ExclusiveBtw, 2295);
            Assert.AreEqual(price.BtwPrice, 137.7);
            Assert.AreEqual(price.Total, 2432.7);
        }
    }
}
