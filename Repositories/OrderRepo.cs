using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirst.DataContext;
using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        public HotelDataContext hotelDataConext
        {
            get; set;
        }
        public OrderRepo()
        {
            hotelDataConext = new HotelDataContext();
        }

        public Order GetOrderById(int? OrderId)
        {
            return hotelDataConext.Orders.Find(OrderId);
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int AddOrder(Order order)
        {
            if (order.Id == 0)
                hotelDataConext.Orders.Add(order);
            hotelDataConext.SaveChanges();
            return order.Id;
        }

        public void UpdateOrder(Order order)
        {
            hotelDataConext.Entry(order).State = System.Data.Entity.EntityState.Modified;
        }

        public void RemoveOrder(Order order)
        {
            if (order.Id > 0)
                hotelDataConext.Orders.Remove(order);
        }

        public IList<Order> GetOrderList()
        {
            return hotelDataConext.Orders.ToList();
        }

        public void SaveDbChanges()
        {
            hotelDataConext.SaveChanges();
        }

        public IList<OrderItem> GetOrderItemList(int orderId)
        {
            return hotelDataConext.OrderItems.Where(s => s.OrderId == orderId).ToList();
        }

        public List<OrderedItem> GetOrderedItems(int orderId)
        {
            var orderITems = GetOrderItemList(orderId);
            var orderedItems = new List<OrderedItem>();
            foreach(var oitem in orderITems)
            {
                var item = hotelDataConext.Items.Find(oitem.ItemId);
                orderedItems.Add(new OrderedItem {
                    ItemId = oitem.ItemId,
                    ItemName = item.Name,
                    Price = item.Price,
                    Quantity = oitem.Quantity,
                    Total = (item.Price * oitem.Quantity) 
                });
            }
            return orderedItems;
        }

        public void AddItemtoOrder(int orderId, int itemId)
        {
            var orderItem = GetOrderItem(itemId, orderId);
            if (orderItem == null)
            {
                orderItem = new OrderItem
                {
                    ItemId = itemId,
                    OrderId = orderId,
                    Quantity = 1,
                };
                hotelDataConext.OrderItems.Add(orderItem);
            }          
            
        }

        public OrderItem GetOrderItem(int itemId, int orderId)
        {
            return hotelDataConext.OrderItems.FirstOrDefault(s => s.ItemId == itemId && s.OrderId == orderId);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            
            

        }

        
    }
}