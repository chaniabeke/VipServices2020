using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain
{
    public static class PriceCalculator
    {
        public static Price PerHourPriceCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime)
        {
            Price price = new Price();
            //eerste uur is volledige prijs
            price.FirstHourPrice = limousine.FirstHourPrice;

            //tweede uurprijs 65% van de eerste-uur prijs, afgerond naar een veelvoud van € 5
            price.SecondHourCount = totalHours.Hours - 1;
            price.SecondHourPrice = Math.Round(((decimal)(65.0 / 100.0) * ((decimal)price.SecondHourCount * (decimal)limousine.FirstHourPrice)) / 5) * 5;
            //nachtuur wordt gerekend aan 140% van de eerste-uur prijs, afgerond naar een veelvoud van € 5 (22u - 7u)??
            //bereken subtotaal 
            price.SubTotal = price.FirstHourPrice + price.SecondHourPrice + price.SecondHourPrice + price.NightHourPrice;
            return price;

        }
        public static Price WeddingPriceCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime)
        {
            Price price = new Price();
            //overuur berekent als eerste uur, tweede uur prijs,... (na 7 u huren)
            price.FixedPrice = limousine.WeddingPrice;
            int overtimeHours = totalHours.Hours - 7;
            if (overtimeHours > 0)
            {
                price.OvertimeCount = overtimeHours;
                price.FirstHourPrice = limousine.FirstHourPrice;
                overtimeHours--;
                if (overtimeHours > 1)
                { 
                    price.OvertimePrice = Math.Round(((decimal)(65.0 / 100.0) * ((decimal)overtimeHours * (decimal)limousine.FirstHourPrice))/5)*5;
                }
                //if nachtuur??
            }
            price.SubTotal = price.FixedPrice + price.FirstHourPrice + price.OvertimePrice;
            return price;
        }
        public static Price WelnessCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime)
        {
            Price price = new Price();
            //Fixed price 
            price.FixedPrice = limousine.WelnessPrice;
            price.SubTotal = price.FixedPrice;
            return price;
        }
        public static Price NightLifeCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime)
        {
            Price price = new Price();
            //overuur =  nachtuur , wordt gerekend aan 140% van de eerste-uur prijs, afgerond naar een veelvoud van € 5 (na 7 u huren)
            price.FixedPrice = limousine.NightLifePrice;
            int overtimeHours = totalHours.Hours - 7;
            if (overtimeHours > 0)
            {
                price.NightHourCount = overtimeHours;
                price.FirstHourPrice = limousine.FirstHourPrice;
                overtimeHours--;
                if (overtimeHours > 1)
                {
                    price.NightHourPrice = Math.Round(((decimal)(140.0 / 100.0) * ((decimal)overtimeHours * (decimal)limousine.FirstHourPrice)) / 5) * 5;
                }
            }
            price.SubTotal = price.FixedPrice + price.FirstHourPrice + price.OvertimePrice;
            return price;
        }
        private static Price TotalPriceCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime)
        {
            Price price = new Price();
            //price.SubTotal, price.ExclusiveBtw;
            //bereken staffelkorting op btw
            //bereken btw-bedrag
            //bereken totaal prijs
            return price;
        }
    }
}
