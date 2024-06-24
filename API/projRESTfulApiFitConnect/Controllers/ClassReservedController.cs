using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO.Coach;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassReservedController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public ClassReservedController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // Get: api/ClassReserved/{id}
        // 依照篩選取得課程資料
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClassReserved(int id)
        {
            var reservedClass = await _context.TclassReserves.Where(x => x.ClassScheduleId == id).ToListAsync();
            return Ok(new { currentNumberOfStudent = reservedClass.Count });
        }

        [HttpGet("add/{id}")]
        public async Task<ActionResult> addClassReserved(int id, int schid)
        {
            if (_context.TclassReserves.Any(x => x.MemberId == id && x.ClassScheduleId == schid))
                return BadRequest();

            TclassReserve newreserve = new TclassReserve
            {
                ClassScheduleId = schid,
                MemberId = id,
                PaymentStatus = false,
                ReserveStatus = false,
            };
            _context.TclassReserves.Add(newreserve);
            _context.SaveChanges();

            return Ok();
        }
    }
}
