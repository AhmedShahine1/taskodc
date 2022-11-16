using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using taskodc.Models;

namespace taskodc.Controllers
{
    public class CatagoriesController : ApiController
    {
        private ODCEntities1 db = new ODCEntities1();

        // GET: api/Catagories
        public IQueryable<Catagory> GetCatagories()
        {
            return db.Catagories;
        }

        // GET: api/Catagories/5
        [ResponseType(typeof(Catagory))]
        public async Task<IHttpActionResult> GetCatagory(int id)
        {
            Catagory catagory = await db.Catagories.FindAsync(id);
            if (catagory == null)
            {
                return NotFound();
            }

            return Ok(catagory);
        }

        // PUT: api/Catagories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCatagory(int id, Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != catagory.Id)
            {
                return BadRequest();
            }

            db.Entry(catagory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CatagoryExists(id))
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

        // POST: api/Catagories
        [ResponseType(typeof(Catagory))]
        public async Task<IHttpActionResult> PostCatagory(Catagory catagory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Catagories.Add(catagory);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CatagoryExists(catagory.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = catagory.Id }, catagory);
        }

        // DELETE: api/Catagories/5
        [ResponseType(typeof(Catagory))]
        public async Task<IHttpActionResult> DeleteCatagory(int id)
        {
            Catagory catagory = await db.Catagories.FindAsync(id);
            if (catagory == null)
            {
                return NotFound();
            }

            db.Catagories.Remove(catagory);
            await db.SaveChangesAsync();

            return Ok(catagory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CatagoryExists(int id)
        {
            return db.Catagories.Count(e => e.Id == id) > 0;
        }
    }
}