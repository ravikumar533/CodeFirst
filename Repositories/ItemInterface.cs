using CodeFirst.DataContext;
using CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Repositories
{
    public interface ItemInterface:IDisposable
    {
        HotelDataContext hotelDataContext { get; set; }
        Item GetItemById(int? itemId);
        IList<Item> GetItemList();

        void AddItem(Item item);
        void RemoveItem(Item item);
        void SaveDataBase();
    }
}
