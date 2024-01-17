using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IGiftRepository
    {
        ICollection<Gift> GetGifts();
        Gift GetGift(int id);
        ICollection<Gift> GetGiftsByCost(float minCost, float maxCost);
        ICollection<Gift> GetGiftsByOrder(int orderId);
        bool GiftExists(int id);
        bool CreateGift(Gift gift);
        bool Save();
    }
}
