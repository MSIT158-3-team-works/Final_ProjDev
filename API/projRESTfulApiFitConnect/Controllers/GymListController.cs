using Microsoft.AspNetCore.Mvc;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Printing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymListController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public GymListController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/GymList
        //讀取所有場館資訊
        [HttpGet(Name = "GetGymLists")]
        public async Task<ActionResult<IEnumerable<GymListDto>>> GetGymsList()
        {
            string filepath = "";

            List<GymListDto> gymDtos = new List<GymListDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var gyms = await _context.TGyms
                .Include(x => x.Region)
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

                GymListDto gymDto = new GymListDto()
                {
                    GymId = item.GymId,
                    RegionId = item.RegionId,
                    Region = item.Region.Region,
                    GymName = item.GymName,
                    GymAddress = item.GymAddress,
                    GymPhone = item.GymPhone,
                    GymTime = item.GymTime,
                    GymPhoto = base64Image,
                    GymPark = item.GymPark,
                    GymTraffic = item.GymTraffic,
                    GymDescribe = item.GymDescribe
                };
                gymDtos.Add(gymDto);
            }
            return Ok(gymDtos);
        }
        // GET api/<GymListController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TGym>> GetTGym(int id)
        {
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var tGym = await _context.TGyms.FindAsync(id);

            if (tGym == null)
            {
                return NotFound();
            }

            return tGym;
        }

        // POST api/<GymListController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GymListController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GymListController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
