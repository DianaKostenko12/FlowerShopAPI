namespace FlowerShop.Models
{
    public class Flower
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public int FlowerCount { get; set; }
        public float FlowerCost { get; set; }
        public ICollection<BouquetFlower> BouquetsFlowers { get; set; }
    }
}
