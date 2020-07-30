using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipServices2020.Domain.Model {
    public class Price {
        //aantallen uren met soort (eerste uur, nachtuur, overuur) eenheidsprijs of vaste prijs ..
        [Key]
        public int Id { get; set; }
        public int FirstHourPrice { get; set; } = 0;
        public int SecondHourPrice { get; set; } = 0;
        public int NightHourPrice { get; set; } = 0;
        public int OvertimePrice { get; set; } = 0;
        public int FixedPrice { get; set; } = 0;
        [Required]
        public decimal SubTotal { get; set; }
        //public StaffelDiscount StaffelDiscount { get; set; }
        [Required]
        public decimal ExclusiveBtw { get; set; }
        [NotMapped]
        public readonly int Btw = 6;
        [Required]
        public decimal InclusiveBtw { get; set; }
        [Required]
        public decimal Total { get; set; }
    }
}