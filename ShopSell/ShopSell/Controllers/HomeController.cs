using ShopSell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopSell.Controllers
{
    [Authorize(Roles = "admin, manager, employee, shipper")]
    public class HomeController : Controller
    {
        private ShopSellDb db = new ShopSellDb();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.CountProducts = db.Products.Count();
            ViewBag.CountOrders = db.Orders.Count();

            return View();
        }
    }
}
