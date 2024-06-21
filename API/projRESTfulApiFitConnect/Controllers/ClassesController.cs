using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly GymContext _context;

        public ClassesController(GymContext context)
        {
            _context = context;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tclass>>> GetTclasses()
        {
            return await _context.Tclasses.ToListAsync();
        }

        // GET: api/Classes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tclass>> GetTclass(int id)
        {
            var tclass = await _context.Tclasses.FindAsync(id);

            if (tclass == null)
            {
                return NotFound();
            }

            return tclass;
        }

        // PUT: api/Classes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTclass(int id, Tclass tclass)
        {
            if (id != tclass.ClassId)
            {
                return BadRequest();
            }

            _context.Entry(tclass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TclassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Classes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tclass>> PostTclass(Tclass tclass)
        {
            _context.Tclasses.Add(tclass);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTclass", new { id = tclass.ClassId }, tclass);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTclass(int id)
        {
            var tclass = await _context.Tclasses.FindAsync(id);
            if (tclass == null)
            {
                return NotFound();
            }

            _context.Tclasses.Remove(tclass);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TclassExists(int id)
        {
            return _context.Tclasses.Any(e => e.ClassId == id);
        }
    }
}
