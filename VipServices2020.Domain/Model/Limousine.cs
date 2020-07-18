namespace VipServices2020.Domain.Model
{
    public class Limousine
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
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
    }
}