using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;

namespace FlowerShop.Repository
{
    public class BouquetRepository : IBouquetRepository
    {
        private DataContext _context;
        public BouquetRepository(DataContext context)
        {
            _context = context;
        }
        public bool BouquetExists(int id)
        {
            return _context.Bouquets.Any(b => b.BouquetId == id);
        }

        public Bouquet GetBouquet(int id)
        {
            return _context.Bouquets.Where(b => b.BouquetId == id).FirstOrDefault();
        }

        public ICollection<Bouquet> GetBouquetsByCost(float minCost, float maxCost)
        {
            return _context.Bouquets.Where(b => b.BouquetCost >= minCost && b.BouquetCost <= maxCost).ToList();
        }

        public ICollection<Bouquet> GetBouquets()
        {
            return _context.Bouquets.ToList();
        }

        public ICollection<Bouquet> GetBouquetsByFlower(int flowerId)
        {
            return _context.BouquetFlowers.Where(f => f.FlowerId == flowerId).Select(b => b.Bouquet).ToList();
        }

        public ICollection<Bouquet> GetBouquetsByOrder(int orderId) 
        {
            return _context.OrderBouquets.Where(o => o.OrderId == orderId).Select(b => b.Bouquet).ToList();
        }

        public bool CreateBouquet(Bouquet bouquet)
        {
            _context.Add(bouquet);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
