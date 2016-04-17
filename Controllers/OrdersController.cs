using System.Net;
using System.Web.Mvc;
using CodeFirst.Models;
using CodeFirst.Repositories;
using System.Collections.Generic;
using System;

namespace CodeFirst.Controllers
{
    public class OrdersController : Controller
    {
        private IOrderRepo _IOrderRepo { get; set; }
        private ItemInterface _iItemRepo { get; set; }

        public OrdersController(IOrderRepo iOrderRepo, ItemInterface iItemRepo)
        {
            _IOrderRepo = iOrderRepo;
            _iItemRepo = iItemRepo;
        }

        // GET: Orders
        public ActionResult Index()
        {
            return View(_IOrderRepo.GetOrderList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _IOrderRepo.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            var model = new OrderModel();
            model.OrderDetails = new Order { Name = " ", UserName = " " , Phone = "", TotalAmount = 0};
            model.FoodMenuItems = _iItemRepo.GetItemList();
            return View(model);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "Id,UserName,Phone")] Order order)
        {
            if (ModelState.IsValid)
            {
                var orderInfo = _IOrderRepo.GetOrderById(order.Id);
                orderInfo.UserName = order.UserName;
                orderInfo.Phone = order.Phone;
                _IOrderRepo.UpdateOrder(orderInfo);
                _IOrderRepo.SaveDbChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _IOrderRepo.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UserName,Phone,TotalAmount")] Order order)
        {
            if (ModelState.IsValid)
            {
                _IOrderRepo.UpdateOrder(order);
                _IOrderRepo.SaveDbChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = _IOrderRepo.GetOrderById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = _IOrderRepo.GetOrderById(id);
            _IOrderRepo.RemoveOrder(order);
            _IOrderRepo.SaveDbChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderedItemList(int orderId)
        {
            var model = new List<OrderedItem>();
            model = _IOrderRepo.GetOrderedItems(orderId);
            return View("OrderedItem",model);
        }
        
        public ActionResult SaveOrderItem(int itemId, int orderId)
        {
            string currentTime = DateTime.Now.ToString("yyyyMMddhhmmss");
            if (orderId == 0)
                orderId = _IOrderRepo.AddOrder(new Order { Name = "Order" + currentTime, Phone = "", TotalAmount = 0, UserName = " " });

            var orderItem = _IOrderRepo.GetOrderItem(itemId, orderId);
            if (orderItem != null)
            {
                orderItem.Quantity += 1;                
            }
            else
            {
                _IOrderRepo.AddItemtoOrder(orderId, itemId);
            }
            _IOrderRepo.SaveDbChanges();  
            return Json(orderId,JsonRequestBehavior.AllowGet);
        }
        public ActionResult SaveOrderInfo(int orderId,string userName,string phone)
        {
            var order = _IOrderRepo.GetOrderById(orderId);
            order.UserName = userName;
            order.Phone = phone;
            _IOrderRepo.UpdateOrder(order);
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _IOrderRepo.hotelDataConext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
