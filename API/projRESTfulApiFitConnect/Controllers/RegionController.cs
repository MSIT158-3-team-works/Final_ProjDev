using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.DTO.Gym;
using projRESTfulApiFitConnect.Models;
using System.Drawing.Drawing2D;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public RegionController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: api/<RegionController>
        //讀取所有場館資訊
        [HttpGet(Name = "GetRegions")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetRegions()
        {

            List<CityDto> cityDtos = new List<CityDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }

            var citys = await _context.TregionTables
                .Include(x => x.City) // 確保你正確地包含了City
                .Include(x => x.TGyms)
                .ToListAsync();
            foreach (var item in citys)
            {
                // 只查找 Gym_status 為 false 的場館
                var inactiveGym = item.TGyms.FirstOrDefault(g => g.GymStatus == true);
                int gymId = item.TGyms.FirstOrDefault()?.GymId ?? 0; // 查找第一個場館的 GymId，如果沒有，則設置為0
                CityDto cityDto = new CityDto()
                {
                    GymId = gymId,
                    RegionId = item.RegionId,
                    CityId = item.CityId,
                    Region = item.Region,
                    City = item.City.City // 使用Tcity的City屬性
                };
                cityDtos.Add(cityDto);
            }

            return Ok(cityDtos);
        }
        // GET api/<GymListController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TGym>> GetTRegion(int id)
        {
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var tRegion = await _context.TGyms.FindAsync(id);

            if (tRegion == null)
            {
                return NotFound();
            }

            return tRegion;
        }

    }
}
