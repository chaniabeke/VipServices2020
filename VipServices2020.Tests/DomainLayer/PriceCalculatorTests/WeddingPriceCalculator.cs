using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain;
using VipServices2020.Domain.Models;

namespace VipServices2020.Tests.DomainLayer.PriceCalculatorTests
{
    [TestClass]
    public class WeddingPriceCalculator
    {
        [TestMethod]
        public void SevenTotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
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
        public void EightTotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
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
        public void ElevenTotalHours_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 12, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 23, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
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
        public void Start7hEnd18h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 7, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 18, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 3);
            Assert.AreEqual(price.OvertimePrice, 1170);
            Assert.AreEqual(price.SubTotal, 4270);
            Assert.AreEqual(price.ExclusiveBtw, 4056.5);
            Assert.AreEqual(price.BtwPrice, 243.39);
            Assert.AreEqual(price.Total, 4299.89);
        }
        [TestMethod]
        public void Start15hEnd2h_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 15, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 23, 2, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 5;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 600);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
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
        public void With0ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 0;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 2500);
            Assert.AreEqual(price.ExclusiveBtw, 2500);
            Assert.AreEqual(price.BtwPrice, 150);
            Assert.AreEqual(price.Total, 2650);
        }
        [TestMethod]
        public void With15ProcentStaffelDiscount_ShouldBeCorrect()
        {
            Limousine limousine = new Limousine("Tesla", "Model X", "White", 600, 1500, 2500, 2700);
            DateTime startTime = new DateTime(2020, 09, 22, 10, 0, 0);
            DateTime endTime = new DateTime(2020, 09, 22, 17, 0, 0);
            TimeSpan totalHours = endTime - startTime;
            double discountPercentage = 15;

            Price price = PriceCalculator.WeddingPriceCalculator
                (limousine, totalHours, startTime, endTime, discountPercentage);

            Assert.AreEqual(price.FixedPrice, 2500);
            Assert.AreEqual(price.FirstHourPrice, 0);
            Assert.AreEqual(price.SecondHourCount, 0);
            Assert.AreEqual(price.SecondHourPrice, 0);
            Assert.AreEqual(price.NightHourCount, 0);
            Assert.AreEqual(price.NightHourPrice, 0);
            Assert.AreEqual(price.OvertimeCount, 0);
            Assert.AreEqual(price.OvertimePrice, 0);
            Assert.AreEqual(price.SubTotal, 2500);
            Assert.AreEqual(price.ExclusiveBtw, 2125);
            Assert.AreEqual(price.BtwPrice, 127.5);
            Assert.AreEqual(price.Total, 2252.5);
        }
    }
}
