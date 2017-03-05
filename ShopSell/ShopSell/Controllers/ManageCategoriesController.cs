using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShopSell.Models;
using System.Text.RegularExpressions;
using System.Text;

namespace ShopSell.Controllers
{
    [Authorize(Roles = "admin, manager")]
    public class ManageCategoriesController : Controller
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: ManageCategories
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }

        // GET: ManageCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: ManageCategories/Create
        public ActionResult Create()
        {
            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường ParentId
            var categories = db.Categories.Where(x => x.ParentId == 0).ToList();
            //var categories = db.Categories.ToList();

            var root = new Category();
            root.Id = 0;
            root.Name = "Tạo mới";

            categories.Add(root);

            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường ParentId

            return View();
        }

        //// begin lọc unikey
        //private static readonly string[] VietNamChar = new string[]
        //{
        //    "aAeEoOuUiIdDyY",
        //    "áàạảãâấầậẩẫăắằặẳẵ",
        //    "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
        //    "éèẹẻẽêếềệểễ",
        //    "ÉÈẸẺẼÊẾỀỆỂỄ",
        //    "óòọỏõôốồộổỗơớờợởỡ",
        //    "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
        //    "úùụủũưứừựửữ",
        //    "ÚÙỤỦŨƯỨỪỰỬỮ",
        //    "íìịỉĩ",
        //    "ÍÌỊỈĨ",
        //    "đ",
        //    "Đ",
        //    "ýỳỵỷỹ",
        //    "ÝỲỴỶỸ"
        //};
        //public static string LocDau(string str)
        //{
        //    //Thay thế và lọc dấu từng char      
        //    for (int i = 1; i < VietNamChar.Length; i++)
        //    {
        //        for (int j = 0; j < VietNamChar[i].Length; j++)
        //            str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
        //    }
        //    return System.Text.RegularExpressions.Regex.Replace(str, "\\s+", " ");
        //}
        ////end lọc unikey
        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ParentId,ParentLevel,Name,SortOrder,Status")] Category category)
        {
            if (ModelState.IsValid)
            {              
                if (category.ParentId == 0) { category.ParentLevel = 1; } else { category.ParentLevel = 2; };
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường ParentId
            var categories = db.Categories.Where(x => x.ParentId == 0).ToList();
            //var categories = db.Categories.ToList();

            var root = new Category();
            root.Id = 0;
            root.Name = "Tạo mới";

            categories.Add(root);

            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường ParentId

            return View(category);
        }

        // GET: ManageCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường ParentId
            var categories = db.Categories.Where(x => x.ParentId == 0).ToList();
            //var categories = db.Categories.ToList();

            var root = new Category();
            root.Id = 0;
            root.Name = "Tạo mới";

            categories.Add(root);

            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường ParentId

            return View(category);
        }

        // POST: ManageCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ParentId,ParentLevel,Name,SortOrder,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.ParentId == 0) { category.ParentLevel = 1; } else { category.ParentLevel = 2; };
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // BEGIN: Tạo ra dropdownlist cho trường Status
            var statusItems = new[]
            {
                new { Id = "ACTIVE", Name = "Active" },
                new { Id = "DEACTIVE", Name = "Deactive" },
            };

            ViewBag.Status = new SelectList(statusItems, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường Status

            // BEGIN: Tạo ra dropdownlist cho trường ParentId
            var categories = db.Categories.Where(x => x.ParentId == 0).ToList();
            //var categories = db.Categories.ToList();

            var root = new Category();
            root.Id = 0;
            root.Name = "Tạo mới";

            categories.Add(root);

            ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            // END: Tạo ra dropdownlist cho trường ParentId

            return View(category);
        }

        // GET: ManageCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: ManageCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
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
