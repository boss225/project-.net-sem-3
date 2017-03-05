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
    public class ApiNewsServicesController : ApiController
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: api/ApiNewsServices
        public IQueryable<NewsService> GetNewsServices()
        {
            return db.NewsServices;
        }

        // GET: api/ApiNewsServices/5
        [ResponseType(typeof(NewsService))]
        public IHttpActionResult GetNewsService(int id)
        {
            NewsService newsService = db.NewsServices.Find(id);
            if (newsService == null)
            {
                return NotFound();
            }

            return Ok(newsService);
        }

        //// PUT: api/ApiNewsServices/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutNewsService(int id, NewsService newsService)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != newsService.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(newsService).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!NewsServiceExists(id))
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

        //// POST: api/ApiNewsServices
        //[ResponseType(typeof(NewsService))]
        //public IHttpActionResult PostNewsService(NewsService newsService)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.NewsServices.Add(newsService);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = newsService.Id }, newsService);
        //}

        //// DELETE: api/ApiNewsServices/5
        //[ResponseType(typeof(NewsService))]
        //public IHttpActionResult DeleteNewsService(int id)
        //{
        //    NewsService newsService = db.NewsServices.Find(id);
        //    if (newsService == null)
        //    {
        //        return NotFound();
        //    }

        //    db.NewsServices.Remove(newsService);
        //    db.SaveChanges();

        //    return Ok(newsService);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsServiceExists(int id)
        {
            return db.NewsServices.Count(e => e.Id == id) > 0;
        }
    }
}