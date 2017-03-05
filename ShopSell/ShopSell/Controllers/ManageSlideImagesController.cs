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
    public class ManageSlideImagesController : Controller
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: ManageSlideImages
        public ActionResult Index()
        {
            return View(db.SlideImages.ToList());
        }

        // GET: ManageSlideImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // GET: ManageSlideImages/Create
        public ActionResult Create()
        {
            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Hiển thị" },
                new { Id = "DEACTIVE", Name = "Ẩn đi" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            return View();
        }

        // POST: ManageSlideImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ImageUrl,Title,Status")] SlideImage slideImage)
        {
            if (ModelState.IsValid)
            {
                db.SlideImages.Add(slideImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideImage);
        }

        // GET: ManageSlideImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Còn hàng" },
                new { Id = "DEACTIVE", Name = "Hết hàng" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            return View(slideImage);
        }

        // POST: ManageSlideImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ImageUrl,Title,Status")] SlideImage slideImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slideImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideImage);
        }

        // GET: ManageSlideImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // POST: ManageSlideImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (var db = new ShopSellDb())
            {
                var item = db.SlideImages.Find(id);
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

            SlideImage slideImage = db.SlideImages.Find(id);
            db.SlideImages.Remove(slideImage);
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
            var defaultFolderToSaveFile = "~/Uploads/Images/Products/S" + id + "/";

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
                        var slides = db.SlideImages.Find(id);
                        slides.ImageUrl = imageUrl;
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
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
