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
    public class ApiAboutShopsController : ApiController
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: api/ApiAboutShops
        public IQueryable<AboutShop> GetAboutShops()
        {
            return db.AboutShops;
        }

        // GET: api/ApiAboutShops/5
        [ResponseType(typeof(AboutShop))]
        public IHttpActionResult GetAboutShop(int id)
        {
            AboutShop aboutShop = db.AboutShops.Find(id);
            if (aboutShop == null)
            {
                return NotFound();
            }

            return Ok(aboutShop);
        }

        //// PUT: api/ApiAboutShops/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAboutShop(int id, AboutShop aboutShop)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != aboutShop.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(aboutShop).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AboutShopExists(id))
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

        //// POST: api/ApiAboutShops
        //[ResponseType(typeof(AboutShop))]
        //public IHttpActionResult PostAboutShop(AboutShop aboutShop)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.AboutShops.Add(aboutShop);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = aboutShop.Id }, aboutShop);
        //}

        //// DELETE: api/ApiAboutShops/5
        //[ResponseType(typeof(AboutShop))]
        //public IHttpActionResult DeleteAboutShop(int id)
        //{
        //    AboutShop aboutShop = db.AboutShops.Find(id);
        //    if (aboutShop == null)
        //    {
        //        return NotFound();
        //    }

        //    db.AboutShops.Remove(aboutShop);
        //    db.SaveChanges();

        //    return Ok(aboutShop);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AboutShopExists(int id)
        {
            return db.AboutShops.Count(e => e.Id == id) > 0;
        }
    }
}