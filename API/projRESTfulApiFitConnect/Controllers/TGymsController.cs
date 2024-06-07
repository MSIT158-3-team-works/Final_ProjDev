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
    public class TGymsController : ControllerBase
    {
        private readonly GymContext _context;

        public TGymsController(GymContext context)
        {
            _context = context;
        }

        // GET: api/TGyms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TGym>>> GetTGyms()
        {
            return await _context.TGyms.ToListAsync();
        }

        // GET: api/TGyms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TGym>> GetTGym(int id)
        {
            var tGym = await _context.TGyms.FindAsync(id);

            if (tGym == null)
            {
                return NotFound();
            }

            return tGym;
        }

        // PUT: api/TGyms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTGym(int id, TGym tGym)
        {
            if (id != tGym.GymId)
            {
                return BadRequest();
            }

            _context.Entry(tGym).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TGymExists(id))
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

        // POST: api/TGyms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TGym>> PostTGym(TGym tGym)
        {
            _context.TGyms.Add(tGym);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTGym", new { id = tGym.GymId }, tGym);
        }

        // DELETE: api/TGyms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTGym(int id)
        {
            var tGym = await _context.TGyms.FindAsync(id);
            if (tGym == null)
            {
                return NotFound();
            }

            _context.TGyms.Remove(tGym);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TGymExists(int id)
        {
            return _context.TGyms.Any(e => e.GymId == id);
        }
    }
}
