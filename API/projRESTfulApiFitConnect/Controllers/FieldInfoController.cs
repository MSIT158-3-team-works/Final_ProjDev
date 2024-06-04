using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projRESTfulApiFitConnect.DTO;
using projRESTfulApiFitConnect.Models;


namespace projRESTfulApiFitConnect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldInfoController : ControllerBase
    {
        private readonly GymContext _context;
        private readonly IWebHostEnvironment _env;

        public FieldInfoController(GymContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET api/Field
        [HttpGet(Name = "GetFieldInfos")]
        public async Task<ActionResult<IEnumerable<FieldInfoDto>>> GetFields()
        {
            string filepath = "";

            List<FieldInfoDto> fieldinfoDtos = new List<FieldInfoDto>();
            if (_context.TfieldPhotos == null)
            {
                return NotFound();
            }
            var fieldinfos = await _context.Tfields
                                .Include(x => x.TfieldPhotos)
                                .ToListAsync();
            foreach (var item in fieldinfos)
            {
                string base64Image = "";

                var firstPhoto = item.TfieldPhotos.FirstOrDefault();
                if (firstPhoto != null)
                {
                    filepath = Path.Combine(_env.ContentRootPath, "Images", "FieldImages", firstPhoto.FieldPhoto);
                    if (System.IO.File.Exists(filepath))
                    {
                        byte[] bytes = await System.IO.File.ReadAllBytesAsync(filepath);
                        base64Image = Convert.ToBase64String(bytes);
                    }
                }

                FieldInfoDto fieldinfoDto = new FieldInfoDto()
                {
                    Floor = item.Floor,
                    FieldName = item.FieldName,
                    //FieldPhoto = base64Image
                    //FieldDescribe = item.FieldDescribe,
                };
                fieldinfoDtos.Add(fieldinfoDto);
            }
            return Ok(fieldinfoDtos);
        }

        // POST api/<FieldInfoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<FieldInfoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FieldInfoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
