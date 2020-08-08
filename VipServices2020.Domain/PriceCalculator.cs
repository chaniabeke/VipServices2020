using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Models;

namespace VipServices2020.Domain
{
    public static class PriceCalculator
    {
        public static Price PerHourPriceCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime, Staffel staffel)
        {
            Price price = new Price();
            price.FirstHourPrice = limousine.FirstHourPrice;
            TimeSpan oneHour = new TimeSpan(1, 0, 0);
            totalHours = totalHours - oneHour;

            price.NightHourCount = CalculateNightHours(totalHours, startTime, endTime);
            price.NightHourPrice = Math.Round(((decimal)(price.NightHourPercentage / 100.0) * ((decimal)price.NightHourCount * (decimal)limousine.FirstHourPrice)) / 5) * 5;

            price.SecondHourCount = totalHours.Hours - price.NightHourCount;
            price.SecondHourPrice = Math.Round(((decimal)(price.SecondHourPercentage / 100.0) * ((decimal)price.SecondHourCount * (decimal)limousine.FirstHourPrice)) / 5) * 5;

            price.SubTotal = price.FirstHourPrice + price.SecondHourPrice + price.SecondHourPrice + price.NightHourPrice;

            TotalPriceCalculator(price, staffel);

            return price;
        }
        public static Price WeddingPriceCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime, Staffel staffel)
        {
            Price price = new Price();
            price.FixedPrice = limousine.WeddingPrice;
            TimeSpan extraHours = new TimeSpan((int)totalHours.TotalHours - 7, 0, 0);

            price.NightHourCount = CalculateNightHours(extraHours, startTime, endTime);
            price.NightHourPrice = Math.Round(((decimal)(price.NightHourPercentage / 100.0) * ((decimal)price.NightHourCount * (decimal)limousine.FirstHourPrice)) / 5) * 5;

            price.OvertimeCount = totalHours.Hours - price.NightHourCount;
            price.OvertimePrice = Math.Round(((decimal)(price.SecondHourPercentage / 100.0) * ((decimal)price.OvertimeCount * (decimal)limousine.FirstHourPrice)) / 5) * 5;

            price.SubTotal = price.FixedPrice + price.FirstHourPrice + price.OvertimePrice;

            TotalPriceCalculator(price, staffel);

            return price;
        }
        public static Price WelnessCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime, Staffel staffel)
        {
            Price price = new Price();
            price.FixedPrice = limousine.WelnessPrice;
            price.SubTotal = price.FixedPrice;

            TotalPriceCalculator(price, staffel);

            return price;
        }
        public static Price NightLifeCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime, Staffel staffel)
        {
            Price price = new Price();
            price.FixedPrice = limousine.NightLifePrice;
            int overtimeHours = totalHours.Hours - 7;
            if (overtimeHours > 0)
            {
                price.NightHourCount = overtimeHours;
                price.FirstHourPrice = limousine.FirstHourPrice;
                price.NightHourCount--;
                if (price.NightHourCount > 1)
                {
                    price.NightHourPrice = Math.Round(((decimal)(price.NightHourPercentage / 100.0) * ((decimal)price.NightHourCount * (decimal)limousine.FirstHourPrice)) / 5) * 5;
                }
            }
            price.SubTotal = price.FixedPrice + price.FirstHourPrice + price.OvertimePrice;

            TotalPriceCalculator(price, staffel);

            return price;
        }

        private static Price TotalPriceCalculator(Price price, Staffel staffel)
        {
            price.ExclusiveBtw = (decimal)(staffel.Discount / 100.0) * price.SubTotal + price.SubTotal;
            price.BtwPrice = price.ExclusiveBtw * (decimal)(price.Btw / 100.0);
            price.Total = Math.Round(price.ExclusiveBtw + price.BtwPrice, 2);
            return price;
        }

        private static bool BetweenTime(DateTime time, DateTime startTime, DateTime endTime)
        {
            if (time.TimeOfDay == startTime.TimeOfDay) return true;
            if (time.TimeOfDay == endTime.TimeOfDay) return true;
            if (startTime.TimeOfDay <= endTime.TimeOfDay)
                return (time.TimeOfDay >= startTime.TimeOfDay && time.TimeOfDay <= endTime.TimeOfDay);
            else
                return !(time.TimeOfDay >= endTime.TimeOfDay && time.TimeOfDay <= startTime.TimeOfDay);
        }
        private static int CalculateNightHours(TimeSpan totalHours, DateTime startTime, DateTime endTime )
        {
            int dayHour = 0; int nightHour = 0;
            DateTime DayStart = new DateTime(startTime.Year, startTime.Month, startTime.Day, 7, 0, 0);
            DateTime DayEnd = new DateTime(startTime.Year, startTime.Month, startTime.Day, 21, 59, 59);
            DateTime NightStart = new DateTime(endTime.Year, endTime.Month, endTime.Day, 22, 0, 0);
            DateTime NightEnd = new DateTime(endTime.Year, endTime.Month, endTime.Day, 6, 59, 59);
            DateTime dateTime = startTime;
            for (int i = 0; i < totalHours.TotalHours; i++)
            {
                if (BetweenTime(dateTime, DayStart, DayEnd))
                {
                    dayHour++;
                }
                if (BetweenTime(dateTime, NightStart, NightEnd))
                {
                    nightHour++;
                }
                dateTime = dateTime.AddHours(1);
            }
            return nightHour;
        }
    }
}
