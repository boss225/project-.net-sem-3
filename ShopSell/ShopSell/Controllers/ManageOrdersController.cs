using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopSell.Models;

namespace ShopSell.Controllers
{
    [Authorize(Roles = "admin, manager, shipper")]
    public class ManageOrdersController : Controller
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: ManageOrders
        public ActionResult Index()
        {
            return View(db.Orders.ToList());
        }

        // GET: ManageOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: ManageOrders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,CreatedDate,ContactName,ContactAddress,ContactPhone,ContactEmail,ContactReceiverName,ContactReceiverAddress,ContactReceiverPhone,ContactReceiverEmail,Note,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: ManageOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: ManageOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,CreatedDate,ContactName,ContactAddress,ContactPhone,ContactEmail,ContactReceiverName,ContactReceiverAddress,ContactReceiverPhone,ContactReceiverEmail,Note,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: ManageOrders/Delete/5
        public ActionResult Delete(string code)
        {
            if (code == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(code);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: ManageOrders/Delete/TV1234567890
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string code)
        {
            db.OrderDetails.RemoveRange(db.OrderDetails.Where(c => c.OrderCode == code));
            Order order = db.Orders.Find(code);           
            db.Orders.Remove(order);            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Vinh
        public ActionResult OrderDetails(string code)
        {
            using (var db = new ShopSellDb())
            {
                var orderdetail = db.OrderDetails.Where(x => x.OrderCode == code).Include(p => p.Order);
                return View(orderdetail.ToList());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
