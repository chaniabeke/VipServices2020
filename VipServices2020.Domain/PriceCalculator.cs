using System;
using System.Collections.Generic;
using VipServices2020.Domain.Models;
using VipServices2020.Domain.Repositories;

namespace VipServices2020.Domain
{
    /// <summary>
    /// Deze class berekent de prijzen van de verschillende arrangementen.
    /// </summary>
    public static class PriceCalculator
    {
        /// <summary>
        /// Deze method berekent de netto prijs voor de arrangement die per uur moeten berekent worden. (Airport & Bussines)
        /// </summary>
        public static Price PerHourPriceCalculator(Limousine limousine, TimeSpan totalHours,  
            DateTime startTime, DateTime endTime, double discountPercentage)
        {
            Price price = new Price();

            //Zet de eerste uur prijs van de limo naar het prijs object
            price.FirstHourPrice = limousine.FirstHourPrice;
            //Trek 1 uur af van de totale tijd en tel 1 uur op bij het start uur voor een juiste uur berekening later in de code
            TimeSpan oneHour = new TimeSpan(1, 0, 0);
            totalHours = totalHours - oneHour;
            DateTime startTimeMinusStartHour = startTime + oneHour;

            //Bereken het aantal nachturen
            price.NightHourCount = CalculateNightHours(totalHours, startTimeMinusStartHour, endTime);
            //Bereken op basis van het aantal nachturen, de nacht prijs in totaal, afgerond op 5
            price.NightHourPrice = Math.Round(((double)(price.NightHourPercentage / 100.0) 
                * ((double)price.NightHourCount * (double)limousine.FirstHourPrice)) / 5) * 5;

            //Bereken het aantal tweede uren minus het aantal nacht uren
            price.SecondHourCount = totalHours.Hours - price.NightHourCount;
            //Bereken op basis van het aantal tweede uren, het tweede uur prijs in het totaal, afgerond op 5
            price.SecondHourPrice = Math.Round(((double)(price.SecondHourPercentage / 100.0) 
                * ((double)price.SecondHourCount * (double)limousine.FirstHourPrice)) / 5) * 5;

            //Bereken de subtotaalprijs op basis van de vorige bedragen
            price.SubTotal = price.FirstHourPrice + price.SecondHourPrice + price.NightHourPrice;

            //Stel de staffelkorting in
            price.StaffelDiscount = discountPercentage;
            //Bereken de totaalprijs in een algemene method
            TotalPriceCalculator(price);

            //Stel totaaluur terug correct in
            totalHours = totalHours + oneHour;

            return price;
        }
        /// <summary>
        /// Deze method berekent de netto prijs voor het Wedding arrangement
        /// </summary>
        public static Price WeddingPriceCalculator(Limousine limousine, TimeSpan totalHours, 
            DateTime startTime, DateTime endTime, double discountPercentage)
        {
            Price price = new Price();
            //Wedding is minstens 7uur en heeft een vaste prijs, stel de vaste prijs in voor de gekozen limo
            price.FixedPrice = limousine.WeddingPrice;
            
            //Indien het totale uur groter is dan 7, stel de eerste uur prijs in
            if (totalHours.Hours > 7)
            { 
                price.FirstHourPrice = limousine.FirstHourPrice;

                //Indien het totale uur groter is dan 8, bereken de nachtprijs en de overuren
                if (totalHours.Hours > 8)
                {
                    //Trek 8 uur af van de totale tijd en tel 8 uur op bij het start uur voor een juiste uur berekening later in de code
                    TimeSpan eightHours = new TimeSpan(8, 0, 0);
                    totalHours = totalHours - eightHours;
                    DateTime startTimeMinusStartHour = startTime + eightHours;

                    //Bereken het aantal nachturen
                    price.NightHourCount = CalculateNightHours(totalHours, startTimeMinusStartHour, endTime);
                    //Bereken op basis van het aantal nachturen, de nacht prijs in totaal, afgerond op 5
                    price.NightHourPrice = Math.Round(((double)(price.NightHourPercentage / 100.0) 
                        * ((double)price.NightHourCount * (double)limousine.FirstHourPrice)) / 5) * 5;

                    //Bereken het aantal over uren  uren minus het aantal nacht uren
                    price.OvertimeCount = totalHours.Hours - price.NightHourCount;
                    //Bereken op basis van het aantal ove uren, de overuur prijs in het totaal, afgerond op 5
                    price.OvertimePrice = Math.Round(((double)(price.SecondHourPercentage / 100.0) 
                        * ((double)price.OvertimeCount * (double)limousine.FirstHourPrice)) / 5) * 5;

                    //Stel totaaluur terug correct in
                    totalHours = totalHours + eightHours;
                }
            }

            //Bereken de subtotaalprijs op basis van de vorige bedragen
            price.SubTotal = price.FixedPrice + price.FirstHourPrice + price.NightHourPrice + price.OvertimePrice;

            //Stel de staffelkorting in
            price.StaffelDiscount = discountPercentage;
            //Bereken de totaalprijs in een algemene method
            TotalPriceCalculator(price);

            return price;
        }
        /// <summary>
        /// Deze method berekent de netto prijs voor het Welnees arrangement
        /// </summary>
        public static Price WelnessCalculator(Limousine limousine, TimeSpan totalHours, DateTime startTime, 
            DateTime endTime, double discountPercentage)
        {
            Price price = new Price();
            //Welness is een arrangement met een vaste 10 uur, stel de welness prijs in als subtotaal want het kan geen nachturen ect,.. hebben
            price.FixedPrice = limousine.WelnessPrice;
            price.SubTotal = price.FixedPrice;

            //Stel de staffelkorting in
            price.StaffelDiscount = discountPercentage;
            //Bereken de totaalprijs in een algemene method
            TotalPriceCalculator(price);

            return price;
        }
        public static Price NightLifeCalculator(Limousine limousine, TimeSpan totalHours, 
            DateTime startTime, DateTime endTime, double discountPercentage)
        {
            Price price = new Price();
            price.FixedPrice = limousine.NightLifePrice;

            if (totalHours.Hours > 7)
            {
                price.FirstHourPrice = limousine.FirstHourPrice;

                if (totalHours.Hours > 8)
                {
                    TimeSpan eightHours = new TimeSpan(8, 0, 0);
                    totalHours = totalHours - eightHours;
                    DateTime startTimeMinusStartHour = startTime + eightHours;

                    price.NightHourCount = CalculateNightHours(totalHours, startTimeMinusStartHour, endTime);
                    price.NightHourPrice = Math.Round(((double)(price.NightHourPercentage / 100.0) 
                        * ((double)price.NightHourCount * (double)limousine.FirstHourPrice)) / 5) * 5;

                    price.OvertimeCount = totalHours.Hours - price.NightHourCount;
                    price.OvertimePrice = Math.Round(((double)(price.SecondHourPercentage / 100.0) 
                        * ((double)price.OvertimeCount * (double)limousine.FirstHourPrice)) / 5) * 5;

                    totalHours = totalHours + eightHours;
                }
            }

            price.SubTotal = price.FixedPrice + price.FirstHourPrice + price.NightHourPrice + price.OvertimePrice;

            price.StaffelDiscount = discountPercentage;
            TotalPriceCalculator(price);

            return price;
        }

        private static Price TotalPriceCalculator(Price price)
        {
            price.ExclusiveBtw = price.SubTotal - (price.SubTotal * (double)(price.StaffelDiscount / 100.0));
            price.BtwPrice = price.ExclusiveBtw * (double)(price.Btw / 100.0);
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
