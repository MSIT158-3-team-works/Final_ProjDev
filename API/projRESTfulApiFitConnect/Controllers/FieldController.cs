using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.Models;


namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public FieldController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET api/Field
        [HttpGet(Name = "GetFields")]
        public async Task<ActionResult<IEnumerable<FieldDto>>> GetFields()
        {
            string filepath = "";

            List<FieldDto> fieldinfoDtos = new List<FieldDto>();
            if (_context.TfieldPhotos == null)
            {
                return NotFound();
            }
            var fields = await _context.TGyms
                                .Include(x =>x.Tfields)
                                .ToListAsync();
            foreach (var item in fields)
            {
                //string base64Image = "";

                //var firstPhoto = item.ClassPhoto.FirstOrDefault();
                //if (firstPhoto != null)
                //{
                //    filepath = Path.Combine(_env.ContentRootPath, "Images", "ClassPic", firstPhoto.ClassPhoto);
                //    if (System.IO.File.Exists(filepath))
                //    {
                //        byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                //        base64Image = Convert.ToBase64String(bytes);
                //    }
                //}

                FieldDto fieldDto = new FieldDto()
                {
                    GymName = item.GymName,
                    GymAddress = item.GymAddress,
                    GymTime = item.GymTime,
                    //GymPhoto =base64Image,
                    GymPark = item.GymPark,
                    GymTraffic = item.GymTraffic,
                    GymDescribe = item.GymDescribe
                };
                fieldinfoDtos.Add(fieldDto);
            }
            return Ok(fieldinfoDtos);
        }

        // POST api/<FieldController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FieldController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FieldController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
