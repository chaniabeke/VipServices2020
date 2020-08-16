using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VipServices2020.Domain.Models
{
    public class Limousine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Brand { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Model { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Color { get; set; }

        [Required]
        public int FirstHourPrice { get; set; }
        public int NightLifePrice { get; set; }
        public int WeddingPrice { get; set; }
        public int WelnessPrice { get; set; }

        public Limousine(string brand, string model, string color, int firstHourPrice, int nightLifePrice, int weddingPrice, int welnessPrice)
        {
            Brand = brand;
            Model = model;
            Color = color;
            FirstHourPrice = firstHourPrice;
            NightLifePrice = nightLifePrice;
            WeddingPrice = weddingPrice;
            WelnessPrice = welnessPrice;
        }
        public override string ToString()
        {
            return $"{Brand} {Model} {Color}";
        }
    }
}