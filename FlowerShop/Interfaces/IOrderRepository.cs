using FlowerShop.Models;
using FlowerShop.Views;

namespace FlowerShop.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int orderId);
        Client GetClientByOrder(int orderId);
        bool OrderExists(int orderId);
        ICollection<OrderBouquetWithCount> GetOrderBouquetsWithCount(int OrderId);
        ICollection<OrderGiftWithCount> GetOrderGiftsWithCount(int OrderId);
        bool CreateOrder(Order order);
        bool Save();
    }
}
