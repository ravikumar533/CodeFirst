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
    }
}