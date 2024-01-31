using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;

namespace FlowerShop.Repository
{
    public class OrderBouquetRepository : IOrderBouquetRepository
    { 
        private DataContext _context;

        public OrderBouquetRepository(DataContext context)
        {
            _context = context;
        }

        public void CreateRange(IEnumerable<OrderBouquet> orderBouquets)
        {
            _context.AddRange(orderBouquets);
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
