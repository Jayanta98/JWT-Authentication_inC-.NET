using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;

using System.Web.Http;

using System.Web.Http.Description;
using APIuser.Models;

namespace APIuser.Controllers
{
    [Authorize]
    public class MyUsersController : ApiController
    {
        private SecureDBEntities db = new SecureDBEntities();

        // GET: api/MyUsers
        [Route("api/data")]
        public IQueryable<MyUser> GetMyUsers()
        {
            return db.MyUsers;
        }

        // GET: api/MyUsers/5
        [ResponseType(typeof(MyUser))]
        public IHttpActionResult GetMyUser(int id)
        {
            MyUser myUser = db.MyUsers.Find(id);
            if (myUser == null)
            {
                return NotFound();
            }

            return Ok(myUser);
        }

        // PUT: api/MyUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMyUser(int id, MyUser myUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != myUser.UserID)
            {
                return BadRequest();
            }

            db.Entry(myUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MyUsers
        [ResponseType(typeof(MyUser))]
        public IHttpActionResult PostMyUser(MyUser myUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MyUsers.Add(myUser);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = myUser.UserID }, myUser);
        }

        // DELETE: api/MyUsers/5
        [ResponseType(typeof(MyUser))]
        public IHttpActionResult DeleteMyUser(int id)
        {
            MyUser myUser = db.MyUsers.Find(id);
            if (myUser == null)
            {
                return NotFound();
            }

            db.MyUsers.Remove(myUser);
            db.SaveChanges();

            return Ok(myUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MyUserExists(int id)
        {
            return db.MyUsers.Count(e => e.UserID == id) > 0;
        }
    }
}