namespace FlowerShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderSum { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderBouquet> OrderBouquets { get; set; }
        public ICollection<OrderGift> OrderGifts { get; set; }
        public Client Client { get; set; }
    }
}
