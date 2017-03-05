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
    public class ApiUserCommentsController : ApiController
    {
        private ShopSellDb db = new ShopSellDb();

        // GET: api/ApiUserComments
        public IQueryable<UserComment> GetUserComments()
        {
            return db.UserComments;
        }

        // GET: api/ApiUserComments/5
        [ResponseType(typeof(UserComment))]
        public IHttpActionResult GetUserComment(int id)
        {
            UserComment userComment = db.UserComments.Find(id);
            if (userComment == null)
            {
                return NotFound();
            }

            return Ok(userComment);
        }

        //// PUT: api/ApiUserComments/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUserComment(int id, UserComment userComment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != userComment.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(userComment).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserCommentExists(id))
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

        //// POST: api/ApiUserComments
        //[ResponseType(typeof(UserComment))]
        //public IHttpActionResult PostUserComment(UserComment userComment)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.UserComments.Add(userComment);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = userComment.Id }, userComment);
        //}

        //// DELETE: api/ApiUserComments/5
        //[ResponseType(typeof(UserComment))]
        //public IHttpActionResult DeleteUserComment(int id)
        //{
        //    UserComment userComment = db.UserComments.Find(id);
        //    if (userComment == null)
        //    {
        //        return NotFound();
        //    }

        //    db.UserComments.Remove(userComment);
        //    db.SaveChanges();

        //    return Ok(userComment);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserCommentExists(int id)
        {
            return db.UserComments.Count(e => e.Id == id) > 0;
        }
    }
}