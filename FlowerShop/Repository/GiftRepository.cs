using AutoMapper;
using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;

namespace FlowerShop.Repository
{
    public class GiftRepository : IGiftRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public GiftRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateGift(Gift gift)
        {
            _context.Add(gift);
            return Save(); ;
        }

        public Gift GetGift(int id)
        {
            return _context.Gifts.Where(g => g.GiftId == id).FirstOrDefault();
        }

        public IEnumerable<Gift> GetGifts()
        {
            return _context.Gifts.ToList();
        }

        public ICollection<Gift> GetGiftsByCost(float minCost, float maxCost)
        {
            return _context.Gifts.Where(g => g.GiftCost >= minCost && g.GiftCost <= maxCost).ToList();
        }

        public ICollection<Gift> GetGiftsByOrder(int orderId)
        {
            return _context.OrderGifts.Where(o => o.OrderId == orderId).Select(g => g.Gift).ToList();
        }

        public bool GiftExists(int id)
        {
            return _context.Gifts.Any(g => g.GiftId == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; 
        }
    }
}
