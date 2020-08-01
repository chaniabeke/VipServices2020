using System;
using System.Collections.Generic;
using System.Text;
using VipServices2020.Domain.Model;

namespace VipServices2020.Domain
{
    public static class PriceCalculator
    {
        public static void PerHourPriceCalculator(Reservation reservation, Price price)
        {
            //eerste uur is volledige prijs
            //tweede uurprijs 65% van de eerste-uur prijs, afgerond naar een veelvoud van € 5
            //nachtuur wordt gerekend aan 140% van de eerste-uur prijs, afgerond naar een veelvoud van € 5
            //bereken subtotaal 

        }
        public static void WeddingPriceCalculator(Reservation reservation, Price price)
        {
           //overuur berekent als eerste uur, tweede uur prijs,...
        }
        public static void WelnessCalculator(Reservation reservation, Price price)
        {
            //Fixed price 
        }
        public static void NightLifeCalculator(Reservation reservation, Price price)
        {
            //overuur berekent als eerste uur, tweede uur prijs,...
            //overuur =  nachtuur , wordt gerekend aan 140% van de eerste-uur prijs, afgerond naar een veelvoud van € 5
        }
        public static void TotalPriceCalculator(Reservation reservation, Price price)
        {
            //price.SubTotal, price.ExclusiveBtw;
            //bereken staffelkorting op btw
            //bereken btw-bedrag
            //bereken totaal prijs
        }
    }
}
