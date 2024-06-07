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
    public class ClassSort1Controller : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public ClassSort1Controller(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TclassSort有氧>>> GetTclassSort有氧s()
        {
            return await _context.TclassSort有氧s.ToListAsync();
        }

        // GET: api/test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TclassSort有氧>> GetTclassSort有氧(int id)
        {
            var tclassSort有氧 = await _context.TclassSort有氧s.FindAsync(id);

            if (tclassSort有氧 == null)
            {
                return NotFound();
            }

            return tclassSort有氧;
        }

        // PUT: api/test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTclassSort有氧(int id, TclassSort有氧 tclassSort有氧)
        {
            if (id != tclassSort有氧.ClassSort1Id)
            {
                return BadRequest();
            }

            _context.Entry(tclassSort有氧).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TclassSort有氧Exists(id))
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

        // POST: api/test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TclassSort有氧>> PostTclassSort有氧(TclassSort有氧 tclassSort有氧)
        {
            _context.TclassSort有氧s.Add(tclassSort有氧);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTclassSort有氧", new { id = tclassSort有氧.ClassSort1Id }, tclassSort有氧);
        }

        // DELETE: api/test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTclassSort有氧(int id)
        {
            var tclassSort有氧 = await _context.TclassSort有氧s.FindAsync(id);
            if (tclassSort有氧 == null)
            {
                return NotFound();
            }

            _context.TclassSort有氧s.Remove(tclassSort有氧);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TclassSort有氧Exists(int id)
        {
            return _context.TclassSort有氧s.Any(e => e.ClassSort1Id == id);
        }
    }
}
