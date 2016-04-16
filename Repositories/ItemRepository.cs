using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeFirst.DataContext;
using CodeFirst.Models;

namespace CodeFirst.Repositories
{
    public class ItemRepository : ItemInterface
    {
        public HotelDataContext hotelDataContext
        {
            get;

            set;
        }
        public ItemRepository()
        {
            hotelDataContext = new HotelDataContext();   
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Item GetItemById(int? itemId)
        {
            return hotelDataContext.Items.Find(itemId);
        }

        public IList<Item> GetItemList()
        {
            return hotelDataContext.Items.ToList();
        }
        public void AddItem(Item item)
        {
            if (item.Id == 0)
                hotelDataContext.Items.Add(item);
            else
                hotelDataContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
        public void RemoveItem(Item item)
        {
            if(item.Id != 0)
            hotelDataContext.Items.Remove(item);
        }

        public void SaveDataBase()
        {
            hotelDataContext.SaveChanges();
        }
    }
}