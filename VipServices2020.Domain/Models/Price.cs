using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VipServices2020.Domain.Models {
    public class Price {
        //aantallen uren met soort (eerste uur, nachtuur, overuur) eenheidsprijs of vaste prijs ..
        [Key]
        public int Id { get; set; }
        public int FirstHourPrice { get; set; } = 0;
        public int SecondHourCount { get; set; } = 0;
        public double SecondHourPrice { get; set; } = 0;
        [NotMapped]
        public readonly int SecondHourPercentage = 65;
        public int NightHourCount { get; set; } = 0;
        public double NightHourPrice { get; set; } = 0;
        [NotMapped]
        public readonly int NightHourPercentage = 140;
        public int OvertimeCount { get; set; } = 0;
        public double OvertimePrice { get; set; } = 0;
        public int FixedPrice { get; set; } = 0;
        [Required]
        public double SubTotal { get; set; }
        [Required]
        public Staffel Staffel { get; set; }
        [Required]
        public double ExclusiveBtw { get; set; }
        [NotMapped]
        public readonly int Btw = 6;
        [Required]
        public double BtwPrice { get; set; }
        [Required]
        public double Total { get; set; }
    }
}