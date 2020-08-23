using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;

namespace VipServices2020.Tests.DomainLayer.PriceCalculatorTests
{
    [TestClass]
    public class NightLifePriceCalculator
    {

        [TestMethod]
        public void SevenTotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 3, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 1500);
            Assert.AreEqual(price.ExclusiveBtw, 1425);
            Assert.AreEqual(price.BtwPrice, 85.5);
            Assert.AreEqual(price.Total, 1510.5);
        }
        [TestMethod]
        public void EightTotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 4, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 1);
            Assert.AreEqual(price.NightHourPrice, 840);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 2340);
            Assert.AreEqual(price.ExclusiveBtw, 2223);
            Assert.AreEqual(price.BtwPrice, 133.38);
            Assert.AreEqual(price.Total, 2356.38);
        }
        [TestMethod]
        public void ElevenTotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 22, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 9, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 4);
            Assert.AreEqual(price.NightHourPrice, 3360);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 4860);
            Assert.AreEqual(price.ExclusiveBtw, 4617);
            Assert.AreEqual(price.BtwPrice, 277.02);
            Assert.AreEqual(price.Total, 4894.02);
        }
        [TestMethod]
        public void Start20hEnd6h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 6, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 3);
            Assert.AreEqual(price.NightHourPrice, 2520);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 4020);
            Assert.AreEqual(price.ExclusiveBtw, 3819);
            Assert.AreEqual(price.BtwPrice, 229.14);
            Assert.AreEqual(price.Total, 4048.14);
        }
        [TestMethod]
        public void Start24hEnd11h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 23, 0, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 11, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 4);
            Assert.AreEqual(price.NightHourPrice, 3360);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 4860);
            Assert.AreEqual(price.ExclusiveBtw, 4617);
            Assert.AreEqual(price.BtwPrice, 277.02);
            Assert.AreEqual(price.Total, 4894.02);
        }
        [TestMethod]
        public void With0ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 3, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 0;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 1500);
            Assert.AreEqual(price.ExclusiveBtw, 1500);
            Assert.AreEqual(price.BtwPrice, 90);
            Assert.AreEqual(price.Total, 1590);
        }
        [TestMethod]
        public void With15ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 20, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 3, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 15;

            Price price = PriceCalculator.NightlifePriceCalculator(limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 1500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 1500);
            Assert.AreEqual(price.ExclusiveBtw, 1275);
            Assert.AreEqual(price.BtwPrice, 76.5);
            Assert.AreEqual(price.Total, 1351.5);
        }
    }
}
