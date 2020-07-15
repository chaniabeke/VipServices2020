namespace VipServices2020.Domain.Model {
    public class Price {
        //aantallen uren met soort (eerste uur, nachtuur, overuur) eenheidsprijs
        public decimal SubTotal { get; set; }
        //aangerekendekortingen staffeldiscount 
        public decimal ExclusiveBtw { get; set; }
        public int Btw { get; set; }
        public decimal InclusiveBtw { get; set; }
        public decimal Total { get; set; }
    }
}