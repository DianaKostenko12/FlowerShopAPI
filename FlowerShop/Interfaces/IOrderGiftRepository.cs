using FlowerShop.Models;

namespace FlowerShop.Interfaces
{
    public interface IOrderGiftRepository
    {
        void CreateRange(IEnumerable<OrderGift> orderGifts);
        bool Save();
    }
}
