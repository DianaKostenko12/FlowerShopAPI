using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IOrderBouquetRepository
    {
        void CreateRange(IEnumerable<OrderBouquet> orderBouquets);
        bool Save();
    }
}
