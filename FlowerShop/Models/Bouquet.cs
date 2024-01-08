namespace FlowerShop.Models
{
    public class Bouquet
    {
        public int BouquetId { get; set; }
        public string BouquetName { get; set; }
        public float BouquetCost { get; set; }
        public ICollection<BouquetFlower> BouquetsFlowers { get; set; }
        public ICollection<OrderBouquet> OrderBouquets { get;set; }
    }
}
