using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;

namespace FlowerShop.Repository
{
    public class OrderGiftRepository : IOrderGiftRepository
    {
        private DataContext _context;

        public OrderGiftRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateRange(IEnumerable<OrderGift> orderGifts)
        {
            _context.AddRange(orderGifts);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
