using FlowerShop.Models;

namespace FlowerShop.DTO
{
    public class AddOrderModel
    {
        public IEnumerable<Item> Bouquets { get; set; }
        public IEnumerable<Item> Gifts { get; set; }
    }
    public class Item
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
