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
    public class courseContentsController : ApiController
    {
        private ODCEntities1 db = new ODCEntities1();

        // GET: api/courseContents
        public IQueryable<courseContent> GetcourseContents()
        {
            return db.courseContents;
        }

        // GET: api/courseContents/5
        [ResponseType(typeof(courseContent))]
        public async Task<IHttpActionResult> GetcourseContent(int id)
        {
            courseContent courseContent = await db.courseContents.FindAsync(id);
            if (courseContent == null)
            {
                return NotFound();
            }

            return Ok(courseContent);
        }

        // PUT: api/courseContents/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutcourseContent(int id, courseContent courseContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != courseContent.id)
            {
                return BadRequest();
            }

            db.Entry(courseContent).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!courseContentExists(id))
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

        // POST: api/courseContents
        [ResponseType(typeof(courseContent))]
        public async Task<IHttpActionResult> PostcourseContent(courseContent courseContent)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.courseContents.Add(courseContent);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = courseContent.id }, courseContent);
        }

        // DELETE: api/courseContents/5
        [ResponseType(typeof(courseContent))]
        public async Task<IHttpActionResult> DeletecourseContent(int id)
        {
            courseContent courseContent = await db.courseContents.FindAsync(id);
            if (courseContent == null)
            {
                return NotFound();
            }

            db.courseContents.Remove(courseContent);
            await db.SaveChangesAsync();

            return Ok(courseContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool courseContentExists(int id)
        {
            return db.courseContents.Count(e => e.id == id) > 0;
        }
    }
}