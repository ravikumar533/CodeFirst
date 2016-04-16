using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CodeFirst.DataContext;
using CodeFirst.Models;
using CodeFirst.Repositories;

namespace CodeFirst.Controllers
{
    public class ItemsController : Controller
    {
        private ItemInterface _ItemInterface { get; set; }
        public ItemsController(ItemInterface itemInterface)
        {
            _ItemInterface = itemInterface;
        }

        // GET: Items
        public ActionResult Index()
        {
            return View(_ItemInterface.GetItemList());
        }

        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _ItemInterface.GetItemById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _ItemInterface.AddItem(item);
                _ItemInterface.SaveDataBase();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _ItemInterface.GetItemById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price")] Item item)
        {
            if (ModelState.IsValid)
            {
                _ItemInterface.AddItem(item);
                _ItemInterface.SaveDataBase();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = _ItemInterface.GetItemById(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = _ItemInterface.GetItemById(id);
            _ItemInterface.RemoveItem(item);
            _ItemInterface.SaveDataBase();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ItemInterface.hotelDataContext.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
