namespace FlowerShop.Models
{
    public class OrderGift
    {
        public int GiftId { get; set; }
        public int OrderId { get; set; }
        public int GiftCount { get; set; }
        public Gift Gift { get; set; }
        public Order Order { get; set; }
    }
}
