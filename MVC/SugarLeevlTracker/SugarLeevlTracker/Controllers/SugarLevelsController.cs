using SugarLeevlTracker.Data;
using SugarLeevlTracker.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace SugarLeevlTracker.Controllers
{
    public class SugarLevelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SugarLevels
        public IQueryable<SugarLevel> GetSugarLevels()
        {
            return db.SugarLevels;
        }

        // GET: api/SugarLevels/5
        [ResponseType(typeof(SugarLevel))]
        public async Task<IHttpActionResult> GetSugarLevel(int id)
        {
            SugarLevel sugarLevel = await db.SugarLevels.FindAsync(id);
            if (sugarLevel == null)
            {
                return NotFound();
            }

            return Ok(sugarLevel);
        }

        // PUT: api/SugarLevels/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSugarLevel(int id, SugarLevel sugarLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sugarLevel.Id)
            {
                return BadRequest();
            }

            db.Entry(sugarLevel).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SugarLevelExists(id))
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

        // POST: api/SugarLevels
        [ResponseType(typeof(SugarLevel))]
        public async Task<IHttpActionResult> PostSugarLevel(SugarLevel sugarLevel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.SugarLevels.Add(sugarLevel);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sugarLevel.Id }, sugarLevel);
        }

        // DELETE: api/SugarLevels/5
        [ResponseType(typeof(SugarLevel))]
        public async Task<IHttpActionResult> DeleteSugarLevel(int id)
        {
            SugarLevel sugarLevel = await db.SugarLevels.FindAsync(id);
            if (sugarLevel == null)
            {
                return NotFound();
            }

            db.SugarLevels.Remove(sugarLevel);
            await db.SaveChangesAsync();

            return Ok(sugarLevel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SugarLevelExists(int id)
        {
            return db.SugarLevels.Count(e => e.Id == id) > 0;
        }
    }
}