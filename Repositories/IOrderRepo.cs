using CodeFirst.DataContext;
using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Repositories
{
    public interface IOrderRepo : IDisposable
    {
        HotelDataContext hotelDataConext { get; set; }
        Order GetOrderById(int? Id);
        OrderItem GetOrderItem(int itemId, int orderId);
        IList<Order> GetOrderList();
        IList<OrderItem> GetOrderItemList(int orderId);
        List<OrderedItem> GetOrderedItems(int OrderId);
        int AddOrder(Order order);
        void AddItemtoOrder(int orderId, int itemId);
        void UpdateOrderItem(OrderItem orderItem);
        void UpdateOrder(Order order);
        void RemoveOrder(Order order);
        void SaveDbChanges();
    }
}
