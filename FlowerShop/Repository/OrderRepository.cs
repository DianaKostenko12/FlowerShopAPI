using FlowerShop.Data;
using FlowerShop.Interfaces;
using FlowerShop.Models;
using FlowerShop.Views;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        
        public Client GetClientByOrder(int orderId)
        {
            return _context.Orders
                  .Where(o => o.OrderId == orderId)
                  .Select(o => o.Client)
                  .FirstOrDefault();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Where(o => o.OrderId == orderId).FirstOrDefault();
        }

        public ICollection<OrderBouquetWithCount> GetOrderBouquetsWithCount(int OrderId)
        {
            return _context.Orders
            .Include(o => o.OrderBouquets)
            .ThenInclude(ob => ob.Bouquet)
            .SelectMany(o => o.OrderBouquets.Select(ob => new OrderBouquetWithCount
            {
                Bouquet = ob.Bouquet,
                BouquetCount = ob.BouquetCount
            }))
            .ToList();
        }

        public ICollection<OrderGiftWithCount> GetOrderGiftsWithCount(int OrderId)
        {
            return _context.Orders
           .Include(o => o.OrderGifts)
           .ThenInclude(ob => ob.Gift)
           .SelectMany(o => o.OrderGifts.Select(ob => new OrderGiftWithCount
           {
               Gift = ob.Gift,
               GiftCount = ob.GiftCount
           }))
           .ToList();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.OrderId == orderId);
        }
    }
}
