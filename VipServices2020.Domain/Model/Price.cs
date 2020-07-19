namespace VipServices2020.Domain.Model {
    public class Price {
        //aantallen uren met soort (eerste uur, nachtuur, overuur) eenheidsprijs of vaste prijs ..
        public int Id { get; set; }
        public int FirstHourPrice { get; set; } = 0;
        public int SecondHourPrice { get; set; } = 0;
        public int NightHourPrice { get; set; } = 0;
        public int OvertimePrice { get; set; } = 0;
        public int FixedPrice { get; set; } = 0;
        public decimal SubTotal { get; set; }
        public StaffelDiscount StaffelDiscount { get; set; }
        public decimal ExclusiveBtw { get; set; }
        public readonly int Btw = 6;
        public decimal InclusiveBtw { get; set; }
        public decimal Total { get; set; }
    }
}