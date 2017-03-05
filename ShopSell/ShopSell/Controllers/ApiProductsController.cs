using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ShopSell.Models;
using System.Web.Http.Cors;

namespace ShopSell.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApiProductsController : ApiController
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: api/ApiProducts
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/ApiProducts/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // GET: api/ApiProducts/GetProducts?name={name}
        public IList<Product> GetProducts(string name)
        {

            return db.Products.Where(x => x.Name.Contains(name)).ToList();
        }

        // GET: api/ApiProducts/GetProductsPromote
        public IList<Product> GetProductsPromote()
        {

            return db.Products.Where(x => x.Discount > 0 ).ToList();
        }
        // GET: api/ApiProductsGetProductsNew
        public IList<Product> GetProductsNew()
        {

            return db.Products.Where(x => x.DisplayPosition == "NEW").ToList();
        }
        // GET: api/ApiProducts/GetProductsBestsele
        public IList<Product> GetProductsBestsele()
        {

            return db.Products.Where(x => x.DisplayPosition == "BESTSELE").ToList();
        }
        // GET: api/ApiProducts/GetProductsHot
        public IList<Product> GetProductsHot()
        {

            return db.Products.Where(x => x.DisplayPosition == "HOT").ToList();
        }
        // GET: api/ApiProducts/GetSimilartProducts/id
        public IList<Product> GetSimilartProducts(int id)
        {
            string catename = db.Products.Find(id).CategoryName.ToString();
            var similar = db.Products.Where(x => x.CategoryName == catename && x.Id != id ).ToList();
            return similar;
        }
        // GET: api/ApiProducts/GetProductsbyCategoryId/id
        public IList<Product> GetProductsbyCategoryId (int id)
        {
            return db.Products.Where(x => x.CategoryId == id).ToList();
        }

        //// PUT: api/ApiProducts/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutProduct(int id, Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/ApiProducts
        //[ResponseType(typeof(Product))]
        //public IHttpActionResult PostProduct(Product product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Products.Add(product);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        //}

        //// DELETE: api/ApiProducts/5
        //[ResponseType(typeof(Product))]
        //public IHttpActionResult DeleteProduct(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Products.Remove(product);
        //    db.SaveChanges();

        //    return Ok(product);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}