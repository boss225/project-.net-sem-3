﻿using System;
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
    public class ManageNewsServicesController : Controller
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: ManageNewsServices
        public ActionResult Index()
        {
            return View(db.NewsServices.ToList());
        }

        // GET: ManageNewsServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsService newsService = db.NewsServices.Find(id);
            if (newsService == null)
            {
                return HttpNotFound();
            }
            return View(newsService);
        }

        // GET: ManageNewsServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageNewsServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Title,ImageUrl,Source,Description,CreatedDate")] NewsService newsService)
        {
            if (ModelState.IsValid)
            {
                newsService.CreatedDate = DateTime.Now;
                db.NewsServices.Add(newsService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsService);
        }

        // GET: ManageNewsServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsService newsService = db.NewsServices.Find(id);
            if (newsService == null)
            {
                return HttpNotFound();
            }
            return View(newsService);
        }

        // POST: ManageNewsServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Title,ImageUrl,Source,Description,CreatedDate")] NewsService newsService)
        {
            if (ModelState.IsValid)
            {
                newsService.CreatedDate = DateTime.Now;
                db.Entry(newsService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newsService);
        }

        // GET: ManageNewsServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsService newsService = db.NewsServices.Find(id);
            if (newsService == null)
            {
                return HttpNotFound();
            }
            return View(newsService);
        }

        // POST: ManageNewsServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new ShopSellDb())
            {
                var item = db.NewsServices.Find(id);
                if (item != null)
                {
                    // XÓA HÌNH
                    var pathToDetele = Server.MapPath(item.ImageUrl);
                    if (System.IO.File.Exists(pathToDetele))
                    {
                        System.IO.File.Delete(pathToDetele);
                    }
                }

            }

            NewsService newsService = db.NewsServices.Find(id);
            db.NewsServices.Remove(newsService);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region UPLOAD IMAGE

        // TUNG.NGO
        public ActionResult UploadImage(int id)
        {
            return View();
        }

        // TUNG.NGO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(HttpPostedFileBase file, int id)
        {
            var defaultFolderToSaveFile = "~/Uploads/Images/Products/N" + id + "/";

            // BEGIN: Kiểm tra nếu chưa tồn tại thư mục trên thì tạo mới. 
            if (Directory.Exists(Server.MapPath(defaultFolderToSaveFile)) == false)
            {
                Directory.CreateDirectory(Server.MapPath(defaultFolderToSaveFile));
            }
            // END: Kiểm tra nếu chưa tồn tại thư mục trên thì tạo mới. 

            if (ModelState.IsValid)
            {
                // Kiểm tra nếu người dùng có chọn file
                if (file != null && file.ContentLength > 0)
                {
                    // Lấy tên file
                    var fileName = Path.GetFileName(file.FileName);
                    if (fileName != null)
                    {
                        // Đường dẫn đầy đủ trên Server gồm path + filename
                        var path = Path.Combine(Server.MapPath(defaultFolderToSaveFile), fileName);

                        var i = 1;
                        while (System.IO.File.Exists(path))
                        {
                            path = Path.Combine(Server.MapPath(defaultFolderToSaveFile), i + "_" + fileName); // id + ".jpg"
                            i++;
                        }

                        // Upload file lên Server ở thư mục ~/Uploads/...
                        file.SaveAs(path);

                        // Lấy imageurl để lưu vào database, có định dạng "~/Uploads/Products/Id/ten_file.jpg"
                        var imageUrl = defaultFolderToSaveFile + fileName;
                        if (i > 1)
                        {
                            imageUrl = defaultFolderToSaveFile + (i - 1) + "_" + fileName;
                        }
                        // Lưu thông tin image url vào product
                        var newservice = db.NewsServices.Find(id);
                        newservice.ImageUrl = imageUrl;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
        #endregion

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
