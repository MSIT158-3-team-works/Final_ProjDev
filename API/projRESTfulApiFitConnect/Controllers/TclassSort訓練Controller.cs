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
    public class TclassSort訓練Controller : ControllerBase
    {
        private readonly GymContext _context;

        public TclassSort訓練Controller(GymContext context)
        {
            _context = context;
        }

        // GET: api/TclassSort訓練
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TclassSort訓練>>> GetTclassSort訓練s()
        {
            return await _context.TclassSort訓練s.ToListAsync();
        }

        // GET: api/TclassSort訓練/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TclassSort訓練>> GetTclassSort訓練(int id)
        {
            var tclassSort訓練 = await _context.TclassSort訓練s.FindAsync(id);

            if (tclassSort訓練 == null)
            {
                return NotFound();
            }

            return tclassSort訓練;
        }

        // PUT: api/TclassSort訓練/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTclassSort訓練(int id, TclassSort訓練 tclassSort訓練)
        {
            if (id != tclassSort訓練.ClassSort2Id)
            {
                return BadRequest();
            }

            _context.Entry(tclassSort訓練).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TclassSort訓練Exists(id))
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

        // POST: api/TclassSort訓練
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TclassSort訓練>> PostTclassSort訓練(TclassSort訓練 tclassSort訓練)
        {
            _context.TclassSort訓練s.Add(tclassSort訓練);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTclassSort訓練", new { id = tclassSort訓練.ClassSort2Id }, tclassSort訓練);
        }

        // DELETE: api/TclassSort訓練/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTclassSort訓練(int id)
        {
            var tclassSort訓練 = await _context.TclassSort訓練s.FindAsync(id);
            if (tclassSort訓練 == null)
            {
                return NotFound();
            }

            _context.TclassSort訓練s.Remove(tclassSort訓練);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TclassSort訓練Exists(int id)
        {
            return _context.TclassSort訓練s.Any(e => e.ClassSort2Id == id);
        }
    }
}
