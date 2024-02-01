using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IBouquetRepository
    {
        IEnumerable<Bouquet> GetBouquets();
        Bouquet GetBouquet(int id);
        ICollection<Bouquet> GetBouquetsByFlower(int flowerId);
        bool BouquetExists(int id);
        ICollection<Bouquet> GetBouquetsByCost(float minCost, float maxCost);
        ICollection<Bouquet> GetBouquetsByOrder(int orderId);
        bool CreateBouquet(Bouquet bouquet);
        bool Save();
    }
}
