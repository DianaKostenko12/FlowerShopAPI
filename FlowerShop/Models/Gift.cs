namespace FlowerShop.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string GiftName { get; set;}
        public float GiftCost { get; set; }
        public string Description { get; set; }
        public ICollection<OrderGift> OrderGifts { get; set; } 
    }
}
