using Microsoft.AspNetCore.Mvc;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public GymController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/Gym
        //讀取所有場館資訊
        [HttpGet(Name = "GetGyms")]
        public async Task<ActionResult<IEnumerable<GymDto>>> GetGyms()
        {
            string filepath = "";

            List<GymDto> gymDtos = new List<GymDto>();
            if (_context.TGyms == null)
            {
                return NotFound();
            }
            var gyms = await _context.TGyms
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

                GymDto gymDto = new GymDto()
                {
                    GymName=item.GymName,
                    GymAddress=item.GymAddress,
                    GymPhone=item.GymPhone,
                    GymTime=item.GymTime,
                    GymPhoto =base64Image,
                    GymPark=item.GymPark,
                    GymTraffic=item.GymTraffic
                };
                gymDtos.Add(gymDto);
            }
            return Ok(gymDtos);
        }
        // GET api/<GymController>/5
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

        // POST api/<GymController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<GymController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GymController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
