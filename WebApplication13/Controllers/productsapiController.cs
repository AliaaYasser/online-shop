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
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    public class productsapiController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/productsapi
        public IQueryable<product> Getproducts()
        {
            return db.products;
        }

        // GET: api/productsapi/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Getproduct(int id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/productsapi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putproduct(int id, product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!productExists(id))
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

        // POST: api/productsapi
        [ResponseType(typeof(product))]
        public IHttpActionResult Postproduct(product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.id }, product);
        }

        // DELETE: api/productsapi/5
        [ResponseType(typeof(product))]
        public IHttpActionResult Deleteproduct(int id)
        {
            product product = db.products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool productExists(int id)
        {
            return db.products.Count(e => e.id == id) > 0;
        }
    }
}