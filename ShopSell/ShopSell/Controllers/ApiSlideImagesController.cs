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
    public class ApiSlideImagesController : ApiController
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: api/ApiSlideImages
        public IQueryable<SlideImage> GetSlideImages()
        {
            return db.SlideImages;
        }

        // GET: api/ApiSlideImages/5
        [ResponseType(typeof(SlideImage))]
        public IHttpActionResult GetSlideImage(int id)
        {
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return NotFound();
            }

            return Ok(slideImage);
        }

        //// PUT: api/ApiSlideImages/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutSlideImage(int id, SlideImage slideImage)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != slideImage.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(slideImage).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SlideImageExists(id))
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

        //// POST: api/ApiSlideImages
        //[ResponseType(typeof(SlideImage))]
        //public IHttpActionResult PostSlideImage(SlideImage slideImage)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.SlideImages.Add(slideImage);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = slideImage.Id }, slideImage);
        //}

        //// DELETE: api/ApiSlideImages/5
        //[ResponseType(typeof(SlideImage))]
        //public IHttpActionResult DeleteSlideImage(int id)
        //{
        //    SlideImage slideImage = db.SlideImages.Find(id);
        //    if (slideImage == null)
        //    {
        //        return NotFound();
        //    }

        //    db.SlideImages.Remove(slideImage);
        //    db.SaveChanges();

        //    return Ok(slideImage);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SlideImageExists(int id)
        {
            return db.SlideImages.Count(e => e.Id == id) > 0;
        }
    }
}