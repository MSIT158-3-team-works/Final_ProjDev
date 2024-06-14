using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminGymlistController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminGymlistController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        [HttpGet(Name = "GetGymList")]
        public async Task<ActionResult<IEnumerable<GymReviewDetailDto>>> GetGymsList()
        {
            string filepath = "";

            List<GymReviewDetailDto> gymDtos = new List<GymReviewDetailDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var gyms = await _context.TGyms
         .Include(x => x.Company)
         .ThenInclude(c => c.Owner)
         .Include(x => x.Region)
         .ThenInclude(r => r.City)
         .ToListAsync();

            foreach (var item in gyms)
            {
                string base64Image = "";
                filepath = Path.Combine(_env.ContentRootPath, "Images", "GymImages", item.GymPhoto);
                if (System.IO.File.Exists(filepath))
                {
                    byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                    base64Image = Convert.ToBase64String(bytes);
                }

                var ownerName = item.Company?.Owner?.Owner ?? "Unknown Owner";
                var companyName = item.Company?.Name ?? "Unknown Company";
                var regionName = item.Region?.Region ?? "Unknown Region";
                var cityName = item.Region?.City?.City ?? "Unknown City";

                string startTime = "", endTime = "";
                if (!string.IsNullOrEmpty(item.GymTime))
                {
                    var times = item.GymTime.Split('-');
                    if (times.Length == 2)
                    {
                        startTime = times[0];
                        endTime = times[1];
                    }
                }

                GymReviewDetailDto gymDto = new GymReviewDetailDto()
                {
                    GymId = item.GymId,
                    Owner = ownerName,
                    RegionId = item.RegionId,
                    Name = companyName,
                    CompanyId = item.CompanyId,
                    GymName = item.GymName,
                    GymAddress = item.GymAddress,
                    GymPhoto = base64Image,
                    GymPhone = item.GymPhone,
                    GymPark = item.GymPark,
                    GymTraffic = item.GymTraffic,
                    GymDescribe = item.GymDescribe,
                    GymStatus = item.GymStatus,
                    Region = regionName,
                    City = cityName,
                    CityId = item.Region.CityId,
                    start_time = startTime,
                    end_time = endTime,
                };
                gymDtos.Add(gymDto);
            }
            return Ok(gymDtos);
        }
        // GET api/<AdminListController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymReviewDetailDto>> GetGymReviewDetail(int id)
        {
            var item = await _context.TGyms
             .Include(x => x.Company)
                 .ThenInclude(c => c.Owner)
             .Include(x => x.Region)
                 .ThenInclude(r => r.City)
             .FirstOrDefaultAsync(x => x.GymId == id);

            if (item == null)
            {
                return NotFound();
            }

            string base64Image = "";
            string filepath = Path.Combine(_env.ContentRootPath, "Images", "GymImages", item.GymPhoto);
            if (System.IO.File.Exists(filepath))
            {
                byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                base64Image = Convert.ToBase64String(bytes);
            }

            var ownerName = item.Company?.Owner?.Owner ?? "Unknown Owner";
            var companyName = item.Company?.Name ?? "Unknown Company";
            var regionName = item.Region?.Region ?? "Unknown Region";
            var cityName = item.Region?.City?.City ?? "Unknown City";

            string startTime = "", endTime = "";
            if (!string.IsNullOrEmpty(item.GymTime))
            {
                var times = item.GymTime.Split('-');
                if (times.Length == 2)
                {
                    startTime = times[0];
                    endTime = times[1];
                }
            }

            GymReviewDetailDto gymDto = new GymReviewDetailDto()
            {
                GymId = item.GymId,
                Owner = ownerName,
                RegionId = item.RegionId,
                Name = companyName,
                CompanyId = item.CompanyId,
                GymName = item.GymName,
                GymAddress = item.GymAddress,
                GymTime = item.GymTime,
                GymPhoto = base64Image,
                GymPhone = item.GymPhone,
                GymPark = item.GymPark,
                GymTraffic = item.GymTraffic,
                GymDescribe = item.GymDescribe,
                GymStatus = item.GymStatus,
                Region = regionName,
                City = cityName,
                CityId = item.Region.CityId,
                start_time = startTime,
                end_time = endTime,
            };

            return Ok(gymDto);
        }
        // DELETE api/<AdminListController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            var gym = await _context.TGyms.FindAsync(id);

            if (gym == null)
            {
                return NotFound();
            }

            _context.TGyms.Remove(gym);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
