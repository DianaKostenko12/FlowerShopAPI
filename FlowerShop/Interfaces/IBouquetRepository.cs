using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IBouquetRepository
    {
        ICollection<Bouquet> GetBouquets();
        Bouquet GetBouquet(int id);
        ICollection<Bouquet> GetBouquetsByFlower(int flowerId);
        bool BouquetExists(int id);
        Bouquet GetBouquetByCost(int costId);
    }
}
