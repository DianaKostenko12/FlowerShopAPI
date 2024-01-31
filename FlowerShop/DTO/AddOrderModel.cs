using FlowerShop.Models;

namespace FlowerShop.DTO
{
    public class AddOrderModel
    {
        public ICollection<(int id, int count)> Bouquets { get; set; }
        public ICollection<(int id, int count)> Gifts { get; set; }
    }
}
