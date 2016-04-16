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
    }
}
