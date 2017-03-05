using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopSell.Models;
using System.IO;

namespace ShopSell.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class ManageAboutShopsController : Controller
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: ManageAboutShops
        public ActionResult Index()
        {
            return View(db.AboutShops.ToList());
        }

        // GET: ManageAboutShops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutShop aboutShop = db.AboutShops.Find(id);
            if (aboutShop == null)
            {
                return HttpNotFound();
            }
            return View(aboutShop);
        }

        // GET: ManageAboutShops/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageAboutShops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text")] AboutShop aboutShop)
        {
            if (ModelState.IsValid)
            {
                db.AboutShops.Add(aboutShop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutShop);
        }

        // GET: ManageAboutShops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutShop aboutShop = db.AboutShops.Find(id);
            if (aboutShop == null)
            {
                return HttpNotFound();
            }
            return View(aboutShop);
        }

        // POST: ManageAboutShops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text")] AboutShop aboutShop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aboutShop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutShop);
        }

        // GET: ManageAboutShops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AboutShop aboutShop = db.AboutShops.Find(id);
            if (aboutShop == null)
            {
                return HttpNotFound();
            }
            return View(aboutShop);
        }

        // POST: ManageAboutShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutShop aboutShop = db.AboutShops.Find(id);
            db.AboutShops.Remove(aboutShop);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region SUMMERNOTE UPLOAD IMAGES

        [HttpPost]
        public ActionResult SummernoteUploadImage()
        {
            foreach (var fileKey in Request.Files.AllKeys)
            {
                var file = Request.Files[fileKey];
                try
                {
                    var fileName = Path.GetFileName(file?.FileName);
                    if (fileName != null)
                    {
                        var path = Server.MapPath("~/Uploads/SummernoteImages");
                        if (System.IO.File.Exists(path) == false)
                        {
                            System.IO.Directory.CreateDirectory(path);
                        }

                        path = Path.Combine(Server.MapPath("~/Uploads/SummernoteImages"), fileName);
                        file.SaveAs(path);
                        return Json(new { Url = Url.Content("~/Uploads/SummernoteImages/" + fileName) });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Message = $"Error in saving file ({ex.Message})" });
                }
            }
            return Json(new { Message = "File saved" });
        }

        #endregion
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