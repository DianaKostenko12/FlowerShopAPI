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

        public bool CreateOrder(Order order)
        {
            _context.Add(order);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        //public void GetOrderSum(int orderId)
        //{
        //    var order = _context.Orders
        //        .Include(o => o.OrderBouquets)
        //            .ThenInclude(ob => ob.Bouquet)
        //        .Include(o => o.OrderGifts)
        //            .ThenInclude(og => og.Gift)
        //        .FirstOrDefault(o => o.OrderId == orderId);

        //    if (order != null)
        //    {
        //        float bouquetSum = order.OrderBouquets
        //            .Sum(ob => ob.Bouquet.BouquetCost * ob.BouquetCount);

        //        float giftSum = order.OrderGifts
        //            .Sum(og => og.Gift.GiftCost * og.GiftCount);

        //        order.OrderSum = bouquetSum + giftSum;
        //        _context.SaveChanges();
        //    }
        //    else
        //    {
        //        Console.WriteLine("Замовлення не знайдено.");
        //    }
        //}
    }
}
