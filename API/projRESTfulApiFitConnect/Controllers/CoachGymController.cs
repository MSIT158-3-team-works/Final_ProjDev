using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.Models;

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachGymController : ControllerBase
    {
        private readonly GymContext _db;
        private readonly IWebHostEnvironment _env;
        public CoachGymController(GymContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        //取得所有場館
        [HttpGet("GetGym")]
        public async Task<IActionResult> GetGym()
        {
            var roads = await _db.TGyms.Select(x => new { x.GymId,x.GymName, x.GymAddress }).OrderBy(x=>x.GymAddress).ToListAsync();
            return Ok(roads);
        }
        //取得特定場館內的場地
        [HttpGet("GetField/{id}")]
        public async Task<IActionResult> GetField(int id)
        {
            var fields = await _db.Tfields.Where(x=>x.GymId==id).Select(x => new { x.FieldId, x.Floor, x.FieldName,x.FieldPayment }).OrderBy(x=>x.Floor).ToListAsync();
            return Ok(fields);
        }
        //取得特定場館營業時間
        [HttpGet("GetOpeningHours/{id}")]
        public async Task<IActionResult> GetGymOpeningHours(int id)
        {
            var fields = await _db.TGymTimes.Where(x => x.GymId == id).Include(x=>x.GymTimeNavigation).Select(x=>new { x.GymTimeNavigation.TimeId,x.GymTimeNavigation.TimeName }).ToListAsync();
            return Ok(fields);
        }

    }
}
