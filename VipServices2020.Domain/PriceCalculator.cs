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
            //nachtuur wordt gerekend aan 140% van de eerste-uur prijs, afgerond naar een veelvoud van € 5 (22u - 7u)
            //bereken subtotaal 
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
                { //afrond 5
                    price.OvertimePrice = (decimal)(65.0 / 100.0) * ((decimal)overtimeHours * (decimal)limousine.FirstHourPrice);
                    //number = (percentage / 100) * totalNumber;
                }
                //if nachtuur
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
            return price;
        }
        public static Price TotalPriceCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, DateTime endTime)
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
